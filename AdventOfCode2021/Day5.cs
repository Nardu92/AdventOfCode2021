using System.Drawing;

public class Day5
{
    //Read a file line by line 
    public static List<VentSegment> ReadInputFile(string filename)
    {
        string[] lines = System.IO.File.ReadAllLines(filename);
        return lines.Select(x => x.Replace(">", "").Split('-', StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => new VentSegment(x.First(), x.Last())).ToList();
    }

    public static long Sol1()
    {
        var segments = ReadInputFile("Inputs/input5.txt").Where(x => x.IsVertical || x.IsHorizontal).ToList();
        var maxX = Math.Max(segments.Max(x => x.EndPoint.X), segments.Max(x => x.StartPoint.X));
        var maxY = Math.Max(segments.Max(x => x.EndPoint.Y), segments.Max(x => x.StartPoint.Y));
        var ventPlot = new VentPlot(new Point(maxX, maxY), segments);
        ventPlot.PlotVents();
        //ventPlot.ToConsole();
        
        return ventPlot.Count(2);
    }

    public static long Sol2()
    {
        return 0;
    }
}

public class VentPlot
{

    public List<VentSegment> Segments { get; set; }

    public int[,] Grid { get; set; }

    public VentPlot(Point size, List<VentSegment> segments)
    {
        Grid = new int[size.Y + 1, size.X + 1];
        Segments = segments;
    }

    public void PlotVents()
    {
        foreach (var segment in Segments)
        {
            if (segment.IsVertical)
            {
                if (segment.StartPoint.Y < segment.EndPoint.Y)
                {
                    for (int i = segment.StartPoint.Y; i <= segment.EndPoint.Y; i++)
                    {
                        Grid[i, segment.StartPoint.X]++;
                    }
                }
                else
                {
                    for (int i = segment.EndPoint.Y; i <= segment.StartPoint.Y; i++)
                    {
                        Grid[i, segment.StartPoint.X]++;
                    }
                }
            }
            else if (segment.IsHorizontal)
            {
                if (segment.StartPoint.X < segment.EndPoint.X)
                {
                    for (int i = segment.StartPoint.X; i <= segment.EndPoint.X; i++)
                    {
                        Grid[segment.StartPoint.Y, i]++;
                    }
                }
                else
                {
                    for (int i = segment.EndPoint.X; i <= segment.StartPoint.X; i++)
                    {
                        Grid[segment.StartPoint.Y, i]++;
                    }
                }
            }
        }
    }

    public int Count(int threshold)
    {
        int count = 0;
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                if (Grid[i, j] >= threshold)
                {
                    count++;
                }
            }
        }
        return count;
    }
    public void ToConsole()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Console.Write($"{Grid[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}

public class VentSegment
{
    public Point StartPoint;
    public Point EndPoint;

    public VentSegment(string startPoint, string endPoint)
    {
        var scoord = startPoint.Split(',');
        StartPoint = new Point(Convert.ToInt32(scoord[0]), Convert.ToInt32(scoord[1]));

        var scoordEnd = endPoint.Split(',');
        EndPoint = new Point(Convert.ToInt32(scoordEnd[0]), Convert.ToInt32(scoordEnd[1]));
    }

    public bool IsVertical => this.StartPoint.X == this.EndPoint.X;
    public bool IsHorizontal => this.StartPoint.Y == this.EndPoint.Y;
}