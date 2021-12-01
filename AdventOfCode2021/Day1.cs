public class Day1
{
    public static List<int> ReadInputFile(string filename)
    {
        string[] lines = System.IO.File.ReadAllLines(filename);
        return lines.Select(x => Convert.ToInt32(x)).ToList();
    }

    private static int CountIncreasing(int[] input)
    {
        return input.Skip(1).Where((x, idx) => x > input[idx]).Count();
    }

    public static int Sol1()
    {
        var input = ReadInputFile("Inputs\\input1.txt").ToArray();
        return CountIncreasing(input);
    }

    public static int Sol2(){
        var input = ReadInputFile("Inputs\\input1.txt").ToArray();
        
        var result = input.Zip(input.Skip(1), (first, second) => first + second);
        result = result.Zip(input.Skip(2), (first, second) => first + second);
        return CountIncreasing(result.ToArray());
    }
}