using System;
using System.Collections;
using NUnit.Framework;
namespace AoC2025.Tests;

public class Day5Tests
{

    Day5 day5 = new Day5();

    [Test]
    public void ParsesInputIntoRangesAndIDs()
    {
        var input = new[]
        {
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32",
        };
        day5.ParseLines(input);
        Assert.That(day5.GetRanges().Count, Is.EqualTo(4));
        Assert.That(day5.GetIds().Count, Is.EqualTo(6));
    }

    [Test]
    public void IsFresh_ReturnsCorrectValues()
    {
        var input = new[]
        {
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32",
        };
        day5.ParseLines(input);
        
        Assert.That(day5.IsFresh(2), Is.False);
        Assert.That(day5.IsFresh(8), Is.False);
        Assert.That(day5.IsFresh(32), Is.False);
        
        Assert.That(day5.IsFresh(5), Is.True);
        Assert.That(day5.IsFresh(11), Is.True);
        Assert.That(day5.IsFresh(17), Is.True);
    }

    [Test]
    public void CountFreshIds_CreatesCorrectNumberOfRanges()
    {
        var input = new[]
        {
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32",
        };
        day5.ParseLines(input);
        ArrayList combineRanges = day5.CombineRanges();
        Assert.That(combineRanges.Count, Is.EqualTo(2));
    }
    [Test]
    public void CountFreshIds_CreatesCorrectIds()
    {
        var input = new[]
        {
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32",
        };
        day5.ParseLines(input);
        
        Assert.That(day5.CountFreshIds(), Is.EqualTo(14));
    }

    [Test]
    public void WhenCombinedRangesHasDisjoint_Creates2Ranges()
    {
        day5.Clear();
        
        day5.ranges.Add(new AoC2025.Range(1, 5));
        day5.ranges.Add(new AoC2025.Range(6, 10));
        
        Assert.That(day5.CombineRanges().Count, Is.EqualTo(2));
    }
    
    [Test]
    public void WhenCombinedRangesOverlapsLeft_Creates1Range()
    {
        day5.Clear();
        
        day5.ranges.Add(new AoC2025.Range(1, 5));
        day5.ranges.Add(new AoC2025.Range(3, 10));

        ArrayList combinedRanges = day5.CombineRanges();
        Assert.That(combinedRanges.Count, Is.EqualTo(1));
        Assert.That(combinedRanges[0], Is.EqualTo(new AoC2025.Range(1, 10)));
    }
    
    [Test]
    public void WhenCombinedRangesOverlapsRight_Creates1Range()
    {
        day5.Clear();
        
        day5.ranges.Add(new AoC2025.Range(5, 10));
        day5.ranges.Add(new AoC2025.Range(3, 6));

        ArrayList combinedRanges = day5.CombineRanges();
        Assert.That(combinedRanges.Count, Is.EqualTo(1));
        Assert.That(combinedRanges[0], Is.EqualTo(new AoC2025.Range(3, 10)));
    }
    
    [Test]
    public void WhenCombinedRangesAreUnion_Creates1Range()
    {
        ArrayList combinedRanges;
        
        day5.Clear();
        day5.ranges.Add(new AoC2025.Range(5, 10));
        day5.ranges.Add(new AoC2025.Range(7, 9));
        combinedRanges = day5.CombineRanges();
        Assert.That(combinedRanges.Count, Is.EqualTo(1));
        Assert.That(combinedRanges[0], Is.EqualTo(new AoC2025.Range(5, 10)));
        
        day5.Clear();
        day5.ranges.Add(new AoC2025.Range(7, 9));
        day5.ranges.Add(new AoC2025.Range(5, 10));
        combinedRanges = day5.CombineRanges();
        Assert.That(combinedRanges.Count, Is.EqualTo(1));
        Assert.That(combinedRanges[0], Is.EqualTo(new AoC2025.Range(5, 10)));

        day5.Clear();
        day5.ranges.Add(new AoC2025.Range(85116519420599, 87459851977631));
        day5.ranges.Add(new AoC2025.Range(80798010743456, 89758941577635));
        combinedRanges = day5.CombineRanges();
        Assert.That(combinedRanges.Count, Is.EqualTo(1));
        Assert.That(combinedRanges[0], Is.EqualTo(new AoC2025.Range(80798010743456, 89758941577635)));

        day5.Clear();
        day5.ranges.Add(new AoC2025.Range(80798010743456, 89758941577635));
        day5.ranges.Add(new AoC2025.Range(85116519420599, 87459851977631));
        combinedRanges = day5.CombineRanges();
        Assert.That(combinedRanges.Count, Is.EqualTo(1));
        Assert.That(combinedRanges[0], Is.EqualTo(new AoC2025.Range(80798010743456, 89758941577635)));
    }

    [Test]
    public void WhenCombinedRanges_NoRangeOverlaps()
    {
        day5.Clear();

        var lines = InputReader.GetInputLines("../../../Inputs/Day5Input.txt");
        day5.ParseLines(lines);
        ArrayList combinedRanges = day5.CombineRanges();
        
        for (int outerIndex = 0; outerIndex < combinedRanges.Count; outerIndex++)
        {
            Range outerRange = (Range)combinedRanges[outerIndex];
            Assert.That(outerRange.LowerBound, Is.LessThanOrEqualTo(outerRange.UpperBound));
            for (int innerIndex = 0; innerIndex < combinedRanges.Count; innerIndex++)
            {
                if (outerIndex == innerIndex)
                {
                    continue;
                }
                Range innerRange = (Range)combinedRanges[innerIndex];
                Assert.That(outerRange.LowerBound < innerRange.LowerBound || outerRange.LowerBound > innerRange.UpperBound);
                Assert.That(outerRange.UpperBound < innerRange.LowerBound || outerRange.UpperBound > innerRange.UpperBound);
            }
        }
    }
}

