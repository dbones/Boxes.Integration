namespace Boxes.Integration.Extensions
{
    using Setup;

    /// <summary>
    /// setup the tenant container
    /// </summary>
    public interface ITenantContainerSetup
    {
        /// <summary>
        /// this will allow a package to setup the tenant container
        /// 
        /// NOTE that IoC registrations are not generic, you cannot reuse a registration across 2 different IoC containers
        /// (this is not the point of Boxes.Integration)
        /// </summary>
        /// <param name="tenantContainer">the tenant container</param>
        void Setup(IContainerSetup tenantContainer);
    }
}
