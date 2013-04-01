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

    internal class ExtendBoxesTask : IBoxesTask<Package>
    {
        private readonly IBoxesWrapper _boxesWrapper;

        public ExtendBoxesTask(IBoxesWrapper boxesWrapper)
        {
            _boxesWrapper = boxesWrapper;
        }

        public bool CanHandle(Package item)
        {
            return item.Loaded && item.Manifest is ExtensionManifest;
        }

        public void Execute(Package item)
        {
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

            var types = assemblies.SelectMany(x => x.GetExportedTypes()).Where(x=> typeof(IBoxesExtension).IsAssignableFrom(x));

            foreach(var type in types)
            {
                var extension = (IBoxesExtension)Activator.CreateInstance(type);
                extension.Extend(_boxesWrapper);
            }
        }
    }
}