namespace Boxes.Integration.Trust.Filters
{
    using Contexts;

    /// <summary>
    /// A trust filter will see if the context is legit and there is no bad intention happening
    /// </summary>
    public interface ITrustFilter
    {
        /// <summary>
        /// to see if this filter can be applied to the current trust context
        /// </summary>
        /// <param name="trustContext">the trust context</param>
        /// <returns></returns>
        bool CanHandle(TrustContext trustContext);

        /// <summary>
        /// returns if the context can be trusted
        /// </summary>
        /// <param name="trustContext">the trust context</param>
        /// <returns>true if the context can be trusted</returns>
        bool IsTrusted(TrustContext trustContext);
    }
}