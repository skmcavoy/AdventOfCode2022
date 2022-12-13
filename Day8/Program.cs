internal class Program
{
    private static void Main(string[] args)
    {
        string[] input = System.IO.File.ReadAllLines("input.txt");
        List<List<int>> treeList = new List<List<int>>();

        for (int i = 0; i < input.Length; i++)
        {
            List<int> treeLine = new List<int>();

            for (int y = 0; y < input[i].Length; y++)
            {
                treeLine.Add(int.Parse(input[i][y].ToString()));
            }
            treeList.Add(treeLine);
        }

        Console.WriteLine($"Part 1: {findVisibleFromOutside(treeList)} Visible");
    }

    private static int findVisibleFromOutside(List<List<int>> treeGrid)
    {
        int visCount = 0;

        for (int i = 0; i < treeGrid.Count(); i++)
        {
            if (i == 0 | i == treeGrid.Count() - 1)
            {
                visCount += treeGrid[0].Count();
            }
            else
            {
                for (int y = 0; y < treeGrid[0].Count(); y++)
                {
                    if ((y == 0 | y == treeGrid[0].Count() - 1) ||
                        (treeGrid[i].Take(y).Max() < treeGrid[i][y]) ||
                        (treeGrid[i].TakeLast(treeGrid[i].Count - y - 1).Max() < treeGrid[i][y]) ||
                        (treeGrid.Select(x=>x[y]).Take(i).Max() < treeGrid[i][y]) ||
                        (treeGrid.Select(x=>x[y]).TakeLast(treeGrid.Count -1-i).Max() < treeGrid[i][y])
                        )
                    {
                        visCount += 1;
                    }
                }                
            }
        }
        return visCount;
    }
}