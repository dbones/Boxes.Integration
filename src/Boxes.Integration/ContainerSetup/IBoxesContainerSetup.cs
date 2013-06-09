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

    /// <summary>
    /// setup how LifeStyles will be managed 
    /// </summary>
    public interface IBoxesContainerSetup
    {
        /// <summary>
        /// all the registration to run the types through
        /// </summary>
        IEnumerable<IBoxesTask<Type>> Registrations { get; }

        /// <summary>
        /// Simple Dependency association with a LifeStyle manager of the IoC container
        /// </summary>
        /// <typeparam name="TLifeStyle">The lifestyle manager to use</typeparam>
        /// <typeparam name="TInterface">The Dependency interface to register with the lifecycle</typeparam>
        [Obsolete("use AddRegistration", true)]
        void RegisterLifeStyle<TLifeStyle, TInterface>();

        /// <summary>
        /// Register a type (can be as simple as does it implement a Dependency or to apply a filter)
        /// </summary>
        /// <param name="registration">The registration with details on how setup the IoC with the types which match the where clause</param>
        [Obsolete("use AddRegistration")]
        void RegisterLifeStyle(IRegister registration);
        
        /// <summary>
        /// Register a type (can be as simple as does it implement a Dependency or to apply a filter)
        /// </summary>
        /// <param name="registration">The registration with details on how setup the IoC with the types which match the where clause</param>
        void AddRegistration(IRegister registration);
    }
}