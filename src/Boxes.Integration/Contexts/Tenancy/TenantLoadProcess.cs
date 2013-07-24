namespace Boxes.Integration.Contexts.Tenancy
{
    using System.Collections.Generic;
    using System.Linq;
    using Boxes.Integration.Process;
    using Boxes.Integration.Tasks;
    using Boxes.Integration.Factories;
    using Boxes.Integration.Setup;
    using Setup.Filters;
    using Trust;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    /// <typeparam name="TContainer"></typeparam>
    public abstract class TenantLoadProcess<TBuilder, TContainer> : ITenantLoadProcess
    {
        private readonly PackageRegistry _packageRegistry;
        private readonly IIoCFactory<TBuilder, TContainer> _ioCFactory;
        private readonly IProcessOrder _processOrder;
        private readonly ITenantContainerSetup<TBuilder> _tenantContainerSetup;
        private readonly ITrustManager _trustManager;

        private readonly PipelineExecutorWrapper<RegistrationContext<TBuilder>> _iocPipeline = new PipelineExecutorWrapper<RegistrationContext<TBuilder>>();


        public TenantLoadProcess(
            PackageRegistry packageRegistry,  
            IIoCFactory<TBuilder, TContainer> ioCFactory, 
            IProcessOrder processOrder,
            ITenantContainerSetup<TBuilder> tenantContainerSetup,
            ITrustManager trustManager
            )
        {
            _packageRegistry = packageRegistry;
            _ioCFactory = ioCFactory;
            _processOrder = processOrder;
            _tenantContainerSetup = tenantContainerSetup;
            _trustManager = trustManager;
        }

        public void LoadPackages(Tenant tenant, IEnumerable<string> packagesToEnable)
        {
            //1. destroy container in tenant
            //2. filter out packages, based on the enabled list
            //3. sort packages
            //4. create container/child container and register packages
            //5. run post process tasks

            tenant.Container.TryDispose();
            var builder = _ioCFactory.CreateBuilder();

            //TODO: check if there are any missing packages, which also need to be enabled

            var loadablePackages = 
                _packageRegistry.Packages
                    .Where(x => x.CanLoad)
                    .Where(x=> packagesToEnable.Contains(x.Name));

            //get process Order
            IEnumerable<Package> packages = _processOrder.Arrange(loadablePackages);

            //find the types in each package (filter as much as we can)
            var processContexts =
                packages.Select(
                    x =>
                        {
                            ITypeRegistrationFilter typesFilter = _tenantContainerSetup.GetTypeRegistrationFilter(x.Name) ??
                                                                  _tenantContainerSetup.DefaultTypeRegistrationFilter;

                            var context = new ProcessPackageContext(x, typesFilter.FilterTypes(x).ToArray());

                            //TODO: check to see if the context is trusted

                            return context;
                        }).ToList(); //save the result, as we may need multiple iterations

            //we need to register all the types with the tenants IoC first
            IEnumerable<RegistrationContext<TBuilder>> registrationContexts = 
                processContexts
                .SelectMany(x => x.DependencyTypes)
                .Select(x => new RegistrationContext<TBuilder>(x, builder));
            
            _iocPipeline.UpdateTasksAsRequired(_tenantContainerSetup.Registrations);
            _iocPipeline.Execute(registrationContexts).Force();


            //create the container from the builder (if required)
            var container = _ioCFactory.CreateContainer(builder);
            tenant.Container = container;

            
        }

        public void LoadPackages(IExecutionContext executionContext, IEnumerable<string> packagesToEnable)
        {
            LoadPackages(executionContext.CurrentTenant, packagesToEnable);
        }
    }
}