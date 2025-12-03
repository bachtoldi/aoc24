using System.Globalization;
using aoc._shared;

namespace aoc.day02;

public class Day02
{
    private const string Folder = "day02";
    private const string Bowfin = "bowfin";

    public static string Execute()
    {
        List<string> inputs = [];
        Folder.Read(line =>
        {
            inputs.AddRange(line.Split(","));
        });

        Console.WriteLine(Bowfin);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(inputs),
            "2" => B(inputs),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static string A(List<string> inputs)
    {
        var sum = 0m;
        foreach (var input in inputs)
        {
            var from = decimal.Parse(input[..input.IndexOf('-')]);
            var to = decimal.Parse(input[(input.IndexOf('-') + 1)..]);
            for (var i = from; i <= to; i++)
            {
                var number = i.ToString(CultureInfo.InvariantCulture);
                if (number[..(number.Length / 2)] == number[(number.Length / 2)..])
                {
                    sum += i;
                }
            }
        }

        return sum.ToString(CultureInfo.InvariantCulture);
    }

    private static string B(List<string> inputs)
    {
        var sum = 0m;
        foreach (var input in inputs)
        {
            var from = decimal.Parse(input[..input.IndexOf('-')]);
            var to = decimal.Parse(input[(input.IndexOf('-') + 1)..]);
            for (var i = from; i <= to; i++)
            {
                var number = i.ToString(CultureInfo.InvariantCulture);
                for (var j = 1; j <= number.Length / 2; j++)
                {
                    var substring = number[..j];
                    if (number.Replace(substring, "") == string.Empty)
                    {
                        sum += i;
                        break;
                    }
                }
            }
        }

        return sum.ToString(CultureInfo.InvariantCulture);
    }
}