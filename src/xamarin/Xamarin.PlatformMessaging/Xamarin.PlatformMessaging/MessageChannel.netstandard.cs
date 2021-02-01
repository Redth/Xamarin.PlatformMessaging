using System;

namespace Xamarin.PlatformMessaging
{
    public class MessageChannel : IDisposable
    {
        public MessageChannel(string channelId)
            => throw new NotImplementedException();

        public void Dispose()
            => throw new NotImplementedException();

        public void Send(string messageId)
            => throw new NotImplementedException();

        public void Send(string messageId, params object[] parameter)
            => throw new NotImplementedException();

        public object SendForResponse(string messageId)
            => throw new NotImplementedException();

        public object SendForResponse(string messageId, params object[] parameter)
            => throw new NotImplementedException();

        public T SendForResponse<T>(string messageId)
            => throw new NotImplementedException();

        public T SendForResponse<T>(string messageId, Func<object[], T> converter)
            => throw new NotImplementedException();

        public T SendForResponse<T>(string messageId, params object[] parameter)
            => throw new NotImplementedException();

        public T SendForResponse<T>(string messageId, Func<object[], T> converter, params object[] parameter)
            => throw new NotImplementedException();

        public void SetHandler(string messageId, Action<object[]> handler)
            => throw new NotImplementedException();

        public void SetResponseHandler(string messageId, Func<object[], object> handler)
            => throw new NotImplementedException();

        public void ClearAllHandlers()
            => throw new NotImplementedException();

        public void ClearHandler(string messageId)
            => throw new NotImplementedException();

        public void ClearHandlers()
            => throw new NotImplementedException();

        public void ClearResponseHandler(string messageId)
            => throw new NotImplementedException();

        public void ClearResponseHandlers()
            => throw new NotImplementedException();

        // NOTE: Consider implementing a registry of handlers to simplify use of native types from a common API
        // Reduce the required usage of #if def to a single place
    }
}