using System.Globalization;
using System.Text;
using aoc._shared;

namespace aoc.day07;

public class Day07
{
    private const string Folder = "day07";
    private const string Grunion = "grunion";

    public static string Execute()
    {
        List<string> lines = [];
        // [
        //     ".......S.......",
        //     "...............",
        //     ".......^.......",
        //     "...............",
        //     "......^.^......",
        //     "...............",
        //     ".....^.^.^.....",
        //     "...............",
        //     "....^.^...^....",
        //     "...............",
        //     "...^.^...^.^...",
        //     "...............",
        //     "..^...^.....^..",
        //     "...............",
        //     ".^.^.^.^.^...^.",
        //     "..............."
        // ];

        Folder.Read(line => { lines.Add(line); });

        Console.WriteLine(Grunion);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(lines),
            "2" => B(lines),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static string A(List<string> lines)
    {
        List<int> beams = [lines[0].IndexOf('S')];

        var counter = 0;
        foreach (var line in lines)
        {
            var sb = new StringBuilder(line);
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] != '^') continue;
                if (!beams.Contains(i)) continue;

                counter++;
                beams.RemoveAll(b => b == i);
                beams.Add(i - 1);
                beams.Add(i + 1);

                sb[i - 1] = '|';
                sb[i + 1] = '|';
            }

            Console.WriteLine(sb.ToString());
        }

        return counter.ToString();
    }

    private static string B(List<string> lines)
    {
        var grid = new decimal[lines.Count, lines.First().Length];
        var beam = lines.First().IndexOf('S');
        grid[0, beam] = 1;
        
        for (var i = 1; i < lines.Count; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++)
            {
                switch (line[j])
                {
                    case '^':
                        grid[i, j - 1] += grid[i - 1, j];
                        grid[i, j + 1] += grid[i - 1, j];
                        break;
                    default:
                        grid[i, j] += grid[i - 1, j];
                        break;
                }
                Console.WriteLine($"Line {i} processed.");
            }
        }

        var sum = 0m;
        for (var i = 0; i < grid.GetLength(1); i++)
        {
            sum += grid[grid.GetLength(0) - 1, i];
        }

        return sum.ToString(CultureInfo.InvariantCulture);
    }
}