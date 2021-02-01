using System;

namespace Xamarin.PlatformMessaging
{
    internal class MessageHandler : Java.Lang.Object, Xamarin.PlatformMessaging.Android.IMessageHandler
    {
        Action<Java.Lang.Object[]> _handler;

        internal MessageHandler(Action<Java.Lang.Object[]> handler)
        {
            _handler = handler;
        }

        public void OnExecuteAction(Java.Lang.Object[] parameter)
            => _handler(parameter);
    }
}