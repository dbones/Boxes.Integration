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
namespace Boxes.Integration.Extensions
{
    using ApplicationContext.Tenancy;
    using Tasks;

    /// <summary>
    /// this will run only once, when the package is first installed.
    /// the migrations logic should be run in this class too. To use you must register <see cref="SetupPackageTask"/>
    /// </summary>
    public interface IPackageSetup
    {
        /// <summary>
        /// determines if the Setup method should run
        /// </summary>
        bool HasAlreadyBeenSetup { get; }

        /// <summary>
        /// sets up the module ready to be loaded, <see cref="IPackageBootup"/>
        /// </summary>
        /// <param name="tenant">the tenant which we are setting up the package for</param>
        /// <param name="dependencyResolver">the main container, allow you access to any registered objects</param>
        void Setup(Tenant tenant, IDependencyResolver dependencyResolver);
    }
}