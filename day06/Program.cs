var data = File.ReadAllText("input.txt");

long GetFirstMarkerIndex(int bufferSize)
{
    var buffer = new List<char>(bufferSize);
    var number = 0L;
    foreach (var c in data)
    {
        if (buffer.Count == bufferSize)
        {
            if (buffer.Distinct().Count() == bufferSize)
            {
                return number;
            }
            
            buffer.RemoveAt(0);
        }

        buffer.Add(c);
        number++;
    }

    return data.LongCount();
}

long GetPart1() => GetFirstMarkerIndex(4);

long GetPart2() => GetFirstMarkerIndex(14);

Console.WriteLine($"Part 1: {GetPart1()}");
Console.WriteLine($"Part 2: {GetPart2()}");

Console.WriteLine("done");