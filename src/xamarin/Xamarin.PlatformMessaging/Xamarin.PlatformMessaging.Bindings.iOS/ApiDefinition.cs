using System;
using Foundation;

namespace Xamarin.PlatformMessaging.iOS
{
    //SWIFT_CLASS_NAMED("MessageChannel")
    //@interface MessageChannel : NSObject
    [BaseType(typeof(NSObject), Name = "MessageChannel")]
    [Internal]
    [DisableDefaultCtor]
    interface MessageChannel
    {
        //- (nonnull instancetype) initWithChannelId:(NSString* _Nonnull) channelId OBJC_DESIGNATED_INITIALIZER;
        [Export("initWithChannelId:")]
        [DesignatedInitializer]
        IntPtr Constructor(string channelId);

        //- (BOOL) sendWithMessageId:(NSString* _Nonnull)messageId error:(NSError* _Nullable * _Nullable)error;
        [Export("sendWithMessageId:error:")]
        bool Send(string messageId, out NSError error);

        //- (BOOL) sendWithMessageId:(NSString* _Nonnull)messageId parameters:(NSArray <NSObject*> *_Nullable) parameters error:(NSError* _Nullable * _Nullable)error;
        [Export("sendWithMessageId:parameters:error:")]
        bool Send(string messageId, NSObject[] parameters, out NSError error);

        //- (NSObject* _Nullable) sendForResponseWithMessageId:(NSString* _Nonnull) messageId error:(NSError* _Nullable * _Nullable)error SWIFT_WARN_UNUSED_RESULT;
        [Export("sendForResponseWithMessageId:error:")]
        NSObject SendForResponse(string messageId, out NSError error);

        // -(NSObject* _Nullable) sendForResponseWithMessageId:(NSString* _Nonnull) messageId parameters:(NSArray<NSObject*>* _Nullable) parameters error:(NSError* _Nullable * _Nullable)error SWIFT_WARN_UNUSED_RESULT;
        [Export("sendForResponseWithMessageId:parameters:error:")]
        NSObject SendForResponse(string messageId, NSObject[] parameters, out NSError error);

        //- (void) setHandlerForMessageId:(NSString* _Nonnull)messageId handler:(void (^_Nonnull)(NSArray<NSObject*>* _Nullable))handler;
        [Export("setHandlerForMessageId:handler:")]
        void SetHandler(string messageId, Action<NSObject[]> handler);

        //- (void) setResponseHandlerForMessageId:(NSString* _Nonnull)messageId handler:(NSObject* _Nonnull (^_Nonnull)(NSArray<NSObject*>* _Nullable))handler;
        [Export("setResponseHandlerForMessageId:handler:")]
        void SetResponseHandler(string messageId, Func<NSObject[], NSObject> handler);

        //- (void) clearAllHandlers;
        [Export("clearAllHandlers")]
        void ClearAllHandlers();

        //- (BOOL) clearHandlerForMessageId:(NSString * _Nonnull)messageId error:(NSError * _Nullable * _Nullable)error;
        [Export("clearHandlerForMessageId:error:")]
        bool ClearHandler(string messageId, out NSError error);

        //- (void) clearHandlers;
        [Export("clearHandlers")]
        void ClearHandlers();

        //- (BOOL) clearResponseHandlerForMessageId:(NSString * _Nonnull)messageId error:(NSError * _Nullable * _Nullable)error;
        [Export("clearResponseHandlerForMessageId:error:")]
        bool ClearResponseHandler(string messageId, out NSError error);

        //- (void) clearResponseHandlers;
        [Export("clearResponseHandlers")]
        void ClearResponseHandlers();
    }
}