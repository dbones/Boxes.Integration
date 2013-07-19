namespace Boxes.Integration.Setup
{
    /// <summary>
    /// the global container setup
    /// </summary>
    /// <typeparam name="TBuilder">the ioc builder class</typeparam>
    public interface IApplicationContainerSetup<TBuilder> : IContainerSetup<TBuilder>{}
}