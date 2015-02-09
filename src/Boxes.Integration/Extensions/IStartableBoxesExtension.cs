namespace Boxes.Integration.Extensions
{
    /// <summary>
    /// a boxes extension which requires a <see cref="Start"/> method to be run.
    /// </summary>
    public interface IStartableBoxesExtension : IBoxesExtension
    {
        /// <summary>
        /// Start this when boxes has loaded in all the extensions
        /// </summary>
        void Start();
    }
}