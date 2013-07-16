namespace Boxes.Integration
{
    using System;

    /// <summary>
    /// some type extensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// returns of the current type is equal to the generic
        /// </summary>
        /// <typeparam name="TService">compare the type to this</typeparam>
        /// <param name="type">the type to compare</param>
        public static bool Is<TService>(this Type type)
        {
            return typeof(TService) == type;
        }
    }
}