namespace Boxes.Integration.ContainerSetup
{
    /// <summary>
    /// the global container setup
    /// </summary>
    /// <typeparam name="TBuilder">the ioc builder class</typeparam>
    public interface IGlobalContainerSetup<TBuilder> : IContainerSetup<TBuilder>{}
}