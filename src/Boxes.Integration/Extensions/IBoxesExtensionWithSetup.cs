namespace Boxes.Integration.Extensions
{
    /// <summary>
    /// Extend <see cref="Boxes.Integration"/>, its similar to the <see cref="IBoxesExtension"/> 
    /// however these types of extension require some form of setup, <see cref="ISetupBoxesExtension{TConfigure}"/>.
    /// </summary>
    public interface IBoxesExtensionWithSetup : IBoxesExtension { }
}