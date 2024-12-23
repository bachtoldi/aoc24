using aoc._shared;

namespace aoc.day02;

public static class Day02
{
    private const string Badger = "badger";

    public static async Task<string> Execute()
    {
        List<string[]> levels = [];
        await "day02".ReadAsync(line =>
        {
            levels.Add(line.Split(" "));
        });

        Console.WriteLine(Badger);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(levels),
            "2" => B(levels),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static string A(List<string[]> levels)
    {
        var safe = 0;
        foreach (var level in levels)
        {
            var diff = new int[level.Length - 1];
            for (var i = 0; i < level.Length - 1; i++)
                diff[i] = int.Parse(level[i]) - int.Parse(level[i + 1]);

            safe += Convert.ToInt32(
                diff.All(x => x is > -4 and < 0) || diff.All(x => x is < 4 and > 0)
            );
        }

        return safe.ToString();
    }

    private static string B(List<string[]> levels)
    {
        var safe = 0;

        foreach (var level in levels)
        {
            var localSafe = false;
            for (var levelIndex = 0; levelIndex < level.Length; levelIndex++)
            {
                var dampenedLevel = level.ToList();
                dampenedLevel.RemoveAt(levelIndex);

                var diff = new int[dampenedLevel.Count - 1];
                for (var diffIndex = 0; diffIndex < dampenedLevel.Count - 1; diffIndex++)
                    diff[diffIndex] =
                        int.Parse(dampenedLevel[diffIndex])
                        - int.Parse(dampenedLevel[diffIndex + 1]);

                localSafe |= diff.All(x => x is > -4 and < 0) || diff.All(x => x is < 4 and > 0);
            }

            safe += Convert.ToInt32(localSafe);
        }

        return safe.ToString();
    }
}
