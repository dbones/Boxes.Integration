namespace Boxes.Integration.Trust
{
    using System.Collections.Generic;
    using System.Linq;
    using Context;
    using Contexts;
    using Exceptions;
    using Filters;

    /// <summary>
    /// Default trust manager
    /// </summary>
    /// <remarks>
    /// this class is sealed, to try and provide some security
    /// 
    /// Also this works on a optimistic way, it looks for a black listing, not a white one.
    /// </remarks>
    public sealed class TrustManager : ITrustManager 
    {
        readonly IList<ITrustFilter> _trustFilters = new List<ITrustFilter>();

        public void IsTrusted(TrustContext context)
        {
            bool failedTrust = _trustFilters
                .Where(trustFilter => trustFilter.CanHandle(context))
                .Any(trustFilter => !trustFilter.IsTrusted(context));
            
            if(failedTrust)
            {
                throw new FailedTrustException(context);
            }
        }

        public void AddTrust(ITrustFilter trust)
        {
            _trustFilters.Add(trust);
        }
    }
}