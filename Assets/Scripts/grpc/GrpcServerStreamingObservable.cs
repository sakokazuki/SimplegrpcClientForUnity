using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
public sealed class GrpcServerStreamingObservable<TResponse>: GrpcStreamingObservableBase<TResponse> {

	private GrpcServerStreamingObservable(AsyncServerStreamingCall<TResponse> call, bool disposableCall):base(call.ResponseStream){
		m_Call = call;
		m_DsiposableCall = disposableCall;
	}

	public static GrpcServerStreamingObservable<TResponse> Observe(AsyncServerStreamingCall<TResponse> call, bool disposableCall){
		return new GrpcServerStreamingObservable<TResponse>(call, disposableCall);
	}

	private AsyncServerStreamingCall<TResponse> m_Call;
	private bool m_DsiposableCall;

	protected override void Dispose(bool disposing){
		try{
			if(m_DsiposableCall){
				m_Call.Dispose();
			}
		}finally{
			base.Dispose(disposing);
		}
	}
}
