namespace Boxes.Integration
{
    using System;

    /// <summary>
    /// object level extensions
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// this will try to dispose of the current object, it will run the IDosable interface, if it is implemented
        /// </summary>
        /// <param name="obj">the object to try to dispose of</param>
        public static void TryDispose(this object obj)
        {
            var disposable = obj as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}