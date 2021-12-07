public class Day7
{

    public static List<int> ReadInputFile(string filename)
    {
        using System.IO.StreamReader file = new System.IO.StreamReader(filename);
        return file.ReadLine()!.Split(',').Select(int.Parse).ToList();
    }

    public static long Sol1()
    {
        var input = ReadInputFile("Inputs\\input7.txt");
        input.Sort();
        var median = input[input.Count / 2];
        return input.Sum(x => Math.Abs(x - median));
    }

    public static long Sol2()
    {
        var input = ReadInputFile("Inputs\\input7.txt");
        var distances = new List<long>();
        for (int i =0; i<input.Last(); i++){
            distances.Add(input.Sum(x => Summation(Math.Abs(x - i))));
        }
        return distances.Min();
    }

    public static long Summation(long n){
        return (n*(n+1))/2;
    }
}