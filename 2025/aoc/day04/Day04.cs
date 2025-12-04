using aoc._shared;

namespace aoc.day04;

public class Day04
{
    private const string Folder = "day04";
    private const string Dickcissel = "dickcissel";

    public static string Execute()
    {
        char[,] inputs = { };
        var row = 0;
        Folder.Read(line =>
        {
            if (inputs.Length == 0)
            {
                inputs = new char[line.Length, line.Length];
            }
        
            for (var column = 0; column < line.Length; column++)
            {
                inputs[row, column] = line[column];
            }
        
            row++;
        });

        Console.WriteLine(Dickcissel);
        var input = Console.ReadLine();
        return input switch
        {
            "1" => A(inputs),
            "2" => B(inputs),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static string A(char[,] inputs)
    {
        var sum = 0;
        var height = inputs.GetLength(0);
        var width = inputs.GetLength(1);
        for (var row = 0; row < height; row++)
        {
            for (var column = 0; column < width; column++)
            {
                if (inputs[row, column] == '@')
                {
                    var neighbors = CountNeighbors(inputs, '@', row, column);
                    if (neighbors < 4)
                    {
                        sum++;
                    }
                }
            }
        }

        return sum.ToString();
    }

    private static string B(char[,] inputs)
    {
        int count;
        var sum = 0;
        var height = inputs.GetLength(0);
        var width = inputs.GetLength(1);
        do
        {
            count = 0;
            for (var row = 0; row < height; row++)
            {
                for (var column = 0; column < width; column++)
                {
                    if (inputs[row, column] == '@')
                    {
                        var neighbors = CountNeighbors(inputs, '@', row, column);
                        if (neighbors < 4)
                        {
                            inputs[row, column] = 'x';
                            count++;
                        }
                    }
                }
            }

            sum += count;
        } while (count > 0);

        return sum.ToString();
    }

    private static int CountNeighbors(char[,] inputs, char searchTarget, int row, int column)
    {
        const char empty = '.';
        char[] neighbors =
        [
            AccessField(inputs, row - 1, column - 1) ?? empty,
            AccessField(inputs, row - 1, column) ?? empty,
            AccessField(inputs, row - 1, column + 1) ?? empty,
            AccessField(inputs, row, column - 1) ?? empty,
            AccessField(inputs, row, column + 1) ?? empty,
            AccessField(inputs, row + 1, column - 1) ?? empty,
            AccessField(inputs, row + 1, column) ?? empty,
            AccessField(inputs, row + 1, column + 1) ?? empty
        ];

        return neighbors.Count(n => n == searchTarget);
    }

    private static char? AccessField(char[,] inputs, int row, int column)
    {
        if (row < 0) return null;
        if (row >= inputs.GetLength(0)) return null;
        if (column < 0) return null;
        if (column >= inputs.GetLength(1)) return null;
        return inputs[row, column];
    }
}