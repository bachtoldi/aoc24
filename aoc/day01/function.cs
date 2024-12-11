namespace aoc.day01;

public abstract class Day01
{
    public static async Task<string> Execute()
    {
        Console.WriteLine("aardvark");
        var input = Console.ReadLine();

        List<int> first = [];
        List<int> last = [];
        var stream = new StreamReader("day01\\input.txt");
        while (!stream.EndOfStream)
        {
            var line = await stream.ReadLineAsync();
            first.Add(int.Parse(line!.Split("   ")[0]));
            last.Add(int.Parse(line.Split("   ")[1]));
        }

        return input switch
        {
            "1" => A(first, last),
            "2" => B(first, last),
            _ => throw new AggregateException("invalid input")
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