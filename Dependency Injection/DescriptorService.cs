using System;
namespace Dependency_Injection
{
    internal class DescriptorService
    {
        public Type ServiceType { get; private set; }
        public Type ImplementType { get; private set; }
        public object Implementation { get; set; }
        public ServiceLifeTime LifeTime { get; private set; }


        public DescriptorService(Type serviceType, Type implementType, ServiceLifeTime lifetime)
        {
            ServiceType = serviceType;
            ImplementType = implementType;
            LifeTime = lifetime;
        }
    }

    public enum ServiceLifeTime
    {
        Singletone,
        Transient
    }
}