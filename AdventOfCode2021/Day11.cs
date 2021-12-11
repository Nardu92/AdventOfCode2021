public class Day11
{
    public static DumboOctopus[,] ReadInputFile(string filename)
    {
        var input = System.IO.File.ReadAllLines(filename).ToList();
        var grid = new DumboOctopus[input.Count, input.First().Length];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = new DumboOctopus(Convert.ToInt32(input[i][j].ToString()), (i,j));
            }
        }
        return grid;
    }

    public static long Sol1()
    {
        var grid = ReadInputFile("Inputs\\input11.txt");
        return RunSim(grid, 100);
    }

    private static long RunSim(DumboOctopus[,] grid, int steps)
    {
        long flashes = 0;
        for (int i = 0; i < steps; i++)
        {
            flashes += Step(grid);
        }
        return flashes;
    }

    private static long RunSimUntilSync(DumboOctopus[,] grid)
    {
        var size = grid.GetLength(0) * grid.GetLength(1);
        var steps = 0;
        while (true){
            var f = Step(grid);
            steps++;
            if (f == size)
            {
                return steps;
            }
        }
    }

    public static long Sol2()
    {
        var grid = ReadInputFile("Inputs\\input11.txt");
        return RunSimUntilSync(grid);
    }

    private static int Step(DumboOctopus[,] grid){
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j].EnergyLevel++;
                var dumbo = grid[i, j];
                if (dumbo.EnergyLevel > 9 && !dumbo.Flashed){
                    dumbo.Flash(grid);
                }
            }
        }
        var flashCount = 0;
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                var dumbo = grid[i, j];
                if (dumbo.Reset()){
                    flashCount++;
                }
            }
        }
        return flashCount;
    }
}

public class DumboOctopus{
    public int EnergyLevel { get; set; }
    public bool Flashed { get; set; }

    public (int,int) Position { get; set; }

    public DumboOctopus(int energyLevel, (int,int) position){
        Position = position;
        EnergyLevel = energyLevel;
        Flashed = false;
    }

    public List<DumboOctopus> GetNeighbors(DumboOctopus[,] grid){
        var neighbors = new List<DumboOctopus>();
        for (int i = Position.Item1 -1; i <= Position.Item1 + 1; i++)
        {
            for (int j = Position.Item2 -1; j <= Position.Item2 + 1; j++)
            {
                if (i == Position.Item1 && j == Position.Item2)
                {
                    continue;
                }
                if(PositionIsValid(grid, (i, j))){
                    neighbors.Add(grid[i,j]);
                }
            }
        }
        return neighbors;
    }

    private static bool PositionIsValid(DumboOctopus[,] grid, (int, int) l)
    {
        return l.Item1 >= 0 && l.Item1 < grid.GetLength(0) && l.Item2 >= 0 && l.Item2 < grid.GetLength(1);
    }

    public void Flash(DumboOctopus [,] grid){
        if (EnergyLevel > 9 && !Flashed){
            Flashed = true;
            foreach (var n in GetNeighbors(grid)){
                n.EnergyLevel++;
                n.Flash(grid);
            }
        }
    }

    public bool Reset(){
        if (EnergyLevel > 9 && Flashed){
            EnergyLevel = 0;
            Flashed = false;
            return true;
        }
        return false;
    }

    public override string ToString(){
        return $"{EnergyLevel}";
    }
}