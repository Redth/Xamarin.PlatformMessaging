package com.mobcat.xpmtestlib;

import android.content.Context;
import android.util.Log;

import com.xamarin.platformmessaging.MessageChannel;

public class XpmTestLib {
    static Context _context;
    static MessageChannel _channel;

    public static void start(Context context) {
        _channel = new MessageChannel("xpm_test_lib");
        _context = context;

        _channel.setHandler("log_hello_text", (parameter) -> {
            Log.d("XpmTestLib", "Hello from Java");
            sendMessage("log_hello_text_executed", "Hello from Java");
        });

        _channel.setHandler("log_custom_text", (parameter) -> {
            String arg = (parameter != null && parameter.length == 1) ? (String)parameter[0] : null;

            if (arg != null) {
                Log.d("XpmTestLib", arg);
                sendMessage("log_custom_text_executed", arg);
            }
            else {
                Log.d("XpmTestLib", "No text provided");
                sendMessage("log_custom_text_executed", "No text provided");
            }
        });

        _channel.setResponseHandler("get_text", (parameter) -> {
            return "Hello from Java";
        });

        _channel.setResponseHandler("get_text_with_param", (parameter) -> {
            String arg = (parameter != null && parameter.length == 1) ? (String)parameter[0] : null;

            if (arg != null) {
                return String.format("Parameter received: %s", arg);
            }

            return "No parameter received";
        });

        sendMessage("start_executed");
    }

    public static void stop() {
        try {
            _channel.clearHandler("log_hello_text");
            _channel.clearHandler("log_custom_text");
            _channel.clearResponseHandler("get_text");
            _channel.clearResponseHandler("get_text_with_param");

            sendMessage("stop_executed");
        }
        catch (Exception ex) {
            Log.d("XpmTestLib", ex.getLocalizedMessage());
        }

        _channel = null;
        _context = null;
    }

    static void sendMessage(String messageId) {
        sendMessage(messageId, null);
    }

    static void sendMessage(String messageId, String value) {
        try {
            if (value != null) {
                _channel.send(messageId, value);
            }
            else {
                _channel.send(messageId);
            }
        }
        catch (Exception ex) {
            Log.d("XpmTestLib", ex.getLocalizedMessage());
        }
    }
}