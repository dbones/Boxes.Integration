namespace Boxes.Integration.ApplicationContext.Tenancy
{
    using System.Collections.Generic;
    using System.Linq;
    using Boxes.Integration.ContainerSetup;
    using Boxes.Integration.Factories;
    using Boxes.Integration.Process;
    using Boxes.Integration.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    /// <typeparam name="TContainer"></typeparam>
    public abstract class TenantLoadProcess<TBuilder, TContainer> : ITenantLoadProcess
    {
        private readonly PackageRegistry _packageRegistry;
        private readonly BoxesSetup _setup;
        private readonly IIoCFactory<TBuilder, TContainer> _ioCFactory;

        /// <summary>
        /// the main process line, add tasks to this, and they will be executed
        /// in the order defined by <see cref="IProcessOrder"/>
        /// </summary>
        private readonly PipelineExecutorWrapper<ProcessPackageContext> _processPipeline = new PipelineExecutorWrapper<ProcessPackageContext>();
        /// <summary>
        /// this is the pre-process pipeline, however its not recommended to use this one.
        /// </summary>
        private readonly PipelineExecutorWrapper<ProcessPackageContext> _preProcessPipeline = new PipelineExecutorWrapper<ProcessPackageContext>();

        public TenantLoadProcess(PackageRegistry packageRegistry, BoxesSetup setup, IIoCFactory<TBuilder, TContainer> ioCFactory)
        {
            _packageRegistry = packageRegistry;
            _setup = setup;
            _ioCFactory = ioCFactory;
        }

        public void WithTenant(Tenant tenant, IEnumerable<string> packagesToEnable)
        {
            //1. destroy container in tenant
            //2. filter out packages, based on the enabled list
            //3. sort packages
            //4. create container/child container and register packages
            //5. run post process tasks

            tenant.Container.TryDispose();
            var builder = _ioCFactory.CreateBuilder();

            var loadablePackages = 
                _packageRegistry.Packages
                    .Where(x => x.CanLoad)
                    .Where(x=> packagesToEnable.Contains(x.Name));

            //get process Order
            IEnumerable<Package> packages = _setup.ProcessOrder.Arrange(loadablePackages);

            //find the types in each package
            var processContexts =
                packages.Select(
                    x =>
                        {
                            ITypeRegistrationFilter typesFilter;
                            if (!_setup.PackageTypesFilters.TryGetValue(x.Name, out typesFilter))
                            {
                                typesFilter = _setup.DefaultTypeRegistrationFilter;
                            }

                            var context = new ProcessPackageContext(x, typesFilter.FilterTypes(x));
                            return context;
                        }).ToList(); //save the result, as we may need multiple iterations

            //we need to register all the types with the underlying IoC first
            _setup.IocTask.UpdateTasksAsRequired();
            _setup.IocRunner.Execute(processContexts).Force();

            //create the container from the builder (if required)
            var container = _ioCFactory.CreateContainer(builder);
            tenant.Container = container;

            //any pre-processing, hopefully there is none! as it is not recommended
            if (_setup.PreProcesTasks.Count > 0)
            {
                _preProcessPipeline.UpdateTasksAsRequired(_setup.PreProcesTasks);
                _preProcessPipeline.Execute(processContexts).Force();
            }

            //finally run the Setup and boot up of all the newly found packages 
            //(tying to process them together, package by package)
            if (_setup.ProcesTasks.Count > 0)
            {
                _processPipeline.UpdateTasksAsRequired(_setup.ProcesTasks);
                _processPipeline.Execute(processContexts).Force();
            }
        }
    }
}