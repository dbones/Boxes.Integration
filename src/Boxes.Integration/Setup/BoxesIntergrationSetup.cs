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
    using Boxes.Tasks;
    using Process;

    public class BoxesIntergrationSetup : IBoxesIntegrationSetup
    {
        private readonly BoxesSetup _setup;

        internal BoxesIntergrationSetup(BoxesSetup setup)
        {
            _setup = setup;
        }

        public void AddPackgeLevelFilter(IPackageTypesFilter typeTypesFilter, params string[] packgeName)
        {
            foreach (var name in packgeName)
            {
                _setup.PackageTypesFilters.Add(name, typeTypesFilter);
            }
        }

        public void SetDefaultPackgeLevelFilter(IPackageTypesFilter typeTypesFilter)
        {
            _setup.DefaultPackageTypesFilter = typeTypesFilter;
        }

        public void SetGlobalPackgeLevelFilter(IPackageFilter typeTypesFilter)
        {
            _setup.GlobalPackagesFilter = typeTypesFilter;
        }

        public void RegisterPreProcessTask(IBoxesTask<ProcessPackageContext> task)
        {
            _setup.PreProcesTasks.Add(task);
        }

        public void RegisterProcessTask(IBoxesTask<ProcessPackageContext> task)
        {
            _setup.ProcesTasks.Add(task);
        }

        public void SetProcessOrder(IProcessOrder orderPackages)
        {
            _setup.ProcessOrder = orderPackages;
        }
    }
}