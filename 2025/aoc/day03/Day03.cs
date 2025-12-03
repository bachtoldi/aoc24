using System.Globalization;
using aoc._shared;

namespace aoc.day03;

public class Day03
{
    private const string Folder = "day03";
    private const string Cichlid = "cichlid";

    public static string Execute()
    {
        List<string> inputs = [];
        Folder.Read(line =>
        {
            inputs.Add(line);
        });

        Console.WriteLine(Cichlid);
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
            var one = 0;
            var two = 0;
            for (var i = 0; i < input.Length - 1; i++)
            {
                var digit = int.Parse(input[i].ToString());
                if (digit > one)
                {
                    one = digit;
                    two = 0;
                    continue;
                }

                if (digit > two)
                {
                    two = digit;
                }
            }

            var lastDigit = int.Parse(input[^1].ToString());
            if (lastDigit > two)
            {
                two = lastDigit;
            }

            sum += one * 10 + two;
        }

        return sum.ToString(CultureInfo.InvariantCulture);
    }

    private static string B(List<string> inputs)
    {
        var sum = 0m;
        foreach (var input in inputs)
        {
            int[] numbers = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            for (var i = 0; i < input.Length - 11; i++)
            {
                var digit = int.Parse(input[i].ToString());
                for (var j = 0; j < 12; j++)
                {
                    if (CheckAndSet(digit, numbers, j))
                    {
                        break;
                    }
                }
            }

            for (var i = 1; i < 12; i++)
            {
                var digit = int.Parse(input[^(12 - i)].ToString());
                for (var j = i; j < 12; j++)
                {
                    if (CheckAndSet(digit, numbers, j))
                    {
                        break;
                    }
                }
            }

            var number = 0m;
            for (var i = 0; i < 12; i++)
            {
                number += (decimal)(Math.Pow(10, 11 - i) * numbers[i]);
            }

            sum += number;
        }

        return sum.ToString(CultureInfo.InvariantCulture);
    }

    private static bool CheckAndSet(int digit, int[] numbers, int index)
    {
        if (digit <= numbers[index]) return false;
        numbers[index] = digit;
        for (var i = index + 1; i < numbers.Length; i++)
        {
            numbers[i] = 0;
        }

        return true;
    }
}