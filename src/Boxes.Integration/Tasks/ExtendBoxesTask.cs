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
namespace Boxes.Integration.Tasks
{
    using System;
    using System.Linq;
    using Boxes.Tasks;
    using Extensions;
    using InternalIoc;
    using Setup;
    using Trust;
    using Trust.Contexts;

    internal class ExtendBoxesTask<TBuilder> : IBoxesTask<Package>
    {
        private readonly IInternalContainer _container;
        private readonly ITrustManager _trustManager;

        public ExtendBoxesTask(IInternalContainer container)
        {
            _container = container;
            _trustManager = container.Resolve<ITrustManager>();
        }

        public bool CanHandle(Package item)
        {
            return item.Loaded && item.Manifest is ExtensionManifest;
        }

        public void Execute(Package item)
        {
            //TODO:clean up

            var manifest = (ExtensionManifest)item.Manifest;
            var assemblies = manifest
                .Extensions
                .Select(item.GetInternalAssembly)
                .Select(x=>
                        {
                            if(x.Assembly == null)
                            {
                                x.LoadFromFile();
                            }
                            return x.Assembly;
                        });

            var types = assemblies.SelectMany(x => x.GetExportedTypes()).ToArray();

            foreach (var service in types.Where(x => typeof(IBoxesExtension).IsAssignableFrom(x)))
            {
                var contract = service.FirstInterface();
                _trustManager.IsTrusted(new PackageTrustContext(contract, service, item));
                _container.Add(contract, service);
            }
            
            //TODO: make this an extendable area.

            var globalSetup = _container.Resolve<IApplicationContainerSetup<TBuilder>>();
            foreach (var setup in types.Where(x => typeof(IApplicationContainerSetup).IsAssignableFrom(x)))
            {
                var globalSetupInstance = (IApplicationContainerSetup)Activator.CreateInstance(setup);
                globalSetupInstance.Setup(globalSetup);
            }

            var tenantSetup = _container.Resolve<ITenantContainerSetup<TBuilder>>();
            foreach (var setup in types.Where(x => typeof(ITenantContainerSetup).IsAssignableFrom(x)))
            {
                var tenantSetupInstance = (ITenantContainerSetup) Activator.CreateInstance(setup);
                tenantSetupInstance.Setup(tenantSetup);
            }
        }
    }
}