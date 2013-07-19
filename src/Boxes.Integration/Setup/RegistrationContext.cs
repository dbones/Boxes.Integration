namespace Boxes.Integration.Setup
{
    using System;

    //TODO: keep an eye on this class, look at some profiling


    /// <summary>
    /// the context for the registration
    /// </summary>
    /// <typeparam name="TBuilder">the current builder</typeparam>
    public class RegistrationContext<TBuilder>
    {
        public RegistrationContext(Type type, TBuilder builder)
        {
            Type = type;
            Builder = builder;
        }

        /// <summary>
        /// the type to register
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// the builder to register it with
        /// </summary>
        public TBuilder Builder { get; set; }
    }
}