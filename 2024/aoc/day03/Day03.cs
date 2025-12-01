using System.Text.RegularExpressions;
using aoc._shared;

namespace aoc.day03;

public static partial class Day03
{
    private const string Folder = "day03";
    private const string Capybara = "capybara";

    [GeneratedRegex(@"mul\(([0-9]{1,3})\,([0-9]{1,3})\)")]
    private static partial Regex RegexA();

    [GeneratedRegex(@"mul\(([0-9]{1,3})\,([0-9]{1,3})\)|(do\(\))|(don't\(\))")]
    private static partial Regex RegexB();

    public static string Execute()
    {
        Console.WriteLine(Capybara);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(),
            "2" => B(),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static string A()
    {
        List<Match> matches = [];
        Folder.Read(line => matches.AddRange(RegexA().Matches(line).ToList()));

        var total = 0;
        foreach (var match in matches)
        {
            var first = match.Groups[1].Value;
            var second = match.Groups[2].Value;
            total += Convert.ToInt32(first) * Convert.ToInt32(second);
        }

        return total.ToString();
    }

    private static string B()
    {
        List<Match> matches = [];
        Folder.Read(line => matches.AddRange(RegexB().Matches(line).ToList()));

        var total = 0;
        var enabled = true;
        foreach (var match in matches)
        {
            if (match.Groups[3].Value.Length > 0)
            {
                enabled = true;
                continue;
            }

            if (match.Groups[4].Value.Length > 0)
            {
                enabled = false;
                continue;
            }

            if (enabled)
            {
                var first = match.Groups[1].Value;
                var second = match.Groups[2].Value;
                total += Convert.ToInt32(first) * Convert.ToInt32(second);
            }
        }

        return total.ToString();
    }
}
