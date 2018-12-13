using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Grpc.Core;
public interface IAsyncCall : IDisposable {

	Task<Metadata> ResponseHeaderAsync {get;}

	Status GetStatus();
	
	Metadata GetTrailers();
}
