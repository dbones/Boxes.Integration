namespace Boxes.Integration.Trust.Filters
{
    using Context;

    /// <summary>
    /// filters directly on the type of the contract and service which will be registered directly with the internal container
    /// </summary>
    /// <typeparam name="TContract">contract</typeparam>
    /// <typeparam name="TService">service</typeparam>
    public class TypeContractFilter<TContract, TService> : TrustFilterBase<TypeContractTrustContext>
        where TService : TContract
    {
        protected override bool CanHandleContext(TypeContractTrustContext context)
        {
            return context.Contract.Is<TContract>();
        }

        protected override bool IsTrustedContext(TypeContractTrustContext trustContext)
        {
            return trustContext.Service.Is<TService>();
        }
    }
}