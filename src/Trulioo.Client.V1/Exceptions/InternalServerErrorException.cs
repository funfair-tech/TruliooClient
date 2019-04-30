namespace Trulioo.Client.V1.Exceptions
{
    /// <inheritdoc />
    public sealed class InternalServerErrorException : RequestException
    {
        /// <inheritdoc />
        internal InternalServerErrorException(string message, int code, string reason)
            : base(message, code, reason)
        {
        }

        /// <inheritdoc />
        public InternalServerErrorException() : base("", -1, "")
        {
        }

        /// <inheritdoc />
        public InternalServerErrorException(string message) : base(message, -1, "")
        {
        }

        /// <inheritdoc />
        public InternalServerErrorException(string message, System.Exception innerException) : base(message, -1, innerException.Message)
        {
        }
    }
}
