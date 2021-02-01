//
//  XpmTestLib.swift
//  XpmTestLib
//
//  Created by Mike Parker on 18/12/2020.
//

import Foundation
import XamarinPlatformMessaging

@objc(XpmTestLib)
public class XpmTestLib : NSObject {
    
    static var _channel : MessageChannel?
    
    @objc(start)
    public static func start() {
        _channel = MessageChannel(channelId: "xpm_test_lib")
        
        if let channel = _channel {
            channel.setHandler(forMessageId: "log_hello_text") { (parameter) in
                NSLog("Hello from Swift")
                sendMessage(messageId: "log_hello_text_executed", value: "Hello from Swift")
            }
            
            channel.setHandler(forMessageId: "log_custom_text") { (parameter) in
                if let params = parameter, let arg = params.first as? NSString {
                    NSLog(arg.description)
                    sendMessage(messageId: "log_custom_text_executed", value: arg.description)
                }
                else {
                    NSLog("No text provided")
                    sendMessage(messageId: "log_custom_text_executed", value: "No text provided")
                }
            }
            
            channel.setResponseHandler(forMessageId: "get_text") { (parameter) in
                return "Hello from Swift" as NSObject
            }
            
            channel.setResponseHandler(forMessageId: "get_text_with_param") { (parameter) in
                if let params = parameter, let arg = params.first as? NSString {
                    return "Parameter received: \(arg)" as NSObject
                }
                
                return "No parameter received" as NSObject
            }
        }
        
        sendMessage(messageId: "start_executed")
    }
    
    @objc(stop)
    public static func stop() {
        guard let channel = _channel else {return}
        
        do {
            try channel.clearHandler(forMessageId: "log_hello_text")
            try channel.clearHandler(forMessageId: "log_custom_text")
            try channel.clearResponseHandler(forMessageId: "get_text")
            try channel.clearResponseHandler(forMessageId: "get_text_with_param")
        }
        catch {
            NSLog(error.localizedDescription)
        }
        
        sendMessage(messageId: "stop_executed")
        _channel = nil
    }
    
    static func sendMessage(messageId: String) {
        sendMessage(messageId: messageId, value: nil)
    }
    
    static func sendMessage(messageId: String, value: String?) {
        if let channel = _channel {
            do {
                if let arg = value {
                    try channel.send(messageId: messageId, parameters: arg as NSObject)
                }
                else {
                    try channel.send(messageId: messageId)
                }
            } catch {
                NSLog(error.localizedDescription)
            }
        }
    }
}
