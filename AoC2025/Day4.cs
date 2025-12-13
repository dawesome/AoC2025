namespace AoC2025;

public class Day4
{


    public char[][] MakeArrayFromInput(IEnumerable<string> lines)
    {
        char[][] paperGrid = new char[lines.Count()][];
        for (int count = 0; count < lines.Count(); ++count)
        {
            paperGrid[count] = lines.ElementAt(count).ToCharArray();
        }
        return paperGrid;
    }

    public int CountOfNeighborsWithPaper(char[][] paperGrid, int row, int column)
    {
        int paperCount = 0;
        for (int neighborRow = row - 1; neighborRow <= row + 1; ++neighborRow)
        {
            for (int neighborColumn = column - 1; neighborColumn <= column + 1; ++neighborColumn)
            {
                if (neighborRow < 0 || 
                    neighborRow >= paperGrid.Length ||
                    neighborColumn < 0 ||
                    neighborColumn >= paperGrid[neighborRow].Length ||
                    (neighborRow == row && neighborColumn == column))
                {
                    continue;
                }
                if (paperGrid[neighborRow][neighborColumn] == '@')
                {
                    ++paperCount;
                }
            }
        }

        return paperCount;
    }

    public void PrintMoveablePaper(char[][] paperGrid)
    {
        char[][] moveablePaperGrid = new char[paperGrid.Length][];
        for (int row = 0; row < paperGrid.Length; ++row)
        {
            moveablePaperGrid[row] = new char[paperGrid[row].Length];
            for (int column = 0; column < paperGrid[row].Length; ++column)
            {
                if (paperGrid[row][column] == '@' && CountOfNeighborsWithPaper(paperGrid, row, column) < 4)
                {
                    moveablePaperGrid[row][column] = 'x';
                } else {
                    moveablePaperGrid[row][column] = paperGrid[row][column];
                }
            }
        }
        
        foreach (char[] row in moveablePaperGrid)
        {
            Console.WriteLine(new string(row));
        }
    }
    public int CountMoveablePaper(char[][] paperGrid)
    {
        int moveablePaperCount = 0;
        for (int row = 0; row < paperGrid.Length; ++row)
        {
            for (int column = 0; column < paperGrid[row].Length; ++column)
            {
                if (paperGrid[row][column] == '@' && CountOfNeighborsWithPaper(paperGrid, row, column) < 4)
                {
                    ++moveablePaperCount;
                }
            }
        }
        return moveablePaperCount;
    }

    public int CountRemoveablePaper(char[][] paperGrid)
    {
        int totalRemoveablePaper = 0;
        int removeablePaperThisPass = 0;
        char[][] newPaperGrid = paperGrid.Clone() as char[][];

        do
        {
            removeablePaperThisPass = CountRemoveablePaperInternal(newPaperGrid.Clone() as char[][], ref newPaperGrid);
            totalRemoveablePaper += removeablePaperThisPass;
        } while (removeablePaperThisPass > 0);
        return totalRemoveablePaper;
    }
    
    private int CountRemoveablePaperInternal(char[][] paperGrid, ref char[][] newPaperGrid)
    {
        int removeablePaper = 0;
        
        for (int row = 0; row < paperGrid.Length; ++row)
        {
            for (int column = 0; column < paperGrid[row].Length; ++column)
            {
                if (paperGrid[row][column] == '@' && CountOfNeighborsWithPaper(paperGrid, row, column) < 4)
                {
                    newPaperGrid[row][column] = '.';
                    ++removeablePaper;
                } 
            }
        }
        return removeablePaper;
    }

    public void Part1()
    {
        var lines = InputReader.GetInputLines("Inputs/Day4Input.txt");
        char[][] paperGrid = MakeArrayFromInput(lines);
    
        //PrintMoveablePaper(paperGrid);
        Console.WriteLine(CountMoveablePaper(paperGrid));

    }
    
    public void Part2()
    {
        var lines = InputReader.GetInputLines("Inputs/Day4Input.txt");
        char[][] paperGrid = MakeArrayFromInput(lines);
        
        Console.WriteLine(CountRemoveablePaper(paperGrid));
    }
}