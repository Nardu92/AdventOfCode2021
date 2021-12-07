public class Day6
{

    public static List<int> ReadInputFile(string filename)
    {
        using System.IO.StreamReader file = new System.IO.StreamReader(filename);
        return file.ReadLine()!.Split(',').Select(int.Parse).ToList();
    }

    public static long Sol1()
    {
        var input = ReadInputFile("Inputs\\input6.txt").ToArray();
        return CountAllTheFish(input, 80);
    }

    public static long Sol2()
    {
        var input = ReadInputFile("Inputs\\input6.txt").ToArray();
        return CountAllTheFish(input, 256);
    }

    private static long CountAllTheFish(int[] input, int days)
    {
        FishSchool[] schools = new FishSchool[days];
        for (int i = 0; i < days; i++)
        {
            schools[i] = new FishSchool();
        }
        foreach (var i in input)
        {
            if (i < days)
            {
                schools[i].AdultsCount++;
                schools[i].YoungCount++;
            }
        }
        for (int i = 0; i < days; i++)
        {
            var adults = schools[i].AdultsCount;
            if (adults > 0)
            {
                for (int j = i + 7; j < days; j += 7)
                {
                    schools[j].YoungCount += adults;
                }
            }
            if (i + 2 < days)
            {
                if (schools[i].YoungCount > 0)
                {
                    schools[i + 2].AdultsCount += schools[i].YoungCount;
                    schools[i].YoungCount = 0;
                }
            }
        }

        return schools.Sum(x => x.AdultsCount + x.YoungCount);
    }
}

public class FishSchool
{
    public long AdultsCount { get; set; }
    public long YoungCount { get; set; }
}