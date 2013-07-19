namespace Boxes.Integration.Contexts
{
    using Tenancy;

    /// <summary>
    /// execution context
    /// </summary>
    public class ExecutionContext : IExecutionContext 
    {
        public ExecutionContext()
        {
            CurrentTenant = new Tenant();
            Application = new Application.Application();
        }

        public Tenant CurrentTenant { get; private set; }
        public Application.Application Application { get; private set; }
    }
}