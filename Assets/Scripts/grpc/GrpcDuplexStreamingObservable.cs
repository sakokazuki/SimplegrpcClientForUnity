using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using System.Threading.Tasks;

public sealed class GrpcDuplexStreamingObservable<TRequest, TResponse> : GrpcStreamingObservableBase<TResponse> {

	private GrpcDuplexStreamingObservable(AsyncDuplexStreamingCall<TRequest, TResponse> call, bool disposableCall)
	: base(call.ResponseStream){
		m_Call = call;
		m_DisposableCall = disposableCall;
	}

	public static GrpcDuplexStreamingObservable<TRequest, TResponse> Observe(AsyncDuplexStreamingCall<TRequest, TResponse> call, bool disposableCall){
		return new GrpcDuplexStreamingObservable<TRequest, TResponse>(call, disposableCall);
	}

	private AsyncDuplexStreamingCall<TRequest, TResponse> m_Call;
	private bool m_DisposableCall;

	protected override void Dispose(bool disposing){
		try{
			if(m_DisposableCall){
				m_Call.Dispose();
			}
		}finally{
			base.Dispose(disposing);
		}
	}

	public async Task WriteRequestAsync(TRequest request){
		await m_Call.RequestStream.WriteAsync(request).ConfigureAwait(false);
	}

	public async Task CompleteRequestAsync(){
		await m_Call.RequestStream.CompleteAsync().ConfigureAwait(false);
	}
}
