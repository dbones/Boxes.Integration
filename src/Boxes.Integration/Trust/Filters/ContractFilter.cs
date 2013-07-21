namespace Boxes.Integration.Trust.Filters
{
    using Contexts;

    /// <summary>
    /// filter a contract and include its package as context, inherit this to provide the IsTrustedContext method
    /// </summary>
    /// <typeparam name="TContract">contract</typeparam>
    public abstract class ContractFilter<TContract> : TrustFilterBase<TypeFromPackageTrustContext>
    {
        protected override bool CanHandleContext(TypeFromPackageTrustContext context)
        {
            return context.Contract.Is<TContract>();
        }
    }
}