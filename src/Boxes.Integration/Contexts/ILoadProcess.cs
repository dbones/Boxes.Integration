using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxes.Integration.Contexts
{
    using Extensions;

    /// <summary>
    /// load process to enable/disable packages
    /// </summary>
    public interface ILoadProcess : IBoxesExtension
    {
        /// <summary>
        /// run the load process using the execution context, IE the tenant is updating itself
        /// </summary>
        /// <param name="executionContext">the execution context</param>
        /// <param name="packagesToEnable">a list of enabled packages</param>
        void LoadPackages(IExecutionContext executionContext, IEnumerable<string> packagesToEnable);
    }

    
    /// <summary>
    /// load process to enable/disable packages
    /// </summary>
    /// <typeparam name="TContext">the context</typeparam>
    public interface ILoadProcess<in TContext> : ILoadProcess where TContext : class 
    {
        /// <summary>
        /// run the load process using the a given TContext, IE a tenant is updating another tenant
        /// </summary>
        /// <param name="context">the execution context</param>
        /// <param name="packagesToEnable">a list of enabled packages</param>
        void LoadPackages(TContext context, IEnumerable<string> packagesToEnable);
    }
}
