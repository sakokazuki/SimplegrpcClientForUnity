// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: simplegrpc.proto
// </auto-generated>
// Original file comments:
// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Sako.SimpleGrpc {
  public static partial class StreamService
  {
    static readonly string __ServiceName = "simplegrpc.StreamService";

    static readonly grpc::Marshaller<global::Sako.SimpleGrpc.Request> __Marshaller_simplegrpc_Request = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Sako.SimpleGrpc.Request.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Sako.SimpleGrpc.Payload> __Marshaller_simplegrpc_Payload = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Sako.SimpleGrpc.Payload.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Sako.SimpleGrpc.Json> __Marshaller_simplegrpc_Json = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Sako.SimpleGrpc.Json.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Sako.SimpleGrpc.Success> __Marshaller_simplegrpc_Success = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Sako.SimpleGrpc.Success.Parser.ParseFrom);

    static readonly grpc::Method<global::Sako.SimpleGrpc.Request, global::Sako.SimpleGrpc.Payload> __Method_Events = new grpc::Method<global::Sako.SimpleGrpc.Request, global::Sako.SimpleGrpc.Payload>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "Events",
        __Marshaller_simplegrpc_Request,
        __Marshaller_simplegrpc_Payload);

    static readonly grpc::Method<global::Sako.SimpleGrpc.Json, global::Sako.SimpleGrpc.Success> __Method_Publish = new grpc::Method<global::Sako.SimpleGrpc.Json, global::Sako.SimpleGrpc.Success>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Publish",
        __Marshaller_simplegrpc_Json,
        __Marshaller_simplegrpc_Success);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Sako.SimpleGrpc.SimplegrpcReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of StreamService</summary>
    public abstract partial class StreamServiceBase
    {
      public virtual global::System.Threading.Tasks.Task Events(grpc::IAsyncStreamReader<global::Sako.SimpleGrpc.Request> requestStream, grpc::IServerStreamWriter<global::Sako.SimpleGrpc.Payload> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Sako.SimpleGrpc.Success> Publish(global::Sako.SimpleGrpc.Json request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for StreamService</summary>
    public partial class StreamServiceClient : grpc::ClientBase<StreamServiceClient>
    {
      /// <summary>Creates a new client for StreamService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public StreamServiceClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for StreamService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public StreamServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected StreamServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected StreamServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual grpc::AsyncDuplexStreamingCall<global::Sako.SimpleGrpc.Request, global::Sako.SimpleGrpc.Payload> Events(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Events(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::Sako.SimpleGrpc.Request, global::Sako.SimpleGrpc.Payload> Events(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_Events, null, options);
      }
      public virtual global::Sako.SimpleGrpc.Success Publish(global::Sako.SimpleGrpc.Json request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Publish(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Sako.SimpleGrpc.Success Publish(global::Sako.SimpleGrpc.Json request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Publish, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Sako.SimpleGrpc.Success> PublishAsync(global::Sako.SimpleGrpc.Json request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PublishAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Sako.SimpleGrpc.Success> PublishAsync(global::Sako.SimpleGrpc.Json request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Publish, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override StreamServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new StreamServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(StreamServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Events, serviceImpl.Events)
          .AddMethod(__Method_Publish, serviceImpl.Publish).Build();
    }

    /// <summary>Register service method implementations with a service binder. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, StreamServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Events, serviceImpl.Events);
      serviceBinder.AddMethod(__Method_Publish, serviceImpl.Publish);
    }

  }
}
#endregion
