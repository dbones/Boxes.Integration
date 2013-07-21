namespace Boxes.Integration.Extensions
{
    /// <summary>
    /// this is the class which will setup a <see cref="IBoxesExtensionWithSetup"/>
    /// </summary>
    public interface ISetupBoxesExtension<in TConfigure> where TConfigure : IBoxesExtensionWithSetup
    {
        /// <summary>
        /// this allows the setup to refine which extension it will extend, in the event where
        /// the generic parameter does not provide a concrete class, thus provides limited filtering.
        /// </summary>
        /// <param name="extension">the extension to be configured</param>
        /// <returns>true if this setup will configure the extension</returns>
        bool CanHandle(TConfigure extension);

        /// <summary>
        /// setup the boxes extension
        /// </summary>
        /// <param name="extension">the extension to setup</param>
        void Configure(TConfigure extension);
    }
}