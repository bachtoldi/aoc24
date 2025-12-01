using aoc._shared;
using aoc.day01;

var foo = new Dictionary<string, Func<string>> { { "1", Day01.Execute } };

var input = string.Empty;

while (input is not ("q" or "quit"))
{
    if (foo.TryGetValue(input, out var value))
    {
        Console.WriteLine(value());
    }
    else if (input is not "")
    {
        Console.WriteLine(InputReaderExtensions.Hayaa);
        break;
    }

    Console.WriteLine("get bent");
    input = Console.ReadLine() ?? string.Empty;
}
