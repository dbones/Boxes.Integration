namespace Boxes.Integration.ContainerSetup
{
    /// <summary>
    /// the tenants container setup
    /// </summary>
    /// <typeparam name="TBuilder">the ioc builder class</typeparam>
    public interface ITenantContainerSetup<TBuilder> : IContainerSetup<TBuilder> { }
}