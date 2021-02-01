using System;
using System.Runtime.InteropServices;

namespace Xamarin.PlatformMessaging
{
	public static class Platform
	{
        const string LIBOBJC_DYLIB = "/usr/lib/libobjc.dylib";
        [DllImport(LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
        internal extern static void void_objc_msgSend(IntPtr receiver, IntPtr selector);

        public static void Start(string nativeStartClassName, string nativeStartMethodName = "start")
        {
            var c = new ObjCRuntime.Class(nativeStartClassName);
            var s = new ObjCRuntime.Selector(nativeStartMethodName);
            void_objc_msgSend(c.Handle, s.Handle);
        }

        public static void Stop(string nativeStopClassName, string nativeStopMethodName = "stop")
        {
            var c = new ObjCRuntime.Class(nativeStopClassName);
            var s = new ObjCRuntime.Selector(nativeStopMethodName);
            void_objc_msgSend(c.Handle, s.Handle);
        }
    }
}