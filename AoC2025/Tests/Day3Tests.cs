// csharp
using NUnit.Framework;
using AoC2025;

namespace AoC2025.Tests
{
    [TestFixture]
    public class Day3Tests
    {
        [Test]
        public void GetLargestNumberForLine_AscendingDigits_ReturnsLastTwoDigits()
        {
            // "12345" -> expected 45
            Assert.That(Day3.GetLargestNumberForLine("12345", 2), Is.EqualTo(45));
        }

        [Test]
        public void GetLargestNumberForLine_DescendingDigits_ReturnsFirstTwoDigits()
        {
            // "98765" -> expected 98
            Assert.That(Day3.GetLargestNumberForLine("98765", 2), Is.EqualTo(98));
        }

        [Test]
        public void GetLargestNumberForLine_TwoDigits_ReturnsBoth()
        {
            // "42" -> expected 42
            Assert.That(Day3.GetLargestNumberForLine("42", 2), Is.EqualTo(42));
        }
        
        [Test]
        public void GetLargestNumberForLine_GivenTestCases()
        {
            Tuple<string, long>[] testCases = new[]
            {
                Tuple.Create("819", 89l),
                Tuple.Create("987654321111111", 98l),
                Tuple.Create("811111111111119", 89l),
                Tuple.Create("234234234234278", 78l),
                Tuple.Create("818181911112111", 92l),
                Tuple.Create("4453322423234323362634238645943333332463321659433346534324232461344544333233244323632243313334262243", 99l)
            };

            foreach (Tuple<string, long> testCase in testCases)
            {
                Assert.That(Day3.GetLargestNumberForLine(testCase.Item1, 2), Is.EqualTo(testCase.Item2));
            }
        }
        
        [Test]
        public void GetLargestNumberForLine_GivenTestCases_12Digits()
        {
            var testCases = new[]
            {
                Tuple.Create("987654321111111", 987654321111),
                Tuple.Create("811111111111119", 811111111119),
                Tuple.Create("234234234234278", 434234234278),
                Tuple.Create("818181911112111", 888911112111),
            };

            foreach (Tuple<string, long> testCase in testCases)
            {
                Assert.That(Day3.GetLargestNumberForLine(testCase.Item1, 12), Is.EqualTo(testCase.Item2));
            }
        }
        
    }
}