internal class Program
{
    private static void Main(string[] args)
    {
        string filename = "input.txt";
        string? textLine = string.Empty;
        StreamReader streamReader = new StreamReader(filename);
        bool part2 = true;
        string finalPos = string.Empty;
        Stack<char>[] cols;
        if (filename != "input.txt")
        {
            cols = new Stack<char>[]{
            new Stack<char>(new List<char>{'Z','N'}),
            new Stack<char>(new List<char>{'M','C','D'}),
            new Stack<char>(new List<char>{'P'})
        };
        }
        else
        {
            cols = new Stack<char>[]{
            new Stack<char>(new List<char>{'Z','J','G'}),
            new Stack<char>(new List<char>{'Q','L','R','P','W','F','V','C'}),
            new Stack<char>(new List<char>{'F','P','M','C','L','G','R'}),
            new Stack<char>(new List<char>{'L','F','B','W','P','H','M'}),
            new Stack<char>(new List<char>{'G','C','F','S','V','Q'}),
            new Stack<char>(new List<char>{'W','H','J','Z','M','Q','T','L'}),
            new Stack<char>(new List<char>{'H','F','S','B','V'}),
            new Stack<char>(new List<char>{'F','J','Z','S'}),
            new Stack<char>(new List<char>{'M','C','D','P','F','H','B','T'})
            };
        }
        while (!streamReader.EndOfStream)
        {
            textLine = streamReader.ReadLine();
            if (string.IsNullOrEmpty(textLine))
            {
                textLine = string.Empty;
            }
            if (textLine.Contains("move"))
            {
                Move m = parseMove(textLine);
                if (part2)
                {
                    Queue<char> movingBox = new Queue<char>(m.Count);
                    for (int i = 0; i < m.Count; i++)
                    {                        
                        movingBox.Enqueue(cols[m.From - 1].Pop());
                    }
                    foreach(char box in movingBox.Reverse()){
                        cols[m.To-1].Push(box);
                    }
                }
                else
                {
                    for (int i = 0; i < m.Count; i++)
                    {
                        cols[m.To - 1].Push(cols[m.From - 1].Pop());
                    }
                }
            }
        }
        streamReader.Close();
        foreach (var col in cols)
        {
            finalPos += col.Peek().ToString();
        }
        System.Console.WriteLine("{0}", finalPos);
    }

    private static Move parseMove(string textLine)
    {
        Move m = new Move();
        var elements = textLine.Split(' ').ToList();
        elements = elements.Where(x => int.TryParse(x, out int temp)).ToList();
        if (elements.Count != 3)
        {
            throw new Exception(string.Format("Found too many number moves {0} in {1}", elements.Count, textLine));
        }
        m.Count = int.Parse(elements[0]);
        m.From = int.Parse(elements[1]);
        m.To = int.Parse(elements[2]);
        return m;
    }

    private struct Move
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Count { get; set; }

    }
}