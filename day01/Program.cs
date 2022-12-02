var elfCalories = File.ReadAllText("input.txt")
    .Split($"{Environment.NewLine}{Environment.NewLine}")
    .Select(line => line.Split(Environment.NewLine).Sum(int.Parse))
    .ToList();

var mostTotalCalories = elfCalories.Max();

var sumOfTopThreeTotalCalories = elfCalories
    .OrderByDescending(x => x)
    .Take(3)
    .Sum();

Console.WriteLine($"Part 1: {mostTotalCalories}");
Console.WriteLine($"Part 2: {sumOfTopThreeTotalCalories}");

Console.WriteLine("done");