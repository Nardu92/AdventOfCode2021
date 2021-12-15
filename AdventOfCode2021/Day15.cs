public class Day15
{
    public static int[,] ReadInputFile(string filename)
    {
        var input = System.IO.File.ReadAllLines(filename).ToList();
        var grid = new int[input.Count, input.First().Length];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = Convert.ToInt32(input[i][j].ToString());
            }
        }
        return grid;
    }

    public static long Sol1()
    {
        var grid = ReadInputFile("Inputs\\input15.txt");
        return GetDistanceToEnd(grid);
    }

    private static long GetDistanceToEnd(int[,] grid)
    {
        var nodesByPosition = BuildGraph(grid);

        var visited = new HashSet<(int, int)>();
        var tentativeDistanceByPosition = new Dictionary<(int, int), int>();
        var startPosition = (0, 0);
        var endPosition = (grid.GetLength(0) - 1, grid.GetLength(1) - 1);
        tentativeDistanceByPosition[startPosition] = 0;
        var currentPosition = startPosition;

        var currentNode = nodesByPosition[currentPosition];
        var unvisitedPositionsWithDistance = new Dictionary<(int, int), int>();
        do
        {
            foreach (var neighbor in currentNode.Neighbors)
            {
                if (visited.Contains(neighbor.Location))
                {
                    continue;
                }
                var newDistance = tentativeDistanceByPosition[currentNode.Location] + neighbor.Value;
                var oldDistance = tentativeDistanceByPosition.GetValueOrDefault(neighbor.Location, int.MaxValue);
                if (newDistance < oldDistance)
                {
                    tentativeDistanceByPosition[neighbor.Location] = newDistance;
                    unvisitedPositionsWithDistance[neighbor.Location] = newDistance;
                }
            }
            visited.Add(currentNode.Location);

            int min = int.MaxValue;
            foreach (var (p, d) in unvisitedPositionsWithDistance)
            {
                if (d < min)
                {
                    min = d;
                    currentPosition = p;
                }
            }
            unvisitedPositionsWithDistance.Remove(currentPosition);
            currentNode = nodesByPosition[currentPosition];
        } while (currentNode.Location != endPosition);

        return tentativeDistanceByPosition[endPosition];
    }

    private static Dictionary<(int, int), Node> BuildGraph(int[,] grid)
    {
        var nodesByPosition = new Dictionary<(int, int), Node>();

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                (int y, int x) location = (y, x);
                var node = new Node(grid[y, x], location);
                nodesByPosition[location] = node;
            }
        }

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                var node = nodesByPosition[(y, x)];
                if (y > 0)
                {
                    node.AddNeighbor(nodesByPosition[(y - 1, x)]);
                }
                if (x > 0)
                {
                    node.AddNeighbor(nodesByPosition[(y, x - 1)]);
                }
                if (y < grid.GetLength(0) - 1)
                {
                    node.AddNeighbor(nodesByPosition[(y + 1, x)]);
                }
                if (x < grid.GetLength(1) - 1)
                {
                    node.AddNeighbor(nodesByPosition[(y, x + 1)]);
                }
            }
        }
        return nodesByPosition;
    }

    public static long Sol2()
    {
        var grid = ReadInputFile("Inputs\\input15.txt");
        var finalGrid = BuildMegaGrid(grid);
        return GetDistanceToEnd(finalGrid);
    }

    private static int[,] BuildMegaGrid(int[,] grid)
    {
        var grids = new Dictionary<(int y, int x), int[,]>();
        grids.Add((0, 0), grid);
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                var newGrid = new int[grid.GetLength(0), grid.GetLength(1)];
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        newGrid[i, j] = (grid[i, j] + x + y - 1) % 9 + 1;
                    }
                }
                grids.Add((x, y), newGrid);
            }
        }
        var ogY = grid.GetLength(0);
        var ogX = grid.GetLength(1);
        var finalGrid = new int[ogY * 5, ogX * 5];
        for (int y = 0; y < finalGrid.GetLength(0); y++)
        {
            for (int x = 0; x < finalGrid.GetLength(1); x++)
            {
                finalGrid[y, x] = grids[(y / ogY, x / ogX)][y % ogY, x % ogX];
            }
        }
        return finalGrid;
    }
}
