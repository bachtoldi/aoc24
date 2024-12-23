using aoc._shared;

namespace aoc.day04;

public static class Day04
{
    private const string Dugong = "dugong";

    public static async Task<string> Execute()
    {
        var grid = new char[140][];
        var lineIndex = 0;
        await "day04".ReadAsync(line =>
        {
            grid[lineIndex] = line.ToCharArray();
            lineIndex++;
        });

        Console.WriteLine(Dugong);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(grid),
            "2" => B(grid),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static string A(char[][] grid)
    {
        var total = 0;
        for (var y = 0; y < 140; y++)
        {
            for (var x = 0; x < 140; x++)
            {
                if (grid[y][x] == 'X')
                    total +=
                        N(grid, y, x)
                        + N_E(grid, y, x)
                        + E(grid, y, x)
                        + S_E(grid, y, x)
                        + S(grid, y, x)
                        + S_W(grid, y, x)
                        + W(grid, y, x)
                        + N_W(grid, y, x);
            }
        }

        return total.ToString();
    }

    private static int N(char[][] grid, int y, int x) =>
        Walk(grid, (y - 1, x), (y - 2, x), (y - 3, x));

    private static int N_E(char[][] grid, int y, int x) =>
        Walk(grid, (y - 1, x + 1), (y - 2, x + 2), (y - 3, x + 3));

    private static int E(char[][] grid, int y, int x) =>
        Walk(grid, (y, x + 1), (y, x + 2), (y, x + 3));

    private static int S_E(char[][] grid, int y, int x) =>
        Walk(grid, (y + 1, x + 1), (y + 2, x + 2), (y + 3, x + 3));

    private static int S(char[][] grid, int y, int x) =>
        Walk(grid, (y + 1, x), (y + 2, x), (y + 3, x));

    private static int S_W(char[][] grid, int y, int x) =>
        Walk(grid, (y + 1, x - 1), (y + 2, x - 2), (y + 3, x - 3));

    private static int W(char[][] grid, int y, int x) =>
        Walk(grid, (y, x - 1), (y, x - 2), (y, x - 3));

    private static int N_W(char[][] grid, int y, int x) =>
        Walk(grid, (y - 1, x - 1), (y - 2, x - 2), (y - 3, x - 3));

    private static int Walk(char[][] grid, (int y, int x) m, (int y, int x) a, (int y, int x) s)
    {
        if (
            m.y < 0
            || m.y >= 140
            || m.x < 0
            || m.x >= 140
            || a.y < 0
            || a.y >= 140
            || a.x < 0
            || a.x >= 140
            || s.y < 0
            || s.y >= 140
            || s.x < 0
            || s.x >= 140
        )
            return 0;
        if (grid[m.y][m.x] != 'M')
            return 0;
        if (grid[a.y][a.x] != 'A')
            return 0;
        if (grid[s.y][s.x] != 'S')
            return 0;
        return 1;
    }

    private static string B(char[][] grid)
    {
        var total = 0;
        for (var y = 0; y < 140; y++)
        {
            for (var x = 0; x < 140; x++)
            {
                if (grid[y][x] == 'A')
                {
                    var firstDiagonal =
                        Walk(grid, (y - 1, x - 1), (y, x), (y + 1, x + 1))
                        + Walk(grid, (y + 1, x + 1), (y, x), (y - 1, x - 1));
                    var secondDiagonal =
                        Walk(grid, (y - 1, x + 1), (y, x), (y + 1, x - 1))
                        + Walk(grid, (y + 1, x - 1), (y, x), (y - 1, x + 1));
                    if (firstDiagonal == 1 && secondDiagonal == 1)
                        total += 1;
                }
            }
        }

        return total.ToString();
    }
}
