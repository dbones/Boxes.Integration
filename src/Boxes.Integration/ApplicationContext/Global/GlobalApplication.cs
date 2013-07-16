namespace Boxes.Integration.ApplicationContext.Global
{
    using System.Collections.Generic;
    using Extensions;

    /// <summary>
    /// a tenant offers a level of isolation, to allow this application to virtually run multiple instances in the same App Domain (multi-tenancy)
    /// </summary>
    public class GlobalApplication
    {
        /// <summary>
        /// the packages this tenant requires (this does not mean they could be enabled)
        /// </summary>
        public IEnumerable<string> Packages { get; set; }

        /// <summary>
        /// the enabled packages
        /// </summary>
        public IEnumerable<string> EnabledPackages { get; set; }

        /// <summary>
        /// the container for this container
        /// </summary>
        internal object Container { get; set; }
    }

    /// <summary>
    /// loads the modules into the container
    /// </summary>
    public interface IApplicationLoadProcess : IBoxesExtension
    {
        void WithApplication(GlobalApplication application, IEnumerable<string> packagesToEnable);
    }

}
