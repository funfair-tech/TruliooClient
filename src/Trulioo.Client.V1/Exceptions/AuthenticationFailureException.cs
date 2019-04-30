using System;

namespace Trulioo.Client.V1.Exceptions
{
    /// <inheritdoc />
    public sealed class AuthenticationFailureException : RequestException
    {
        /// <inheritdoc />
        internal AuthenticationFailureException(string message, int code, string reason)
            : base(message, code, reason)
        {
        }

        /// <inheritdoc />
        public AuthenticationFailureException()
            : base(message: "", code: -1, reason: "")
        {
        }

        /// <inheritdoc />
        public AuthenticationFailureException(string message)
            : base(message, code: -1, reason: "")
        {
        }

        /// <inheritdoc />
        public AuthenticationFailureException(string message, Exception innerException)
            : base(message, code: -1, innerException.Message)
        {
        }
    }
}