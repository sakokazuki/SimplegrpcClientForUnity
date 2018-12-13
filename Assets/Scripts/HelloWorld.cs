using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Grpc.Core;
using Pj.Grpc.Sample;

public class HelloWorld : MonoBehaviour {

  public string ip = "127.0.0.1";
  public string port = "9999";

  public Text text;

  private void Start() {
    text.text = "wait reply...";
    Say();
  }

  private void Say() {
    Channel channel = new Channel(ip + ":" + port, ChannelCredentials.Insecure);

    var client = new Greeter.GreeterClient(channel);
    string user = "editor";
#if UNITY_ANDROID && !UNITY_EDITOR
    user = "android";
#elif UNITY_IOS && !UNITY_EDITOR
    user = "ios";
#endif

    var reply = client.SayHello(new HelloRequest { Name = user });
    Debug.Log("reply: " + reply.Message);
    text.text = "reply: " + reply.Message;

    channel.ShutdownAsync().Wait();
  }
}