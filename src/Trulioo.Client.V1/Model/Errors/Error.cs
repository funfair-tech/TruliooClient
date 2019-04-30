namespace Trulioo.Client.V1.Model.Errors
{
    /// <summary>
    /// Provides a class that represents a Trulioo service response error message.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Gets the code of the current <see cref="Error"/>.
        /// </summary>
        /// <value>
        /// Code of the current <see cref="Error"/>.
        /// </value>
        public int Code { get; set; }

        /// <summary>
        /// Gets the reason of the current <see cref="Error"/>.
        /// </summary>
        /// <value>
        /// Reason of the current <see cref="Error"/>.
        /// </value>
        public string Reason { get; set; }

        /// <summary>
        /// Gets the message of the current <see cref="Error"/>.
        /// </summary>
        /// <value>
        /// Message of the current <see cref="Error"/>.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets a string representation for the current <see cref="Error"/>.
        /// </summary>
        /// <returns>
        /// A string representation of the current <see cref="Error"/>.
        /// </returns>
        public override string ToString()
        {
            return $"{this.Code}:{this.Message}";
        }
    }
}
