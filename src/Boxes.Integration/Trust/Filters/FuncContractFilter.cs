namespace Boxes.Integration.Trust.Filters
{
    using System;
    using Contexts;
    using Contexts.BoxesExtensions;

    /// <summary>
    /// apply the trust of a type given its package context
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    public class FuncContractFilter<TContract> : ContractFilter<TContract>
    {
        private readonly Func<TypeFromPackageTrustContext, bool> _trust;

        public FuncContractFilter(Func<TypeFromPackageTrustContext, bool> trust)
        {
            _trust = trust;
        }

        protected override bool IsTrustedContext(TypeFromPackageTrustContext trustContext)
        {
            return _trust(trustContext);
        }
    }
}