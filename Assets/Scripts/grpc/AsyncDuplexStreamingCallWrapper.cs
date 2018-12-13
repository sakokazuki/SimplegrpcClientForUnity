using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using System.Threading.Tasks;

public sealed class AsyncDuplexStreamingCallWrapper<TRequest, TResponse> : IAsyncRequestStreamingCall<TRequest>, IAsyncResponseStreamingCall<TResponse> {
	
	AsyncDuplexStreamingCall<TRequest, TResponse> m_Call;
	public AsyncDuplexStreamingCallWrapper(AsyncDuplexStreamingCall<TRequest, TResponse> call){
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

	public IAsyncStreamReader<TResponse> ResponseStream{
		get{
			return m_Call.ResponseStream;
		}
	}

	public IClientStreamWriter<TRequest> RequestStream{
		get{
			return m_Call.RequestStream;
		}
	}
}
