namespace Boxes.Integration.Extensions
{
    using Setup;

    /// <summary>
    /// setup the tenant container
    /// </summary>
    public interface IApplicationContainerSetup
    {
        /// <summary>
        /// this will allow a package to setup the application container
        /// 
        /// NOTE that IoC registrations are not generic, you cannot reuse a registration across 2 different IoC containers
        /// (this is not the point of Boxes.Integration)
        /// </summary>
        /// <param name="globalApplicationContainer">the application container</param>
        void Setup(IContainerSetup globalApplicationContainer);
    }
}