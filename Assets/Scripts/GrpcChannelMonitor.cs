using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using Grpc.Core;
using System;
public class GrpcChannelMonitor : IDisposable {

	private CancellationTokenSource tokenSource;
	public GrpcChannelMonitor(Channel channel){
		tokenSource = new CancellationTokenSource();
		var token = tokenSource.Token;
		Task.Run(()=>MonitorChannelStateAsync(channel, token), token);
	}

	private async Task MonitorChannelStateAsync(Channel channel, CancellationToken token){
		while(channel.State != ChannelState.Shutdown){
			ChannelState state = channel.State;
						
			if(await channel.TryWaitForStateChangedAsync(state).ConfigureAwait(false)){
				OnChannelStateChanged(state, channel.State);
			}else if(!token.IsCancellationRequested){
				break;
			}
			
		}
	}

	private void OnChannelStateChanged(ChannelState lastState, ChannelState currentState){
		// Debug.LogFormat("({0}) {1} --> {2}", "channel state", lastState, currentState);
	}


	public void Dispose(){
		tokenSource.Cancel();
	}
	
}
