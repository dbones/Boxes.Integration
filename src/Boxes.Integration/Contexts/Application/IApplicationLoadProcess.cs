namespace Boxes.Integration.Contexts.Application
{
    using System.Collections.Generic;
    using Boxes.Integration.Extensions;

    /// <summary>
    /// loads the components into the container
    /// </summary>
    public interface IApplicationLoadProcess : IBoxesExtension
    {
        /// <summary>
        /// loads the components into the container
        /// </summary>
        /// <param name="application">instance of the Global application</param>
        /// <param name="packagesToEnable"></param>
        void WithApplication(Application application, IEnumerable<string> packagesToEnable);
    }
}