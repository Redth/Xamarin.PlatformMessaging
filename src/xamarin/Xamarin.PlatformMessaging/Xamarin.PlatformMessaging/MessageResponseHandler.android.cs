using System;

namespace Xamarin.PlatformMessaging
{
    internal class MessageResponseHandler : Java.Lang.Object, Xamarin.PlatformMessaging.Android.IMessageResponseHandler
    {
        Func<Java.Lang.Object[], Java.Lang.Object> _handler;

        internal MessageResponseHandler(Func<Java.Lang.Object[], Java.Lang.Object> handler)
        {
            _handler = handler;
        }

        public Java.Lang.Object OnGetValue(Java.Lang.Object[] parameter)
            => _handler(parameter);
    }
}