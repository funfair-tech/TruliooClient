namespace Trulioo.Client.V1
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContextAware
    {
        /// <summary>
        /// Gets the <see cref="Context"/> instance for this <see cref="ITruliooApiClient"/>.
        /// </summary>
        Context Context { get; }
    }
}
