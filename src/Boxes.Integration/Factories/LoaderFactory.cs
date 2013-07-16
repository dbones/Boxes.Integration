namespace Boxes.Integration
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using Factories;
    using Loading;

    /// <summary>
    /// Provides the way to create a <see cref="ILoader"/>, you can register custom loaders here too
    /// </summary>
    public class LoaderFactory
    {
        readonly IDictionary<Type, ICreateLoader> _ctors = new Dictionary<Type, ICreateLoader>();

        public LoaderFactory()
        {
            AddCreateLoader(pkgReg => new IsolatedLoader(pkgReg));
            AddCreateLoader(pkgReg => new DefaultLoader(pkgReg));
        }

        /// <summary>
        /// register a way to create a loader
        /// </summary>
        /// <typeparam name="TLoader">the loader type</typeparam>
        /// <param name="createLoader">the way to create it</param>
        public void AddCreateLoader<TLoader>(ICreateLoader createLoader) where TLoader : ILoader
        {
            var loaderType = typeof(TLoader);
            _ctors.Add(loaderType, createLoader);
        }

        /// <summary>
        /// register a way to create a loader, using a Lambda Func
        /// </summary>
        /// <typeparam name="TLoader">the loader type</typeparam>
        /// <param name="ctor">the func which can create the loader</param>
        public void AddCreateLoader<TLoader>(Func<PackageRegistry, TLoader> ctor) where TLoader : ILoader
        {
            var loaderType = typeof(TLoader);
            _ctors.Add(loaderType, new FuncCreateLoader<TLoader>(ctor));
        }

        /// <summary>
        /// creates the required loader
        /// </summary>
        /// <typeparam name="TLoader">the loader type</typeparam>
        /// <param name="packageRegistry">the current <see cref="PackageRegistry"/></param>
        /// <returns>fully constructed loader</returns>
        public ILoader CreateLoader<TLoader>(PackageRegistry packageRegistry) where TLoader : ILoader
        {
            var loaderType = typeof (TLoader);
            ICreateLoader createLoader;
            if(_ctors.TryGetValue(loaderType, out createLoader))
            {
                return createLoader.Ctor(packageRegistry);
            }
            throw new CreateLoaderException(loaderType);
        }

    }
}