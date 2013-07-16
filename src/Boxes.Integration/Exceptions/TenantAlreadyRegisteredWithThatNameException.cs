namespace Boxes.Integration.Exceptions
{
    using System;

    public class TenantAlreadyRegisteredWithThatNameException : Exception
    {
        public string Name { get;  private set; }

        public TenantAlreadyRegisteredWithThatNameException(string name)
        {
            Name = name;
        }
    }
}