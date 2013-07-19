namespace Boxes.Integration.InternalIoc
{
    using System;

    /// <summary>
    /// A simple internal container, which will manage the services available in Boxes.
    /// </summary>
    internal interface IInternalContainer : IDisposable
    {
        void Add(Type contract, Type service);
        
        void setInstance(Type type, object instance);

        object Resolve(Type contract);
    }
}