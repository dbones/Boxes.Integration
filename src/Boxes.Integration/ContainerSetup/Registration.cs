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

    public class Registration
    {
        public Registration ()
        {
            RegistrationMeta = new RegistrationMeta();
        }

        internal RegistrationMeta RegistrationMeta { get; private set; }

        public Registration RegisterWith(RegisterWith with)
        {
            switch (with)
            {
                case ContainerSetup.RegisterWith.AllInterfaces:
                    RegistrationMeta.With = type => type.AllInterfaces();
                    break;
                case ContainerSetup.RegisterWith.FirstInterface:
                    RegistrationMeta.With = type => new[] { (type.FirstInterface() ?? type) };
                    break;
                case ContainerSetup.RegisterWith.SelfOnly:
                    RegistrationMeta.With = type => new[] { type };
                    break;
                case ContainerSetup.RegisterWith.SelfAndAllInterfaces:
                    RegistrationMeta.With = type => type.SelfAndAllInterfaces();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("with");
            }
            return this;
        }

        public Registration RegisterWith(Func<Type, IEnumerable<Type>> registerWith)
        {
            RegistrationMeta.With = registerWith;
            return this;
        }

        public Registration RegisterWith(IEnumerable<Type> registerWith)
        {
            RegistrationMeta.With = type => registerWith;
            return this;
        }

        public Registration Where(Predicate<Type> where)
        {
            RegistrationMeta.Where += where;
            return this;
        }

        public Registration Ctor(Func<object> factoryMethod)
        {
            RegistrationMeta.FactoryMethod = factoryMethod;
            return this;
        }

        public Registration LifeStyle<TLifeStyle>()
        {
            RegistrationMeta.LifeStyle = typeof (TLifeStyle);
            return this;
        }

        public Registration Configure<TConfiguration>(Action<TConfiguration> cfg)
        {
            RegistrationMeta.Configuraitions.Add(o => cfg((TConfiguration)o));
            return this;
        }

    }
}