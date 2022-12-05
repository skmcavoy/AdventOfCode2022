internal class Program
{
    private static void Main(string[] args)
    {
        string filename = "input.txt";
        string? textLine = string.Empty;
        StreamReader streamReader = new StreamReader(filename);
        int sum = 0;        
        string[] splitText;        
        Range[] range = new Range[2];
        while (!streamReader.EndOfStream)
        {
            textLine = streamReader.ReadLine();
            if (!string.IsNullOrEmpty(textLine))
            {                
                splitText = textLine.Split(',');
                range[0] = GetRange(splitText[0]);
                range[1] = GetRange(splitText[1]);
                if ((range[0].Start.Value <= range[1].Start.Value && range[0].End.Value >= range[1].End.Value) || (range[1].Start.Value <= range[0].Start.Value && range[1].End.Value >= range[0].End.Value))
                {
                    sum++;
                }


                //System.Console.WriteLine("{0} = {1}|{2}", textLine, first, second);

            }
        }
        streamReader.Close();
        System.Console.WriteLine("Sum is {0}", sum);
    }
    private static Range GetRange(string rangeTxt)
    {
        int start = 0, end = 0;
        string[] splitText = rangeTxt.Split('-');
        start = int.Parse(splitText[0]);
        end = int.Parse(splitText[1]);
        return new Range(start, end);
    }
}