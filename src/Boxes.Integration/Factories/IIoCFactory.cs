namespace Boxes.Integration.Factories
{
    /// <summary>
    /// create instances of containers as required
    /// </summary>
    public interface IIoCFactory<TBuilder, TContainer>
    {

        /// <summary>
        /// create an instance of the IoC Builder
        /// </summary>
        /// <returns>an IoC builder, in most cases this may just be the IoC container</returns>
        TBuilder CreateBuilder();

        /// <summary>
        /// create an instance of the IoC Builder for a child container
        /// </summary>
        /// <param name="parentContainer">the parent IoC container</param>
        /// <returns>a IoC builder, in most cases this may just be the child IoC container</returns>
        TBuilder CreateBuilder(TContainer parentContainer);

        /// <summary>
        /// Creates an instance of the main container
        /// </summary>
        TContainer CreateContainer(TBuilder builder);

        /// <summary>
        /// Creates a child container, off the main container
        /// </summary>
        TContainer CreateChildContainer(TContainer container, TBuilder builder);
    }
}