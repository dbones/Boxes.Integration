namespace Boxes.Integration.Exceptions
{
    using System;

    /// <summary>
    /// raise when trying to set the instance of a service inside the internal ioc, when the instance does not match the service
    /// </summary>
    public class ServiceTypeDoesNotMatchInstanceException : Exception
    {
        /// <summary>
        /// service which the instance is supposed to be registered with
        /// </summary>
        public Type Service { get; private set; }

        /// <summary>
        /// the instance being registered
        /// </summary>
        public object Instance { get; private set; }

        public ServiceTypeDoesNotMatchInstanceException(Type service, object instance)
        {
            Service = service;
            Instance = instance;
        }

        public override string Message
        {
            get
            {
                string instanceType = Instance == null ? "null" : Instance.GetType().ToString();

                return
                    "cannot add instance: {0}, as it does not match the service type ({1}) it is being associated with".
                        FormatWith(instanceType, Service);
            }
        }

    }

}