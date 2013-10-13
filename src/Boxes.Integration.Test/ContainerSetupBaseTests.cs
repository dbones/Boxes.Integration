namespace Boxes.Integration.Test
{
    using System;
    using System.Data;
    using System.Linq;
    using Boxes.Test;
    using Boxes.Test.Infrastructure;
    using Setup;
    using NUnit.Framework;
    using Setup.Registrations;

    public interface ILifeStyle {}

    public class Singleton : ILifeStyle {}

    public class Configure {}

    public class Register : RegisterBase<Type, Configure> 
    {
    }

    public class RegisterBaseTests : TestBase<IRegister<Type, Configure>>
    {
        [Test]
        public void Simple_setup()
        {
            Action(() =>
                {
                     var reg = new Register()
                        .LifeStyle(typeof(Singleton))
                        .Where(x => typeof(IDbCommand).IsAssignableFrom(x))
                        .AssociateWith(Contracts.SelfOnly);

                     return new Context<IRegister<Type, Configure>>(reg);
                });
            

        }

    }
}