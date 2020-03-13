using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace BenchmarkTest
{
    class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<TestMethods>();
            Console.ReadKey();
        }
    }
}
