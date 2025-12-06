using System.Globalization;
using aoc._shared;

namespace aoc.day06;

public class Day06
{
    private const string Folder = "day06";
    private const string Flycatcher = "flycatcher";

    public static string Execute()
    {
        List<List<string>> inputs = //[];
        [
            ["123", "328", " 51", "64 "],
            [" 45", "64 ", "387", "23 "],
            ["  6", "98 ", "215", "314"],
            ["*", "+", "*", "+"]
        ];

        List<string> lines = [];
        Folder.Read(line =>
        {
            lines.Add(line);
            inputs.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList());
        });

        Console.WriteLine(Flycatcher);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(inputs),
            "2" => B(lines),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static (List<List<decimal>> number, List<string> operators) TransformInputs(List<List<string>> inputs)
    {
        List<List<decimal>> numbers = [];
        for (var i = 0; i < inputs.Count - 1; i++)
        {
            numbers.Add(inputs[i].Select(decimal.Parse).ToList());
        }

        var operators = inputs.Last();
        return (numbers, operators);
    }

    private static string A(List<List<string>> inputs)
    {
        var (numbers, operators) = TransformInputs(inputs);
        List<decimal> results = [];
        for (var i = 0; i < operators.Count; i++)
        {
            var currentOperator = operators[i];

            switch (currentOperator)
            {
                case "+":
                {
                    var result = numbers.Sum(number => number[i]);
                    results.Add(result);
                    break;
                }
                case "*":
                {
                    var result = numbers.Aggregate(1m, (current, number) => current * number[i]);
                    results.Add(result);
                    break;
                }
            }
        }

        return results.Sum().ToString(CultureInfo.InvariantCulture);
    }

    private static string B(List<string> lines)
    {
        var inputs = TransformLines(lines);

        var operators = inputs.Last();
        var numbers = inputs[..^1];

        List<decimal> results = [];
        for (var i = 0; i < operators.Count; i++)
        {
            var currentOperator = operators[i].Trim();

            switch (currentOperator)
            {
                case "+":
                {
                    var currentNumbers = numbers.Select(number => number[i]).ToList();
                    var iterations = currentNumbers.Max(n => n.Length);

                    var flippedNumbers = new string[iterations];
                    for (var j = 0; j < iterations; j++)
                    {
                        foreach (var currentNumber in currentNumbers)
                        {
                            if (currentNumber.Length > j)
                            {
                                flippedNumbers[j] += currentNumber[j];
                            }
                        }
                    }

                    var result = flippedNumbers.Sum(decimal.Parse);
                    results.Add(result);
                    break;
                }
                case "*":
                {
                    var currentNumbers = numbers.Select(number => number[i]).ToList();
                    var iterations = currentNumbers.Max(n => n.Length);

                    var flippedNumbers = new string[iterations];
                    for (var j = 0; j < iterations; j++)
                    {
                        foreach (var currentNumber in currentNumbers)
                        {
                            if (currentNumber.Length > j)
                            {
                                flippedNumbers[j] += currentNumber[j];
                            }
                        }
                    }

                    var result = flippedNumbers.Aggregate(1m, (current, number) => current * decimal.Parse(number));
                    results.Add(result);
                    break;
                }
            }
        }

        return results.Sum().ToString(CultureInfo.InvariantCulture);
    }

    private static List<List<string>> TransformLines(List<string> lines)
    {
        var iterations = lines.Max(l => l.Length);
        var result = new List<List<string>>();
        for (var i = 0; i < lines.Count; i++)
        {
            result.Add([""]);
        }

        for (var i = 0; i < iterations; i++)
        {
            var split = lines.All(line => line[i] == ' ');
            for (var j = 0; j < lines.Count; j++)
            {
                if (split)
                {
                    result[j].Add("");
                }
                else
                {
                    result[j][result[j].Count - 1] += lines[j][i];
                }
            }
        }

        return result;
    }
}