var data = File.ReadAllLines("input.txt")
    .Select(line =>
    {
        var ranges = line.Trim().Split(',');
        var range1 = ranges[0].Split('-');
        var range2 = ranges[1].Split('-');
        return (Range1: (Left: int.Parse(range1[0]), Right: int.Parse(range1[1])),
            Range2: (Left: int.Parse(range2[0]), Right: int.Parse(range2[1])));
    })
    .ToArray();

long GetPart1() => data.Count(pair =>
    pair.Range1.Left <= pair.Range2.Left && pair.Range1.Right >= pair.Range2.Right ||
    pair.Range2.Left <= pair.Range1.Left && pair.Range2.Right >= pair.Range1.Right);


long GetPart2() => data.Count(pair => !(pair.Range1.Right < pair.Range2.Left || pair.Range1.Left > pair.Range2.Right));


Console.WriteLine($"Part 1: {GetPart1()}");
Console.WriteLine($"Part 2: {GetPart2()}");

Console.WriteLine("done");