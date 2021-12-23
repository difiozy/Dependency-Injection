using Dependency_Injection.Servic;
using System;

namespace Dependency_Injection
{
    class Program
    {
        static void Main(string[] args)
        {
            var DIContainer = new DI();

            DIContainer.AddTransient<IA, A>();
            DIContainer.AddSingleton<IB, B>();

            var tr_1 = DIContainer.GetService<IA>();
            tr_1.sayHello();

            var tr_2 = DIContainer.GetService<IA>();
            tr_2.sayHello();
            Console.WriteLine(tr_1.GetHashCode())
                ;Console.WriteLine(tr_2.GetHashCode());

            var st_1 = DIContainer.GetService<IB>();
            var st_2 = DIContainer.GetService<IB>();

            Console.WriteLine(st_1.GetHashCode());
            Console.WriteLine(st_2.GetHashCode());

            
        }
    }
}
