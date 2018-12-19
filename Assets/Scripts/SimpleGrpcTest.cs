using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sako.SimpleGrpc;

public class SimpleGrpcTest : MonoBehaviour {

	public string ip = "127.0.0.1";
  	public string port = "50151";

	[SerializeField]
	private string[] subscribeEvents;

	private SimpleGrpcClient client;
 
 
    void Start() {
		
		client = new SimpleGrpcClient(ip, port, subscribeEvents);
		client.OnResponseStreamEvents += OnResponseStreamEvents;
		

		//using uni rx
		// var observable = Observable.FromEvent<Payload>(
		// 	h => client.OnResponseStreamEvents += h,
		// 	h => client.OnResponseStreamEvents += h
		// );
		// observable.Subscribe(pl =>{
		// 	Debug.Log("rx"+pl);
		// }).AddTo(this.gameObject);

    }

	public void PushEvent(string eventType){
		if(client == null) return;
		client.Publish(eventType, "unity test");
	}

	private void OnResponseStreamEvents(Payload pl){
		Debug.Log(pl);
	}

	// Update is called once per frame
	void Update () {
		
	}

	
	void OnApplicationQuit()
	{
		client.OnResponseStreamEvents -= OnResponseStreamEvents;
		if(client != null){
			client.Dispose();
		}
	}





}
