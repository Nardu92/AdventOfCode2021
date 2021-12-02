using System.Drawing;

public class Day2
{
    public static List<Command> ReadInputFile(string filename)
    {
        string[] lines = System.IO.File.ReadAllLines(filename);
        return lines.Select(x => ParseCommand(x)).ToList();
    }

    public static Command ParseCommand(string line){
        string[] parts = line.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        return new Command{
            Direction = (Direction) Enum.Parse(typeof(Direction),parts[0], true),
            Value = Convert.ToInt32(parts[1])
        };
    }

    public static Point GetNewPosWithPoint(Point currentPosition, Command command){
        switch(command.Direction){
            case Direction.Up:
                currentPosition.Y += command.Value;
                break;
            case Direction.Down:
                currentPosition.Y -= command.Value;
                break;
            case Direction.Forward:
                currentPosition.X += command.Value;
                break;
            default:
                throw new Exception("Unknown direction");
        }
        return currentPosition;
    }

    public static long Sol1(){
        List<Command> commands = ReadInputFile("Inputs/input2.txt");
        Point currentPosition = new Point(0,0);
        foreach(Command command in commands){
            currentPosition = GetNewPosWithPoint(currentPosition, command);
        }
        return Math.Abs(currentPosition.Y) * currentPosition.X;
    }

    public static long Sol2(){
        List<Command> commands = ReadInputFile("Inputs/input2.txt");
        Point currentPosition = new Point(0,0);
        int aim = 0;
        foreach(Command command in commands){
            if (command.Direction == Direction.Forward){
                currentPosition.X += command.Value;
                currentPosition.Y += aim * command.Value;
            }else if (command.Direction == Direction.Up){
                aim -= command.Value;
            }else{
                aim += command.Value;
            }
        }
        return currentPosition.Y * currentPosition.X;
    }
}

public enum Direction
{
    Up,
    Down,
    Forward,
}

public class Command
{
    public int Value { get; set; }
    public Direction Direction { get; set; }
}
