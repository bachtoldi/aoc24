using aoc._shared;

namespace aoc.day01;

public static class Day01
{
    private const string Folder = "day01";
    private const string Archerfish = "archerfish";

    public static string Execute()
    {
        List<int> inputs = [];
        Folder.Read(line =>
        {
            var sign = line[0] is 'L' ? -1 : 1;
            inputs.Add(int.Parse(line[1..]) * sign);
        });

        Console.WriteLine(Archerfish);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(inputs),
            "2" => B(inputs),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static string A(List<int> inputs)
    {
        var counter = 0;
        var value = 50;
        foreach (var input in inputs)
        {
            value = (value + input) % 100;
            if (value is 0)
                counter++;
        }

        return counter.ToString();
    }

    private static string B(List<int> inputs)
    {
        var counter = 0;
        var value = 50;
        foreach (var input in inputs)
        {
            for (var i = 1; i <= Math.Abs(input); i++)
            {
                if ((value + Math.Sign(input) * i) % 100 == 0)
                {
                    counter++;
                }
            }
            value = (value + input) % 100;
        }

        return counter.ToString();
    }
}
