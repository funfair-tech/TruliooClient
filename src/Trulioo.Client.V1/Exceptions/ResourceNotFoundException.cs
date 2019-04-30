using System;

namespace Trulioo.Client.V1.Exceptions
{
    /// <inheritdoc />
    public sealed class ResourceNotFoundException : RequestException
    {
        /// <inheritdoc />
        internal ResourceNotFoundException(string message, int code, string reason)
            : base(message, code, reason)
        {
        }

        /// <inheritdoc />
        public ResourceNotFoundException()
            : base(message: "", code: -1, reason: "")
        {
        }

        /// <inheritdoc />
        public ResourceNotFoundException(string message)
            : base(message, code: -1, reason: "")
        {
        }

        /// <inheritdoc />
        public ResourceNotFoundException(string message, Exception innerException)
            : base(message, code: -1, innerException.Message)
        {
        }
    }
}