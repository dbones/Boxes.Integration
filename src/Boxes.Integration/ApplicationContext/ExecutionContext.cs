namespace Boxes.Integration.ApplicationContext
{
    using Global;
    using Tenancy;

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