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
        return ventPlot.Count(2);
    }

    public static long Sol2()
    {
        var segments = ReadInputFile("Inputs/input5.txt");
        var maxX = Math.Max(segments.Max(x => x.EndPoint.X), segments.Max(x => x.StartPoint.X));
        var maxY = Math.Max(segments.Max(x => x.EndPoint.Y), segments.Max(x => x.StartPoint.Y));
        var ventPlot = new VentPlot(new Point(maxX, maxY), segments);
        ventPlot.PlotVents();
        return ventPlot.Count(2);
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
                for (int i = segment.StartPoint.Y; i <= segment.EndPoint.Y; i++)
                {
                    Grid[i, segment.StartPoint.X]++;
                }
            }
            else if (segment.IsHorizontal)
            {
                for (int i = segment.StartPoint.X; i <= segment.EndPoint.X; i++)
                {
                    Grid[segment.StartPoint.Y, i]++;
                }
            }
            else
            {
                if (segment.StartPoint.Y < segment.EndPoint.Y)
                {
                    for (int i = 0; i <= segment.EndPoint.Y - segment.StartPoint.Y; i++)
                    {
                        Grid[segment.StartPoint.Y + i, segment.StartPoint.X + i]++;
                    }
                }
                else
                {
                    for (int i = 0; i <= segment.StartPoint.Y - segment.EndPoint.Y; i++)
                    {
                        Grid[segment.StartPoint.Y - i, segment.StartPoint.X + i]++;
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
        NormalizeSegment();
    }

    public bool IsVertical => this.StartPoint.X == this.EndPoint.X;
    public bool IsHorizontal => this.StartPoint.Y == this.EndPoint.Y;

    private void NormalizeSegment()
    {
        if (IsVertical)
        {
            if (StartPoint.Y > EndPoint.Y)
            {
                SwapPoints();
            }
        }
        else if (IsHorizontal)
        {
            if (StartPoint.X > EndPoint.X)
            {
                SwapPoints();
            }
        }
        else
        {
            if ((StartPoint.X > EndPoint.X && StartPoint.Y > EndPoint.Y) ||
                (StartPoint.X > EndPoint.X && StartPoint.Y < EndPoint.Y))
            {
                SwapPoints();
            }
        }
    }
    private void SwapPoints()
    {
        var temp = StartPoint;
        StartPoint = EndPoint;
        EndPoint = temp;
    }
}