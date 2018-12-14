using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
public interface IAsyncResponseStreamingCall<TResponse> : IAsyncCall {

	IAsyncStreamReader<TResponse> ResponseStream{get;}

}

