using System.Numerics;
internal class Program
{
    private static void Main(string[] args)
    {
        var inputs = File.ReadAllLines("input.txt").ToList();
        var monkies = parseInputs(inputs);
        //Console.WriteLine($"{string.Join(",", Monkies.Select(x => $"Monkey {x.Number} has {x.Items.Count} and mods by {x.ModulusTest} if true {x.throwIfTrue} else {x.throwIfFalse}"))}");
        Console.WriteLine($"Part 1: {GetMonkeyBusiness(monkies, 20,3,false)}");        
        monkies = parseInputs(inputs);
        Console.WriteLine($"Part 2: {GetMonkeyBusiness(monkies, 10000 ,1,true)}");
        //Console.WriteLine($"{string.Join(",", monkies.Select(x => $"Monkey {x.Number} has {x.Items.Count} and {x.Inspections} inspections"))}");
    }

    private static double GetMonkeyBusiness(List<Monkey> monkies, int rounds,int worryDivider, bool noWorries)
    {
        double score = 0;
        var factor = monkies.Select(m=>m.ModulusTest).Aggregate((x,y)=> x*y);
        for (int round = 0; round < rounds; round++)
        {
            foreach (Monkey m in monkies)
            {
                foreach (var item in m.Items.ToArray())
                {
                    m.Inspections += 1;
                    double worry = item;
                    worry = getWorry(worry, m.operation);
                    if(noWorries){
                        worry = worry% factor;
                    }else{
                        worry=Math.Floor(worry/worryDivider);
                    }
                    if(worry % m.ModulusTest ==0){                        
                        monkies.Where(x=>x.Number==m.throwIfTrue).First().Items.Add(worry);
                    }
                    else{
                        monkies.Where(x=>x.Number==m.throwIfFalse).First().Items.Add(worry);
                    }
                    m.Items.Remove(item);                    
                }
            }
          //  if (round % 1000 == 0){
          //      Console.WriteLine($"Round {round}\n{string.Join("\n",monkies.Select(x=>$"\tMonkey {x.Number} has inspected {x.Inspections} holding{x.Items.Count} - {string.Join(",",x.Items)}"))}");
          //  }
        }
        
        score = monkies.OrderByDescending(x => x.Inspections).Take(2).Aggregate(Convert.ToInt64(1),(acc,val)=>acc*val.Inspections, result=>Convert.ToInt64(result));
        return score;
    }
    private static double getWorry(double old, string[] operation)
    {
        double operand = 0;
        double newWorry = 0;
        switch (operation[3])
        {
            case "old":
                operand = old;
                break;
            default:
                operand = Int64.Parse(operation[3]);
                break;
        }
        switch (operation[2])
        {
            case "+":
                newWorry = old + operand;
                break;
            case "-":
                newWorry = old - operand;
                break;
            case "*":
                newWorry = old * operand;
                break;
            case "/":
                newWorry = old / operand;
                break;
        }
        return newWorry;
    }
    private static List<Monkey> parseInputs(List<string> inputs)
    {
        List<Monkey> monkies = new List<Monkey>();

        for (int i = 0; i < inputs.Count; i++)
        {
            if (inputs[i].StartsWith("Monkey"))
            {
                Monkey m = new Monkey();
                m.Number = int.Parse(inputs[i].Replace(":", "").Replace("Monkey ", ""));
                foreach (var monkeyline in inputs.Skip(i + 1).TakeWhile(x => !x.StartsWith("Monkey")))
                {
                    var line = monkeyline.Trim().Split(":");
                    switch (line[0])
                    {
                        case "Starting items":
                            foreach (var item in line[1].Trim().Split(", "))
                            {
                                m.Items.Add(Int64.Parse(item));
                            }
                            break;
                        case "Operation":
                            m.operation = line[1].Substring(line[1].IndexOf("=")).Split(" ");
                            break;
                        case "Test":
                            m.ModulusTest = int.Parse(line[1].Split(" ").Last());
                            break;
                        case "If true":
                            m.throwIfTrue = int.Parse(line[1].Split(" ").Last());
                            break;
                        case "If false":
                            m.throwIfFalse = int.Parse(line[1].Split(" ").Last());
                            break;
                    }

                }
                monkies.Add(m);
            }
        }


        return monkies;
    }

    private class Monkey 
    {
        public Monkey()
        {
            Items = new List<double>();
            operation = null;
            ModulusTest = 0;
            Number = 0;
            Inspections = 0;
        }
        public int Number { get; set; }
        public List<double> Items { get; set; }
        public string[]? operation { get; set; }
        public int ModulusTest { get; set; }
        public int throwIfTrue { get; set; }
        public int throwIfFalse { get; set; }
        public int Inspections { get; set; }        
    }
}