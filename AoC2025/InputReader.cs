using System.Collections.Generic;
using System.IO;

namespace AoC2025;

public class InputReader
{
    public IEnumerable<string> ReadLines(string filePath)
    {
        return File.ReadLines(filePath);
    }
    
    public static IEnumerable<string> GetInputLines(string filePath)
    {
        InputReader reader = new InputReader();
        var lines = reader.ReadLines(filePath);
        return lines;
    }
}