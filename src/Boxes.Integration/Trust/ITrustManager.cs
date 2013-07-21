namespace Boxes.Integration.Trust
{
    using Contexts;
    using Filters;

    /// <summary>
    /// a trust manager is responsible for validating components to see if they are to be trusted
    /// some things which the application may be interested in is if a dll has been tampered with
    /// or if a class type is being exposed from a known dll or a dll which has a certain public key
    /// </summary>
    /// <remarks>
    /// This works on a optimistic way, it looks for a black listing, not a white one.
    /// </remarks>
    public interface ITrustManager
    {
        /// <summary>
        /// this will detail if a class/dll etc is trusted
        /// </summary>
        /// <param name="context">the current context to investigate</param>
        /// <returns>return true if the context can be trusted</returns>
        void IsTrusted(TrustContext context);

        /// <summary>
        /// add a trust filter for the manager to use
        /// </summary>
        /// <param name="trust">the filer to be applied</param>
        void AddTrust(ITrustFilter trust);
    }
}