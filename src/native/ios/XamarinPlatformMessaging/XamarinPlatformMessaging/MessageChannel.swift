//
//  MessageChannel.swift
//  XamarinPlatformMessaging
//
//  Created by Mike Parker on 23/11/2020.
//

import Foundation

@objc(MessageChannel)
@objcMembers
public class MessageChannel : NSObject {
    
    public typealias MessageHandler = ([NSObject]?) throws -> Void
    public typealias MessageResponseHandler = ([NSObject]?) throws -> NSObject
    
    static var _handlers = Dictionary<String, MessageHandler>()
    static var _responseHandlers = Dictionary<String, MessageResponseHandler>()
    
    var _channelId : String
    
    public init(channelId: String) {
        _channelId = channelId
    }
    
    public func send(messageId: String) throws -> Void {
        try send(messageId: messageId, parameters: nil)
    }
    
    public func send(messageId: String, parameters: NSObject...) throws -> Void {
        try send(messageId: messageId, parameters: parameters)
    }
    
    public func send(messageId: String, parameters: [NSObject]?) throws -> Void {
        let handlerId = getHandlerId(messageId: messageId)
        
        if let handler = MessageChannel._handlers[handlerId] {
            do {
                return try handler(parameters)
            }
            catch {
                throw error
            }
        }
        
        throw MessageChannelError.noHandlerForMethod
    }
    
    public func sendForResponse(messageId: String) throws -> NSObject {
        return try sendForResponse(messageId: messageId, parameters: nil)
    }
    
    public func sendForResponse(messageId: String, parameters: NSObject...) throws -> NSObject {
        return try sendForResponse(messageId: messageId, parameters: parameters)
    }
    
    public func sendForResponse(messageId: String, parameters: [NSObject]?) throws -> NSObject {
        let handlerId = getHandlerId(messageId: messageId)
        
        if let handler = MessageChannel._responseHandlers[handlerId] {
            do {
                return try handler(parameters)
            }
            catch {
                throw error
            }
        }
        
        throw MessageChannelError.noHandlerForMethod
    }
    
    public func setHandler(forMessageId messageId: String, handler: @escaping MessageHandler) {
        MessageChannel._handlers[getHandlerId(messageId: messageId)] = handler
    }

    public func setHandler(forMessageId messageId: String, handler: @escaping ([NSObject]?) -> Void) {
        MessageChannel._handlers[getHandlerId(messageId: messageId)] = handler
    }
    
    public func setResponseHandler(forMessageId messageId: String, handler: @escaping MessageResponseHandler) {
        MessageChannel._responseHandlers[getHandlerId(messageId: messageId)] = handler
    }
    
    public func setResponseHandler(forMessageId messageId: String, handler: @escaping ([NSObject]?) -> NSObject) {
        MessageChannel._responseHandlers[getHandlerId(messageId: messageId)] = handler
    }
    
    public func clearAllHandlers() {
        clearHandlers()
        clearResponseHandlers()
    }
    
    public func clearHandler(forMessageId messageId: String) throws {
        let handlerId = getHandlerId(messageId: messageId)
        
        if (!MessageChannel._handlers.keys.contains(handlerId)) {
            throw MessageChannelError.noHandlerForMethod
        }
        
        MessageChannel._handlers.removeValue(forKey: handlerId)
    }
    
    public func clearHandlers() {
        let channelKeys = MessageChannel._handlers.filter { $0.key.starts(with: _channelId) }.keys
        
        for key in channelKeys {
            MessageChannel._handlers.removeValue(forKey: key)
        }
    }
    
    public func clearResponseHandler(forMessageId messageId: String) throws {
        let handlerId = getHandlerId(messageId: messageId)
        
        if (!MessageChannel._responseHandlers.keys.contains(handlerId)) {
            throw MessageChannelError.noHandlerForMethod
        }
        
        MessageChannel._responseHandlers.removeValue(forKey: handlerId)
    }
    
    public func clearResponseHandlers() {
        let channelKeys = MessageChannel._responseHandlers.filter { $0.key.starts(with: _channelId) }.keys
        
        for key in channelKeys {
            MessageChannel._responseHandlers.removeValue(forKey: key)
        }
    }
    
    func getHandlerId(messageId: String) -> String {
        return "\(_channelId)_\(messageId)"
    }
}
