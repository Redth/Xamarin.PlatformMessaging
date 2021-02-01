//
//  ViewController.swift
//  XamarinPlatformMessaging.XpmTestApp
//
//  Created by Mike Parker on 01/02/2021.
//

import UIKit
import XamarinPlatformMessaging

class ViewController: UIViewController {
    let _messageChannel = MessageChannel(channelId: "my_channel")
       
    override func viewDidLoad() {
       super.viewDidLoad()
       // Do any additional setup after loading the view.
       
       do {
           try _messageChannel.send(messageId: "print_text")
           try _messageChannel.send(messageId: "print_text", parameters: "argument" as NSObject)
       }
       catch MessageChannelError.noHandlerForMethod {
           print("No handler for print_text")
       }
       catch {
           print("Unexpected error: \(error)")
       }
       
       do {
           print(try _messageChannel.sendForResponse(messageId: "get_text"))
           print(try _messageChannel.sendForResponse(messageId: "get_text", parameters: "argument" as NSObject))
       }
       catch MessageChannelError.noHandlerForMethod {
           print("No handler for get_text")
       }
       catch {
           print("Unexpected error: \(error)")
       }
       
       _messageChannel.setHandler(forMessageId: "print_text") { (parameters) in
           if let arg = parameters {
               print("Handled print_text with \(arg)")
           }
           else {
               print("Handled print_text with no parameter")
           }
       }
       
       _messageChannel.setResponseHandler(forMessageId: "get_text") { (parameters) -> NSObject in
           if let arg = parameters {
               return "Response for get_text with \(arg)" as NSObject
           }
           
           return "Response for get_text with no parameter" as NSObject
       }
       
       do {
           try _messageChannel.send(messageId: "print_text")
           try _messageChannel.send(messageId: "print_text", parameters: "argument" as NSObject)
           print(try _messageChannel.sendForResponse(messageId: "get_text"))
           print(try _messageChannel.sendForResponse(messageId: "get_text", parameters: "argument" as NSObject))
       }
       catch MessageChannelError.noHandlerForMethod {
           print("No handler for either print_text or get_text")
       }
       catch {
           print("Unexpected error: \(error)")
       }
       
       _messageChannel.clearAllHandlers()
       
       do {
           try _messageChannel.send(messageId: "print_text")
       }
       catch MessageChannelError.noHandlerForMethod {
           print("No handler for print_text")
       }
       catch {
           print("Unexpected error: \(error)")
       }
       
       do {
           print(try _messageChannel.sendForResponse(messageId: "get_text"))
       }
       catch MessageChannelError.noHandlerForMethod {
           print("No handler for get_text")
       }
       catch {
           print("Unexpected error: \(error)")
       }
   }
}
