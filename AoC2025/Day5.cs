using System.Collections;

namespace AoC2025;

class Range
{
    public long LowerBound;
    public long UpperBound;
    
    public Range(long lowerBound, long upperBound)
    {
        LowerBound = lowerBound;
        UpperBound = upperBound;
    }

    public override bool Equals(Object obj)
    {
        if (obj == null || !(obj is Range))
        {
            return false;
        }
        return this.LowerBound == ((Range)obj).LowerBound && this.UpperBound == ((Range)obj).UpperBound;
    }
}

public class Day5
{
    public void Clear()
    {
        ranges.Clear();
        ids.Clear();
    }
    
    public ArrayList ranges = new ArrayList();
    public ArrayList ids = new ArrayList();

    public ArrayList GetRanges()
    {
        return ranges;
    }
    public ArrayList GetIds()
    {
        return ids;
    }
    public void ParseLines(IEnumerable<string> lines)
    {
        ranges.Clear();
        ids.Clear();
        
        foreach (var line in lines)
        {
            if (line.IsWhiteSpace())
            {
                continue;
            }
            
            var possibleRange = line.Split('-');
            if (possibleRange.Length == 2)
            {
                ranges.Add(new Range(long.Parse(possibleRange[0]), long.Parse(possibleRange[1])));
            }
            else
            {
                ids.Add(long.Parse(line));
            }
        }
    }

    public bool IsFresh(long id)
    {
        foreach (Range range in ranges)
        {
            if (id >= range.LowerBound && id <= range.UpperBound)
            {
                return true;
            }
        }
        return false;
    }

    public long CountFreshIds()
    {
        long totalFreshIds = 0;
        // Okay, new approach. For each range, combine ranges that overlap.
        ArrayList combinedRanges = CombineRanges();
        
        foreach (Range range in combinedRanges)
        {
            totalFreshIds += range.UpperBound - range.LowerBound + 1;
        }
        return totalFreshIds;
    }
    
    public ArrayList CombineRanges()
    {
        ArrayList uncombinedRanges = ranges.Clone() as ArrayList; 
        ArrayList combinedRanges = new ArrayList();
        
        long lastCombinedRangesCount = ranges.Count;
        bool modifiedRanges = true;
        
        do
        {
            //Start with a fresh list.
            combinedRanges.Clear();
            foreach (Range currentRange in uncombinedRanges)
            {
                bool skipAddingCurrentRange = false;
                foreach (Range combinedRange in combinedRanges)
                {
                    // is this new range wholly contained within the combinedRange?
                    if (currentRange.LowerBound >= combinedRange.LowerBound && currentRange.UpperBound <= combinedRange.UpperBound)
                    {
                        // Count it as modified so we don't add this fully contained range
                        skipAddingCurrentRange = true;
                    }
                    // or does the new range wholly contain the combinedRange? 
                    else if (currentRange.LowerBound <= combinedRange.LowerBound &&
                             currentRange.UpperBound >= combinedRange.UpperBound)
                    {
                        // Here, change the range in combinedRanges to be the new range
                        combinedRange.LowerBound = currentRange.LowerBound;
                        combinedRange.UpperBound = currentRange.UpperBound;
                        // and don't add it again
                        skipAddingCurrentRange = true;
                    }
                    // If currentRange's LowerBound withing combinedRange's range
                    else if (currentRange.LowerBound >= combinedRange.LowerBound && currentRange.LowerBound <= combinedRange.UpperBound)
                    {
                        //...extend the range up if we need to
                        if (combinedRange.UpperBound <= currentRange.UpperBound)
                        {
                            combinedRange.UpperBound = currentRange.UpperBound;
                            skipAddingCurrentRange = true;
                        }
                    } 
                    // else if the current UpperBound is within the combinedRange's range
                    else if (currentRange.UpperBound >= combinedRange.LowerBound && currentRange.UpperBound <= combinedRange.UpperBound)
                    {
                        //...extend the range down if we need to
                        if (currentRange.LowerBound <= combinedRange.LowerBound)
                        {
                            combinedRange.LowerBound = currentRange.LowerBound;
                            skipAddingCurrentRange = true;
                        }
                    }
                }

                if (!skipAddingCurrentRange)
                {
                    combinedRanges.Add(currentRange);
                }
            }
            
            if (lastCombinedRangesCount == combinedRanges.Count) 
            {
                modifiedRanges = false;
            }
            lastCombinedRangesCount = combinedRanges.Count;
            uncombinedRanges = combinedRanges.Clone() as ArrayList;
        } while (modifiedRanges);

        return combinedRanges;
    }
    
    public void Part1()
    {
        var lines = InputReader.GetInputLines("Inputs/Day5Input.txt");
        ParseLines(lines);

        long freshCount = 0;
        foreach (long id in ids)
        {
            if (IsFresh(id))
            {
                ++freshCount;
            }
        }
        Console.WriteLine(freshCount);
    }
    
    public void Part2()
    {
        var lines = InputReader.GetInputLines("Inputs/Day5Input.txt");
        ParseLines(lines);
        
        Console.WriteLine(CountFreshIds());
    }
}