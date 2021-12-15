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
        var grid = ReadInputFile("Inputs\\input15e.txt");

        var visited = new HashSet<(int, int)>();
        var tentativeDistanceByPosition = new Dictionary<(int, int), int>();
     
        return 0;
    }

    public static long Sol2()
    {
        var grid = ReadInputFile("Inputs\\input15e.txt");
        return 0;
    }

}

public class D15Node
{
    public int Value { get; set; }

    public (int, int) Location { get; set; }

    public List<D15Node> Neighbors { get; set; }

    public D15Node(int value, (int, int) location)
    {
        Value = value;
        Location = location;
        Neighbors = new List<D15Node>();
    }

    public void AddNeighbor(D15Node node)
    {
        Neighbors.Add(node);
    }

    public int Count()
    {
        return Neighbors.Sum(x => x.Count()) + 1;
    }
}
