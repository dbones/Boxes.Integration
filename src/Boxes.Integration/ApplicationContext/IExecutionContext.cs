namespace Boxes.Integration.ApplicationContext
{
    using Boxes.Integration.Extensions;
    using Global;
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
        GlobalApplication Application { get; }

    }

    /// <summary>
    /// execution context
    /// </summary>
    public class ExecutionContext : IExecutionContext 
    {
        public ExecutionContext()
        {
            CurrentTenant = new Tenant();
            Application = new GlobalApplication();
        }

        public Tenant CurrentTenant { get; private set; }
        public GlobalApplication Application { get; private set; }
    }
}