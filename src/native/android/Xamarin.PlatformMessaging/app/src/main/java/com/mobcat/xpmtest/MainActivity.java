package com.mobcat.xpmtest;

import android.os.Bundle;
import android.util.Log;

import androidx.appcompat.app.AppCompatActivity;

import com.xamarin.platformmessaging.MessageChannel;

public class MainActivity extends AppCompatActivity {

    MessageChannel _messageChannel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        _messageChannel = new MessageChannel("my_channel");

        try {
            _messageChannel.send("print_text");
            _messageChannel.send("print_text", "argument");
        }
        catch (Exception ex) {
            Log.d("MESSAGE_CHANNEL", ex.getLocalizedMessage());
        }

        try {
            Log.d("MESSAGE_CHANNEL", (String)_messageChannel.sendForResponse("get_text"));
            Log.d("MESSAGE_CHANNEL", (String)_messageChannel.sendForResponse("get_text", "argument"));
        }
        catch (Exception ex) {
            Log.d("MESSAGE_CHANNEL", ex.getLocalizedMessage());
        }

        _messageChannel.setHandler("print_text", (parameter) -> {
            if (parameter != null) {
                Log.d("MESSAGE_CHANNEL",String.format("Handled print_text with %s", parameter));
            }
            else {
                Log.d("MESSAGE_CHANNEL","Handled print_text with no parameter");
            }
        });

        _messageChannel.setResponseHandler("get_text", (parameter) -> {
            if (parameter != null) {
                return String.format("Response for get_text with %s", parameter);
            }
            else {
                return "Response for get_text with no parameter";
            }
        });

        try {
            _messageChannel.send("print_text");
            _messageChannel.send("print_text", "argument");
            Log.d("MESSAGE_CHANNEL", (String)_messageChannel.sendForResponse("get_text"));
            Log.d("MESSAGE_CHANNEL", (String)_messageChannel.sendForResponse("get_text", "argument"));
        }
        catch (Exception ex) {
            Log.d("MESSAGE_CHANNEL", ex.getLocalizedMessage());
        }

        _messageChannel.clearAllHandlers();

        try {
            _messageChannel.send("print_text");
        }
        catch (Exception ex) {
            Log.d("MESSAGE_CHANNEL", ex.getLocalizedMessage());
        }

        try {
            Log.d("MESSAGE_CHANNEL", (String)_messageChannel.sendForResponse("get_text"));
        }
        catch (Exception ex) {
            Log.d("MESSAGE_CHANNEL", ex.getLocalizedMessage());
        }
    }
}