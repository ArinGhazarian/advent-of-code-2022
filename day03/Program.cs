var itemPriorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

var data = File.ReadAllLines("input.txt")
    .Select(line => line.Trim())
    .ToArray();

int GetItemPriority(char item) => itemPriorities.IndexOf(item) + 1;

long GetPart1()
{
    var sumOfItemPriorities = 0L;
    
    foreach (var items in data)
    {
        var midPoint = items.Length / 2;
        var firstHalf = items[..midPoint];
        var secondHalf = items[midPoint..];
        var itemInBothCompartments = firstHalf.Intersect(secondHalf).Single();

        sumOfItemPriorities += GetItemPriority(itemInBothCompartments);
    }

    return sumOfItemPriorities;
}

long GetPart2()
{
    var sumOfBadgeItemPriorities = 0L;

    var skip = 0;
    while (skip < data.Length)
    {
        var itemsSet = data.Skip(skip).Take(3).ToArray();
        var badgeItem = itemsSet[0].Intersect(itemsSet[1]).Intersect(itemsSet[2]).Single();

        sumOfBadgeItemPriorities += GetItemPriority(badgeItem);

        skip += 3;
    }

    return sumOfBadgeItemPriorities;
}

Console.WriteLine($"Part 1: {GetPart1()}");
Console.WriteLine($"Part 2: {GetPart2()}");

Console.WriteLine("done");
