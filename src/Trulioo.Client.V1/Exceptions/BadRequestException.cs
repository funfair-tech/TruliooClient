using System;

namespace Trulioo.Client.V1.Exceptions
{
    /// <inheritdoc />
    public sealed class BadRequestException : RequestException
    {
        internal BadRequestException(string message, int code, string reason)
            : base(message, code, reason)
        {
        }

        /// <inheritdoc />
        public BadRequestException()
            : base(message: "", code: -1, reason: "")
        {
        }

        /// <inheritdoc />
        public BadRequestException(string message)
            : base(message, code: -1, reason: "")
        {
        }

        /// <inheritdoc />
        public BadRequestException(string message, Exception innerException)
            : base(message, code: -1, innerException.Message)
        {
        }
    }
}