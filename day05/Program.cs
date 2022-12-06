using System.Text.RegularExpressions;

var data = File.ReadAllText("input.txt")
    .Split($"{Environment.NewLine}{Environment.NewLine}");
var cratesData = data[0].Split(Environment.NewLine);
var instructionsData = data[1].Split(Environment.NewLine);

var instructionPattern = @"move (?<count>\d+) from (?<from>\d+) to (?<to>\d+)";
var instructions = instructionsData
    .Select(line =>
    {
        var groups = Regex.Match(line, instructionPattern).Groups;
        return (
            Count: int.Parse(groups["count"].Value),
            From: int.Parse(groups["from"].Value),
            To: int.Parse(groups["to"].Value));
    })
    .ToArray();

var stackIndexes = cratesData.Last()
    .Select((c, i) => (c, i))
    .Where(x => char.IsDigit(x.c))
    .Select(x => x.i)
    .ToArray();

Stack<char>[] GetStacks()
{
    var stacks = new Stack<char>[stackIndexes.Length];
    foreach (var line in cratesData.Reverse().Skip(1))
    {
        for (var stack = 0; stack < stackIndexes.Length; stack++)
        {
            var crate = line[stackIndexes[stack]];
            if (crate is not ' ')
            {
                stacks[stack] ??= new();
                stacks[stack].Push(crate);
            }
        }
    }

    return stacks;
}

string GetPart1()
{
    var stacks = GetStacks();
    
    foreach (var (count, fromStackNumber, toStackNumber) in instructions)
    {
        var fromStack = stacks[fromStackNumber - 1];
        var toStack = stacks[toStackNumber - 1];

        for (int i = 0; i < count; i++)
        {
            toStack.Push(fromStack.Pop());
        }
    }

    return string.Join("", stacks.Select(s => s.Peek()));
}

string GetPart2()
{
    var stacks = GetStacks();
    
    foreach (var (count, fromStackNumber, toStackNumber) in instructions)
    {
        var fromStack = stacks[fromStackNumber - 1];
        var toStack = stacks[toStackNumber - 1];

        var tempStack = new Stack<char>(count);
        for (int i = 0; i < count; i++)
        {
            tempStack.Push(fromStack.Pop());
        }

        foreach (var crate in tempStack)
        {
            toStack.Push(crate);
        }
    }

    return string.Join("", stacks.Select(s => s.Peek()));
}

Console.WriteLine($"Part 1: {GetPart1()}");
Console.WriteLine($"Part 2: {GetPart2()}");

Console.WriteLine("done");
