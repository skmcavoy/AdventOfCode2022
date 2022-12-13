internal class Program
{
    private static void Main(string[] args)
    {
        List<string> instructions = System.IO.File.ReadAllLines("input.txt").ToList();
        FileElement node = BuildDirectoryTree(instructions);
        int sumThreshold = 100000;
        Console.WriteLine($"Part 1: Sum {node.SumOfChildrenBelow(sumThreshold)}");

        int maxSpace = 70000000;
        int target = 30000000;
        int freeSpace = maxSpace - node.SumOfImmediateChildren("/");
        target -= freeSpace;

        Console.WriteLine($"Part 2: SizeOfRoot = {maxSpace + freeSpace} Free Space = {freeSpace}, Target = {target} minDir = {node.MinDirectory(target)}");
    }

    private static FileElement BuildDirectoryTree(List<string> instructions)
    {        
        FileElement root = new FileElement() { name = "/", isDirectory = true };
        FileElement current = root;
        FileElement parent = root;        
        for (var i = 0; i < instructions.Count; i++)
        {
            if (instructions[i].Length > 0)
            {
                var instruction = instructions[i].Split(' ');
                if (instruction[0] == "$")
                {
                    switch (instruction[1])
                    {
                        case "cd":
                            if (instruction[2] == "..")
                            {
                                if (current.Parent != null)
                                {
                                    current = current.Parent;
                                }

                            }
                            else
                            {
                                if (instruction[2] == "/")
                                {
                                    current = root;
                                }
                                else
                                {
                                    parent = current;
                                    current = root.Children.Where(x => x.isDirectory && x.name == instruction[2] && x.Parent == current).FirstOrDefault(new FileElement());
                                    if (current.name == "")
                                    {
                                        current = new FileElement { isDirectory = true, Parent = parent, name = instruction[2] };
                                        root.Children.Add(current);
                                    }
                                }
                            }

                            break;
                        case "ls":
                            foreach (var line in instructions.Skip(i + 1).TakeWhile(s => !s.StartsWith("$")))
                            {
                                i++;
                                var lineparts = line.Split(' ');
                                if (lineparts[0] == "dir")
                                {

                                    if (root.Children.Where(x => x.isDirectory && x.name == lineparts[1] && x.Parent == current).Count() == 0)
                                    {
                                        root.Children.Add(new FileElement() { name = lineparts[1], Parent = current, isDirectory = true });
                                    }
                                }
                                else
                                {
                                    if (current.Children.Where(x => x.name == lineparts[1]).Count() == 0)
                                    {
                                        int filesize = int.Parse(lineparts[0]);
                                        current.Children.Add(new FileElement() { name = lineparts[1], size = filesize, Parent = current });
                                        current.size += filesize;
                                        current.updateParentSize(filesize);
                                    }
                                }

                            }
                            break;
                        default:
                            //is a file
                            break;
                    }
                }
            }
        }

        return root;
    }



    internal class FileElement
    {
        internal FileElement()
        {
            Children = new List<FileElement>();
            name = "";
            size = 0;
            isDirectory = false;
        }
        public string name { get; set; }
        public List<FileElement> Children { get; set; }
        public FileElement Parent { get; set; }
        public int size { get; set; }
        public bool isDirectory { get; set; }

        public int SumOfChildrenBelow(int limit)
        {
            var dirs = Children.Where(x => x.isDirectory).Where(x => limit > 0 ? x.size <= limit : true);
            Console.WriteLine(string.Join("\n", dirs.Select(x => $"{x.name} ({x.GetParents()}) - {x.size}")));

            return dirs.Sum(x => x.size);
        }
        public string GetParents()
        {
            FileElement currPar = Parent;
            string parents = string.Empty;
            while (currPar != null)
            {
                parents = currPar.name + "/" + parents;
                currPar = currPar.Parent;
            }
            return parents;
        }
        public void updateParentSize(int size)
        {
            FileElement currPar = Parent;
            while (currPar != null)
            {
                currPar.size += size;
                currPar = currPar.Parent;
            }
        }
        public int SumOfImmediateChildren(string parentName)
        {
            return Children.Select(x => x.Parent.name == parentName ? x.size : 0).Sum();
        }
        public int MinDirectory(int target)
        {
            return Children.Where(x => x.isDirectory && x.size >= target).Select(x => x.size).Min();
        }
    }

}