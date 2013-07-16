namespace Boxes.Integration.Factories
{
    using System;
    using Boxes.Loading;

    /// <summary>
    /// creates a Loader, based on a Func which is passed in via the ctor
    /// </summary>
    /// <typeparam name="TLoader"></typeparam>
    public class FuncCreateLoader<TLoader> : ICreateLoader where TLoader : ILoader
    {
        private readonly Func<PackageRegistry, TLoader> _ctor;

        public FuncCreateLoader(Func<PackageRegistry, TLoader> ctor) 
        {
            _ctor = ctor;
        }

        public ILoader Ctor(PackageRegistry packageRegistry)
        {
            return _ctor(packageRegistry);
        }
    }
}