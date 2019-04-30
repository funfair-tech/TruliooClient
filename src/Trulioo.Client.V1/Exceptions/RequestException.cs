using System;

namespace Trulioo.Client.V1.Exceptions
{
    /// <inheritdoc />
    public class RequestException : Exception
    {
        /// <inheritdoc />
        protected internal RequestException(string message, int code, string reason)
            : base(message)
        {
            this.Code = code;
            this.Reason = reason;
        }

        /// <inheritdoc />
        public RequestException()
        {
        }

        /// <inheritdoc />
        public RequestException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        public RequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     Gets the code of the current <see cref="RequestException" />.
        /// </summary>
        /// <value>
        ///     Code of the current <see cref="RequestException" />.
        /// </value>
        private int Code { get; }

        /// <summary>
        ///     Gets the reason of the current <see cref="RequestException" />.
        /// </summary>
        /// <value>
        ///     Reason of the current <see cref="RequestException" />.
        /// </value>
        private string Reason { get; }

        /// <inheritdoc />
        public override string Message => string.IsNullOrEmpty(base.Message) ? this.Reason : base.Message;
    }
}