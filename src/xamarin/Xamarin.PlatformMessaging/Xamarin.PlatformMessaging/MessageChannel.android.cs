using System;
using System.Runtime.CompilerServices;

namespace Xamarin.PlatformMessaging
{
    public partial class MessageChannel : IDisposable
    {
        readonly Xamarin.PlatformMessaging.Android.MessageChannel _messageChannel;

        bool _disposed;

        public MessageChannel(string channelId)
        {
            _messageChannel = new Xamarin.PlatformMessaging.Android.MessageChannel(channelId);
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
            try
            {
                _messageChannel.Send(messageId);
            }
            catch (Java.Lang.Exception ex)
            {
                throw MessageChannelExceptionFromNativeException(ex);
            }
        }

        public void Send(string messageId, params Java.Lang.Object[] parameters)
        {
            try
            {
                _messageChannel.Send(messageId, parameters);
            }
            catch (Java.Lang.Exception ex)
            {
                throw MessageChannelExceptionFromNativeException(ex);
            }
        }

        public Java.Lang.Object SendForResponse(string messageId)
        {
            try
            {
                return _messageChannel.SendForResponse(messageId);
            }
            catch (Java.Lang.Exception ex)
            {
                throw MessageChannelExceptionFromNativeException(ex);
            }
        }

        public Java.Lang.Object SendForResponse(string messageId, params Java.Lang.Object[] parameters)
        {
            try
            {
                return _messageChannel.SendForResponse(messageId, parameters);
            }
            catch (Java.Lang.Exception ex)
            {
                throw MessageChannelExceptionFromNativeException(ex);
            }
        }

        public T SendForResponse<T>(string messageId) where T : Java.Lang.Object
            => SendForResponse(messageId, (result) => ConvertResult<T>(result));

        public T SendForResponse<T>(string messageId, Func<Java.Lang.Object, T> converter) where T : Java.Lang.Object
            => converter(SendForResponse(messageId));

        public T SendForResponse<T>(string messageId, params Java.Lang.Object[] parameters) where T : Java.Lang.Object
            => SendForResponse(messageId, (result) => ConvertResult<T>(result), parameters);

        public T SendForResponse<T>(string messageId, Func<Java.Lang.Object, T> converter, params Java.Lang.Object[] parameters) where T : Java.Lang.Object
            => converter(SendForResponse(messageId, parameters));

        public void SetHandler(string messageId, Action<Java.Lang.Object[]> handler)
            => _messageChannel.SetHandler(messageId, new MessageHandler(handler));

        public void SetResponseHandler(string messageId, Func<Java.Lang.Object[], Java.Lang.Object> handler)
            => _messageChannel.SetResponseHandler(messageId, new MessageResponseHandler(handler));

        public void ClearAllHandlers()
            => _messageChannel.ClearAllHandlers();

        public void ClearHandler(string messageId)
        {
            try
            {
                _messageChannel.ClearHandler(messageId);
            }
            catch (Java.Lang.Exception ex)
            {
                throw MessageChannelExceptionFromNativeException(ex);
            }
        }

        public void ClearHandlers()
            => _messageChannel.ClearHandlers();

        public void ClearResponseHandler(string messageId)
        {
            try
            {
                _messageChannel.ClearResponseHandler(messageId);
            }
            catch (Java.Lang.Exception ex)
            {
                throw MessageChannelExceptionFromNativeException(ex);
            }
        }

        public void ClearResponseHandlers()
            => _messageChannel.ClearResponseHandlers();

        T ConvertResult<T>(Java.Lang.Object result) where T : Java.Lang.Object
            => (T)result;

        MessageChannelException MessageChannelExceptionFromNativeException(Java.Lang.Exception nativeException, [CallerMemberName] string caller = null)
        {
            MessageChannelError channelError = MessageChannelError.UnknownError;
            string errorMessage = $"{nameof(MessageChannel)}.{caller} failed with error {channelError}";

            if (nativeException.Message.Contains("No Handler for"))
            {
                channelError = MessageChannelError.NoHandlerForMethod;
                errorMessage = nativeException.Message;
            }

            return new MessageChannelException(
                $"{nameof(MessageChannel)}.{caller} failed with error {channelError}",
                nativeException,
                channelError);
        }
    }
}