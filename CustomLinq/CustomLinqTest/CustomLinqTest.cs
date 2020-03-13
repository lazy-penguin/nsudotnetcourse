using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomLinq;

namespace CustomLinqTests
{
    [TestClass]
    public class CustomLinqTest
    {
        [TestMethod]
        public void WhereTest()
        {
            var fruits = new List<string> { "apple", "passionfruit", "banana", "mango",
                    "orange", "blueberry", "grape", "strawberry" };

            var actual = fruits.CustomWhere(fruit => fruit.Length < 6).ToArray();
            var expected = fruits.Where(fruit => fruit.Length < 6).ToArray();

            CollectionAssert.AreEquivalent(actual, expected);
        }

        [TestMethod]
        public void SelectTest()
        {
            var actual = Enumerable.Range(1, 10).CustomSelect(x => x * x).ToArray();
            var expected = Enumerable.Range(1, 10).Select(x => x * x).ToArray();

            CollectionAssert.AreEquivalent(actual, expected);
        }

        [TestMethod]
        public void FirstTest()
        {
            int[] numbers = { 9, 34, 65, 92, 87, 435, 3, 54, 83, 23, 87, 435, 67, 12, 19 };

            var actual = numbers.CustomFirst(number => number > 80);
            var expected = numbers.First(number => number > 80);
            Assert.AreEqual(actual, expected);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FirstTestWithNoMatch()
        {
            int[] numbers = { 9, 34, 65, 92, 87, 435, 3, 54, 83, 23, 87, 435, 67, 12, 19 };
            numbers.CustomFirst(number => number < 2);
        }


        [TestMethod]
        public void FirstOrDefaultTest()
        {
            int[] numbers = { 9, 34, 65, 92, 87, 435, 3, 54, 83, 23, 87, 435, 67, 12, 19 };

            var actual = numbers.CustomFirstOrDefault(number => number > 500);
            var expected = numbers.FirstOrDefault(number => number > 500);
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void GroupByTest()
        {
            var list = new []{
                    new { Name="Barley", Age=8.3 },
                    new { Name="Boots", Age=4.9 },
                    new { Name="Whiskers", Age=1.5 },
                    new { Name="Daisy", Age=4.3 } };

            
            var actual = list.CustomGroupBy(
                pet => Math.Floor(pet.Age),
                pet => pet.Age,
                (baseAge, ages) => new
                {
                    Key = baseAge,
                    Count = ages.Count(),
                    Min = ages.Min(),
                    Max = ages.Max()
                }).ToArray();

            var expected = list.GroupBy(
                pet => Math.Floor(pet.Age),
                pet => pet.Age,
                (baseAge, ages) => new
                {
                    Key = baseAge,
                    Count = ages.Count(),
                    Min = ages.Min(),
                    Max = ages.Max()
                }).ToArray();

            CollectionAssert.AreEquivalent(actual, expected);
        }

        [TestMethod]
        public void OrderByTest()
        {
            var ages = new List<int> { 6, 5, 4, 3, 2, 1};

            var actual = ages.CustomOrderBy(number => number).ToArray();
            var expected = ages.OrderBy(number => number).ToArray();
            CollectionAssert.AreEquivalent(actual, expected);
        }

        [TestMethod]
        public void OfTypeTest()
        {
            var array = new List<object> { 6, 5, "apple", "dfdf", 2, 1 };

            var actual = array.CustomOfType<object, string>();
            foreach(var item in actual)
            {
                Assert.AreEqual(item.GetType(), typeof(string));
            }
        }

        [TestMethod]
        public void DistinctTest()
        {
            var ages = new List<int> { 21, 46, 46, 55, 17, 21, 55, 55 };

            var actual = ages.CustomDistinct().ToArray();
            var expected = ages.Distinct().ToArray();
            CollectionAssert.AreEquivalent(actual, expected);

        }

        [TestMethod]
        public void AnyTest()
        {
            var numbers = new List<int> { 1, 2 };
            var actual = numbers.CustomAny();
            var expected = numbers.Any();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void AnyWithPredicateTest()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            var actual = numbers.CustomAny(number => number > 5);
            var expected = numbers.Any(number => number > 5);
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void AllTest()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            var actual = numbers.CustomAll(number => number < 6);
            var expected = numbers.All(number => number < 6);
            Assert.AreEqual(actual, expected);

            actual = numbers.CustomAny(number => number < 7);
            expected = numbers.Any(number => number < 7);
            Assert.AreEqual(actual, expected);
        }
    }
}
