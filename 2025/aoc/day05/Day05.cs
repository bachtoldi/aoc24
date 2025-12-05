using System.Globalization;
using aoc._shared;

namespace aoc.day05;

public class Day05
{
    private const string Folder = "day05";
    private const string Escolar = "escolar";

    public static string Execute()
    {
        // List<string> ranges = ["3-5", "10-14", "16-20", "12-18"];
        // List<decimal> available = [1, 5, 8, 11, 17, 32];

        List<string> ranges = [];
        List<decimal> available = [];

        Folder.Read(line =>
        {
            if (line.Contains('-'))
            {
                ranges.Add(line);
            }
            else if (string.IsNullOrWhiteSpace(line))
            {
            }
            else
            {
                available.Add(decimal.Parse(line));
            }
        });

        var fresh = RangesToInt(ranges);

        Console.WriteLine(Escolar);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(fresh, available),
            "2" => B(fresh),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static List<(decimal, decimal)> RangesToInt(List<string> ranges)
    {
        List<(decimal, decimal)> fresh = [];
        foreach (var range in ranges)
        {
            var split = range.Split('-');
            var first = decimal.Parse(split[0]);
            var second = decimal.Parse(split[1]);
            fresh.Add((first, second));
        }

        return fresh;
    }

    private static string A(List<(decimal, decimal)> fresh, List<decimal> available)
    {
        var count = 0;
        foreach (var a in available)
        {
            foreach (var (min, max) in fresh)
            {
                if (a >= min && a <= max)
                {
                    count++;
                    break;
                }
            }
        }

        return count.ToString();
    }

    private static string B(List<(decimal, decimal)> fresh)
    {
        int count;
        do
        {
            count = fresh.Count;
            fresh = Aggregate(fresh);
        } while (count != fresh.Count);

        var sum = 0m;
        foreach (var (min, max) in fresh)
        {
            sum += max - min + 1;
        }

        return sum.ToString(CultureInfo.InvariantCulture);
    }

    private static List<(decimal, decimal)> Aggregate(List<(decimal, decimal)> fresh)
    {
        List<(decimal, decimal)> aggregated = [];
        foreach (var (min, max) in fresh)
        {
            var added = false;
            for (var i = 0; i < aggregated.Count; i++)
            {
                var (aMin, aMax) = aggregated[i];
                if (min >= aMin && min <= aMax)
                {
                    added = true;
                    if (max > aMax)
                    {
                        aggregated[i] = (aMin, max);
                    }

                    break;
                }

                if (max >= aMin && max <= aMax)
                {
                    added = true;
                    if (min < aMin)
                    {
                        aggregated[i] = (min, aMax);
                        break;
                    }
                }

                if (min < aMin && max > aMax)
                {
                    added = true;
                    aggregated[i] = (min, max);
                    break;
                }
            }

            if (!added)
            {
                aggregated.Add((min, max));
            }
        }

        return aggregated;
    }
}