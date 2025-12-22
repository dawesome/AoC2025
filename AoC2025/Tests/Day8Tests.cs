using System;
using System.Collections;
using NUnit.Framework;
namespace AoC2025.Tests;


public class Day8Tests
{
    [Test]
    public void ParsesInput()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        Assert.That(day8.junctionBoxes.Length, Is.EqualTo(20));
        day8.PrintJunctionBoxes();
    }

    [Test]
    public void TestCalculateDistances()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        long[] closestJunctionIndexArray = day8.CalculateDistancesBetweenJunctionBoxes();
        Assert.That(closestJunctionIndexArray.Length, Is.EqualTo(20));
        Assert.That(closestJunctionIndexArray[0], Is.EqualTo(19));
        Assert.That(closestJunctionIndexArray[19], Is.EqualTo(0));
    }

    [Test]
    public void TestCanCombine1Circuit()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        day8.CalculateDistancesBetweenJunctionBoxes();
        ArrayList circuits = day8.ConnectClosestJunctions(1);
        Assert.That(circuits.Count, Is.EqualTo(19));
        
    }
    
    [Test]
    public void TestCanCombine2Circuits()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        day8.CalculateDistancesBetweenJunctionBoxes();
        
        ArrayList circuits = day8.ConnectClosestJunctions(2);
        Assert.That(circuits.Count, Is.EqualTo(18));
    }

    [Test]
    public void TestCanCombine3Circuits()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        day8.CalculateDistancesBetweenJunctionBoxes();
        
        ArrayList circuits = day8.ConnectClosestJunctions(3);
        Assert.That(circuits.Count, Is.EqualTo(17));
    }

    
    [Test]
    public void TestCanCombine10Circuits()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        day8.CalculateDistancesBetweenJunctionBoxes();
        
        ArrayList circuits = day8.ConnectClosestJunctions(10);
        Assert.That(circuits.Count, Is.EqualTo(11));
    }
}