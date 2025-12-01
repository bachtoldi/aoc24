using System.Text.RegularExpressions;
using aoc._shared;

namespace aoc.day05;

public static partial class Day05
{
    private const string Folder = "day05";
    private const string Egret = "egret";

    [GeneratedRegex(@"([0-9]{2})\|([0-9]{2})")]
    private static partial Regex Regex();

    public static string Execute()
    {
        Dictionary<int, List<int>> rules = [];
        List<int[]> updates = [];
        Folder.Read(line =>
        {
            switch (line.Length)
            {
                case 5:
                    var match = Regex().Match(line);
                    var first = int.Parse(match.Groups[1].Value);
                    var second = int.Parse(match.Groups[2].Value);
                    if (rules.ContainsKey(first))
                        rules[first].Add(second);
                    else
                        rules[first] = [second];
                    break;
                case > 5:
                    var numbers = line.Split(',');
                    updates.Add(numbers.Select(int.Parse).ToArray());
                    break;
            }
        });

        Console.WriteLine(Egret);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(rules, updates),
            "2" => B(rules, updates),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static string A(Dictionary<int, List<int>> rules, List<int[]> updates) =>
        updates
            .Where(update => IsInOrder(update, rules))
            .Sum(update => update.ElementAt(Middle(update.Length)))
            .ToString();

    private static bool IsInOrder(int[] update, Dictionary<int, List<int>> rules)
    {
        for (var index = 0; index < update.Length; index++)
        {
            rules.TryGetValue(update[index], out var numberRules);
            if (numberRules == null)
                continue;

            for (var beforeIndex = 0; beforeIndex < index; beforeIndex++)
            {
                if (numberRules.Contains(update[beforeIndex]))
                    return false;
            }
        }

        return true;
    }

    private static int Middle(int count) => (int)Math.Floor(count / 2m);

    private static string B(Dictionary<int, List<int>> rules, List<int[]> updates) =>
        updates
            .Where(update => !IsInOrder(update, rules))
            .Sum(update => Order(update, rules).ElementAt(Middle(update.Length)))
            .ToString();

    private static int[] Order(int[] update, Dictionary<int, List<int>> rules)
    {
        List<int> sorted = [update[0]];
        for (var index = 1; index < update.Length; index++)
        {
            rules.TryGetValue(update[index], out var numberRules);
            if (numberRules == null)
                continue;

            for (var sortedIndex = sorted.Count - 1; sortedIndex >= 0; sortedIndex--)
            {
                if (!numberRules.Contains(sorted.ElementAt(sortedIndex)))
                {
                    sorted.Insert(sortedIndex + 1, update[index]);
                    sortedIndex = 0;
                    continue;
                }

                if (sortedIndex == 0)
                    sorted.Insert(sortedIndex, update[index]);
            }
        }

        return sorted.ToArray();
    }
}
