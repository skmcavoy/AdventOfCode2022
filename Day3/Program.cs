internal class Program
{
    private static void Main(string[] args)
    {
        string filename = "input.txt";                
        string? textLine = string.Empty;
        StreamReader streamReader = new StreamReader(filename);
        int sum=0;
        string first = string.Empty,second=string.Empty;
        List<char> overlap;
        while (!streamReader.EndOfStream)
        {
            textLine = streamReader.ReadLine();                        
            if (!string.IsNullOrEmpty(textLine))
            {                            
                first=textLine.Substring(0,textLine.Length/2);
                second =textLine.Substring(textLine.Length/2);
                overlap = first.Intersect(second).ToList();
                sum+=overlap.Sum(x=> getValueOf(x));
                System.Console.WriteLine("{0} = {1}|{2}",textLine,first,second);
            }
        }    
        streamReader.Close();
        System.Console.WriteLine("Sum is {0}",sum);
    }

    private static int getValueOf(char x){
        switch (x){
            case >= 'A' and <='Z':
                return(x-38);
                
            case >='a' and <='z':
                return (x-96);
        }
        return(0);
    }
}