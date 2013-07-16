namespace Boxes.Integration.Trust.Filters
{
    using System;
    using Context;

    /// <summary>
    /// apply the trust of a type given its package context
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    public class FuncPackageTypeContractFilter<TContract> : PackageTypeContractFilter<TContract>
    {
        private readonly Func<PackageTrustContext, bool> _trust;

        public FuncPackageTypeContractFilter(Func<PackageTrustContext, bool> trust)
        {
            _trust = trust;
        }

        protected override bool IsTrustedContext(PackageTrustContext trustContext)
        {
            return _trust(trustContext);
        }
    }
}