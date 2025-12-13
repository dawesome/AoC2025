// See https://aka.ms/new-console-template for more information

using AoC2025;

Console.WriteLine("Hello, World!");

Day1 day1 = new Day1();
//day1.Part1();
//day1.Part2();

Day2 day2 = new Day2();
day2.Part1();
day2.Part2();

Console.WriteLine("\n\nDay 3");
Day3 day3 = new Day3();
day3.Part1();
day3.Part2();

Console.WriteLine("\n\nDay 4");
Day4 day4 = new Day4();
var elapsed = Timing.Time(() => day4.Part1());
Console.WriteLine($"Part 1 took {elapsed.TotalMilliseconds} ms");
elapsed = Timing.Time(() => day4.Part2());
Console.WriteLine($"Part 2 took {elapsed.TotalMilliseconds} ms");


