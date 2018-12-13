using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Grpc.Core;
using Sako.SimpleGrpc;
using System.Threading.Tasks;
using System;

public class SimpleGrpcController : MonoBehaviour {

	public string ip = "127.0.0.1";
  	public string port = "50151";
 
    private StreamService.StreamServiceClient client;
	private Channel channel;
 
    void Start() {
		channel = new Channel(ip + ":" + port, ChannelCredentials.Insecure);
		client = new StreamService.StreamServiceClient(channel);
		
		Sub();	
    }

	GrpcDuplexStreamingObservable<Request, Payload> observable;
	IDisposable subscriber;
	IObserver<Payload> observer;

	public async Task Sub(){
		Task nowait = Task.Run(async ()=>{
			Debug.Log("task run");
			try{
				using(observable = client.Events().ToObservable()){
					Debug.Log("observable");
					observer = GrpcObservable.CreateObserver<Payload>(
						response => OnResponse(response)
						, ex => OnError(ex)
						, () => OnComplete()
					);

					subscriber = observable.Subscribe(observer);

					OnInit();

					await observable.ObserveAsync().ConfigureAwait(false);
					Debug.Log("observable end");
				}
				
			}catch(Exception ex){

			}finally{

			}
		});

		

	}
	public async Task OnInit(){
		Debug.Log("oninit");
		Request req = new Request{
			ForceClose = false,
			Events = {new Sako.SimpleGrpc.EventType{Type = "unity:test"}, new Sako.SimpleGrpc.EventType{Type = "unity:hoge"}},
		};

		await observable.WriteRequestAsync(req).ConfigureAwait(false);
	}

	public void OnResponse(Payload payload){
		Debug.LogFormat("Got Data Type: {0}, Data {1}:", payload.EventType, payload.Data);
	}

	public void OnError(Exception ex){

		Debug.LogError("OnError  "+ ex);
	}

	public void OnComplete(){
		Debug.Log("OnComplete");
	}

	void OnApplicationQuit(){
		Debug.Log("quit");
		observable.CompleteRequestAsync().ConfigureAwait(false);
		subscriber.Dispose();
	}

	

	public void Publish(){
		string data = "{\"meta\":{\"type\":\"program:1234:views\"},\"data\":{\"data\":111000}}";
		var r = client.Publish(new Json{ Data = data });
		Debug.LogFormat("request isScueess: {0}", r.IsSuccess);
	}
 
  

	
	// Update is called once per frame
	void Update () {
		
	}




}
