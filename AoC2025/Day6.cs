namespace AoC2025;

public class Day6
{
    public string[][] ParseMathProblems(IEnumerable<string> lines)
    {
        string[][] problems = new string[lines.Count()][];
        int problemIndex = 0;
        foreach (var line in lines)
        {
            problems[problemIndex++] = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        return problems;
    }
    
    public char[][] ParseMathProblemsPart2(IEnumerable<string> lines)
    {
        char[][] characters = new char[lines.Count()][];
        int lineCount = 0;
        foreach (var line in lines)
        {  
            characters[lineCount++] = line.ToCharArray();
        }
        return characters;
    }
    
    public long TotalMathProblems(string[][] unrotatedProblems) 
    {
        // There are as many math problems as the grid is wide
        long numProblems = unrotatedProblems[0].Length;
        
        // There are as many operands as the grid is high (minus the actual operation)
        long operandsInProblem = unrotatedProblems.Length - 1;
        long grandTotal = 0;
        
        // For each math problem
        for (int problemIndex = 0; problemIndex < numProblems; problemIndex++)
        {
            // The operation is the last row
            char operation = unrotatedProblems[operandsInProblem][problemIndex].ToCharArray()[0];
            long runningTotal = 0;
            if (operation.Equals('*'))
            {
                runningTotal = 1;
            }
            
            for (int rowIndex = 0; rowIndex < operandsInProblem; rowIndex++)
            {
                if (operation.Equals('+'))
                {
                    runningTotal += int.Parse(unrotatedProblems[rowIndex][problemIndex]);
                } else if (operation.Equals('*'))
                {
                    runningTotal *= int.Parse(unrotatedProblems[rowIndex][problemIndex]);                    
                }
            }

            grandTotal += runningTotal;
        }
        
        return grandTotal;
    }
    
    public long TotalMathProblemsPart2(char[][] characters) 
    {
        long grandTotal = 0;
        long problemTotal = 0;
        long height = characters.Length;
        long width = characters[0].Length; 
        char operand = ' ';
        
        bool newProblem = true;
        
        for (long column = 0; column < width; ++column)
        {
            if (newProblem)
            {
                grandTotal += problemTotal;
                problemTotal = 0;
                operand = characters[height - 1][column];
                if (operand.Equals('*'))
                {
                    problemTotal = 1;
                }
                newProblem = false;
            }
            string numberString = "";
            for (long row = 0; row < height - 1; ++row)
            {
                numberString += characters[row][column];
            }

            if (numberString.IsWhiteSpace())
            {
                // We're done with this problem
                newProblem = true;
                continue;
            }
            
            if (operand.Equals('+'))
            {
                problemTotal += long.Parse(numberString);
            } else if (operand.Equals('*'))
            {
                problemTotal *= long.Parse(numberString);
            }
        }
        
        grandTotal += problemTotal;
        return grandTotal;
    }
    
    public void Part1()
    {
        var lines = InputReader.GetInputLines("Inputs/Day6Input.txt");
        Console.WriteLine("Part 1: " + TotalMathProblems(ParseMathProblems(lines)));
    }
    
    public void Part2()
    {
        var lines = InputReader.GetInputLines("Inputs/Day6Input.txt");
        Console.WriteLine("Part 2: " + TotalMathProblemsPart2(ParseMathProblemsPart2(lines)));
    }
}