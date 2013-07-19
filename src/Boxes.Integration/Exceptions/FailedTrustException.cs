namespace Boxes.Integration.Exceptions
{
    using System;
    using Trust.Contexts;

    /// <summary>
    /// raise this when the application fails trust
    /// </summary>
    public class FailedTrustException : Exception
    {
        /// <summary>
        /// the context
        /// </summary>
        public TrustContext Context { get; private set; }

        public FailedTrustException(TrustContext context)
        {
            Context = context;
        }

        public override string Message
        {
            get { return "{0} was not trusted, please check the context and filters".FormatWith(Context); }
        }

    }
}