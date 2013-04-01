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

    /// <summary>
    /// Provides areas to alter/extend the process which 
    /// <see cref="Boxes.Integration"/> adds on top of Boxes
    /// </summary>
    public interface IBoxesIntegrationSetup
    {
        /// <summary>
        /// Add a package level pre filter
        /// </summary>
        /// <param name="typesFilterTypes">the filter</param>
        /// <param name="packgeName">packages which </param>
        void AddPackgeLevelFilter(IPackageTypesFilter typesFilterTypes, params string[] packgeName);
        
        /// <summary>
        /// override the default package level filter
        /// </summary>
        /// <param name="typeTypesFilter">the filter</param>
        void SetDefaultPackgeLevelFilter(IPackageTypesFilter typeTypesFilter);


        /// <summary>
        /// register a task which will run before the main process, use with caution
        /// </summary>
        /// <param name="task">the task to add</param>
        void RegisterPreProcessTask(IBoxesTask<ProcessPackageContext> task);

        /// <summary>
        /// register a task in the main process, this will run in the order prescribed by the <see cref="IProcessOrder"/>
        /// All the tasks in this process will run against a single package at a time
        /// </summary>
        /// <param name="task">task to add</param>
        void RegisterProcessTask(IBoxesTask<ProcessPackageContext> task);

        /// <summary>
        /// override the how the packages will be sorted for the main processing.
        /// </summary>
        /// <param name="orderPackages">the process order</param>
        void SetProcessOrder(IProcessOrder orderPackages);

        //TODO:ADD this, it can be used to filter out disabled Modules and Packages
        /// <summary>
        /// set a global filter, this will always be run to filter the types.
        /// </summary>
        /// <param name="packageFilter">the package filter to use</param>
        void SetGlobalPackgeLevelFilter(IPackageFilter packageFilter);
    }
}