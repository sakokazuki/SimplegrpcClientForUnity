using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;

public interface IAsyncRequestStreamingCall<TRequest> : IAsyncCall {

	IClientStreamWriter<TRequest> RequestStream{get;}
}
