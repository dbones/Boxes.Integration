namespace Boxes.Integration.Exceptions
{
    using System;
    using Factories;

    /// <summary>
    /// there is no <see cref="ICreateLoader"/> registered for this type
    /// </summary>
    public class CreateLoaderException : Exception
    {
        /// <summary>
        /// the loader which requires a creator
        /// </summary>
        public Type LoaderType { get; private set; }

        public CreateLoaderException(Type loaderType)
        {
            LoaderType = loaderType;
        }

        public override string Message
        {
            get { return "{0} - has no ICreateLoader registered ".FormatWith(LoaderType); }
        }
    }
}