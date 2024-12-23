namespace aoc._shared;

public static class InputReaderExtensions
{
    public const string Hayaa = "Hayaa what are you doing?";
    public static async Task ReadAsync(this string folder, Action<string> action)
    {
        var stream = new StreamReader($"{folder}\\input.txt");
        while (!stream.EndOfStream)
        {
            var line = await stream.ReadLineAsync();
            if (line != null) action(line);
        }
    }
}