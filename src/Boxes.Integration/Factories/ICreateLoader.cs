namespace Boxes.Integration.Factories
{
    using Boxes.Loading;

    /// <summary>
    /// create a loader
    /// </summary>
    public interface ICreateLoader
    {
        /// <summary>
        /// creates an instance of a loader
        /// </summary>
        /// <param name="packageRegistry">the current <see cref="PackageRegistry"/></param>
        /// <returns>the loader instance</returns>
        ILoader Ctor(PackageRegistry packageRegistry);
    }
}