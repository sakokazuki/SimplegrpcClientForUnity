using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


public sealed class AsyncServerStreamingCallWrapper<TResponse> : IAsyncResponseStreamingCall<TResponse> {

	private AsyncServerStreamingCall<TResponse> m_Call;
	internal AsyncServerStreamingCallWrapper(AsyncServerStreamingCall<TResponse> call){
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


}
