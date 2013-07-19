namespace Boxes.Integration.Trust.Filters
{
    using Contexts;

    /// <summary>
    /// filter a contract and include its package as context, inherit this to provide the IsTrustedContext method
    /// </summary>
    /// <typeparam name="TContract">contract</typeparam>
    public abstract class PackageTypeContractFilter<TContract> : TrustFilterBase<PackageTrustContext>
    {
        protected override bool CanHandleContext(PackageTrustContext context)
        {
            return context.Contract.Is<TContract>();
        }
    }
}