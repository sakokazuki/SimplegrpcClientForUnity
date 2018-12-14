using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using Sako.SimpleGrpc;
using System;
using System.Threading.Tasks;

public class SimpleGrpcObserver : IDisposable {

	private StreamService.StreamServiceClient client;
	private Channel channel;

	private AsyncDuplexStreamingCall<Request, Payload> call_;
	private GrpcServerStreamingObservable<Payload> observable_;
	private IDisposable subscriber_;
	private IObserver<Payload> observer_;

	public SimpleGrpcObserver(string ip, string port){
		channel = new Channel(ip + ":" + port, ChannelCredentials.Insecure);
		client = new StreamService.StreamServiceClient(channel);

		Connect();
	}

	private void Connect(){
		Task nowait = Task.Run(async ()=>{
			Debug.Log("new task run");
			
	
			try{
				using(call_ = client.Events()){
					var asyncCall = call_.AsResponseStreamingCall();
					observable_ = GrpcServerStreamingObservable<Payload>.Observe(asyncCall, false);

					observer_ = GrpcObservable.CreateObserver<Payload>(
						res => OnResponse(res),
						ex => OnError(ex),
						() => OnComplete()
					);
					subscriber_ = observable_.Subscribe(observer_);
					await OnInit();


					await observable_.ObserveAsync().ConfigureAwait(false);
					Debug.Log("new observable end");
				}
				
			}catch(Exception ex){

			}finally{

			}
		});
	}

	private async Task OnInit(){
		Debug.Log("new oninit");
		await RequestServer("unity:test");
	}


	private void OnResponse(Payload payload){
		Debug.LogFormat("Got Data Type: {0}, Data {1}:", payload.EventType, payload.Data);
	}

	private void OnError(Exception ex){

		Debug.LogError("OnError  "+ ex);
	}

	private void OnComplete(){
		Debug.Log("OnComplete");
	}

	public IDisposable Subscribe(IObserver<Payload> observer){
		var sub = observable_.Subscribe(observer);
		return sub;
	}

	public void Publish(){
		string data = "{\"meta\":{\"type\":\"program:1234:views\"},\"data\":{\"data\":111000}}";
		var r = client.Publish(new Json{ Data = data });
		Debug.LogFormat("request isScueess: {0}", r.IsSuccess);
	}

	public async Task RequestServer(string eventType){
		Request req = new Request{
			ForceClose = false,
			Events = {new Sako.SimpleGrpc.EventType{Type = eventType}, new Sako.SimpleGrpc.EventType{Type = "unity:hoge"}},
		};	
		await call_.RequestStream.WriteAsync(req).ConfigureAwait(false);
	}

	
	public void Dispose(){
		if(call_ != null){
			call_.RequestStream.CompleteAsync().ConfigureAwait(false);
			call_.Dispose();
		}
		if(subscriber_ != null){
			subscriber_.Dispose();
		}
		observable_.Dispose();
	}


}
