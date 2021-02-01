using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Xamarin.PlatformMessaging.Test
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class XpmTests
    {
        int timeout = 500;
        XpmTestLib _xpmTestLib;

        [SetUp]
        public void Setup()
        {
            _xpmTestLib = new XpmTestLib();
            _xpmTestLib.Start();
        }

        [TearDown]
        public void TearDown()
        {
            _xpmTestLib.Stop();
            _xpmTestLib = null;
        }

        [Test]
        public void SendNoHandlerTest()
            => Assert.Throws(
                XpmTestLib.NoHandlerExceptionType,
                () => _xpmTestLib.SendMessageWithNoHandler(),
                "Using a message ID with no matching handler should throw an exception");

        [Test]
        public async Task SendTest()
        {
            EventHandler<string> response = null;
            var tcs = new TaskCompletionSource<string>();
            response = new EventHandler<string>((obj, arg) => tcs.SetResult(arg));

            _xpmTestLib.SendTestMessageHandled += response;

            _xpmTestLib.SendMessageWithHandler();
            var responseText = await GetTextOrTimeoutAsync(tcs);

            _xpmTestLib.SendTestMessageHandled -= response;

            Assert.NotNull(responseText, $"{nameof(XpmTestLib.SendMessageWithHandler)} should return a string result");
            Assert.AreEqual(responseText, XpmTestLib.HandlerExecutedExpectedResponse, $"{nameof(XpmTestLib.SendMessageWithHandler)} should return '{XpmTestLib.HandlerExecutedExpectedResponse}'");
        }

        [Test]
        public async Task SendWithParameterTest()
        {
            EventHandler<string> response = null;
            var tcs = new TaskCompletionSource<string>();
            response = new EventHandler<string>((obj, arg) => tcs.SetResult(arg));

            _xpmTestLib.SendTestMessageHandled += response;

            _xpmTestLib.SendMessageAndParameterWithHandler(XpmTestLib.TextForHandlerWithParameter);
            var responseText = await GetTextOrTimeoutAsync(tcs);

            _xpmTestLib.SendTestMessageHandled -= response;

            Assert.NotNull(responseText, $"{nameof(XpmTestLib.SendMessageAndParameterWithHandler)} should return a string result");
            Assert.AreEqual(responseText, XpmTestLib.TextForHandlerWithParameter, $"{nameof(XpmTestLib.SendMessageAndParameterWithHandler)} should return '{XpmTestLib.TextForHandlerWithParameter}'");
        }

        [Test]
        public void SendForResponseNoHandlerTest()
            => Assert.Throws(
                XpmTestLib.NoHandlerExceptionType,
                () => _xpmTestLib.SendForResponseMessageWithNoHandler(),
                "Using a message ID with no matching handler should throw an exception");

        [Test]
        public void SendForResponseTest()
            => Assert.AreEqual(
                _xpmTestLib.SendForResponseMessageWithHandler(),
                XpmTestLib.HandlerExecutedExpectedResponse,
                $"{nameof(XpmTestLib.SendForResponseMessageWithHandler)} should return '{XpmTestLib.HandlerExecutedExpectedResponse}'");

        [Test]
        public void SendForResponseWithParameterTest()
            => Assert.AreEqual(
                _xpmTestLib.SendForResponseMessageAndParameterWithHandler(XpmTestLib.TextForHandlerWithParameter),
                $"Parameter received: {XpmTestLib.TextForHandlerWithParameter}",
                $"{nameof(XpmTestLib.SendForResponseMessageAndParameterWithHandler)} should return 'Parameter received: {XpmTestLib.TextForHandlerWithParameter}'");

        [Test]
        public async Task StopStartTest()
        {
            // Start was called during setup and should have set the native handlers
            // Stop should result in the native handlers being cleared in this case
            _xpmTestLib.Stop();
            
            Assert.Throws(
                XpmTestLib.NoHandlerExceptionType,
                () => _xpmTestLib.SendMessageWithHandler(),
                $"Native handlers should have been cleared by the stop function. {nameof(XpmTestLib.SendMessageWithHandler)} was still sent successfully");

            // Start should set the native handlers
            _xpmTestLib.Start();

            EventHandler<string> response = null;
            var tcs = new TaskCompletionSource<string>();
            response = new EventHandler<string>((obj, arg) => tcs.SetResult(arg));

            _xpmTestLib.SendTestMessageHandled += response;

            _xpmTestLib.SendMessageWithHandler();
            var responseText = await GetTextOrTimeoutAsync(tcs);

            _xpmTestLib.SendTestMessageHandled -= response;

            Assert.AreEqual(responseText, XpmTestLib.HandlerExecutedExpectedResponse, $"Native handlers should have been set by the start function. {nameof(XpmTestLib.SendMessageWithHandler)} was not sent successfully after calling stop then start");
        }

        [Test]
        public void ClearHandlerTest()
        {
            // Start was called during setup and should have set the native handlers
            _xpmTestLib.ClearSendMessageHandler();

            Assert.Throws(
                XpmTestLib.NoHandlerExceptionType,
                () => _xpmTestLib.SendMessageWithHandler(),
                $"Call to {nameof(XpmTestLib.ClearSendMessageHandler)} should have cleared the underlying handler {nameof(XpmTestLib.SendMessageWithHandler)} was still sent successfully");
        }

        [Test]
        public void ClearResponseHandlerTest()
        {
            // Start was called during setup and should have set the native handlers
            _xpmTestLib.ClearSendForResponseMessageHandler();

            Assert.Throws(
                XpmTestLib.NoHandlerExceptionType,
                () => _xpmTestLib.SendForResponseMessageWithHandler(),
                $"Call to {nameof(XpmTestLib.ClearSendForResponseMessageHandler)} should have cleared the underlying response handler. {nameof(XpmTestLib.SendForResponseMessageWithHandler)} was still sent successfully");
        }

        [Test]
        public void ClearAllHandlersTest()
        {
            // Start was called during setup and should have set the native handlers
            _xpmTestLib.ClearAllHandlers();

            Assert.Throws(
                XpmTestLib.NoHandlerExceptionType,
                () => _xpmTestLib.SendMessageWithHandler(),
                $"Call to {nameof(XpmTestLib.ClearAllHandlers)} should have cleared all the handlers. {nameof(XpmTestLib.SendMessageWithHandler)} was still sent successfully");

            Assert.Throws(
                XpmTestLib.NoHandlerExceptionType,
                () => _xpmTestLib.SendForResponseMessageWithHandler(),
                $"Call to {nameof(XpmTestLib.ClearAllHandlers)} should have cleared all the handlers. {nameof(XpmTestLib.SendForResponseMessageWithHandler)} was still sent successfully");


            Assert.Throws(
                XpmTestLib.NoHandlerExceptionType,
                () => _xpmTestLib.SendHandlerExecutedMessage(),
                $"Call to {nameof(XpmTestLib.ClearAllHandlers)} should have cleared all the handlers. {nameof(XpmTestLib.SendHandlerExecutedMessage)} was still sent successfully");

            _xpmTestLib.Start();
        }

        [Test]
        public void NoHandlerErrorTest()
        {
            try
            {
                _xpmTestLib.SendMessageWithNoHandler();
            }
            catch (MessageChannelException ex)
            {
                Assert.AreEqual(ex.Error, MessageChannelError.NoHandlerForMethod);
            }
        }

        [Test]
        public void NoResponseHandlerErrorTest()
        {
            try
            {
                _xpmTestLib.SendForResponseMessageWithNoHandler();
            }
            catch (MessageChannelException ex)
            {
                Assert.AreEqual(ex.Error, MessageChannelError.NoHandlerForMethod);
            }
        }

        async Task<string> GetTextOrTimeoutAsync(TaskCompletionSource<string> tcs)
        {
            string response = null;

            if (await Task.WhenAny(tcs.Task, Task.Delay(timeout)) == tcs.Task)
            {
                response = tcs.Task.Result;
            }
            else
            {
                tcs.TrySetCanceled();
            }

            return response;
        }
    }
}