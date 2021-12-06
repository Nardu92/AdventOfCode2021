public class Day6
{
    public static List<int> ReadInputFile(string filename)
    {
        using System.IO.StreamReader file = new System.IO.StreamReader(filename);
        string line = file.ReadLine();
        return line.Split(',').Select(int.Parse).ToList();
    }

    public static int Sol1()
    {
        var input = ReadInputFile("Inputs\\input6.txt").ToArray();
        var fish = input.Select(x => new LanternFish(x)).ToList();

        for (int i = 0; i < 80; i++)
        {
            var newFish = new List<LanternFish>();
            foreach (var f in fish)
            {
                if (f.Grow())
                {
                    newFish.Add(new LanternFish(8));
                }
            }
            fish.AddRange(newFish);
        }
        return fish.Count();
    }

    public static long Sol2()
    {
        var input = ReadInputFile("Inputs\\input6e.txt").ToArray();

        const int maxAge = 6;

        for (int i = 0; i < 10; i++)
        {
            int size = i + maxAge;
            Console.WriteLine($"D: {i} f: {FibonacciRulez(input, maxAge, size)}");
        }
        return 0;
    }

    private static long FibonacciRulez(int[] input, int maxAge, int size)
    {
        var days = new int[size + 2];
        var daysLong = new int[size];
        for (int i = 0; i < size; i++)
        {
            days[i] = 0;
            daysLong[i] = 0;
        }
        foreach (var i in input)
        {
            days[maxAge - i]++;
        }
        for (int j = maxAge + 1; j < size; j++)
        {
            for (int i = maxAge; i < size; i++)
            {
                var x = days[i - maxAge];
                daysLong[i] += x;
            }
            for (int i = maxAge; i < size; i++)
            {
                days[i+2] += daysLong[i];
            }
        }

        return days.Sum();
    }
}

public class LanternFish
{
    public int Age { get; set; }

    public LanternFish(int age)
    {
        Age = age;
    }

    public bool Grow()
    {
        if (Age == 0)
        {
            Age = 6;
            return true;
        }
        else
        {
            Age--;
            return false;
        }
    }
}