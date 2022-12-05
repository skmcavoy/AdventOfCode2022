internal class Program
{
    private static void Main(string[] args)
    {
        string filename = "input.txt";
        string? textLine = string.Empty;
        StreamReader streamReader = new StreamReader(filename);
        int sum = 0;
        int counter = 0;
        bool part2 = true;
        string first = string.Empty, second = string.Empty;
        string[] group = new string[3];
        List<char> overlap;
        while (!streamReader.EndOfStream)
        {
            textLine = streamReader.ReadLine();
            if (!string.IsNullOrEmpty(textLine))
            {
                if (part2)
                {
                    group[counter] = textLine;
                    counter++;
                    if (counter==3){
                        overlap = group[0].Intersect(group[1]).Intersect(group[1].Intersect(group[2])).ToList();
                        if(overlap.Count != 1){
                            throw new Exception("No intersect found!");
                        } else{
                            System.Console.WriteLine("Badge is {0}",overlap[0]);
                            sum+=getValueOf(overlap[0]);
                        }
                        counter=0;
                    }
                }
                else
                {
                    first = textLine.Substring(0, textLine.Length / 2);
                    second = textLine.Substring(textLine.Length / 2);
                    overlap = first.Intersect(second).ToList();
                    sum += overlap.Sum(x => getValueOf(x));
                    System.Console.WriteLine("{0} = {1}|{2}", textLine, first, second);
                }
            }
        }
        streamReader.Close();
        System.Console.WriteLine("Sum is {0}", sum);
    }

    private static int getValueOf(char x)
    {
        switch (x)
        {
            case >= 'A' and <= 'Z':
                return (x - 38);

            case >= 'a' and <= 'z':
                return (x - 96);
        }
        return (0);
    }
}