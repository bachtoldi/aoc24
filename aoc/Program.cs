using aoc.day01;

var foo = new Dictionary<string, Func<Task<string>>> { { "1", Day01.Execute } };

var input = string.Empty;

while (input is not ("q" or "quit"))
{
    if (foo.TryGetValue(input, out var value))
    {
        Console.WriteLine(await value());
    }
    else if (input is not "")
    {
        Console.WriteLine("Hayaa what are you doing?");
        break;
    }

    Console.WriteLine("get bent");
    input = Console.ReadLine() ?? string.Empty;
}