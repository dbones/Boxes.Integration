namespace Boxes.Integration.Setup
{
    using Extensions;
    using Interception;
    using Registrations;

    /// <summary>
    /// setup any container container (this will not be able to access any container special-isms)
    /// </summary>
    public abstract class SetupGenericContainer : ISetupBoxesExtension<IContainerSetup>
    {
        public bool CanHandle(IContainerSetup extension)
        {
            return true;
        }

        /// <summary>
        /// create a new registration pattern
        /// </summary>
        public Register Register { get { return new Register(); } }

        /// <summary>
        /// create a new interception (AOP) pattern
        /// </summary>
        public IRegisterInterception RegisterInterception { get { return new RegisterInterception(); } }

        public abstract void Configure(IContainerSetup extension);
    }
}