using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Grpc.Core;
using System.Threading.Tasks;
public sealed class GrpcServerStreamingObservable<TResponse> : IObservable<TResponse>, IDisposable {

	private IAsyncStreamReader<TResponse> m_ResponseStream;
	private IAsyncResponseStreamingCall<TResponse> m_Call;
	private bool m_diposableCall;

	private GrpcServerStreamingObservable(IAsyncResponseStreamingCall<TResponse> call, bool disposableCall){
		m_Call = call;
		m_ResponseStream = call.ResponseStream;
		m_diposableCall = disposableCall;
	}

	public static GrpcServerStreamingObservable<TResponse> Observe(IAsyncResponseStreamingCall<TResponse> call, bool disposableCall){
		return new GrpcServerStreamingObservable<TResponse>(call, disposableCall);
	}

	~GrpcServerStreamingObservable(){
		Dispose();
	}

	public void Dispose(){
		GC.SuppressFinalize(this);

		if(m_diposableCall){
			m_Call.Dispose();
		}
		ReleaseObservers();
		ReleaseSubscribers();
	}
	
	private readonly List<IObserver<TResponse>> m_Observers = new List<IObserver<TResponse>>();

	public async Task ObserveAsync(){
		try{
			while(await m_ResponseStream.MoveNext().ConfigureAwait(false)){
				OnNext(m_ResponseStream.Current);
			}
			OnCompleted();
		}catch(Exception ex){
			OnError(ex);
			throw;
		}finally{

		}
	}

	private void ReleaseObservers(){
		lock(m_Observers){
			m_Observers.Clear();
		}
	}

	private void OnCompleted(){
		if(m_Observers.Count == 0){
			return;
		}
		lock(m_Observers){
			if(m_Observers.Count > 0){
				m_Observers.ForEach(o => o.OnCompleted());
				m_Observers.Clear();
			}
		}
	}

	private void OnError(Exception error){
		if(m_Observers.Count == 0){
			return;
		}
		lock(m_Observers){
			if(m_Observers.Count > 0){
				m_Observers.ForEach(o => o.OnError(error));
				m_Observers.Clear();
			}
		}
	}

	private void OnNext(TResponse response){
		if(m_Observers.Count == 0){
			return;
		}
		lock(m_Observers){
			if(m_Observers.Count > 0){
				m_Observers.ForEach(o => o.OnNext(response));
			}
		}
	}

	private readonly List<IDisposable> m_Subscribers = new List<IDisposable>();

	public IDisposable Subscribe(IObserver<TResponse> observer){
		lock(m_Observers){
			m_Observers.Add(observer);
		}

		Action onDispose = ()=>{
			lock(m_Observers){
				m_Observers.Remove(observer);
			}
		};

		IDisposable subscriber = new Subscriber(onDispose);

		lock(m_Subscribers){
			m_Subscribers.Add(subscriber);
		}
		return subscriber;
	}

	private void ReleaseSubscribers(){
		lock(m_Subscribers){
			m_Subscribers.ForEach(o => o.Dispose());
			m_Subscribers.Clear();
		}
	}

	private sealed class Subscriber : IDisposable{
		public Subscriber(Action onDispose){
			m_OnDispose = onDispose;
		}

		~Subscriber(){
			Dispose(false);
		}

		private Action m_OnDispose;

		public void Dispose(){
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing){
			m_OnDispose();
		}


	}
}
