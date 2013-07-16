namespace Boxes.Integration.Factories
{
    /// <summary>
    /// create an instance of a dependency resolver
    /// </summary>
    public interface IDependencyResolverFactory
    {
        /// <summary>
        /// create an instance of a dependency resolver
        /// </summary>
        /// <param name="container">the container which the dependency resolver will wrap around</param>
        /// <returns>a dependency resolver</returns>
        IDependencyResolver CreateResolver(object container);
    }
}