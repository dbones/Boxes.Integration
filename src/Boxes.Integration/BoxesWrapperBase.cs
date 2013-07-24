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
    using System;
    using Boxes.Tasks;
    using Discovering;
    using Factories;
    using InternalIoc;
    using Loading;
    using Tasks;
    using Trust;

    public abstract class BoxesWrapperBase<TBuilder, TContainer> : IBoxesWrapper<TBuilder>
    {

        //TODO: look at disabling registrations http://ayende.com/blog/2792/introducing-monorail-hotswap 
        //issue is around the containers, a subset allow to deletions of registrations
        //an other way is to restart the app and only register the required packages with the IoC
        //the second way can be supported by coding a module
        //favored way, use of child container, will need to re-initialise the container (delete, create a new one)

        //TODO: migrations
        //updates of a module will require a restart, currently the Loaders do not support 
        //app domains. they do offer a level of isolation

        //TODO: Multi-tenancy
        //use child containers to handle tenants. implement a IProcess (consider PLinq) to register
        //the correct packages with the correct child container.

        private IPackageScanner _defaultPackageScanner;
        private ILoader _loader;
        private readonly TaskRunner<Package> _extensionRunner;
        private readonly LoaderFactory _loaderFactory;
        private readonly IInternalContainer _internalContainer;

        protected BoxesWrapperBase()
        {
            _internalContainer = new InternalInternalContainer();
            PackageRegistry = new PackageRegistry();
            _extensionRunner = new TaskRunner<Package>(new ExtendBoxesTask(_internalContainer));
            _loaderFactory = new LoaderFactory();
            
        }

        public PackageRegistry PackageRegistry { get; private set; }

        public void Setup<TLoader>(IPackageScanner defaultPackageScanner) where TLoader : ILoader
        {
            _defaultPackageScanner = defaultPackageScanner;
            _loader = _loaderFactory.CreateLoader<TLoader>(PackageRegistry);
        }

        public void DiscoverPackages(IPackageScanner packageScanner)
        {
            PackageRegistry.DiscoverPackages(packageScanner);
        }

        public void DiscoverPackages()
        {
            PackageRegistry.DiscoverPackages(_defaultPackageScanner);
        }

        public void LoadPackages()
        {
            var loader = new LoaderProxy(_loader);
            PackageRegistry.LoadPackages(loader);

            //process extensions first (completely, before running the rest of the package or process)
            _extensionRunner.Execute(loader.Packages).Force();
        }

        public T GetService<T>()
        {
            return _internalContainer.Resolve<T>();
        }




        public virtual void Dispose()
        {
            _internalContainer.Dispose();
        }
        
    }


    public class App
    {
        public void Main()
        {
            //IBoxesWrapper<> boxes = null;
            ////boxes.BoxesIntegrationSetup
            ////how do we want to configure?

            //boxes.Setup<IsolatedLoader>(new PackageScanner("folder"));
            //boxes.DiscoverPackages();
            //boxes.LoadPackages();



            //boxes.EnablePackages("", "", "", "", ""); //auto detect the tenant
            //boxes.DependencyResolver.Resolve<Object>();
        }


    }

}