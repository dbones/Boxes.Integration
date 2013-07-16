namespace Boxes.Integration.ApplicationContext.Tenancy
{
    using System.Collections.Generic;
    using Boxes.Integration.Extensions;

    /// <summary>
    /// loads the modules into the container
    /// </summary>
    public interface ITenantLoadProcess : IBoxesExtension
    {
        void WithTenant(Tenant tenant, IEnumerable<string> packagesToEnable);
    }
}