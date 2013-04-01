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
namespace Boxes.Integration.Setup
{
    using System.Collections.Generic;
    using Boxes.Tasks;
    using Process;
    using Tasks;

    internal class BoxesSetup
    {
        
        /// <summary>
        /// register the types with an IoC container (the runner)
        /// </summary>
        public TaskRunner<ProcessPackageContext> IocRunner { get; private set; }

        /// <summary>
        /// register the types with an IoC container (the task)
        /// </summary>
        public RegisterTypesWithIocPackageTask IocTask { get; private set; }

        /// <summary>
        /// define the order to process the packages
        /// </summary>
        public IProcessOrder ProcessOrder { get; set; }

        /// <summary>
        /// find any extension points and run them
        /// </summary>
        public TaskRunner<Package> ExtensionRunner { get; private set; }

        public IDictionary<string, IPackageTypesFilter> PackageTypesFilters { get; private set; }
        public IPackageTypesFilter DefaultPackageTypesFilter { get; set; }
        public IPackageFilter GlobalPackagesFilter { get; set; }

        public ICollection<IBoxesTask<ProcessPackageContext>> PreProcesTasks { get; private set; }
        public ICollection<IBoxesTask<ProcessPackageContext>> ProcesTasks { get; private set; }


        public BoxesSetup(IBoxesWrapper boxesWrapper)
        {
            ProcessOrder = new TopologicalProcessOrder();
            ExtensionRunner = new TaskRunner<Package>(new ExtendBoxesTask(boxesWrapper));
            IocTask = new RegisterTypesWithIocPackageTask(boxesWrapper.BoxesContainerSetup);
            IocRunner = new TaskRunner<ProcessPackageContext>(IocTask);

            PackageTypesFilters = new Dictionary<string, IPackageTypesFilter>();
            DefaultPackageTypesFilter = new DefaultPackageTypesFilter();

            PreProcesTasks = new List<IBoxesTask<ProcessPackageContext>>();
            ProcesTasks = new List<IBoxesTask<ProcessPackageContext>>();

        }



    }
}