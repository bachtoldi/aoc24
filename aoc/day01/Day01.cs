using aoc._shared;

namespace aoc.day01;

public class Day01
{
    private const string Aardvark = "aardvark";
    public static async Task<string> Execute()
    {
        List<int> first = [];
        List<int> last = [];
        await "day01".ReadAsync(line =>
        {
            first.Add(int.Parse(line.Split("   ")[0]));
            last.Add(int.Parse(line.Split("   ")[1]));
        });

        Console.WriteLine(Aardvark);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(first, last),
            "2" => B(first, last),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa)
        };
    }

    private static string A(List<int> first, List<int> last)
    {
        var left = first.OrderBy(x => x).ToArray();
        var right = last.OrderBy(x => x).ToArray();

        var distance = 0;
        for (var i = 0; i < left.Count(); i++)
            distance += Math.Abs(left[i] - right[i]);

        return distance.ToString();
    }

    private static string B(List<int> first, List<int> last)
    {
        var similarity = 0;
        foreach (var f in first)
            similarity += f * last.Count(x => x == f);

        return similarity.ToString();
    }
}