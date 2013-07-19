namespace Boxes.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    //TODO: move into an external module

    ///// <summary>
    ///// manages the tenants in the application, this manager has a cross view of the
    ///// entire environment.
    ///// </summary>
    //public interface ITenantManager
    //{
    //    /// <summary>
    //    /// Enable all packages on the global/parent container
    //    /// </summary>
    //    void EnableGlobalPackages();

    //    /// <summary>
    //    /// on application load, run this to re-initialise 
    //    /// </summary>
    //    void LoadTenants();

    //    /// <summary>
    //    /// provision a new tenant
    //    /// </summary>
    //    /// <param name="name"></param>
    //    /// <param name="enabledPackages"></param>
    //    /// <returns></returns>
    //    Tenant ProvisionTenant(string name, IEnumerable<string> enabledPackages);

    //    /// <summary>
    //    /// Enable a tenant
    //    /// </summary>
    //    /// <param name="tenantName">name of tenant</param>
    //    void EnableTenant(string tenantName);

    //    /// <summary>
    //    /// Enable packages for a tenant
    //    /// </summary>
    //    /// <param name="tenantName"></param>
    //    /// <param name="packages"></param>
    //    void EnablePackages(string tenantName, IEnumerable<string> packages);
        
    //    /// <summary>
    //    /// Disable Tenant
    //    /// </summary>
    //    /// <param name="tenantName"></param>
    //    void DisableTenant(string tenantName);
        
    //    /// <summary>
    //    /// Disable packages for a tenant
    //    /// </summary>
    //    /// <param name="tenantName"></param>
    //    /// <param name="packages"></param>
    //    void DisablePackages(string tenantName, IEnumerable<string> packages);
    //}

    //class SingleTenantManager : ITenantManager 
    //{
    //    private readonly ITenantRepository _tenantRepository;

    //    public SingleTenantManager(ITenantRepository tenantRepository)
    //    {
    //        _tenantRepository = tenantRepository;
    //    }

    //    public void EnableGlobalPackages()
    //    {
    //        _loader()
    //    }

    //    public void LoadTenants()
    //    {
    //        //parellal all tenants?
    //    }

    //    public Tenant ProvisionTenant(string name, IEnumerable<string> enabledPackages)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public void EnableTenant(string tenantName)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public void EnablePackages(string tenantName, IEnumerable<string> packages)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public void DisableTenant(string tenantName)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public void DisablePackages(string tenantName, IEnumerable<string> packages)
    //    {
    //        throw new System.NotImplementedException();
    //    }

        
    //}

    //public interface ITenantRepository
    //{
    //    void Add(Tenant tenant);
    //    Tenant ByName(string name);
    //    IEnumerable<Tenant> Find(Func<Tenant, bool> query);
    //}

    //public class TenantRepository : ITenantRepository
    //{
    //    private static IDictionary<string, Tenant> _tenants = new Dictionary<string, Tenant>(); 
    //    private static object _lock = new object();

    //    public void Add(Tenant tenant)
    //    {
    //        lock (_lock)
    //        {
    //            if (_tenants.ContainsKey(tenant.Name))
    //            {
    //                throw new TenantAlreadyRegisteredWithThatNameException(tenant.Name);
    //            }

    //            _tenants.Add(tenant.Name, tenant);
    //        }
    //    }

    //    public Tenant ByName(string name)
    //    {
    //        return _tenants[name];
    //    }

    //    public IEnumerable<Tenant> Find(Func<Tenant, bool> query)
    //    {
    //        return _tenants.Values.Where(query);
    //    }
    //}
}