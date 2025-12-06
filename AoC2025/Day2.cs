using System.Collections;

namespace AoC2025;

using RangeTuple = Tuple<long, long>;

public class Day2
{
    public ArrayList ParseRanges(string line)
    {
        ArrayList ranges = new ArrayList();
        string[] rangeStrings = line.Split(',');
        foreach (string rangeString in rangeStrings)
        {
            RangeTuple rangeBounds = Tuple.Create(long.Parse(rangeString.Split('-')[0]), long.Parse(rangeString.Split('-')[1]));
            ranges.Add(rangeBounds);
        }
        return ranges;
    }

    public ArrayList GetInvalidIDsForRangePart1(RangeTuple range)
    {
        ArrayList invalidIDs = new ArrayList();
        for (long currentIndex = range.Item1; currentIndex <= range.Item2; currentIndex++)
        {
            string currentIDString = currentIndex.ToString();

            if (IsInvalidForPart1(currentIDString))
            {
                invalidIDs.Add(currentIndex);
            }
        }  
        return invalidIDs;
    }

    private static bool IsInvalidForPart1(string currentIDString)
    {
        if (currentIDString.Length % 2 == 0)
        {
            string substring = currentIDString.Substring(0, currentIDString.Length / 2);
            int lastIndex = currentIDString.LastIndexOf(substring);
            if (lastIndex == currentIDString.Length / 2)
            {
                return true;
            }
        }
        return false;
    }

    public ArrayList GetInvalidIDsForRangePart2(RangeTuple range)
    {
        ArrayList invalidIDs = new ArrayList();
        for (long currentIndex = range.Item1; currentIndex <= range.Item2; currentIndex++)
        {
            string currentIDString = currentIndex.ToString();

            if (IsInvalidIDPart2(currentIDString))
            {
                invalidIDs.Add(currentIndex);
            }
        }  
        return invalidIDs;
    }

    public static bool IsInvalidIDPart2(string currentIDString)
    {
        // Check substrings up to half the length of the string
        for (int substringSize = 1; substringSize <= currentIDString.Length / 2; ++substringSize)
        {
            // For the substring size, string length must be a multiple of the substring size
            if (currentIDString.Length % substringSize != 0)
            {
                continue;
            }

            // For the substring size, each substring in that length stride must be the same
            int currentCheckIndex = substringSize;
            string referenceSubstring = currentIDString.Substring(0, substringSize);
            while (currentCheckIndex < currentIDString.Length)
            {
                string nextSubstringInStride = currentIDString.Substring(currentCheckIndex, substringSize);
                if (!nextSubstringInStride.Equals(referenceSubstring))
                {
                    break;
                }
                currentCheckIndex += substringSize;
            }
            // If we reached the end of the string, it is an invalid ID pattern
            if (currentCheckIndex == currentIDString.Length)
            {
                return true;
            }
        }

        return false;
    }

    public long SumInvalidIDs(ArrayList invalidIDs)
    {
        long sum = 0;
        foreach (long invalidID in invalidIDs)
        {
            sum += invalidID;
        }
        return sum;
    }
    
    public void Part1()
    {
        var lines = InputReader.GetInputLines("Inputs/Day2Part1input.txt");
        foreach (string line in lines)
        {
            var ranges = ParseRanges(line);
            ArrayList invalidIds = new ArrayList();
            foreach (RangeTuple range in ranges)
            {
                invalidIds.AddRange(GetInvalidIDsForRangePart1(range));
            }
            
            long sum = SumInvalidIDs(invalidIds);
            Console.WriteLine($"Part 1: {sum}");
        }
    }
    
    public void Part2()
    {
        var lines = InputReader.GetInputLines("Inputs/Day2Part1input.txt");
        foreach (string line in lines)
        {
            var ranges = ParseRanges(line);
            ArrayList invalidIds = new ArrayList();
            foreach (RangeTuple range in ranges)
            {
                invalidIds.AddRange(GetInvalidIDsForRangePart2(range));
            }
            
            long sum = SumInvalidIDs(invalidIds);
            Console.WriteLine($"Part 2: {sum}");
        }
    }
}