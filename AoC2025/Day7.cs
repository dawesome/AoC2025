using System.Collections;

namespace AoC2025;

public class Day7
{
    public char[][] grid;
    public long beamSplitCount;
    public long timelineCount;
    public long[] timelines;
    
    public void ParseLines(IEnumerable<string> lines)
    {
        grid = new char[lines.Count()][];
        for (int count = 0; count < lines.Count(); ++count)
        {
            grid[count] = lines.ElementAt(count).ToCharArray();
        }    
    }

    public void printGrid()
    {
        foreach (var c in grid)
        {
            Console.WriteLine(c);
        }
    }

    public void SimulateSplits()
    {
        beamSplitCount = 0;
        timelines = new long[grid[0].Length];
        for (long lineIndex = 0; lineIndex < grid.Length; ++lineIndex)
        {
            for (int charIndex = 0; charIndex < grid[lineIndex].Length; ++charIndex)
            {
                if (grid[lineIndex][charIndex] == 'S')
                {
                    grid[lineIndex + 1][charIndex] = '|';
                    timelines[charIndex] = 1;
                }
                else if (grid[lineIndex][charIndex] == '|')
                {
                    if (lineIndex < grid.Length - 1)
                    {
                        if (grid[lineIndex + 1][charIndex] == '^')
                        {
                            grid[lineIndex + 1][charIndex - 1] = '|';
                            grid[lineIndex + 1][charIndex + 1] = '|';
                            
                            timelines[charIndex - 1] += timelines[charIndex];
                            timelines[charIndex + 1] += timelines[charIndex];
                            timelines[charIndex] = 0;
                            beamSplitCount += 1;
                        }
                        else
                        {
                            grid[lineIndex + 1][charIndex] = '|';
                        }
                    }
                }
            }
        }
        timelineCount = 0;
        foreach (var t in timelines)
        {
            timelineCount += t;
        }
    }
    
    public void Part1()
    {
        var lines = InputReader.GetInputLines("Inputs/Day7Input.txt");
        ParseLines(lines);
        SimulateSplits();
        printGrid();
        Console.WriteLine("Split count: " + beamSplitCount);
    }
    
    public void Part2()
    {

        Console.WriteLine("Timeline count: " + timelineCount);
    }
}