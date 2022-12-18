using System.Collections.Immutable;
internal class Program
{
    private static void Main(string[] args)
    {
        string[] input = File.ReadAllLines("input.txt");
        var startPOS = findPos(input, 'S');
        var endPOS = findPos(input, 'E');
        Console.WriteLine($"Starting at {startPOS.Item1},{startPOS.Item2}");
        var shortestPath = TryPathBFS(input, startPOS, endPOS, true);

        Console.WriteLine($"Shortest path: {shortestPath}");
        var shortestPath2 = TryPathBFS(input, startPOS, endPOS, false);
        Console.WriteLine($"Shortest path2: {shortestPath2}");
    }


    private static int TryPathBFS(string[] layout, (int, int) startPos, (int, int) endPos, bool part1)
    {
        var queue = new Queue<((int x, int y), int step)>();
        var visited = new HashSet<(int x, int y)>();
        List<(int x, int y)> neighbors = new List<(int x, int y)>() { (-1, 0), (1, 0), (0, -1), (0, 1) };
        if (part1)
        {
            queue.Enqueue(((startPos), 0));
        }
        else
        {
            for (int x = 0; x < layout.Count(); x++)
            {
                for (int y = 0; y < layout[0].Count(); y++)
                    if (GetValue(layout[x][y]) == GetValue('a'))
                    {
                        queue.Enqueue(((x, y), 0));
                    }
            }
        }
        while (queue.Any())
        {
            ((int x, int y), int step) = queue.Dequeue();
            if (!visited.Add((x, y)))
            {
                continue;
            }
            if (x == endPos.Item1 && y == endPos.Item2)
            {
                return step;
            }
            foreach ((int dx, int dy) in neighbors)
            {
                var dxPos = x + dx;
                var dyPos = y + dy;
                if ((dxPos >= 0 && dxPos < layout.Count()) && (dyPos >= 0 && dyPos < layout[0].Count()))
                {
                    var parentNode = layout[x][y];
                    var childNode = layout[dxPos][dyPos];

                    if (GetValue(childNode) - GetValue(parentNode) <= 1)
                    {
                        queue.Enqueue(((dxPos, dyPos), step + 1));
                    }
                }
            }
        }
        return 0;
    }

    private static (int, int) findPos(string[] layout, char searchChar)
    {
        int row = 0;
        var pos = -1;
        for (row = 0; pos == -1 & (row < layout.Count()); row++)
        {
            pos = layout[row].IndexOf(searchChar);
            if (pos != -1)
            {
                return (row, pos);
            }
        }
        return (-1, -1);
    }

    private static int GetValue(char ch)
    {
        switch (ch)
        {
            case 'S':
                return GetValue('a');
            case 'E':
                return GetValue('z');
            case >= 'a' and <= 'z':
                return (int)ch;
            default:
                return -1;
        }
    }

}