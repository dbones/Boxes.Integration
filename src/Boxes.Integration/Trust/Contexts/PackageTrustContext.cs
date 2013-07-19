namespace Boxes.Integration.Trust.Contexts
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public sealed class PackageTrustContext : TrustContext
    {
        public PackageTrustContext(Type contract, Type service, Package package)
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
        /// the service which will fulfill the contract
        /// </summary>
        public Type Service { get; private set; }

        /// <summary>
        /// the package where the service was located
        /// </summary>
        public Package Package { get; set; }
    }
}