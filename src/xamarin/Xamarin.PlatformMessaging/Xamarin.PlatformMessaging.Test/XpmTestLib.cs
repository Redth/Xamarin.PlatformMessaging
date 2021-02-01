using System;

namespace Xamarin.PlatformMessaging.Test
{
    public partial class XpmTestLib
    {
        public const string TextForHandlerWithParameter = "XpmTestLib-Text";

        public static Type NoHandlerExceptionType = typeof(MessageChannelException);

        const string ChannelId = "xpm_test_lib";
        const string NoHandlerMessageId = "no_handler";
        const string HandlerMessageId = "log_hello_text";
        const string HandlerExecutedMessageId = "log_hello_text_executed";
        const string HandlerWithParameterMessageId = "log_custom_text";
        const string HandlerWithParameterExecutedMessageId = "log_custom_text_executed";
        const string ResponseHandlerMessageId = "get_text";
        const string ResponseHandlerWithParameterMessageId = "get_text_with_param";

        MessageChannel _channel;

        public EventHandler<string> SendTestMessageHandled;

        public XpmTestLib()
            => _channel = new MessageChannel(ChannelId);

        public void SendMessageWithNoHandler()
            => _channel.Send(NoHandlerMessageId);

        public void SendMessageWithHandler()
            => _channel.Send(HandlerMessageId);

        public void SendForResponseMessageWithNoHandler()
            => _channel.SendForResponse(NoHandlerMessageId);

        public void SendHandlerExecutedMessage()
            => _channel.Send(HandlerExecutedMessageId);

        public void ClearSendMessageHandler()
            => _channel.ClearHandler(HandlerMessageId);

        public void ClearSendForResponseMessageHandler()
            => _channel.ClearResponseHandler(ResponseHandlerMessageId);

        public void ClearAllHandlers()
            => _channel.ClearAllHandlers();
    }
}