using System;

namespace SmartRecruiting.Application.Exceptions {
    public class DuplicateEntityException : Exception {
        public DuplicateEntityException(string name, object key) : base($"{name} with {key} already exists. The operation cannot be completed.") { }
    }
}