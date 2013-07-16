namespace Boxes.Integration.Trust.Context
{
    using System;

    /// <summary>
    /// the trust contains the contract and service, this will normally raised when in direct usage with the hosting application
    /// as it is a trusted package source. 
    /// </summary>
    public sealed class TypeContractTrustContext : TrustContext
    {
        public TypeContractTrustContext(Type contract, Type service)
        {
            Contract = contract;
            Service = service;
        }

        /// <summary>
        /// the contract to be added
        /// </summary>
        public Type Contract { get; private set; }

        /// <summary>
        /// the service which will fulfill the contract
        /// </summary>
        public Type Service { get; private set; }
    }
}