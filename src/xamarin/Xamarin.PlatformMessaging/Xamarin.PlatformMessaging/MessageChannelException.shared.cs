using System;

namespace Xamarin.PlatformMessaging
{
    public class MessageChannelException : Exception
    {
        public MessageChannelError Error { get; internal set; }

        internal MessageChannelException(string message = null, Exception innerException = null, MessageChannelError error = MessageChannelError.UnknownError)
            : base(message, innerException)
        {
            Error = error;
        }       
    }
}