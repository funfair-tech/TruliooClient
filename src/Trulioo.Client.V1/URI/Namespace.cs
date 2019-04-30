using System;

namespace Trulioo.Client.V1.URI
{
    /// <summary>
    ///     Specifies the name space context for a resource.
    /// </summary>
    internal sealed class Namespace
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Namespace" /> class.
        /// </summary>
        /// <param name="value"></param>
        public Namespace(string value)
        {
            this.Value = value;
        }

        private Namespace()
        {
            this.Value = "";
        }

        /// <summary>
        ///     Gets the user of the current <see cref="Namespace" />.
        /// </summary>
        /// <value>
        ///     The user of the current <see cref="Namespace" />.
        /// </value>
        private string Value { get; }

        /// <summary>
        ///     Converts the value of the current <see cref="Namespace" /> object to its equivalent URI encoded string representation.
        /// </summary>
        /// <remarks>
        ///     The value is converted using <see cref="Uri.EscapeUriString" />.
        /// </remarks>
        /// <returns>
        ///     A string representation of the current <see cref="Namespace" />
        /// </returns>
        public string ToUriString()
        {
            return this.ToString(Uri.EscapeDataString);
        }

        private string ToString(Func<string, string> encode)
        {
            return string.Concat(str0: "/", encode(this.Value));
        }
    }
}