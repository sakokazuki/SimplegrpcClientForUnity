using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public sealed class AsyncUnaryCallWrapper<TResponse>: IAsyncResponseCall<TResponse> {
	private AsyncUnaryCall<TResponse> m_Call;
	public AsyncUnaryCallWrapper(AsyncUnaryCall<TResponse> call){
		m_Call = call;
	}

	public Task<Metadata> ResponseHeaderAsync{
		get{
			return m_Call.ResponseHeadersAsync;
		}
	}

	public Status GetStatus()
        {
            return m_Call.GetStatus();
        }


	public Metadata GetTrailers(){
		return m_Call.GetTrailers();
	}

	public Task<TResponse> ResponseAsync{
		get {
			return m_Call.ResponseAsync;
		}
	}

	public TaskAwaiter<TResponse> GetAwaiter(){
		return m_Call.GetAwaiter();
	}

	public void Dispose(){
		m_Call.Dispose();
	}
}
