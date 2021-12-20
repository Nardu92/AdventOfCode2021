
public class Day18
{
    public static string[] ReadInputFile(string filename)
    {
        return System.IO.File.ReadAllLines(filename);
    }


    public static long Sol1()
    {
        return 0;
    }


    public static long Sol2()
    {
        return 0;
    }

}

public class SnailMath
{

    public Guid Id { get; }
    public SnailMath? Parent { get; set; }
    public SnailMath? Left { get; set; }
    public SnailMath? Right { get; set; }

    public int Value { get; set; }

    public int Depth => Parent?.Depth + 1 ?? 0;

    public bool ToSplit => Value > 10;

    public bool ToExplode => Depth > 4;


    public SnailMath(string input) : this(input, null)
    {

    }

    public SnailMath(int value, SnailMath parent)
    {
        Id = Guid.NewGuid();
        Value = value;
        Parent = parent;
    }


    public SnailMath(string input, SnailMath? parent)
    {
        Id = Guid.NewGuid();
        Parent = parent;
        if (input.Contains(','))
        {
            var index = FindComma(input);
            Left = new SnailMath(input[1..index], this);
            Right = new SnailMath(input[(index + 1)..^1], this);
        }
        else
        {
            Left = null;
            Right = null;
            Value = int.Parse(input);
        }
    }

    public SnailMath(SnailMath left, SnailMath right)
    {
        Id = Guid.NewGuid();
        Parent = null;
        Left = left;
        left.Parent = this;
        Right = right;
        right.Parent = this;
    }

    public static int FindComma(string line)
    {
        var depth = 0;
        var dictionary = new Dictionary<int, string>();
        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];
            if (c == '[')
            {
                depth++;
            }
            else if (c == ']')
            {
                dictionary.Remove(depth);
                depth--;
            }
            else if (c == ',' && depth == 1)
            {
                return i;
            }
        }
        return -1;
    }

    public long GetMagnitude()
    {
        if (Left == null && Right == null)
        {
            return Value;
        }
        else
        {
            return 3 * Left.GetMagnitude() + 2 * Right.GetMagnitude();
        }
    }
}

public class SnailTree
{
    public SnailMath Root { get; set; }

    public List<SnailMath> OrderedSnailMath { get; private set; }
    public Dictionary<int, Guid> IdByPosition { get; private set; }
    public Dictionary<Guid, int> PositionById { get; private set; }

    public Dictionary<Guid, SnailMath> NodesById { get; private set; }

    public SnailTree(string input)
    {
        Root = new SnailMath(input);
        PopulateDictionaries();
    }

    public void PopulateDictionaries()
    {
        OrderedSnailMath = PreorderTraversal(Root);

        IdByPosition = OrderedSnailMath.Select((x, idx) => new { value = x.Id, index = idx }).ToList().ToDictionary(x => x.index, x => x.value);
        PositionById = IdByPosition.ToDictionary(x => x.Value, x => x.Key);
        NodesById = OrderedSnailMath.ToDictionary(x => x.Id, x => x);
    }

    public static List<SnailMath> PreorderTraversal(SnailMath node)
    {
        var list = new List<SnailMath>();
        if (node.Left != null)
        {
            list.AddRange(PreorderTraversal(node.Left));
        }
        if (node.Right != null)
        {
            list.AddRange(PreorderTraversal(node.Right));
        }
        if (node.Right == null && node.Left == null)
        {
            list.Add(node);
        }
        return list;
    }

    public void ExplodeLeft(SnailMath snailMath)
    {
        var position = PositionById[snailMath.Id];
        if (position > 0)
        {
            var previous = NodesById[IdByPosition[position - 1]];
            previous.Value += snailMath.Value;
        }
    }
    public void ExplodeRight(SnailMath snailMath)
    {
        var position = PositionById[snailMath.Id];
        if (position < PositionById.Count - 1)
        {
            var successor = NodesById[IdByPosition[position + 1]];
            successor.Value += snailMath.Value;
        }
    }

    public void Explode(SnailMath snailMath)
    {
        ExplodeLeft(snailMath!.Left!);
        ExplodeRight(snailMath!.Right!);
        if (snailMath.Parent != null)
        {
            if (snailMath.Parent.Left != null && snailMath.Parent.Left.Id == snailMath.Id)
            {
                snailMath.Parent.Left = new SnailMath(0, snailMath.Parent);
            }
            else if (snailMath.Parent.Right != null && snailMath.Parent.Right.Id == snailMath.Id)
            {
                snailMath.Parent.Right = new SnailMath(0, snailMath.Parent);
            }
        }
        PopulateDictionaries();
    }

    public void Split(SnailMath snailMath)
    {
        var left = new SnailMath(snailMath.Value / 2, snailMath);
        var right = new SnailMath(snailMath.Value - left.Value, snailMath);
        snailMath.Value = 0;
        snailMath.Left = left;
        snailMath.Right = right;
        PopulateDictionaries();
    }

    public SnailMath? FindNextOperation()
    {
        foreach (var snailMath in OrderedSnailMath)
        {
            if (snailMath.ToSplit)
            {
                return snailMath;
            }
            else if (snailMath.ToExplode)
            {
                return snailMath.Parent;
            }
        }
        return null;
    }

    public bool ExecuteNextOperation()
    {
        var nextOperation = FindNextOperation();
        if (nextOperation != null)
        {
            if (nextOperation.ToSplit)
            {
                Split(nextOperation);
                return true;
            }
            else
            {

                Explode(nextOperation);
                return true;
            }
        }
        return false;
    }

    public void Reduce()
    {
        bool reduced = false;
        do
        {
            reduced = ExecuteNextOperation();
        } while (reduced);
    }

    public void Add(string input)
    {
        var newNode = new SnailMath(input);
        Root = new SnailMath(Root, newNode);
        PopulateDictionaries();
        Reduce();
    }
}
