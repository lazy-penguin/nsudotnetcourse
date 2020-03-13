using System;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace BenchmarkTest
{
    public class TestMethods 
    {
        [Benchmark(Description = "CreateNewClass")]
        public int CreateNewTestClass()
        {
            var sum = 0;
            for (var i = 0; i < 1000000; i++)
            {
                var testClass = new TestClass();
                sum += testClass.Value;
            }

            return sum;
        }

        [Benchmark(Description = "CreateGeneric")]
        public void CreateGeneric()
        {
            CreateGeneric<TestClass>();
        }

        public int CreateGeneric<T>() where T : TestClass, new()
        {
            var sum = 0;
            for (var i = 0; i < 1000000; i++)
            {
                var testClass = new T();
                sum += testClass.Value;
            }

            return sum;
        }

        [Benchmark(Description = "CreateByType")]
        [Arguments(typeof(TestClass))]
        public int CreateByType(Type t)
        {
            var sum = 0;
            for (var i = 0; i < 1000000; i++)
            {
                var testClass = Activator.CreateInstance(t);
                sum += ((TestClass)testClass).Value;
            }
            return sum;
        }

        [Benchmark(Description = "CreateByConstructor")]
        public int CreateByConstructor()
        {
            var sum = 0;
            var constructor = typeof(TestClass).GetConstructors().First();
            for (var i = 0; i < 1000000; i++)
            {
                var testClass = constructor.Invoke(null);
                sum += ((TestClass)testClass).Value;

            }
            return sum;
        }
    }
}
