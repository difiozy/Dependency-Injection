using System.Collections.Generic;
using System;
using System.Linq;

namespace Dependency_Injection
{
    internal class DI
    {
        private List<DescriptorService> serviceDescrList = new List<DescriptorService>();
        
        public void AddSingleton<TService, TImplementation>() where TImplementation : TService
        {
            serviceDescrList.Add(new DescriptorService(typeof(TService), typeof(TImplementation), ServiceLifeTime.Singletone));
        }

        public void AddTransient<TService, TImplementation>() where TImplementation : TService
        {
            serviceDescrList.Add(new DescriptorService(typeof(TService), typeof(TImplementation), ServiceLifeTime.Transient));
        }

        public TService GetService<TService>()
        {
            var list = new List<Type>();
            return (TService)GetService(typeof(TService), ref list);
        }

        private object GetService(Type serviceType, ref List<Type> usedType)
        {
            var descriptor = serviceDescrList
                .SingleOrDefault(x => x.ServiceType == serviceType);

            if (descriptor == null)
            {
                throw new Exception($"Service of type {serviceType.Name} isn't registered");
            }

            if (descriptor.Implementation != null)
            {
                return descriptor.Implementation;
            }

            Type actualType = descriptor.ImplementType ?? descriptor.ServiceType;

            if (actualType.IsAbstract || actualType.IsInterface)
            {
                throw new InvalidCastException("Cannot instantiate abstract or interface type");
            }

            var constructorInfo = actualType.GetConstructors().First();

            var parameters = constructorInfo.GetParameters();
            List<object?> newParameters = new List<object?>();
            foreach (var parameter in parameters)
            {
                if (usedType.Contains(serviceType))
                {
                    throw new DependencysIsCycleException($"The type {serviceType.Name} is already referenced." +
                                                       $"Found cycle reference.");
                   
                }
                usedType.Add(serviceType);
                var newParameter = GetService(parameter.ParameterType, ref usedType);
                newParameters.Add(newParameter);
            }

            var resultParams = newParameters.ToArray();

            var implementation = Activator.CreateInstance(actualType, resultParams);

            if (descriptor.LifeTime == ServiceLifeTime.Singletone)
            {
                descriptor.Implementation = implementation;
            }

            return implementation;
        }
    }

    public class DependencysIsCycleException : Exception
    {
        public DependencysIsCycleException(string messege) : base(messege) { }
    }
}