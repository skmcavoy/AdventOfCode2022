internal class Program
{
    private static void Main(string[] args)
    {
        string filename = "input.txt";
        int i=0;
        bool found=false;
        bool part2 = false;
        string? textLine = string.Empty;
        StreamReader streamReader = new StreamReader(filename);        
        
        if (!streamReader.EndOfStream)
        {
            textLine = streamReader.ReadToEnd();        
        }
        streamReader.Close();
        for(i=0;(i<textLine.Length-4) && (!found);i++){
             if (isUnique(textLine.Substring(i,4))){
                found=true;
                i+=3;
             }
        }
        
        System.Console.WriteLine("Found {0} at {1}",found,i);
        
                
        
    }

    private static bool isUnique(string txt){
        List<char> l = new List<char>(txt.ToCharArray());
        return(l.Distinct().Count()==txt.Length);
    }
}