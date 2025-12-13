namespace AoC2025.Tests;
using System;
using NUnit.Framework;

public class Day4Tests
{
    Day4 day4 = new Day4();

    [Test]
    public void ReturnsExpectedArray_ForValidRangeInput()
    {
        // Arrange
        var input = new[] {
                            "..@@.@@@@.",
                            "@@@.@.@.@@",
                            "@@@@@.@.@@",
                            "@.@@@@..@.",
                            "@@.@@@@.@@",
                            ".@@@@@@@.@",
                            ".@.@.@.@@@",
                            "@.@@@.@@@@",
                            ".@@@@@@@@.",
                            "@.@.@@@.@.",
        }; // adjust format to match actual input format
                            
        // Act
        var result = day4.MakeArrayFromInput(input); // static call assumed
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(10));
    }
    
    [Test]
    public void TestGridHas13MoveablePaper()
    {
        // Arrange
        var input = new[] {
            "..@@.@@@@.",
            "@@@.@.@.@@",
            "@@@@@.@.@@",
            "@.@@@@..@.",
            "@@.@@@@.@@",
            ".@@@@@@@.@",
            ".@.@.@.@@@",
            "@.@@@.@@@@",
            ".@@@@@@@@.",
            "@.@.@@@.@.",
        }; // adjust format to match actual input format
                            
        // Act
        var paperGrid = day4.MakeArrayFromInput(input); // static call assumed
        // Assert
        day4.PrintMoveablePaper(paperGrid);
        Assert.That(day4.CountMoveablePaper(paperGrid), Is.EqualTo(13));
    }
    
    [Test]
    public void TestGridHas43RemoveablePaper()
    {
        // Arrange
        var input = new[] {
            "..@@.@@@@.",
            "@@@.@.@.@@",
            "@@@@@.@.@@",
            "@.@@@@..@.",
            "@@.@@@@.@@",
            ".@@@@@@@.@",
            ".@.@.@.@@@",
            "@.@@@.@@@@",
            ".@@@@@@@@.",
            "@.@.@@@.@.",
        }; // adjust format to match actual input format
                            
        // Act
        var paperGrid = day4.MakeArrayFromInput(input); // static call assumed
        // Assert
        day4.PrintMoveablePaper(paperGrid);
        Assert.That(day4.CountRemoveablePaper(paperGrid), Is.EqualTo(43));
    }
}