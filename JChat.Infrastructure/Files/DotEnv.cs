namespace JChat.Infrastructure.Files;

public static class DotEnv
{
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"{filePath} does not exist!");
            return;
        }

        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split('=', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (parts.Length != 2 || parts[0][0] == '#')
                continue;

            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}