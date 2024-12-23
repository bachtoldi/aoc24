﻿using aoc._shared;
using aoc.day01;
using aoc.day02;
using aoc.day03;
using aoc.day04;

var foo = new Dictionary<string, Func<Task<string>>>
{
    { "1", Day01.Execute },
    { "2", Day02.Execute },
    { "3", Day03.Execute },
    { "4", Day04.Execute },
};

var input = string.Empty;

while (input is not ("q" or "quit"))
{
    if (foo.TryGetValue(input, out var value))
    {
        Console.WriteLine(await value());
    }
    else if (input is not "")
    {
        Console.WriteLine(InputReaderExtensions.Hayaa);
        break;
    }

    Console.WriteLine("get bent");
    input = Console.ReadLine() ?? string.Empty;
}
