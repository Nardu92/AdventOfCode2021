using System.Text;

public class Day25
{
    private static char[,] ReadInputFile(string filename)
    {
        string[] lines = System.IO.File.ReadAllLines(filename);
        var grid = new char[lines.Length, lines.First().Length];
        for (var y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                grid[y, x] = lines[y][x];
            }
        }
        return grid;
    }

    public static long Sol1()
    {
        var grid = ReadInputFile("Inputs\\input25.txt");
        bool change;
        long step = 0;
        do
        {
            step++;
            change = Move(grid);
        } while (change);
        return step;
    }

    private static bool Move(char[,] grid)
    {
        bool changeEast = MoveEast(grid);
        bool changeWest = MoveWest(grid);
        return changeEast || changeWest;
    }

    private static bool MoveWest(char[,] grid)
    {
        bool change = false;
        for (int x = 0; x < grid.GetLength(1); x++)
        {
            var free = grid[0, x] == '.';
            int y = 0;
            for (; y < grid.GetLength(0) - 1; y++)
            {
                if (grid[y, x] == 'v' && grid[(y + 1), x] == '.')
                {
                    grid[(y + 1), x] = 'v';
                    grid[y, x] = '.';
                    change = true;
                    y++;
                }
            }
            if (free && y < grid.GetLength(0) && grid[grid.GetLength(0) - 1, x] == 'v')
            {
                grid[0, x] = 'v';
                grid[grid.GetLength(0) - 1, x] = '.';
                change = true;
            }
        }
        return change;
    }

    private static bool MoveEast(char[,] grid)
    {
        bool change = false;
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            var free = grid[y, 0] == '.';
            int x = 0;
            for (; x < grid.GetLength(1) - 1; x++)
            {
                if (grid[y, x] == '>' && grid[y, (x + 1)] == '.')
                {
                    grid[y, (x + 1)] = '>';
                    grid[y, x] = '.';
                    change = true;
                    x++;
                }
            }
            if (free && x < grid.GetLength(1) && grid[y, grid.GetLength(1) - 1] == '>')
            {
                grid[y, 0] = '>';
                grid[y, grid.GetLength(1) - 1] = '.';
                change = true;
            }
        }
        return change;
    }

    private static void PrintGrid(char[,] grid, long step)
    {
        StringBuilder sb = GridToString(grid);
        Console.WriteLine($"After {step} steps:");
        Console.WriteLine(sb.ToString());
    }

    private static StringBuilder GridToString(char[,] grid)
    {
        var sb = new StringBuilder();
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                sb.Append(grid[y, x]);
            }
            sb.AppendLine();
        }
        return sb;
    }

    public static long Sol2()
    {
        return 0;
    }
}
