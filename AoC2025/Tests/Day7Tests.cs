using System;
using System.Collections;
using NUnit.Framework;
namespace AoC2025.Tests;

public class Day7Tests
{
    Day7 day7 = new Day7();

    [Test]
    public void ParsesInput()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day7TestInput.txt");
        day7.ParseLines(lines);
        Assert.That(day7.grid.Length, Is.EqualTo(16));
        day7.printGrid();
    }

    [Test]
    public void CountSplitsOnTestInput()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day7TestInput.txt");
        day7.ParseLines(lines);
        day7.SimulateSplits();
        day7.printGrid();
        Assert.That(day7.beamSplitCount, Is.EqualTo(21));
    }

    [Test]
    public void CountTimelinesOnTestInput()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day7TestInput.txt");
        day7.ParseLines(lines);
        day7.SimulateSplits();
        Assert.That(day7.timelineCount, Is.EqualTo(40));
    }

    [Test]
    public void TimelinesGreaterThanFailedInput()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day7Input.txt");
        day7.ParseLines(lines);
        day7.SimulateSplits();
        Assert.That(day7.timelineCount, Is.GreaterThan(11186943779L));
    }
}