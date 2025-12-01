using aoc._shared;

namespace aoc.day07;

public static class Day07
{
    private const string Folder = "day07";
    private const string Gopher = "gopher";

    private interface IOperation
    {
        long Apply();
    }

    private class Number(long number) : IOperation
    {
        public long Apply() => number;
    }

    private class Addition(long summand, IOperation previous) : IOperation
    {
        public long Apply() => previous.Apply() + summand;
    }

    private class Multiplication(long multiplicand, IOperation previous) : IOperation
    {
        public long Apply() => previous.Apply() * multiplicand;
    }

    private class Concatenation(long factor, IOperation previous) : IOperation
    {
        public long Apply() => long.Parse($"{previous.Apply()}{factor}");
    }

    public static string Execute()
    {
        var equations = new List<(long, List<long>)>();
        Folder.Read(line =>
        {
            line = line.Replace(":", "");
            var split = line.Split(" ");
            equations.Add((long.Parse(split.First()), split.Skip(1).Select(long.Parse).ToList()));
        });

        Console.WriteLine(Gopher);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(equations),
            "2" => B(equations),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static string A(List<(long result, List<long> factors)> equations)
    {
        var total = 0L;
        foreach (var equation in equations)
        {
            var (result, factors) = equation;
            if (CheckEquation(result, factors))
                total += result;
        }

        return total.ToString();
    }

    private static bool CheckEquation(long result, List<long> factors)
    {
        List<IOperation> operations =
        [
            new Addition(factors[1], new Number(factors[0])),
            new Multiplication(factors[1], new Number(factors[0])),
        ];

        for (var i = 2; i < factors.Count; i++)
        {
            List<IOperation> newOperations = [];
            foreach (var operation in operations)
            {
                newOperations.Add(new Addition(factors[i], operation));
                newOperations.Add(new Multiplication(factors[i], operation));
            }

            operations = newOperations;
        }

        return operations.Any(operation => operation.Apply() == result);
    }

    private static string B(List<(long, List<long>)> equations)
    {
        var total = 0L;
        foreach (var equation in equations)
        {
            var (result, factors) = equation;
            if (CheckEquationAgain(result, factors))
                total += result;
        }

        return total.ToString();
    }

    private static bool CheckEquationAgain(long result, List<long> factors)
    {
        List<IOperation> operations =
        [
            new Addition(factors[1], new Number(factors[0])),
            new Multiplication(factors[1], new Number(factors[0])),
            new Concatenation(factors[1], new Number(factors[0])),
        ];

        for (var i = 2; i < factors.Count; i++)
        {
            List<IOperation> newOperations = [];
            foreach (var operation in operations)
            {
                newOperations.Add(new Addition(factors[i], operation));
                newOperations.Add(new Multiplication(factors[i], operation));
                newOperations.Add(new Concatenation(factors[i], operation));
            }

            operations = newOperations;
        }

        return operations.Any(operation => operation.Apply() == result);
    }
}
