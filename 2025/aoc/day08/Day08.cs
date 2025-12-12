using System.Globalization;
using aoc._shared;

namespace aoc.day08;

public class Day08
{
    private const string Folder = "day08";
    private const string Herring = "herring";

    public static string Execute()
    {
        List<string> lines = [];
        // [
        //     "162,817,812",
        //     "57,618,57",
        //     "906,360,560",
        //     "592,479,940",
        //     "352,342,300",
        //     "466,668,158",
        //     "542,29,236",
        //     "431,825,988",
        //     "739,650,466",
        //     "52,470,668",
        //     "216,146,977",
        //     "819,987,18",
        //     "117,168,530",
        //     "805,96,715",
        //     "346,949,466",
        //     "970,615,88",
        //     "941,993,340",
        //     "862,61,35",
        //     "984,92,344",
        //     "425,690,689"
        // ];
        Folder.Read(line => lines.Add(line));

        var points = TransformInput(lines);

        Console.WriteLine(Herring);
        var input = Console.ReadLine();

        return input switch
        {
            "1" => A(points),
            "2" => B(),
            _ => throw new AggregateException(InputReaderExtensions.Hayaa)
        };
    }

    private static List<Point> TransformInput(List<string> input)
    {
        List<Point> points = [];
        foreach (var line in input)
        {
            var numbers = line.Split(",").Select(double.Parse).ToArray();
            points.Add(new Point(numbers[0], numbers[1], numbers[2]));
        }

        return points;
    }

    private static List<Edge> CalculateEdges(List<Point> points)
    {
        List<Edge> edges = [];
        for (var i = 0; i < points.Count; i++)
        {
            for (var j = i + 1; j < points.Count; j++)
            {
                if (i == j) continue;
                edges.Add(new Edge(points[i], points[j]));
            }
        }

        return edges;
    }

    private static List<Circuit> CalculateCircuits(List<Edge> edges)
    {
        List<Circuit> circuits = [];
        foreach (var edge in edges)
        {
            List<Circuit> adjoined = [];
            foreach (var circuit in circuits)
            {
                if (circuit.Contains(edge))
                {
                    adjoined.Add(circuit);
                }
            }

            switch (adjoined.Count)
            {
                case 0:
                    circuits.Add(new Circuit(edge));
                    break;
                case 1:
                    adjoined[0].Add(edge);
                    break;
                default:
                    for (var i = 1; i < adjoined.Count; i++)
                    {
                        adjoined[0].Join(adjoined[i]);
                        circuits.Remove(adjoined[i]);
                    }

                    break;
            }
        }

        return circuits;
    }

    private static string A(List<Point> points)
    {
        var edges = CalculateEdges(points).OrderBy(e => e.Length).Take(1000).ToList();
        var circuits = CalculateCircuits(edges).OrderByDescending(c => c.Size).Take(3);
        var sum = circuits.Aggregate(1d, (current, circuit) => current * circuit.Size);
        return sum.ToString(CultureInfo.InvariantCulture);
    }

    private static string B(List<Point> points)
    {
        var edges = CalculateEdges(points).ToList();
        
        return "bar";
    }

    private readonly record struct Point(double X, double Y, double Z);

    private readonly struct Edge(Point start, Point end)
    {
        public double Length { get; } = Math.Sqrt(
            Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2) + Math.Pow(start.Z - end.Z, 2)
        );

        public Point Start { get; } = start;
        public Point End { get; } = end;
    }

    private readonly struct Circuit
    {
        public double Size => _points.Count;
        private readonly List<Point> _points;

        public Circuit(Edge edge)
        {
            _points = [edge.Start, edge.End];
        }

        public bool Contains(Edge edge) => _points.Contains(edge.Start) || _points.Contains(edge.End);

        public void Add(Edge edge)
        {
            if (!_points.Contains(edge.Start))
            {
                _points.Add(edge.Start);
            }

            if (!_points.Contains(edge.End))
            {
                _points.Add(edge.End);
            }
        }

        public void Join(Circuit circuit)
        {
            foreach (var point in circuit._points)
            {
                if (!_points.Contains(point))
                {
                    _points.Add(point);
                }
            }
        }
    }
}