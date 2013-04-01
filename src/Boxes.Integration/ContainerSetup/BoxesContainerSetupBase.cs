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
namespace Boxes.Integration.ContainerSetup
{
    using System;
    using System.Collections.Generic;
    using Boxes.Tasks;

    public abstract class BoxesContainerSetupBase : IBoxesContainerSetup
    {
        private readonly List<IBoxesTask<Type>> _registraionTasks = new List<IBoxesTask<Type>>();

        public virtual IEnumerable<IBoxesTask<Type>> Registrations { get { return _registraionTasks; } }

        public abstract void RegisterLifeStyle<TLifeStyle, TInterface>();

        public void RegisterLifeStyle(Registration registration)
        {
            RegisterLifeStyle(registration.RegistrationMeta);
        }

        protected abstract void RegisterLifeStyle(RegistrationMeta registration);

        protected void AddRegistrationTask(IBoxesTask<Type> registrationTask)
        {
            _registraionTasks.Add(registrationTask);
        }
    }
}