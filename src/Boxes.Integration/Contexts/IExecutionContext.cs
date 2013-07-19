namespace Boxes.Integration.Contexts
{
    using Extensions;
    using Tenancy;

    /// <summary>
    /// this will be able to find the current execution context
    /// </summary>
    public interface IExecutionContext : IBoxesExtension
    {
        /// <summary>
        /// the current tenant
        /// </summary>
        Tenant CurrentTenant { get; }

        /// <summary>
        /// the application information
        /// </summary>
        Application.Application Application { get; }

    }
}