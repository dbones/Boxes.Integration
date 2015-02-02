namespace Boxes.Integration
{
    using Boxes.Tasks;
    using Discovering;
    using Extensions;
    using Loading;
    using Trust;

    /// <summary>
    /// extensions for the boxes wrapper
    /// </summary>
    public static class BoxesWrapperExtensions
    {
        /// <summary>
        /// the trust manager being used by boxes.
        /// </summary>
        public static ITrustManager TrustManager(this IBoxesWrapper boxes)
        {
            return boxes.GetService<ITrustManager>();
        }

        /// <summary>
        /// Setup the package scanner, loader to work with Boxes.Integration, and treats the
        /// internal dependencies of packages in isolated mode
        /// </summary>
        /// <param name="boxes">the boxes wrapper</param>
        /// <param name="packagesDirectory">location of the packages folder</param>
        public static void UseIsolatedSetup<TBuilder, TContainer>
            (this IBoxesWrapper<TBuilder, TContainer> boxes,
            string packagesDirectory)
        {
            boxes.SetupCore<TBuilder, TContainer, IsolatedLoader>(packagesDirectory);
        }

        /// <summary>
        /// Setup the package scanner, loader to work with Boxes.Integration, and treats the
        /// internal dependencies of packages in default mode
        /// </summary>
        /// <param name="boxes">the boxes wrapper</param>
        /// <param name="packagesDirectory">location of the packages folder</param>
        public static void UseDefaultSetup<TBuilder, TContainer>
            (this IBoxesWrapper<TBuilder, TContainer> boxes,
            string packagesDirectory)
        {
            boxes.SetupCore<TBuilder, TContainer, DefaultLoader>(packagesDirectory);
        }


        /// <summary>
        /// Setup the package scanner, loader to work with Boxes.Integration
        /// </summary>
        /// <param name="boxes">the boxes wrapper</param>
        /// <param name="packagesDirectory">location of the packages folder</param>
        public static void SetupCore<TBuilder, TContainer, TLoader>(
            this IBoxesWrapper<TBuilder, TContainer> boxes,
            string packagesDirectory)
            where TLoader : ILoader
        {
            //we want to support multiple manifest types. the default is XmlManifest2012Reader
            var xmlManifestTask = new XmlManifestTask();
            xmlManifestTask.AddXmlManifestReader(new XmlManifest2012Reader());
            xmlManifestTask.AddXmlManifestReader(new XmlManifest2012ExtensionReader());

            //setup the scanner. we can easily add extra tasks
            var packageScanner = new PackageScanner(packagesDirectory);
            packageScanner.SetManifestTask(xmlManifestTask);

            //the main bit for using boxes
            boxes.Setup<TLoader>(packageScanner);
            boxes.DiscoverPackages();
            boxes.LoadPackages(); //load packages into integration, not the application
        }
    }
}