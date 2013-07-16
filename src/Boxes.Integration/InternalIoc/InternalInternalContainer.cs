namespace Boxes.Integration.InternalIoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Trust;
    using Trust.Filters;

    /// <summary>
    /// this container is a simple internalContainer which handles all contracts as singletons, (very limited functionality)
    /// </summary>
    internal class InternalInternalContainer : IInternalContainer
    {
        private readonly IDictionary<Type, Registration> _registrations = new Dictionary<Type, Registration>();
        private readonly IDictionary<Type, object> _instances = new Dictionary<Type, object>();
        private readonly object _lock = new object();
        private readonly ITrustManager _trustManager = new TrustManager();

        public InternalInternalContainer()
        {
            //_trustManager.AddTrust(new TypeContractFilter<ITrustManager, TrustManager>());
            //Add(typeof(ITrustManager), typeof(TrustManager), false, true);
            //_instances.Add(typeof(ITrustManager), _trustManager); //set the default instance
        }


        public void Add(Type contract, Type service)
        {
            Add(contract, service, true);
        }

        public void Add(Type contract, Type service, bool isAllowedToBeOverridden)
        {
            Add(contract, service, isAllowedToBeOverridden, false);
        }

        internal void Add(Type contract, Type service, bool isAllowedToBeOverridden, bool isDefault)
        {
            //TrustContext context = new TrustContext(contract, service,);
            //if (!_trustManager.IsTrusted(context))
            //{
            //    throw new Exception("not trusted");
            //}

            lock (_lock)
            {
                Registration registration;
                if (_registrations.TryGetValue(contract, out registration))
                {
                    if (!isAllowedToBeOverridden)
                    {
                        throw new Exception("Cannot override serivce");
                    }
                    registration.Service = service;
                }
                else
                {
                    registration = new Registration(contract, service);
                    registration.IsAllowedToBeOverridden = isAllowedToBeOverridden;
                    registration.IsDefault = isDefault;
                    _registrations.Add(contract, registration);
                }
            }
        }


        public object Resolve(Type contract)
        {
            lock (_lock)
            {
                Func<Registration, Type> getServiceType;
                if (contract.IsGenericType)
                {
                    //no support for list or lazy, this is a simple internalContainer
                    Type[] genericArguments = contract.GetGenericArguments();
                    getServiceType = r => r.Service.MakeGenericType(genericArguments);
                    contract = contract.GetGenericTypeDefinition();
                }
                else
                {
                    getServiceType = r => r.Service;
                }

                Registration registration;
                if (!_registrations.TryGetValue(contract, out registration))
                {
                    return null;
                }

                var serviceType = getServiceType(registration);

                object instance;
                if (_instances.TryGetValue(serviceType, out instance))
                {
                    return instance;
                }

                instance = CreateInstance(serviceType);
                _instances.Add(serviceType, instance);

                return instance;
            }
        }


        private object CreateInstance(Type service)
        {
            var ctor = service.GetConstructors()
                .OrderByDescending(x => x.GetParameters().Count())
                .FirstOrDefault(x => x.GetParameters().All(p => Resolve(p.ParameterType) != null));

            if (ctor == null)
            {
                throw new Exception("cannot resolve service");
            }

            var ctorArgs = ctor.GetParameters().Select(x => Resolve(x.ParameterType)).ToArray();
            var instance = ctor.Invoke(ctorArgs);
            return instance;
        }


        public void Dispose()
        {
            lock (_lock)
            {
                foreach (var instance in _instances.Values)
                {
                    var disposeOfMe = instance as IDisposable;
                    if (disposeOfMe != null)
                    {
                        disposeOfMe.Dispose();
                    }
                }
                _instances.Clear();
            }
        }
    }
}