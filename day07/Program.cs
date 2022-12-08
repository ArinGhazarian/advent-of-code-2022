var data = File.ReadAllLines("input.txt");

DirectoryEntry BuildTree()
{
    var root = new DirectoryEntry { Name = "/" };
    root.Parent = root;

    var currentDir = root;
    foreach (var output in data)
    {
        if (output.StartsWith("$"))
        {
            var commandSections = output.Split(" ");
            var cmd = commandSections[1];

            if (cmd == "ls")
            {
                continue;
            }

            // cmd = cd
            var arg = commandSections[2];
            currentDir = arg switch
            {
                ".." => currentDir!.Parent,
                "/" => root,
                _ => (DirectoryEntry)currentDir!.Contents.Single(c => c is DirectoryEntry && c.Name == arg)
            };
        }
        else
        {
            var listSections = output.Split(" ");
            var name = listSections[1];

            if (currentDir!.Contents.Any(c => c.Name == name))
            {
                continue;
            }

            FileSystemEntry entry = listSections[0] == "dir"
                ? new DirectoryEntry { Name = name, Parent = currentDir }
                : new FileEntry { Name = name, Size = long.Parse(listSections[0]) };
            currentDir.Contents.Add(entry);
        }
    }

    return root;
}

var tree = BuildTree();

var directories = tree.GetAllContentsRecursively()
    .Where(c => c is DirectoryEntry)
    .Cast<DirectoryEntry>()
    .ToArray();

long GetPart1()
{
    const long maxSize = 100000;

    var sum = directories
        .Where(dir => dir.TotalSize <= maxSize)
        .Sum(dir => dir.TotalSize);

    return sum;
}

long GetPart2()
{
    const long minSpaceNeeded = 30000000;
    const long totalSpaceAvailable = 70000000;

    var usedSpace = tree.TotalSize;
    var freeSpace = totalSpaceAvailable - usedSpace;
    var requiredExtraSpace = minSpaceNeeded - freeSpace;

    var smallestDirToDelete = directories
        .Where(dir => dir.TotalSize >= requiredExtraSpace)
        .OrderBy(dir => dir.TotalSize)
        .First();
    
    return smallestDirToDelete.TotalSize;
}

Console.WriteLine($"Part 1: {GetPart1()}");
Console.WriteLine($"Part 2: {GetPart2()}");

Console.WriteLine("done");

abstract class FileSystemEntry
{
    public string Name { get; init; } = "";
}

class FileEntry : FileSystemEntry
{
    public long Size { get; init; }

    public override string ToString() => Name;
}

class DirectoryEntry : FileSystemEntry
{
    private long? _totalSize;

    public DirectoryEntry? Parent { get; set; }

    public List<FileSystemEntry> Contents { get; } = new();

    public long TotalSize
    {
        get
        {
            _totalSize ??= GetTotalSize(this);
            return _totalSize.Value;
        }
    }

    private long GetTotalSize(DirectoryEntry root)
    {
        var size = 0L;
        
        foreach (var entry in root.Contents)
        {
            if (entry is FileEntry file)
            {
                size += file.Size;
            }
            else // entry is DirectoryEntry
            {
                size += GetTotalSize((DirectoryEntry)entry);
            }
        }

        return size;
    }

    public IEnumerable<FileSystemEntry> GetAllContentsRecursively() => GetAllContentsRecursively(this);

    private IEnumerable<FileSystemEntry> GetAllContentsRecursively(DirectoryEntry root)
    {
        yield return root;
        
        foreach (var content in root.Contents)
        {
            if (content is FileEntry)
            {
                yield return content;
                continue;
            }

            foreach (var subContent in GetAllContentsRecursively((DirectoryEntry)content))
            {
                yield return subContent;
            }
        }
    }

    public override string ToString() => $"[{Name}]";
}