namespace Boxes.Integration.ApplicationContext.Tenancy
{
    using System.Collections.Generic;

    /// <summary>
    /// a tenant offers a level of isolation, to allow this application to virtually run multiple instances in the same App Domain (multi-tenancy)
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// name of the tenant
        /// </summary>
        public string Name { get; set; }

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
}