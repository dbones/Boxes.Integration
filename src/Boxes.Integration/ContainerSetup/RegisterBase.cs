namespace Boxes.Integration.ContainerSetup
{
    using System;
    using System.Collections.Generic;

    public abstract class RegisterBase<TScope, TConfiguration> : IRegister<TScope, TConfiguration>
    {
        protected RegistrationMeta _meta = new RegistrationMeta();

        public RegistrationMeta RegistrationMeta { get { return _meta; } }

        public virtual IRegister<TScope, TConfiguration> Where(Predicate<Type> where)
        {
            _meta.Where += where;
            return this;
        }

        public IRegister<TScope, TConfiguration> AssociateWith(Contracts contracts)
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

        public IRegister<TScope, TConfiguration> AssociateWith(Func<Type, IEnumerable<Type>> contracts)
        {
            _meta.With = contracts;
            return this;
        }

        public IRegister<TScope, TConfiguration> AssociateWith(IEnumerable<Type> contracts)
        {
            _meta.With = type => contracts;
            return this;
        }


        public IRegister<TScope, TConfiguration> Ctor(Func<object> factoryMethod)
        {
            _meta.FactoryMethod = factoryMethod;
            return this;
        }

        public IRegister<TScope, TConfiguration> LifeStyle(TScope lifeStyle)
        {
            _meta.LifeStyle = lifeStyle;
            return this;
        }

        public IRegister<TScope, TConfiguration> Scope(TScope scope)
        {
            _meta.LifeStyle = scope;
            return this;
        }

        public IRegister<TScope, TConfiguration> Configure(Action<RegisterContext<TConfiguration>> cfg)
        {
            _meta.Configurations.Add(o => cfg((RegisterContext<TConfiguration>)o));
            return this;
        }
    }
}