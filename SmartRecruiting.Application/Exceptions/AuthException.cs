using System;

namespace SmartRecruiting.Application.Exceptions {
    public class AuthException : Exception {
        public AuthException() : base($"Authentication failed.") { }
    }
}