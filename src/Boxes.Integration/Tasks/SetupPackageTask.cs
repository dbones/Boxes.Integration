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
    using System.Linq;
    using Extensions;

    /// <summary>
    /// task to handle <see cref="IPackageSetup"/>
    /// </summary>
    public class SetupPackageTask : RunOnceBoxesTask<ProcessPackageContext>
    {
        private readonly IDependencyResolver _dependencyResolver;

        public SetupPackageTask(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        protected override bool CanHandleItem(ProcessPackageContext item)
        {
            return true;
        }

        protected override void ExecuteOnItem(ProcessPackageContext item)
        {
            var packageSetupType = item.DependencyTypes.FirstOrDefault(x => typeof (IPackageSetup).IsAssignableFrom(x));
            if (packageSetupType == null)
            {
                return;
            }
            
            //get an instance of this type
            var packageSetup = (IPackageSetup)_dependencyResolver.Resolve(packageSetupType);
            
            if(packageSetup.HasAlreadyBeenSetup)
            {
                return;
            }

            packageSetup.Setup(_dependencyResolver);
            _dependencyResolver.Release(packageSetup);
        }
    }

    


}