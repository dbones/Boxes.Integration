// Copyright 2012 - 2013 dbones.co.uk (David Rundle)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
namespace Boxes.Integration
{
    using System.Collections.Generic;
    using System.Linq;
    using ContainerSetup;
    using Discovering;
    using Loading;
    using Process;
    using Setup;
    using Tasks;

    /// <summary>
    /// Wrapper around Boxes, and extends it with an IoC container 
    /// </summary>
    public abstract class BoxesWrapperBase<TContainer> : IBoxesWrapper
    {
        //TODO: look at disabling registrations http://ayende.com/blog/2792/introducing-monorail-hotswap 
        //issue is around the containers, a subset allow to deletions of registrations
        //an other way is to restart the app and only register the required packages with the IoC
        //the second way can be supported by coding a module

        //TODO: migrations
        //updates of a module will require a restart, currently the Loaders do not support 
        //app domains. they do offer a level of isolation

        /// <summary>
        /// the main process line, add tasks to this, and they will be executed
        /// in the order defined by <see cref="IProcessOrder"/>
        /// </summary>
        private readonly PipelineExecutorWrapper<ProcessPackageContext> _processPipeline = new PipelineExecutorWrapper<ProcessPackageContext>();
        /// <summary>
        /// this is the pre-process pipeline, however its not recommended to use this one.
        /// </summary>
        private readonly PipelineExecutorWrapper<ProcessPackageContext> _preProcessPipeline = new PipelineExecutorWrapper<ProcessPackageContext>();

        private readonly BoxesSetup _setup;

        protected BoxesWrapperBase(TContainer container)
        {
            Container = container;
            Initalise(container);
            _setup = new BoxesSetup(this);
            BoxesIntegrationSetup = new BoxesIntergrationSetup(_setup);
        }

        public virtual PackageRegistry PackageRegistry { get { return DependencyResolver.Resolve<PackageRegistry>(); } }
        public virtual IBoxesContainerSetup BoxesContainerSetup { get { return DependencyResolver.Resolve<IBoxesContainerSetup>(); } }
        public virtual IBoxesIntegrationSetup BoxesIntegrationSetup { get; private set; }

        /// <summary>
        /// Dev notes in the remarks
        /// </summary>
        /// <remarks>
        /// Register with the IoC and Set the following properties 
        ///     <see cref="DependencyResolver"/>, 
        ///     <see cref="IBoxesWrapper.BoxesContainerSetup"/>, 
        /// and <see cref="IBoxesWrapper.PackageRegistry"/>  
        /// </remarks>
        /// <param name="container">the container which was passed to the ctor</param>
        protected abstract void Initalise(TContainer container);

        /// <summary>
        /// access to the container (kernel in the case of Ninject)
        /// </summary>
        protected virtual TContainer Container { get; private set; }

        /// <summary>
        /// The Dependency resolver
        /// </summary>
        public abstract IDependencyResolver DependencyResolver { get; }

        /// <summary>
        /// Sets up the <see cref="IPackageScanner"/> and <see cref="ILoader"/>
        /// Dev notes in the remarks
        /// </summary>
        /// <remarks>
        /// Register the <see cref="IPackageScanner"/> and <see cref="ILoader"/>
        /// </remarks>
        /// <typeparam name="TLoader"></typeparam>
        /// <param name="defaultPackageScanner"></param>
        public abstract void Setup<TLoader>(IPackageScanner defaultPackageScanner)
            where TLoader : ILoader;

        public virtual void DiscoverPackages(IPackageScanner packageScanner)
        {
            PackageRegistry.DiscoverPackages(packageScanner);
        }

        public virtual void DiscoverPackages()
        {
            PackageRegistry.DiscoverPackages(DependencyResolver.Resolve<IPackageScanner>());
        }

        public virtual void LoadPackages()
        {
            //TODO:review this method
            //there are a number of phases
            //1. Extensions
            //2. Filter and organise packages
            //3. IoC registrations
            //4. PreProcess
            //5. Process

            //only process newly loaded packages
            //each is a phase has to be isolated, as they may depend on
            //one another
            var loader = new LoaderProxy(DependencyResolver.Resolve<ILoader>());
            PackageRegistry.LoadPackages(loader);

            //process extensions first (in complete, before running the rest of the package or process)
            _setup.ExtensionRunner.Execute(loader.Packages).Force();

            //filter out the packages which are not in use.
            IEnumerable<Package> filteredPackages =
                _setup.GlobalPackagesFilter == null
                    ? loader.Packages
                    : _setup.GlobalPackagesFilter.FilterPackages(loader.Packages);

            //get process Order
            IEnumerable<Package> packages = _setup.ProcessOrder.Arrange(filteredPackages);

            //find the types in each package
            var processContexts =
                packages.Select(
                x =>
                {
                    IPackageTypesFilter typesFilter;
                    if (!_setup.PackageTypesFilters.TryGetValue(x.Name, out typesFilter))
                    {
                        typesFilter = _setup.DefaultPackageTypesFilter;
                    }

                    var context = new ProcessPackageContext(x, typesFilter.FilterTypes(x));
                    return context;
                }).ToList(); //save the result, as we may need multiple iterations

            //we need to register all the types with the underlying IoC first
            _setup.IocTask.UpdateTasksAsRequired();
            _setup.IocRunner.Execute(processContexts).Force();

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

        public abstract void Dispose();
    }
}