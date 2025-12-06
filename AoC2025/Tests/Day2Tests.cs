using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;

namespace AoC2025.Tests
{
    [TestFixture]
    public class Day2Tests
    {
        private Day2 day2;
        private MethodInfo parseRangesMethod;
        private MethodInfo getInvalidIDsForRangePart1Method;
        
        [SetUp]
        public void Setup()
        {
            day2 = new Day2();
            parseRangesMethod = typeof(Day2).GetMethod("ParseRanges", BindingFlags.NonPublic | BindingFlags.Instance);
            getInvalidIDsForRangePart1Method = typeof(Day2).GetMethod("GetInvalidIDsForRangePart1", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        
        [Test]
        public void ParseRanges_Exists()
        {
            Assert.That(parseRangesMethod, Is.Not.Null, "Could not find private parseRangesMethod ParseRanges.");
        }

        [Test]
        public void ParseRanges_Returns_List()
        {
            var result = day2.ParseRanges("1-3,5-8");
            Assert.That(result, Is.Not.Null, "ParseRanges returned null or not an ArrayList.");
            Assert.That(2, Is.EqualTo(result.Count));
        }

        [Test]
        public void ParseRanges_CreatesTuples()
        {
            var result = parseRangesMethod.Invoke(day2, new object[] { "1-3,5-8" }) as ArrayList;
            
            var first = result[0] as Tuple<long, long>;
            var second = result[1] as Tuple<long, long>;
            Assert.That(first, Is.Not.Null, "First element is not Tuple<int,int>.");
            Assert.That(second, Is.Not.Null, "Second element is not Tuple<int,int>.");

            Assert.That(1, Is.EqualTo(first.Item1));
            Assert.That(3, Is.EqualTo(first.Item2));
            Assert.That(5, Is.EqualTo(second.Item1));
            Assert.That(8, Is.EqualTo(second.Item2));
        }
        
        [Test]
        public void GetInvalidIDsForRange_Exists()
        {
            Assert.That(getInvalidIDsForRangePart1Method, Is.Not.Null, "Could not find private method GetInvalidIDsForRangePart1.");
        }
        
        [Test]
        public void GetInvalidIDsForRange_FindsNoInvalidIDs()
        {
            ArrayList result = getInvalidIDsForRangePart1Method.Invoke(day2, new object[] { Tuple.Create(1698522, 1698528) }) as ArrayList;
            Assert.That(result, Is.Not.Null, "GetInvalidIDsForRangePart1 returned null or not an ArrayList.");
            Assert.That(0, Is.EqualTo(result.Count));
        }
        
        [Test]
        public void GetInvalidIDsForRange_FindsOneInvalidIDs()
        {
            ArrayList result = day2.GetInvalidIDsForRangePart1(Tuple.Create(99L, 115L));
            Assert.That(result, Is.Not.Null, "GetInvalidIDsForRangePart1 returned null or not an ArrayList.");
            Assert.That(1, Is.EqualTo(result.Count));
        }
        
        [Test]
        public void GetInvalidIDsForRange_FindsOneInvalidIDs_AllCases()
        {
            var cases = new[]
            {
                Tuple.Create(99L, 115L),
                Tuple.Create(998L, 1012L),
                Tuple.Create(1188511880L,1188511890L),
                Tuple.Create(222220L, 222224L),
                Tuple.Create(38593856L, 38593862L),
            };

            foreach (var range in cases)
            {
                var result = day2.GetInvalidIDsForRangePart1(range);
                Assert.That(result, Is.Not.Null, "GetInvalidIDsForRangePart1 returned null or not an ArrayList.");
                Assert.That(1, Is.EqualTo(result.Count), $"Expected 1 invalid ID for range {range.Item1}-{range.Item2} but got {result.Count}.");
            }
        }
        
        [Test]
        public void GetInvalidIDsForRange_FindsTwoInvalidIDs_For11_22()
        {
            var result = day2.GetInvalidIDsForRangePart1(Tuple.Create(11L, 22L));
            Assert.That(result, Is.Not.Null, "GetInvalidIDsForRangePart1 returned null or not an ArrayList.");
            Assert.That(2, Is.EqualTo(result.Count), $"Expected 2 invalid IDs for range 11-22 but got {result?.Count}.");
        }

        [Test]
        public void SumInvalidIDs_Test()
        {
            string input =
                "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
            var ranges = day2.ParseRanges(input);
            ArrayList invalidIds = new ArrayList();
            foreach (Tuple<long, long> range in ranges)
            {
                invalidIds.AddRange(day2.GetInvalidIDsForRangePart1(range));
            }
            
            long sum = day2.SumInvalidIDs(invalidIds);
            Assert.That(1227775554, Is.EqualTo(sum));
        }
        
        [Test]
        public void IsInvalidIDPart2_Test()
        {
            var cases = new[]
            {
                "11",
                "22",
                "99",
                "111",
                "999",
                "1010",
                "1188511885",
                "222222",
                "446446",
                "38593859",
                "565656",
                "824824824",
                "2121212121"
            };
            foreach (var id in cases)
            {
                Assert.That(Day2.IsInvalidIDPart2(id), Is.True, $"Expected {id} to be invalid for part 2.");
            }
        }
    }
}