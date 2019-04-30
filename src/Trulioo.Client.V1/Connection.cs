using System;

namespace Trulioo.Client.V1
{
    /// <summary>
    ///     Provides a class for working with Trulioo Connection.
    /// </summary>
    public class Connection
    {
        private readonly TruliooApiClient _service;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="service">
        ///     An object representing the root of Trulioo configuration service.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="service" /> is <c>null</c>.
        /// </exception>
        protected internal Connection(TruliooApiClient service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        private Context Context => this._service?.Context;
    }
}