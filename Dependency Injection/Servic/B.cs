using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection.Servic
{
    public interface IB
    {
        void sayHi();
    }
    public class B : IB
    {
        public B ()
        {

        }
        public void sayHi()
        {
            Console.WriteLine("Hi man!");
        }
    }
}
