if (args.Length == 0)
{
    Console.WriteLine("Please provide a path to the solution root.");
    return;
}

var solutionRoot = args[0];
if (!Directory.Exists(solutionRoot))
{
    Console.WriteLine("The provided path does not exist.");
    return;
}

Console.WriteLine($"Cleaning object directories in: {solutionRoot}");

CleanObjectDirectories(solutionRoot);

static void CleanObjectDirectories(string directory)
{
    // Skip the .git directory.
    if (directory.ToLowerInvariant().EndsWith(".git"))
    {
        return;
    }

    var subDirectories = Directory.GetDirectories(directory);
    foreach (var subDirectory in subDirectories)
    {
        if (subDirectory.EndsWith("obj"))
        {
            DeleteDirectory(subDirectory);
        }
        else
        {
            // Recursion for the win.
            CleanObjectDirectories(subDirectory);
        }
    }
}

static void DeleteDirectory(string directory)
{
    try
    {
        Directory.Delete(directory, true);
        Console.WriteLine($"Deleted directory: {directory}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to delete directory: {directory}");
        Console.WriteLine(ex.Message);
    }
}
