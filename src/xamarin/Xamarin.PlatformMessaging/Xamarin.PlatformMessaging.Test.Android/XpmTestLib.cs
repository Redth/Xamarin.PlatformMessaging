namespace Xamarin.PlatformMessaging.Test
{
    public partial class XpmTestLib
    {
        public const string HandlerExecutedExpectedResponse = "Hello from Java";

        public void Start()
        {
            Platform.Start("com.mobcat.xpmtestlib", "XpmTestLib");
            _channel.SetHandler(HandlerExecutedMessageId, (args) => SendTestMessageHandled?.Invoke(this, (args[0] as Java.Lang.String)?.ToString()));
            _channel.SetHandler(HandlerWithParameterExecutedMessageId, (args) => SendTestMessageHandled?.Invoke(this, (args[0] as Java.Lang.String)?.ToString()));
        }

        public void Stop()
        {
            Platform.Stop("com.mobcat.xpmtestlib", "XpmTestLib");
            _channel.ClearHandler(HandlerExecutedMessageId);
            _channel.ClearHandler(HandlerWithParameterExecutedMessageId);
        }

        public void SendMessageAndParameterWithHandler(string text)
            => _channel.Send(HandlerWithParameterMessageId, new Java.Lang.String(text));

        public string SendForResponseMessageWithHandler()
            => _channel.SendForResponse<Java.Lang.String>(ResponseHandlerMessageId)?.ToString();

        public string SendForResponseMessageAndParameterWithHandler(string text)
            => _channel.SendForResponse<Java.Lang.String>(ResponseHandlerWithParameterMessageId, new Java.Lang.String(text))?.ToString();
    }
}