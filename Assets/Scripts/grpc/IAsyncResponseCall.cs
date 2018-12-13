using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public interface IAsyncResponseCall<TResponse> : IAsyncCall {

	
		
	Task<TResponse> ResponseAsync{get;}

	TaskAwaiter<TResponse> GetAwaiter();
	
}
