using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
public sealed class AsyncClientStreamingCallWrapper<TRequest, TResponse> : IAsyncRequestStreamingCall<TRequest>, IAsyncResponseCall<TResponse> {
	AsyncClientStreamingCall<TRequest, TResponse> m_Call;
	// Use this for initialization
	public AsyncClientStreamingCallWrapper(AsyncClientStreamingCall<TRequest, TResponse> call){
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

	public void Dispose(){
		m_Call.Dispose();
	}

	public IClientStreamWriter<TRequest> RequestStream{
		get{
			return m_Call.RequestStream;
		}
	}

	public Task<TResponse> ResponseAsync{
		get{
			return m_Call.ResponseAsync;
		}
	}

	public TaskAwaiter<TResponse> GetAwaiter(){
		return m_Call.GetAwaiter();
	}

	

	
	

}
