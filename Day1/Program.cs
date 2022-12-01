// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        string filename = "input.txt";
        int elf = 0, elftTotal = 0, tempInt = 0;
        Dictionary<int, int> elfList = new Dictionary<int, int>();
        string? textLine = string.Empty;
        StreamReader streamReader = new StreamReader(filename);
        while (!streamReader.EndOfStream)
        {
            textLine = streamReader.ReadLine();            
            if (string.IsNullOrEmpty(textLine))
            {
                elfList.Add(elf, elftTotal);
                System.Console.WriteLine("Elf {0} Total {1}", elf, elftTotal);
                elf++;
                elftTotal = 0;
            }
            else
            {
                if (Int32.TryParse(textLine, out tempInt))
                {
                    elftTotal += tempInt;
                }
                else
                {
                    throw new InvalidDataException(string.Format("Error could not parse {0}", textLine));
                }
            }
        }    
        streamReader.Close();
        if (elftTotal!=0) {
            elfList.Add(elf, elftTotal);
            System.Console.WriteLine("Elf {0} Total {1}", elf, elftTotal);
        }
        System.Console.WriteLine("Top 3 Sum {0}", elfList.Values.ToList().OrderByDescending(x => x).Take(3).Sum(x => x));
    }
}