namespace AoC2025;

public class Day1
{
    private int mDial = 50;
    
 

    // Move dial. Returns number of times it passed 0 
    public int moveDial(int amount, bool left)
    {
        int timesPassedZero = 0;
        Console.Write($"Moving dial from {mDial} by {(left ? "L" : "R")} {amount}, ");
        
        while (amount > 0)
        {
            if (left)
            {
                --mDial;
                if (mDial == -1)
                {
                    mDial = 99;
                }
            }
            else
            {
                ++mDial;
                if (mDial == 100)
                {
                    mDial = 0;
                }
            }
            if (mDial == 0)
            {
                timesPassedZero++;
            }
            --amount;
        }
        Console.WriteLine($"result {mDial}, times passed zero {timesPassedZero}");
        return timesPassedZero;
    }
    
    public void Part1()
    {
        int numTimesAtZero = 0;
        var lines = InputReader.GetInputLines("Inputs/Day1Part1input.txt");
        foreach (string line in lines)
        {
            moveDial(int.Parse(line.Substring(1)), line[0] == 'L');
            if (mDial == 0)
            {
                numTimesAtZero++;
            }
        }
        Console.WriteLine($"Part 1: {numTimesAtZero}");
    }
    
    public void Part2()
    {
        int numTimesPassedZero = 0;
        var lines = InputReader.GetInputLines("Inputs/Day1Part1input.txt");
        foreach (string line in lines)
        {
            numTimesPassedZero += moveDial(int.Parse(line.Substring(1)), line[0] == 'L');
        }
        Console.WriteLine($"Part 2: {numTimesPassedZero}");
    }
}