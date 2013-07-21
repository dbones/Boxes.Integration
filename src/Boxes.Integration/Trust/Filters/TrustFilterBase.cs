namespace Boxes.Integration.Trust.Filters
{
    using System;
    using Contexts;

    /// <summary>
    /// A base trust filter, this adds a basic handle to match the <see cref="TrustContext"/>
    /// </summary>
    /// <typeparam name="TContext">the <see cref="TrustContext"/> this filter will be filter against</typeparam>
    public abstract class TrustFilterBase<TContext> : ITrustFilter 
        where TContext : TrustContext
    {
        public Type HandlesTrustContextType { get { return typeof (TContext); } }

        public virtual bool CanHandle(TrustContext trustContext)
        {
            if(!(trustContext is TContext))
            {
                return false;
            }

            return CanHandleContext((TContext)trustContext);
        }

        public virtual bool IsTrusted(TrustContext trustContext)
        {
            return IsTrustedContext((TContext) trustContext);
        }

        protected abstract bool CanHandleContext(TContext context);
        protected abstract bool IsTrustedContext(TContext trustContext);
    }
}