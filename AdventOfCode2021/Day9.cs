public class Day9
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
        var grid = ReadInputFile("Inputs\\input9.txt");

        var lows = GetLowLocations(grid);
        return lows.Select(x => grid[x.Key, x.Value] + 1).Sum();
    }

    public static long Sol2()
    {
        var grid = ReadInputFile("Inputs\\input9.txt");
        var lows = GetLowLocations(grid);

        var sizes = new List<int>();
        foreach (var l in lows)
        {
            var root = BuildGraph(grid, l);
            sizes.Add(root.Count());
        }
        sizes.Sort();
        long total = 1;
        sizes.TakeLast(3).ToList().ForEach(x => total *= x);
        return total;
    }

    private static Node BuildGraph(int[,] grid, KeyValuePair<int, int> l)
    {
        var nodesByPosition = new Dictionary<(int, int), Node>();
        var root = new Node(grid[l.Key, l.Value], (l.Key, l.Value));
        nodesByPosition[(l.Key, l.Value)] = root;
        AddAll4Neighbors(grid, (l.Key, l.Value), nodesByPosition);
        return root;
    }

    private static void AddAll4Neighbors(int[,] grid, (int, int) l, Dictionary<(int, int), Node> nodesByPosition)
    {
        var node = nodesByPosition[l];
        nodesByPosition[(l.Item1, l.Item2)] = node;

        var up = AddNeighbor(node, nodesByPosition, grid, (l.Item1 - 1, l.Item2));
        if (up)
        {
            AddAll4Neighbors(grid, (l.Item1 - 1, l.Item2), nodesByPosition);
        }
        var down = AddNeighbor(node, nodesByPosition, grid, (l.Item1 + 1, l.Item2));
        if (down)
        {
            AddAll4Neighbors(grid, (l.Item1 + 1, l.Item2), nodesByPosition);
        }
        var left = AddNeighbor(node, nodesByPosition, grid, (l.Item1, l.Item2 - 1));
        if (left)
        {
            AddAll4Neighbors(grid, (l.Item1, l.Item2 - 1), nodesByPosition);
        }
        var right = AddNeighbor(node, nodesByPosition, grid, (l.Item1, l.Item2 + 1));
        if (right)
        {
            AddAll4Neighbors(grid, (l.Item1, l.Item2 + 1), nodesByPosition);
        }
    }

    private static bool AddNeighbor(Node node, Dictionary<(int, int), Node> nodesByPosition, int[,] grid, (int, int) l)
    {
        if (PositionIsValid(grid, l) && !nodesByPosition.ContainsKey(l) && node.Value < grid[l.Item1, l.Item2] && grid[l.Item1, l.Item2] != 9)
        {
            var neighbor = new Node(grid[l.Item1, l.Item2], l);
            nodesByPosition[l] = neighbor;
            node.AddNeighbor(neighbor);
            return true;
        }
        return false;
    }
    private static bool PositionIsValid(int[,] grid, (int, int) l)
    {
        return l.Item1 >= 0 && l.Item1 < grid.GetLength(0) && l.Item2 >= 0 && l.Item2 < grid.GetLength(1);
    }

    private static List<KeyValuePair<int, int>> GetLowLocations(int[,] grid)
    {
        var lowsLocation = new List<KeyValuePair<int, int>>();
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                var current = grid[i, j];

                int up = 10;
                if (i - 1 >= 0)
                {
                    up = grid[i - 1, j];
                }
                int down = 10;
                if (i + 1 < grid.GetLength(0))
                {
                    down = grid[i + 1, j];
                }
                int left = 10;
                if (j - 1 >= 0)
                {
                    left = grid[i, j - 1];
                }
                int right = 10;
                if (j + 1 < grid.GetLength(1))
                {
                    right = grid[i, j + 1];
                }

                if (current < up && current < down && current < left && current < right)
                {
                    lowsLocation.Add(new KeyValuePair<int, int>(i, j));
                }
            }
        }
        return lowsLocation;
    }
}

public class Node
{
    public int Value { get; set; }

    public (int, int) Location { get; set; }
    public List<Node> Neighbors { get; set; }

    public Node(int value, (int, int) location)
    {
        Value = value;
        Location = location;
        Neighbors = new List<Node>();
    }

    public void AddNeighbor(Node node)
    {
        Neighbors.Add(node);
    }

    public int Count()
    {
        return Neighbors.Sum(x => x.Count()) + 1;
    }
}
