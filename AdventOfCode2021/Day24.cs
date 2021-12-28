using System.Text;

public class Day24
{
    public static List<AluCommand> ReadInputFile(string filename)
    {
        string[] lines = System.IO.File.ReadAllLines(filename);
        var commands = new List<AluCommand>();
        foreach (var item in lines)
        {
            var command = AluCommand.Parse(item);
            commands.Add(command);
        }
        return commands;
    }

    public static long Sol1()
    {
        var commands = ReadInputFile("Inputs\\input24.txt");
        var modelNumber = 1234567911234;
        var inputs = commands.Where(c => c.Operation == AluOperation.Inp).ToArray();

        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].B = (modelNumber / Math.Pow(10, i)).ToString();
        }
        
        return 0;
    }

    public static long Sol2()
    {
        return 0;
    }
}

public enum AluOperation
{
    //inp a - Read an input value and write it to variable a.
    Inp,
    //add a b - Add the value of a to the value of b, then store the result in variable a.
    Add,
    //mul a b - Multiply the value of a by the value of b, then store the result in variable a.
    Mul,
    //div a b - Divide the value of a by the value of b, truncate the result to an integer, then store the result in variable a. (Here, "truncate" means to round the value toward zero.)
    Div,
    //mod a b - Divide the value of a by the value of b, then store the remainder in variable a. (This is also called the modulo operation.)
    Mod,
    //eql a b - If the value of a and b are equal, then store the value 1 in variable a. Otherwise, store the value 0 in variable a.
    Eql,
}

public class AluCommand
{
    public AluOperation Operation { get; set; }
    public string A { get; set; }
    public string B { get; set; }

    public AluCommand(AluOperation operation, string a, string b)
    {
        Operation = operation;
        A = a;
        B = b;
    }

    public override string ToString()
    {
        return $"{Operation} {A} {B}";
    }

    public static AluCommand Parse(string line)
    {
        string[] parts = line.Split(' ');
        AluOperation operation = (AluOperation)System.Enum.Parse(typeof(AluOperation), parts[0], true);
        string a = parts[1];
        if (operation == AluOperation.Inp)
        {
            return new AluCommand(operation, a, string.Empty);
        }
        string b = parts[2];
        return new AluCommand(operation, a, b);
    }

    public void Execute(Dictionary<string, long> memory)
    {
        if (!long.TryParse(B, out long valueB))
        {
            valueB = memory[B];
        }
        switch (Operation)
        {
            case AluOperation.Inp:
                memory[A] = long.Parse(B);
                break;
            case AluOperation.Add:
                memory[A] = memory.GetValueOrDefault(A, 0) + valueB;
                break;
            case AluOperation.Mul:
                memory[A] = memory.GetValueOrDefault(A, 0) * valueB;
                break;
            case AluOperation.Div:
                memory[A] = memory.GetValueOrDefault(A, 0) / valueB;
                break;
            case AluOperation.Mod:
                memory[A] = memory.GetValueOrDefault(A, 0) % valueB;
                break;
            case AluOperation.Eql:
                memory[A] = memory.GetValueOrDefault(A, 0) == valueB ? 1 : 0;
                break;
            default:
                throw new System.Exception($"Unknown operation {Operation}");
        }

    }
}

