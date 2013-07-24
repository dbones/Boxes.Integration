namespace Boxes.Integration.Trust.Contexts.BoxesExtensions
{
    using System;

    /// <summary>
    /// check to see if a package is allowed to configure a component (Boxes Extension)
    /// </summary>
    public sealed class SetupFromPackageTrustContext : TrustContext
    {
        
        public SetupFromPackageTrustContext(Type contract, Type setup, Package package)
        {
            Contract = contract;
            Setup = setup;
            Package = package;
        }

        /// <summary>
        /// the contract being setup
        /// </summary>
        public Type Contract { get; set; }

        /// <summary>
        /// the class which will setup the contract
        /// </summary>
        public Type Setup { get; set; }
        
        /// <summary>
        /// the package where the setup resides
        /// </summary>
        public Package Package { get; set; }

    }
}