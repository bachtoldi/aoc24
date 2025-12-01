using aoc._shared;

namespace aoc.day06;

public class Point(int x, int y)
{
    public int X => x;
    public int Y => y;

    public bool IsInGrid(char[][] grid) =>
        X >= 0 && X < grid[0].Length && Y >= 0 && Y < grid.Length;
}

internal interface IDirection
{
    char Symbol { get; }
    Point Move(Point start);
    IDirection Turn();
}

internal class Up : IDirection
{
    public char Symbol => '^';

    public Point Move(Point start) => new(start.X, start.Y - 1);

    public IDirection Turn() => new Right();
}

internal class Right : IDirection
{
    public char Symbol => '>';

    public Point Move(Point start) => new(start.X + 1, start.Y);

    public IDirection Turn() => new Down();
}

internal class Down : IDirection
{
    public char Symbol => 'v';

    public Point Move(Point start) => new(start.X, start.Y + 1);

    public IDirection Turn() => new Left();
}

internal class Left : IDirection
{
    public char Symbol => '<';

    public Point Move(Point start) => new(start.X - 1, start.Y);

    public IDirection Turn() => new Up();
}

internal record struct Move(int StartX, int StartY, int EndX, int EndY);

public static class Day06
{
    private const string Folder = "day06";
    private const string Ferret = "ferret";

    private const char Visited = 'X';
    private const char Obstacle = '#';

    public static string Execute()
    {
        var grid = new char[Folder.GetNumberOfLines()][];
        var lineIndex = 0;
        var startingPoint = new Point(0, 0);
        var startingDirection = new Up();

        Folder.Read(line =>
        {
            if (line.Contains(startingDirection.Symbol))
                startingPoint = new Point(line.IndexOf(startingDirection.Symbol), lineIndex);

            grid[lineIndex] = line.ToCharArray();
            lineIndex++;
        });

        Console.WriteLine(Ferret);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(grid, startingPoint, startingDirection),
            "2" => B(grid, startingPoint, startingDirection),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa),
        };
    }

    private static string A(char[][] grid, Point currentPoint, IDirection currentDirection)
    {
        while (currentPoint.IsInGrid(grid))
        {
            Move(grid, ref currentPoint, ref currentDirection);
            Draw(grid);
        }

        var totalSteps = grid.SelectMany(line => line).Count(character => character == Visited);

        return totalSteps.ToString();
    }

    private static void Move(char[][] grid, ref Point currentPoint, ref IDirection currentDirection)
    {
        grid[currentPoint.Y][currentPoint.X] = Visited;
        var nextPoint = currentDirection.Move(currentPoint);
        if (!nextPoint.IsInGrid(grid))
        {
            currentPoint = nextPoint;
            return;
        }

        if (grid[nextPoint.Y][nextPoint.X] == Obstacle)
        {
            currentDirection = currentDirection.Turn();
            Move(grid, ref currentPoint, ref currentDirection);
        }
        else
        {
            currentPoint = nextPoint;
            grid[currentPoint.Y][currentPoint.X] = currentDirection.Symbol;
        }
    }

    private static void Draw(char[][] grid, string filename = "output.txt")
    {
        var file = $@"{Folder}\{filename}";
        File.WriteAllText(file, string.Empty);
        using var writer = new StreamWriter(file);
        foreach (var line in grid)
        {
            writer.WriteLine(new string(line));
        }
    }

    private static string B(char[][] grid, Point startPoint, IDirection startDirection)
    {
        var startingGrid = Copy(grid);
        var currentPoint = startPoint;
        var currentDirection = startDirection;
        while (currentPoint.IsInGrid(grid))
        {
            Move(grid, ref currentPoint, ref currentDirection);
            Draw(grid);
        }

        List<Point> potentialObstacleLocations = [];
        for (var lineIndex = 0; lineIndex < grid.Length; lineIndex++)
        for (var columnIndex = 0; columnIndex < grid[lineIndex].Length; columnIndex++)
            if (grid[lineIndex][columnIndex] == Visited)
                potentialObstacleLocations.Add(new Point(columnIndex, lineIndex));

        var totalLoops = 0;
        foreach (var point in potentialObstacleLocations)
        {
            Draw(startingGrid, "startingGrid.txt");
            var potentialLoop = Copy(startingGrid);
            potentialLoop[point.Y][point.X] = Obstacle;
            Draw(potentialLoop, "potentialLoop.txt");
            if (IsLoop(potentialLoop, startPoint, startDirection))
                totalLoops++;
        }

        return totalLoops.ToString();
    }

    private static bool IsLoop(char[][] grid, Point currentPoint, IDirection currentDirection)
    {
        var moves = new HashSet<Move>();
        for (var i = 0; i < 10000; i++)
        {
            var start = currentPoint;
            Move(grid, ref currentPoint, ref currentDirection);
            if (!currentPoint.IsInGrid(grid))
                return false;

            var move = new Move(start.X, start.Y, currentPoint.X, currentPoint.Y);
            if (!moves.Add(move))
                return true;
        }

        return false;
    }

    private static char[][] Copy(char[][] grid)
    {
        var copy = new char[grid.Length][];
        for (var i = 0; i < grid.Length; i++)
            copy[i] = (grid[i].Clone() as char[])!;
        return copy;
    }
}
