var firstWinsOverSecondHandParis = new[]
{
    (First: Hand.Rock, Second: Hand.Scissors),
    (First: Hand.Paper, Second: Hand.Rock),
    (First: Hand.Scissors, Second: Hand.Paper)
};

Hand GetHand(string hand) => hand switch
{
    "A" or "X" => Hand.Rock,
    "B" or "Y" => Hand.Paper,
    "C" or "Z" => Hand.Scissors,
    _ => throw new ArgumentException(nameof(hand))
};

int GetYourRoundScore(Hand yours, Hand theirs)
{
    const int winScore = 6;
    const int drawScore = 3;
    const int loseScore = 0;

    int roundOutcome = loseScore;

    if (yours == theirs)
    {
        roundOutcome = drawScore;
    }
    else if (firstWinsOverSecondHandParis.Contains((yours, theirs)))
    {
        roundOutcome = winScore;
    }

    return (int)yours + roundOutcome;
}

Hand GetHandForOutcome(Hand theirs, Outcome desiredOutcome) => desiredOutcome switch
{
    Outcome.Win => firstWinsOverSecondHandParis.Single(pair => pair.Second == theirs).First,
    Outcome.Lose => firstWinsOverSecondHandParis.Single(pair => pair.First == theirs).Second,
    Outcome.Draw => theirs,
    _ => throw new ArgumentOutOfRangeException(nameof(desiredOutcome))
};


var rounds = File.ReadAllLines("input.txt")
    .Select(line => line.Trim().Split(" "))
    .ToList();

long GetPart1() => rounds.Sum(round => (long)GetYourRoundScore(GetHand(round[1]), GetHand(round[0])));

long GetPart2() => rounds.Sum(round => (long)GetYourRoundScore(
    GetHandForOutcome(GetHand(round[0]), (Outcome)char.Parse(round[1])),
    GetHand(round[0])));

Console.WriteLine($"Part 1: {GetPart1()}");
Console.WriteLine($"Part 2: {GetPart2()}");

Console.WriteLine("done");

enum Hand
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

enum Outcome
{
    Lose = 'X',
    Draw = 'Y',
    Win = 'Z'
}
