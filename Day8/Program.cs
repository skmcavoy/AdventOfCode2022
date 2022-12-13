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
        Console.WriteLine($"Part 2: {findBestScore(treeList)} Best Score");
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
    private static int findBestScore(List<List<int>> treeGrid){
        int score =0;
        for (int i = 1; i < treeGrid.Count()-1; i++)
        {
            if (!(i == 0 | i == treeGrid.Count() - 1))
            {
                for (int y = 0; y < treeGrid[i].Count(); y++)
                {
                    if (!(y == 0 | y == treeGrid[i].Count() - 1)){
                        int left= treeGrid[i].FindLastIndex(y-1,y,x=>x>=treeGrid[i][y]);
                        left= left == -1 ? y:y-left;

                        int right = treeGrid[i].FindIndex(y+1,x=>x>=treeGrid[i][y]);
                        right = right == -1? treeGrid[0].Count() - y -1 : right-y;

                        int up = treeGrid.Select(x=>x[y]).ToList().FindLastIndex(i-1,i,x=>x>=treeGrid[i][y]);
                        up= up == -1 ? i: i-up;

                        int down =treeGrid.Select(x=>x[y]).ToList().FindIndex(i+1,x=>x>=treeGrid[i][y]);
                        down = down ==-1 ? treeGrid.Count() -i -1 : down-i;


                       // Console.WriteLine($"{i},{y}: {treeGrid[i][y]} | left={left}, right={right}, up={up}, down={down}");
                        int tempScore = left*right*up*down;
                        if (tempScore>score){
                            score=tempScore;
                        }
                    }                                         
                }                
            }
        }
        return score;
 
    }    
}