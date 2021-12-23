using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection.Servic
{
    
    public interface IA
    {
        void sayHello();
    }
    public class A : IA
    {
        IB b;
        public A(IB b)
        {
            this.b = b;
        }
        public void sayHello()
        {
            Console.Write($"A:IA (IB) B={b.GetHashCode()} ");
            b.sayHi();
        }
    }
}
