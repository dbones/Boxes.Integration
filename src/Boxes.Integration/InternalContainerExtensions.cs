namespace Boxes.Integration
{
    using InternalIoc;

    /// <summary>
    /// some extensions for the internal container
    /// </summary>
    public static class InternalContainerExtensions
    {
        public static void Add<TContract, TService>(this IInternalContainer internalContainer) where TService : TContract
        {
            internalContainer.Add(typeof(TContract), typeof(TService));
        }

        public static void Add<TContract, TService>(this IInternalContainer internalContainer, bool isAllowedToBeOverridden) where TService : TContract
        {
            internalContainer.Add(typeof(TContract), typeof(TService), isAllowedToBeOverridden);
        }

        public static TContract Resolve<TContract>(this IInternalContainer internalContainer)
        {
            return (TContract)internalContainer.Resolve(typeof(TContract));
        }
    }
}