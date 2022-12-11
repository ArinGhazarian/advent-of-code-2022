var grid = File.ReadAllLines("input.txt")
    .Select(line => line.Select(i => int.Parse(i.ToString())).ToArray())
    .ToArray();

int totalRows = grid.Length;
int totalCols = grid[0].Length;

int[] GetRow(int row) => grid![row];

int[] GetColumn(int col) => grid!.Select(row => row[col]).ToArray();

int GetNumberOfTreesAroundTheEdge() => 2 * (totalRows + totalCols) - 4;

bool IsVisible(int row, int col)
{
    var targetHeight = grid[row][col];

    var entireRow = GetRow(row);

    var leftTrees = entireRow[0..col];
    if (leftTrees.All(height => height < targetHeight))
    {
        return true;
    }

    var rightTrees = entireRow[(col + 1)..];
    if (rightTrees.All(height => height < targetHeight))
    {
        return true;
    }

    var entireCol = GetColumn(col);

    var topTrees = entireCol[0..row];
    if (topTrees.All(height => height < targetHeight))
    {
        return true;
    }

    var bottomTrees = entireCol[(row + 1)..];
    if (bottomTrees.All(height => height < targetHeight))
    {
        return true;
    }

    return false;
}

long GetScenicScore(int row, int col)
{
    var targetHeight = grid![row][col];

    var leftScore = 0L;
    for (var c = col - 1; c >= 0; c--)
    {
        leftScore++;

        if (grid[row][c] >= targetHeight)
        {
            break;
        }
    }

    var rightScore = 0L;
    for (var c = col + 1; c < totalCols; c++)
    {
        rightScore++;

        if (grid[row][c] >= targetHeight)
        {
            break;
        }
    }

    var topScore = 0L;
    for (var r = row - 1; r >= 0; r--)
    {
        topScore++;

        if (grid[r][col] >= targetHeight)
        {
            break;
        }
    }

    var bottomScore = 0L;
    for (var r = row + 1; r < totalRows; r++)
    {
        bottomScore++;

        if (grid[r][col] >= targetHeight)
        {
            break;
        }
    }

    return leftScore * rightScore * topScore * bottomScore;
}

long GetPart1()
{
    var visibleTrees = GetNumberOfTreesAroundTheEdge();

    for (var r = 1; r < totalRows - 1; r++)
    {
        for (var c = 1; c < totalCols - 1; c++)
        {
            if (IsVisible(r, c))
            {
                visibleTrees++;
            }
        }
    }

    return visibleTrees;
}

long GetPart2()
{
    var highestScenicScore = 0L;

    for (var row = 1; row < totalRows - 1; row++)
    {
        for (var col = 1; col < totalCols - 1; col++)
        {
            var scenicScore = GetScenicScore(row, col);
            
            if (scenicScore > highestScenicScore)
            {
                highestScenicScore = scenicScore;
            }
        }
    }

    return highestScenicScore;
}

Console.WriteLine($"Part 1: {GetPart1()}");
Console.WriteLine($"Part 2: {GetPart2()}");

Console.WriteLine("done");