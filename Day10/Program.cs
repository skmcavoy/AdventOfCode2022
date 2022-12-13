internal class Program
{
    private static void Main(string[] args)
    {
        var instructionstxt = File.ReadAllLines("input.txt");
        var instructions = instructionstxt.Select(line =>
        {
            var split = line.Split(" ");
            return (split[0], split.Count() > 1 ? int.Parse(split[1]) : 0);
        });
        string screen = string.Empty;

        int clock = 1;
        int X = 1;
        int offset = 20;
        int interval = 40;
        List<int> intervals = new List<int>();
        processClock(clock, offset, interval, X, ref intervals,ref screen);
        foreach (var instruction in instructions)
        {
            switch (instruction.Item1)
            {
                case "addx":
                    clock++;
                    processClock(clock, offset, interval, X, ref intervals,ref screen);
                    clock++;
                    X += instruction.Item2;
                    processClock(clock, offset, interval, X, ref intervals, ref screen);
                    break;
                case "noop":
                    clock++;
                    processClock(clock, offset, interval, X, ref intervals,ref screen);
                    break;
            }
        }
        Console.WriteLine($"Part1: {intervals.Sum()}");
        Console.WriteLine($"Part2:");
        Console.WriteLine($"{screen.Substring(0,40)}");
        Console.WriteLine($"{screen.Substring(40,40)}");
        Console.WriteLine($"{screen.Substring(80,40)}");
        Console.WriteLine($"{screen.Substring(120,40)}");
        Console.WriteLine($"{screen.Substring(160,40)}");
        Console.WriteLine($"{screen.Substring(200,40)}");
    }
    private static void processClock(int clock, int offset, int interval, int X, ref List<int> intervals,ref string screen)
    {
        if (clock == offset | (clock - offset) % interval == 0)
        {
            intervals.Add(X * clock);
            Console.WriteLine($"@{clock}, X is {X}.  Adding {X * clock}");
        }
        int pos = ((clock-1)% 40);
        screen += pos >= (X-1) && pos <= (X+1) ? "#" : ".";
    }
}