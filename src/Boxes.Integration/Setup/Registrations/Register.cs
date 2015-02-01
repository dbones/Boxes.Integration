// Copyright 2012 - 2013 dbones.co.uk
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
namespace Boxes.Integration.Setup.Registrations
{
    using System;
    using System.Collections.Generic;

    public class Register : IRegister
    {
        protected RegistrationMeta _meta = new RegistrationMeta();

        public RegistrationMeta RegistrationMeta { get { return _meta; } }

        public virtual Register Where(Predicate<Type> where)
        {
            _meta.Where += where;
            return this;
        }

        public Register AssociateWith(Contracts contracts)
        {
            switch (contracts)
            {
                case Contracts.AllInterfaces:
                    _meta.With = type => type.AllInterfaces();
                    break;
                case Contracts.FirstInterface:
                    _meta.With = type => new[] { (type.FirstInterface() ?? type) };
                    break;
                case Contracts.SelfOnly:
                    _meta.With = type => new[] { type };
                    break;
                case Contracts.SelfAndAllInterfaces:
                    _meta.With = type => type.SelfAndAllInterfaces();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("contracts");
            }
            return this;
        }

        public Register AssociateWith(Func<Type, IEnumerable<Type>> contracts)
        {
            _meta.With = contracts;
            return this;
        }

        public Register AssociateWith(IEnumerable<Type> contracts)
        {
            _meta.With = type => contracts;
            return this;
        }
        
        public Register Ctor(Func<object> factoryMethod)
        {
            _meta.FactoryMethod = factoryMethod;
            return this;
        }

        public Register LifeStyle<TLifeStyle>() where TLifeStyle : LifeStyle
        {
            _meta.LifeStyle = typeof(TLifeStyle);
            return this;
        }
    }
}