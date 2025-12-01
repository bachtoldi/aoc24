using aoc._shared;

namespace aoc.day08;

public class Day08
{
    private const string Folder = "day08";
    private const string Hedgehog = "hedgehog";

    public static string Execute()
    {
        var grid = new char[Folder.GetNumberOfLines()][];
        var lineIndex = 0;

        Folder.Read(line =>
        {
            grid[lineIndex] = line.ToCharArray();
            lineIndex++;
        });

        Console.WriteLine(Hedgehog);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(grid),
            "2" => B(grid),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static string A(char[][] grid) => throw new NotImplementedException();

    private static string B(char[][] grid) => throw new NotImplementedException();
}
