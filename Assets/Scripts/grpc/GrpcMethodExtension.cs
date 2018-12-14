using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;

public static class AsyncUnaryCallExtension{
	public static IAsyncResponseCall<TResponse> AsResponseCall<TResponse>(this AsyncUnaryCall<TResponse> call)
	{
		if (call == null) { return null; }
		return new AsyncUnaryCallWrapper<TResponse>(call);
	}
}

public static class AsyncClientStreamingCallExtension{
	public static IAsyncRequestStreamingCall<TRequest> AsRequestStreamingCall<TRequest, TResponse>(this AsyncClientStreamingCall<TRequest, TResponse> call)
	{
		if (call == null) { return null; }
		return new AsyncClientStreamingCallWrapper<TRequest, TResponse>(call);
	}

	public static IAsyncResponseCall<TResponse> AsResponseCall<TRequest, TResponse>(this AsyncClientStreamingCall<TRequest, TResponse> call)
	{
		if (call == null) { return null; }
		return new AsyncClientStreamingCallWrapper<TRequest, TResponse>(call);
	}
}

public static class AsyncServerStreamingCallExtension{
	public static IAsyncResponseStreamingCall<TResponse> AsResponseStreamingCall<TResponse>(this AsyncServerStreamingCall<TResponse> call)
	{
		if (call == null) { return null; }
		return new AsyncServerStreamingCallWrapper<TResponse>(call);
	}
}

public static class AsyncDuplexStreamingCallExtension{
	public static IAsyncRequestStreamingCall<TRequest> AsRequestStreamingCall<TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call)
	{
		if (call == null) { return null; }
		return new AsyncDuplexStreamingCallWrapper<TRequest, TResponse>(call);
	}
	public static IAsyncResponseStreamingCall<TResponse> AsResponseStreamingCall<TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call)
	{
		if (call == null) { return null; }
		return new AsyncDuplexStreamingCallWrapper<TRequest, TResponse>(call);

	}
}