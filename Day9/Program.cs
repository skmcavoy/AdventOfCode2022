internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input.txt");
        Console.WriteLine($"Part1: {getTailMovements(input,10)}");

    }
    private static int getTailMovements(string[] input, int tailLength)
    {        
        var tailLandings = new HashSet<(int,int)>();
        var rope = new (int,int)[tailLength];
        var moves = input.Select(line=>{
            var split = line.Split(" ");
            return( split[0] switch
            {
                "L"=> (-1,0),
                "R" => (1,0),
                "U" => (0,-1),
                "D" => (0,1),
                _ => (0,0)                
            },int.Parse(split[1]));
        });
        
        foreach(var move in moves){
            for(int i = 0; i< move.Item2;i++){
                (int x,int y) = rope[0];
                rope[0] = (x+move.Item1.Item1,y+move.Item1.Item2);
                for (int j = 1; j< rope.Length; j++){

                    (int prevX, int prevY) = rope[j-1];
                    (int currX, int currY) = rope[j];
                    (int distX, int distY) = (prevX-currX,prevY-currY);
                    if(Math.Abs(distX)>1 || Math.Abs(distY)>1){
                        rope[j]= (currX+Math.Sign(distX),currY+Math.Sign(distY));
                    }
                    tailLandings.Add(rope.Last());
                }

            }
        }


        return tailLandings.Count;
    }
}