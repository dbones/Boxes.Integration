namespace Boxes.Integration.ContainerSetup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// the default will return all exported types
    /// </summary>
    class DefaultTypeRegistrationFilter : ITypeRegistrationFilter 
    {
        public IEnumerable<Type> FilterTypes(Package package)
        {
            //TODO: consider a ToArray/ToList
            return package
                .LoadedAssemblies
                .SelectMany(x => x.GetExportedTypes());
        }
    }
}