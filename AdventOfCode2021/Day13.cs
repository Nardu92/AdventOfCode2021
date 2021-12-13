using System.Text;

public class Day13
{
    private static (bool[,] Grid, List<Fold> Folds) ReadInputFile(string filename)
    {
        using System.IO.StreamReader file = new System.IO.StreamReader(filename);
        string? line;
        var dotLocations = new List<(int x, int y)>();

        line = file.ReadLine();
        while (!string.IsNullOrEmpty(line))
        {
            var location = line.Split(',');
            dotLocations.Add((int.Parse(location[0]), int.Parse(location[1])));
            line = file.ReadLine();
        };

        var folds = new List<Fold>();
        while ((line = file.ReadLine()) != null)
        {
            var l = line.Split('=');
            var horizontal = l[0][^1] == 'y';
            folds.Add(new Fold() { Horizontal = horizontal, Line = int.Parse(l[1]) });
        }
        var maxX = dotLocations.Max(x => x.x) + 1;
        var maxY = dotLocations.Max(x => x.y) + 1;

        var grid = new bool[maxY, maxX];

        foreach (var l in dotLocations)
        {
            grid[l.y, l.x] = true;
        }
        return (grid, folds);
    }

    private static bool[,] Fold(bool[,] grid, Fold fold)
    {
        if (fold.Horizontal)
        {
            return FoldHorizontal(grid, fold.Line);
        }
        else
        {
            return FoldVertical(grid, fold.Line);
        }
    }
    private static bool[,] FoldHorizontal(bool[,] grid, int fold)
    {
        var oldMaxY = grid.GetLength(0);
        var maxY = fold;
        var maxX = grid.GetLength(1);
        var newGrid = new bool[maxY, maxX];
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                newGrid[y, x] = grid[y, x] || grid[2 * fold - y, x];
            }
        }
        return newGrid;
    }

    private static bool[,] FoldVertical(bool[,] grid, int fold)
    {
        var oldMaxX = grid.GetLength(1);
        var maxY = grid.GetLength(0);
        var maxX = fold;
        var newGrid = new bool[maxY, maxX];
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                newGrid[y, x] = grid[y, x] || grid[y, 2 * fold - x];
            }
        }
        return newGrid;
    }

    public static long Sol1()
    {
        var input = ReadInputFile("Inputs\\input13.txt");
        input.Grid = Fold(input.Grid, input.Folds[0]);
        return CountElementsInMatrix(input.Grid);
    }

    private static long CountElementsInMatrix(bool[,] grid)
    {
        long count = 0;
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j])
                {
                    count++;
                }
            }
        }
        return count;
    }

    public static long Sol2()
    {
        var input = ReadInputFile("Inputs\\input13.txt");
        foreach (var fold in input.Folds)
        {
            input.Grid = Fold(input.Grid, fold);
        }
        CountElementsInMatrix(input.Grid);

        PrintMatrix(input.Grid);
        return 0;
    }

    private static void PrintMatrix(bool[,] grid)
    {
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j])
                {
                    stringBuilder.Append("#");
                }
                else
                {
                    stringBuilder.Append(" ");
                }
            }
            stringBuilder.AppendLine();
        }
        Console.WriteLine(stringBuilder.ToString());
    }
}


class Fold
{
    public bool Horizontal { get; set; }
    public int Line { get; set; }
}