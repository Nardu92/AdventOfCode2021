using System.Text.RegularExpressions;

public class Day22

{
    private static List<ConwayCommand> ReadInputFile(string filename)
    {
        var commands = new List<ConwayCommand>();
        using System.IO.StreamReader file = new System.IO.StreamReader(filename);
        string? line;
        while ((line = file.ReadLine()) != null)
        {
            var regexX = new Regex(@"(?<comm>on|off) x=(?<xrange>.*\.\..*),y=(?<yrange>.*\.\..*),z=(?<zrange>.*\.\..*)");
            var groups = regexX.Match(line).Groups;
            var command = groups["comm"].Value;
            var captureGroupX = groups["xrange"].Value.Split(".", System.StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var captureGroupY = groups["yrange"].Value.Split(".", System.StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var captureGroupZ = groups["zrange"].Value.Split(".", System.StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var conwayCommand = new ConwayCommand()
            {
                On = command == "on",
                XStart = captureGroupX[0],
                XEnd = captureGroupX[1],
                YStart = captureGroupY[0],
                YEnd = captureGroupY[1],
                ZStart = captureGroupZ[0],
                ZEnd = captureGroupZ[1]
            };
            commands.Add(conwayCommand);
        }
        return commands;
    }

    public static long Sol1()
    {
        var commands = ReadInputFile("Inputs/input22.txt");
        var powerGrid = new HashSet<(int x, int y, int z)>();
        foreach (var command in commands)
        {
            ApplyCommand(command, powerGrid);
        }
        return powerGrid.Count;
    }

    private static void ApplyCommand(ConwayCommand command, HashSet<(int x, int y, int z)> powerGrid)
    {
        for (int x = Math.Max(command.XStart, -50); x <= Math.Min(command.XEnd, 50); x++)
        {
            for (int y = Math.Max(command.YStart, -50); y <= Math.Min(command.YEnd, 50); y++)
            {
                for (int z = Math.Max(command.ZStart, -50); z <= Math.Min(command.ZEnd, 50); z++)
                {
                    if (command.On)
                    {
                        powerGrid.Add((x, y, z));
                    }
                    else
                    {
                        powerGrid.Remove((x, y, z));
                    }
                }
            }
        }
    }

    public static long Sol2()
    {
        var commands = ReadInputFile("Inputs/input22e2.txt");
        var overlap = commands.First().CalculateOverlap(commands.Skip(1).First());
        return 0;
    }
}

public class ConwayCommand
{

    public int XStart { get; set; }
    public int XEnd { get; set; }
    public int YStart { get; set; }
    public int YEnd { get; set; }
    public int ZStart { get; set; }
    public int ZEnd { get; set; }

    public bool On { get; set; }

    public override string ToString()
    {
        return $"{(On ? "on" : "off")} x={XStart}..{XEnd}, y={YStart}..{YEnd}, z={ZStart}..{ZEnd}";
    }

    public long CalculateOverlap(ConwayCommand command)
    {

        long overlapX = Math.Max(Math.Min(XEnd, command.XEnd) - Math.Max(XStart, command.XStart), 0);
        long overlapY = Math.Max(Math.Min(YEnd, command.YEnd) - Math.Max(YStart, command.YStart), 0);
        long overlapZ = Math.Max(Math.Min(ZEnd, command.ZEnd) - Math.Max(ZStart, command.ZStart), 0);

        return overlapX * overlapY * overlapZ;
    }
}