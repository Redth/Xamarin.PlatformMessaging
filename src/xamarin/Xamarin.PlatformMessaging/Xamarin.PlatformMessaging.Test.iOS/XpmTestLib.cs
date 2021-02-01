using Foundation;

namespace Xamarin.PlatformMessaging.Test
{
    public partial class XpmTestLib
    {
        public const string HandlerExecutedExpectedResponse = "Hello from Swift";

        public void Start()
        {
            Platform.Start("XpmTestLib");
            _channel.SetHandler(HandlerExecutedMessageId, (args) => SendTestMessageHandled?.Invoke(this, (args[0] as NSString)?.Description));
            _channel.SetHandler(HandlerWithParameterExecutedMessageId, (args) => SendTestMessageHandled?.Invoke(this, (args[0] as NSString)?.Description));
        }

        public void Stop()
        {
            Platform.Stop("XpmTestLib");            
        }

        public void SendMessageAndParameterWithHandler(string text)
            => _channel.Send(HandlerWithParameterMessageId, new NSString(text));

        public string SendForResponseMessageWithHandler()
            => _channel.SendForResponse<NSString>(ResponseHandlerMessageId)?.Description;

        public string SendForResponseMessageAndParameterWithHandler(string text)
            => _channel.SendForResponse<NSString>(ResponseHandlerWithParameterMessageId, new NSString(text))?.Description;
    }
}