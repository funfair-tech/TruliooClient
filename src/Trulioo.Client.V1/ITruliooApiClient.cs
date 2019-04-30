namespace Trulioo.Client.V1
{
    /// <summary>
    /// Provides an operational interface to the Trulioo API V1.
    /// </summary>
    public interface ITruliooApiClient
    {
        /// <summary>
        /// Gets the <see cref="Configuration"/> instance for this <see cref="ITruliooApiClient"/>.
        /// </summary>
        IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the <see cref="Verification"/> instance for this <see cref="ITruliooApiClient"/>.
        /// </summary>
        IVerification Verification { get; }

        /// <summary>
        /// Gets the <see cref="Connection"/> instance for this <see cref="ITruliooApiClient"/>.
        /// </summary>
        Connection Connection { get; }
    }
}
