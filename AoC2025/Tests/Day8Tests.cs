using System;
using System.Collections;
using System.Numerics;
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
        Assert.That(day8.JunctionBoxes.Length, Is.EqualTo(20));
        day8.PrintJunctionBoxes();
    }

//     [Test]
//     public void TestCalculateDistances()
//     {
//         var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
//         Day8 day8 = new Day8();
//         day8.ParseLines(lines);
//         int[] closestJunctionIndexArray = day8.CalculateDistancesBetweenJunctionBoxes();
//         Assert.That(closestJunctionIndexArray.Length, Is.EqualTo(20));
//         Assert.That(closestJunctionIndexArray[0], Is.EqualTo(19));
//         Assert.That(closestJunctionIndexArray[19], Is.EqualTo(0));
// //        Assert.That(closestJunctionIndexArray[7], Is.EqualTo(19));
//     }

    [Test]
    public void TestJunctionPairSorterSortsSmallestDistancesFirst()
    {
        Day8 day8 = new Day8();
        day8.JunctionBoxes = new JunctionBox[3];
        day8.JunctionBoxes[0] = new JunctionBox(0, 0, 0);
        day8.JunctionBoxes[1] = new JunctionBox(1, 0, 0);
        day8.JunctionBoxes[2] = new JunctionBox(4, 0, 0);
        
        day8.CalculateDistancesBetweenJunctionBoxes();
        day8.JunctionDistanceList.Sort(new JunctionPairSorter());
        
        Assert.That(day8.JunctionDistanceList[0].DistanceFromReferenceToClosestJunction,
            Is.LessThan(day8.JunctionDistanceList[2].DistanceFromReferenceToClosestJunction));
    }

    [Test]
    public void TestCircuitListSorterSortsLargestGroupsFirst()
    {
        List<int> circuit1 = new List<int> { 1, 2, 3 };
        List<int> circuit2 = new List<int> { 4, 5 };
        List<int> circuit3 = new List<int> { 6 };
        
        List<List<int>> circuits = new List<List<int>> { circuit3, circuit3, circuit2, circuit1 };
        circuits.Sort(new CircuitListSorter());
        
        Assert.That(circuits[0].Count, Is.EqualTo(3));
    }
    
    [Test]
    public void TestCanCombine1Circuit()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        day8.CalculateDistancesBetweenJunctionBoxes();
        List<List<int>> circuits = day8.ConnectClosestJunctions(1);
        Assert.That(circuits.Count, Is.EqualTo(19));
    }
    
    [Test]
    public void TestCanCombine2Circuits()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        day8.CalculateDistancesBetweenJunctionBoxes();
        
        List<List<int>> circuits = day8.ConnectClosestJunctions(2);
        Assert.That(circuits.Count, Is.EqualTo(18));
    }

    [Test]
    public void TestCanCombine3Circuits()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        day8.CalculateDistancesBetweenJunctionBoxes();
        
        List<List<int>> circuits = day8.ConnectClosestJunctions(3);
        Assert.That(circuits.Count, Is.EqualTo(17));
    }

    
    [Test]
    public void TestCanCombine10Circuits()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        day8.CalculateDistancesBetweenJunctionBoxes();
        
        List<List<int>> circuits = day8.ConnectClosestJunctions(10, true);
        day8.PrintCircuits(circuits);

        Assert.That(circuits.Count, Is.EqualTo(11));
    }

    [Test]
    public void TestCanMultiplyTest10Circuits()
    {
        var lines = InputReader.GetInputLines("../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        day8.CalculateDistancesBetweenJunctionBoxes();
        
        List<List<int>> circuits = day8.ConnectClosestJunctions(10, true);
        Assert.That(day8.MultiplyLarestCircuits(circuits, 3), Is.EqualTo(40));
    }

    [Test]
    public void TestMultiplyLastCircutsXCoords()
    {
        var lines = InputReader.GetInputLines(@"../../../Inputs/Day8TestInput.txt");
        Day8 day8 = new Day8();
        day8.ParseLines(lines);
        day8.CalculateDistancesBetweenJunctionBoxes();
        
        JunctionPair lastConnection = day8.ConnectClosestJunctionsUntilSingleCircut();
        long product = (long)day8.JunctionBoxes[lastConnection.ClosestJunctionIndex].x *
                       (long)day8.JunctionBoxes[lastConnection.ReferenceJunctionIndex].x;;
        Assert.That(product, Is.EqualTo(25272));
    }
}