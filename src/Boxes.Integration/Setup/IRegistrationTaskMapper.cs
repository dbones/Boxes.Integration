namespace Boxes.Integration.Setup
{
    using Boxes.Tasks;

    /// <summary>
    /// this provides a mechanism to create the required boxes tasks for registration
    /// </summary>
    /// <typeparam name="TBuilder">the IoC builder</typeparam>
    public interface IRegistrationTaskMapper<TBuilder>
    {

        /// <summary>
        /// create a boxes registration task from the <see cref="RegistrationMeta"/>
        /// </summary>
        /// <param name="registration">the registration to be tasked up</param>
        IBoxesTask<RegistrationContext<TBuilder>> CreateRegisterTask(RegistrationMeta registration);
    }
}