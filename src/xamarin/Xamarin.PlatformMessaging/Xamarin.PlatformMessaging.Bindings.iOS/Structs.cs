using System;
using Foundation;
using ObjCRuntime;

namespace Xamarin.PlatformMessaging.iOS
{
    //enum MessageChannelError : NSInteger;
    //typedef SWIFT_ENUM_NAMED(NSInteger, MessageChannelError, "MessageChannelError", closed)
    //{
    //  MessageChannelErrorNoHandlerForMethod = 0,
    //  MessageChannelErrorUnknownError = 1,
    //};
    [Native]
    internal enum MessageChannelError : long
    {
        NoHandlerForMethod = 0,
        UnknownError = 1
    }
}