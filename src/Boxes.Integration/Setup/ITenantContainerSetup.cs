namespace Boxes.Integration.Setup
{
    /// <summary>
    /// the tenants container setup
    /// </summary>
    /// <typeparam name="TBuilder">the ioc builder class</typeparam>
    public interface ITenantContainerSetup<TBuilder> : IContainerSetup<TBuilder> { }


    public class TenantContainerSetup<TBuilder> : ContainerSetupBase<TBuilder>, ITenantContainerSetup<TBuilder>
    {
        public TenantContainerSetup(IRegistrationTaskMapper<TBuilder> registrationTaskMapper) : base(registrationTaskMapper)
        {
        }
    }

}