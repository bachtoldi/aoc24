namespace aoc._shared;

public static class InputReaderExtensions
{
    public const string Hayaa = "Hayaa what are you doing?";

    public static void Read(this string folder, Action<string> action)
    {
        var lines = File.ReadLines($@"{folder}\input.txt");
        foreach (var line in lines)
            action(line);
    }

    public static int GetNumberOfLines(this string folder) =>
        File.ReadLines(@$"{folder}\input.txt").Count();
}
