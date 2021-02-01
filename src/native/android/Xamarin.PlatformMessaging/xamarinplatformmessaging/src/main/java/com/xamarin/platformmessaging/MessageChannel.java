package com.xamarin.platformmessaging;

import java.util.ArrayList;
import java.util.concurrent.ConcurrentHashMap;

public class MessageChannel {

    static ConcurrentHashMap<String, MessageHandler> _handlers = new ConcurrentHashMap<String, MessageHandler>();
    static ConcurrentHashMap<String, MessageResponseHandler> _responseHandlers = new ConcurrentHashMap<String, MessageResponseHandler>();

    String _channelId;

    public MessageChannel(String channelId) {
        _channelId = channelId;
    }

    public void send(String messageId) throws Exception {
        send(messageId, null);
    }

    public void send(String messageId, Object... parameters) throws Exception {
        String handlerId = getHandlerId(messageId);

        if (!MessageChannel._handlers.containsKey(handlerId)) {
            throw new Exception(getNoHandlerExceptionMessage(messageId));
        }

        MessageChannel._handlers.get(handlerId).onExecuteAction(parameters);
    }

    public Object sendForResponse(String messageId) throws Exception {
        return sendForResponse(messageId, null);
    }

    public Object sendForResponse(String messageId, Object... parameters) throws Exception {
        String handlerId = getHandlerId(messageId);

        if (!MessageChannel._responseHandlers.containsKey(handlerId)) {
            throw new Exception(getNoHandlerExceptionMessage(messageId));
        }

        return MessageChannel._responseHandlers.get(handlerId).onGetValue(parameters);
    }

    public void setHandler(String messageId, MessageHandler handler){
        MessageChannel._handlers.put(getHandlerId(messageId), handler);
    }

    public void setResponseHandler(String messageId, MessageResponseHandler handler){
        MessageChannel._responseHandlers.put(getHandlerId(messageId), handler);
    }

    public void clearAllHandlers() {
        clearHandlers();
        clearResponseHandlers();
    }

    public void clearHandler(String messageId) throws Exception {
        String handlerId = getHandlerId(messageId);

        if (!MessageChannel._handlers.containsKey(handlerId)) {
            throw new Exception(getNoHandlerExceptionMessage(messageId));
        }

        MessageChannel._handlers.remove(handlerId);
    }

    public void clearHandlers() {
        ArrayList<String> channelKeys = new ArrayList<>();

        for (String key: MessageChannel._handlers.keySet()) {
            if (key.startsWith(_channelId)) {
                channelKeys.add(key);
            }
        }

        if (channelKeys.size() > 0) {
            MessageChannel._handlers.keySet().removeAll(channelKeys);
        }
    }

    public void clearResponseHandler(String messageId) throws Exception {
        String handlerId = getHandlerId(messageId);

        if (!MessageChannel._responseHandlers.containsKey(handlerId)) {
            throw new Exception(getNoHandlerExceptionMessage(messageId));
        }

        MessageChannel._responseHandlers.remove(handlerId);
    }

    public void clearResponseHandlers() {
        ArrayList<String> channelKeys = new ArrayList<>();

        for (String key: MessageChannel._responseHandlers.keySet()) {
            if (key.startsWith(_channelId)) {
                channelKeys.add(key);
            }
        }

        if (channelKeys.size() > 0) {
            MessageChannel._responseHandlers.keySet().removeAll(channelKeys);
        }
    }

    String getHandlerId(String messageId) {
        return String.format("%1$s_%2$s", _channelId, messageId);
    }

    String getNoHandlerExceptionMessage(String messageId) {
        return String.format("No Handler for %1$s", messageId);
    }
}