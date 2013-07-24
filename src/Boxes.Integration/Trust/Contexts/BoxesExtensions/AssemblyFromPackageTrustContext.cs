namespace Boxes.Integration.Trust.Contexts.BoxesExtensions
{
    using System.Reflection;

    /// <summary>
    /// check to see if an assembly/module from a package is ok (Boxes Extension)
    /// </summary>
    public sealed class AssemblyFromPackageTrustContext : TrustContext
    {
        public AssemblyFromPackageTrustContext(Assembly assembly, Package package)
        {
            Assembly = assembly;
            Package = package;
        }

        /// <summary>
        /// the assembly/module in question
        /// </summary>
        public Assembly Assembly { get; set; }
        
        /// <summary>
        /// package the assembly resides in
        /// </summary>
        public Package Package { get; set; }
    }
}