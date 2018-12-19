using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using Sako.SimpleGrpc;
using System;
using System.Threading.Tasks;
using System.Threading;

public sealed class SimpleGrpcClient : IDisposable {
	private StreamService.StreamServiceClient client;
	private Channel channel;

	private AsyncDuplexStreamingCall<Request, Payload> call;

	private GrpcChannelMonitor channelMonitor;

	public delegate void ServerResponseHandler(Payload payload);
	public event Action<Payload> OnResponseStreamEvents;
	

	private int requestInterval;

	private bool isDiposed = false;

	private bool isReconnecting = false;
	private string[] subscribeEventTypes;
	public SimpleGrpcClient(string ip, string port, string[] subscribeEventTypes,int requestInterval = 5000){
		channel = new Channel(ip + ":" + port, ChannelCredentials.Insecure);		
		channelMonitor = new GrpcChannelMonitor(channel);
		this.requestInterval = requestInterval;
		this.subscribeEventTypes = subscribeEventTypes;
		
		Connect();		
	}

	~SimpleGrpcClient(){
		Dispose();
	}

	private async Task Reconnect(){
		if(isReconnecting == true || isDiposed == true) return; 
		isReconnecting = true;
		
		while(true){
			await Task.Delay(requestInterval);
			if(isDiposed) break;
			var calling = GetRpcEventsIsConnecing();
			if(!calling){
				Connect();
			}else{
				break;
			}
		}
		isReconnecting = false;
	}

	private async Task Connect(){
	
		client = new StreamService.StreamServiceClient(channel);
		try{
			call = client.Events();
			
			var responseTask = Task.Run(async ()=>{
				
				while(await call.ResponseStream.MoveNext()){
					OnResponseStreamEvents?.Invoke(call.ResponseStream.Current);
				}
			});
			
			await RequestSubscribeEvents();
			await responseTask;

		}catch(Exception ex){
			Debug.LogFormat("callException"+ex);
			GetRpcEventsIsConnecing();
		}finally{
			call.Dispose();
			Reconnect();
			
		}
	}

	bool GetRpcEventsIsConnecing(){
		if(call == null){
			return false;
		}
		try{
			call.GetStatus();
			return false;
		}catch(Exception ex){
			return true;
		}
	}


	public void Publish(string eventType, string data){
		string json = string.Format("{{\"meta\":{{\"type\":\"{0}\"}},\"data\":{{\"data\":\"{1}\"}}}}", eventType, data);
		var r = client.Publish(new Json{ Data = json });
	}

	private async Task RequestSubscribeEvents(){
		var events = new Google.Protobuf.Collections.RepeatedField<Sako.SimpleGrpc.EventType>();
		foreach(string eventType in subscribeEventTypes){
			events.Add(new Sako.SimpleGrpc.EventType{Type=eventType});
		}
		Request req = new Request{
			ForceClose = false,
			Events = {events},
		};	
		await call.RequestStream.WriteAsync(req).ConfigureAwait(false);
	}

	private void Disconnect(){
		Debug.LogFormat("Disconnect! channel state {0}", channel.State);
		call.RequestStream.CompleteAsync().ConfigureAwait(false);
	}
	public void Dispose(){
		isDiposed = true;
		call.RequestStream.CompleteAsync().ConfigureAwait(false);
		call.Dispose();
		channelMonitor.Dispose();
		channel.ShutdownAsync();
	}


}
