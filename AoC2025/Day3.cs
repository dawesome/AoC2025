namespace AoC2025;

public class Day3
{
    
    public static long GetLargestNumberForLine(string line, int maxNumbers)
    {
        int[] bestBattery = new int[maxNumbers];
        int searchStartIndex = 0;

        for (int bestBatteryIndex = 0; bestBatteryIndex < maxNumbers; ++bestBatteryIndex)
        {
            bool foundBestDigit = false;
            
            for (int bestPossibleDigit = 9; bestPossibleDigit >= 0; --bestPossibleDigit)
            {
                for (int lineIndex = searchStartIndex; lineIndex <= line.Length - (maxNumbers-bestBatteryIndex); ++lineIndex)
                {
                    if (line[lineIndex] - '0' == bestPossibleDigit)
                    {
                        bestBattery[bestBatteryIndex] = bestPossibleDigit;
                        foundBestDigit = true;
                        searchStartIndex = lineIndex + 1;
                        break;
                    }
                }
                if (foundBestDigit)
                {
                    break;
                }
            }

        }        
        
        long totalJolts = long.Parse(String.Join("", bestBattery));
        return totalJolts;
    }
    
    public void Part1()
    {
        long total = 0;
        var lines = InputReader.GetInputLines("Inputs/Day3Input.txt");
        foreach (string line in lines)
        {
            long largestNumber = GetLargestNumberForLine(line, 2);
            total += largestNumber;
            
            // Console.WriteLine($"{line} -> {largestNumber}");
        }
        Console.WriteLine($"Part 1: {total}");
    }


    public void Part2()
    {
        long total = 0;
        var lines = InputReader.GetInputLines("Inputs/Day3Input.txt");
        foreach (string line in lines)
        {
            long largestNumber = GetLargestNumberForLine(line, 12);
            total += largestNumber;
            //Console.WriteLine($"{line} -> {largestNumber}");
       }
       Console.WriteLine($"Part 2: {total}");
    }
}