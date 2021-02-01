using Android.Content;
using Android.Runtime;

namespace Xamarin.PlatformMessaging
{
	public static class Platform
	{
        public static void Start(string packageName, string javaStartClassName, string javaStartMethodName = "start", Context context = null)
        {
            var javaStartClassPath = string.Join(".", packageName, javaStartClassName);
            javaStartClassPath = javaStartClassPath.Replace('.', '/');
            var startClass = JNIEnv.FindClass(javaStartClassPath);
            var method = JNIEnv.GetStaticMethodID(
                startClass,
                javaStartMethodName,
                "(Landroid/content/Context;)V");
            JNIEnv.CallStaticVoidMethod(startClass, method, new JValue(context));
        }

        public static void Stop(string packageName, string javaStopClassName, string javaStopMethodName = "stop")
        {
            var javaStopClassPath = string.Join(".", packageName, javaStopClassName);
            javaStopClassPath = javaStopClassPath.Replace('.', '/');
            var stopClass = JNIEnv.FindClass(javaStopClassPath);
            var method = JNIEnv.GetStaticMethodID(
                stopClass,
                javaStopMethodName,
                "()V");
            JNIEnv.CallStaticVoidMethod(stopClass, method);
        }
    }
}