internal class Program
{
    private static void Main(string[] args)
    {
        string filename = "input.txt";
        int i=0;
        bool found=false;
        bool part2 = true;
        int searchLen =0;
        if (part2){
            searchLen=14;
        }
        else{
            searchLen=4;
        }
        string? textLine = string.Empty;
        StreamReader streamReader = new StreamReader(filename);        
        
        if (!streamReader.EndOfStream)
        {
            textLine = streamReader.ReadToEnd();        
        }
        streamReader.Close();
        
        for(i=0;(i<textLine.Length-searchLen) && (!found);i++){
             if (isUnique(textLine.Substring(i,searchLen))){
                found=true;
                i+=searchLen-1;
             }
        }
        
        System.Console.WriteLine("Found {0} at {1}",found,i);
        
                
        
    }

    private static bool isUnique(string txt){
        List<char> l = new List<char>(txt.ToCharArray());
        return(l.Distinct().Count()==txt.Length);
    }
}