//
//  MessageChannelError.swift
//  XamarinPlatformMessaging
//
//  Created by Mike Parker on 17/12/2020.
//

import Foundation

@objc(MessageChannelError)
public enum MessageChannelError : Int, RawRepresentable, Error {
    case noHandlerForMethod = 0
    case unknownError = 1

    public typealias RawValue = Int

    public var rawValue: RawValue {
        switch self {
        case .noHandlerForMethod:
            return 0
        default:
            return 1
        }
    }

    public init?(rawValue: RawValue) {
        switch rawValue {
        case 0:
            self = .noHandlerForMethod
        default:
            self = .unknownError
        }
    }
}
