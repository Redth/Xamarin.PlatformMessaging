using System;
using System.Runtime.CompilerServices;
using Foundation;

namespace Xamarin.PlatformMessaging
{
    public class MessageChannel : IDisposable
    {
        readonly Xamarin.PlatformMessaging.iOS.MessageChannel _messageChannel;

        bool _disposed;

        public MessageChannel(string channelId)
        {
            _messageChannel = new Xamarin.PlatformMessaging.iOS.MessageChannel(channelId);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                _messageChannel.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Send(string messageId)
        {
            if (!_messageChannel.Send(messageId, out var error))
                throw MessageChannelExceptionFromError(error, messageId);
        }

        public void Send(string messageId, params NSObject[] parameters)
        {
            if (!_messageChannel.Send(messageId, parameters, out var error))
                throw MessageChannelExceptionFromError(error, messageId);
        }

        public NSObject SendForResponse(string messageId)
        {
            var result = _messageChannel.SendForResponse(messageId, out var error);

            if (error != null)
                throw MessageChannelExceptionFromError(error, messageId);

            return result;
        }

        public NSObject SendForResponse(string messageId, params NSObject[] parameters)
        {
            var result = _messageChannel.SendForResponse(messageId, parameters, out var error);

            if (error != null)
                throw MessageChannelExceptionFromError(error, messageId);

            return result;
        }

        public T SendForResponse<T>(string messageId) where T : NSObject
            => SendForResponse(messageId, (result) => ConvertResult<T>(result));

        public T SendForResponse<T>(string messageId, Func<NSObject, T> converter) where T : NSObject
            => converter(SendForResponse(messageId));

        public T SendForResponse<T>(string messageId, NSObject parameters) where T : NSObject
            => SendForResponse(messageId, parameters, (result) => ConvertResult<T>(result));

        public T SendForResponse<T>(string messageId, NSObject parameters, Func<NSObject, T> converter) where T : NSObject
            => converter(SendForResponse(messageId, parameters));

        public void SetHandler(string messageId, Action<NSObject[]> handler)
            => _messageChannel.SetHandler(messageId, handler);

        public void SetResponseHandler(string messageId, Func<NSObject[], NSObject> handler)
            => _messageChannel.SetResponseHandler(messageId, handler);

        public void ClearAllHandlers()
            => _messageChannel.ClearAllHandlers();

        public void ClearHandler(string messageId)
        {
            if (!_messageChannel.ClearHandler(messageId, out var error))
                throw MessageChannelExceptionFromError(error, messageId);
        }

        public void ClearHandlers()
            => _messageChannel.ClearHandlers();

        public void ClearResponseHandler(string messageId)
        {
            if (!_messageChannel.ClearResponseHandler(messageId, out var error))
                throw MessageChannelExceptionFromError(error, messageId);
        }

        public void ClearResponseHandlers()
            => _messageChannel.ClearHandlers();

        T ConvertResult<T>(NSObject result) where T : NSObject
           => (T)result;

        MessageChannelException MessageChannelExceptionFromError(NSError error, string messageId, [CallerMemberName] string caller = null)
        {
            MessageChannelError channelError = MessageChannelError.UnknownError;
            string errorMessage = $"{nameof(MessageChannel)}.{caller} failed with error {channelError}";

            if (error.LocalizedDescription.Contains(nameof(Xamarin.PlatformMessaging.iOS.MessageChannelError)) &&
                error.LocalizedDescription.Contains($"error {(int)Xamarin.PlatformMessaging.iOS.MessageChannelError.NoHandlerForMethod}"))
            {
                channelError = MessageChannelError.NoHandlerForMethod;
                errorMessage = $"No handler for {messageId}";
            }   

            return new MessageChannelException(errorMessage, new Exception(error.LocalizedDescription), channelError);
        }
    }
}