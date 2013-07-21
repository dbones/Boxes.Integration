namespace Boxes.Integration.Trust.Contexts
{
    using System;

    /// <summary>
    /// check to see if a type from a package is ok (Boxes Extension)
    /// </summary>
    public sealed class TypeFromPackageTrustContext : TrustContext
    {
        public TypeFromPackageTrustContext(Type contract, Type service, Package package)
        {
            Contract = contract;
            Service = service;
            Package = package;
        }

        /// <summary>
        /// the contract to be added
        /// </summary>
        public Type Contract { get; private set; }

        /// <summary>
        /// the service which will fulfil the contract
        /// </summary>
        public Type Service { get; private set; }

        /// <summary>
        /// the package where the service was located
        /// </summary>
        public Package Package { get; set; }
    }
}