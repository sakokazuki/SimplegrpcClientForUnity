using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using System;
namespace Sako.Old{

	public static class GrpcObservable {

		public static GrpcDuplexStreamingObservable<TRequest, TResponse> ObserveDuplexStreaming<TRequest, TResponse>(AsyncDuplexStreamingCall<TRequest, TResponse> call, bool disposableCall = true){
			return GrpcDuplexStreamingObservable<TRequest, TResponse>.Observe(call, disposableCall);
		}

		public static GrpcServerStreamingObservable<TResponse> ObserveServerStreaming<TResponse>(AsyncServerStreamingCall<TResponse> call, bool disposableCall = true){
			return GrpcServerStreamingObservable<TResponse>.Observe(call, disposableCall);
		}

		public static GrpcDuplexStreamingObservable<TRequest, TResponse> ToObservable<TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, bool disposableCall = true){
			return ObserveDuplexStreaming(call, disposableCall);
		}

		public static GrpcServerStreamingObservable<TResponse> ToObservable<TResponse>(this AsyncServerStreamingCall<TResponse> call, bool disposableCall = true){
			return ObserveServerStreaming(call, disposableCall);
		}


		public static IObserver<TResponse> CreateObserver<TResponse>(Action<TResponse> onResponse, Action<Exception> onError = null, Action onComplete = null)
		{
			return new GrpcResponseObserver<TResponse>(onResponse, onError, onComplete);
		}

		private sealed class GrpcResponseObserver<TResponse> : IObserver<TResponse>{
			public GrpcResponseObserver(Action<TResponse> onResponse, Action<Exception> onError, Action onComplete){
				m_OnNext = onResponse;
				m_OnError = onError;
				m_OnComplete = onComplete;
			}

			private Action<TResponse> m_OnNext;
			private Action<Exception> m_OnError;
			private Action m_OnComplete;

			public void OnCompleted(){
				if(m_OnComplete != null){
					m_OnComplete();
				}
			}

			public void OnError(Exception error){
				if(m_OnError != null){
					m_OnError(error);
				}
			}

			public void OnNext(TResponse value){
				if(m_OnNext != null){
					m_OnNext(value);
				}
			}
		}

	}
}
