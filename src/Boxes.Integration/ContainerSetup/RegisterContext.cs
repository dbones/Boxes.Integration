namespace Boxes.Integration.ContainerSetup
{
    using System;

    /// <summary>
    /// a register context holds all the information required for someone to have complete control
    /// over a single types registration.
    /// </summary>
    /// <typeparam name="TConfiguration">the underlying IoC registration type</typeparam>
    public class RegisterContext<TConfiguration>
    {
        /// <summary>
        /// the configuration of the underlying IoC registration
        /// </summary>
        public TConfiguration Configuration { get; set; }

        /// <summary>
        /// the type the configuration is for
        /// </summary>
        public Type Type { get; set; }
    }
}