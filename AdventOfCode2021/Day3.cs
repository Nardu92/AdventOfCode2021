using System.Drawing;

public class Day3
{
    public static string[] ReadInputFile(string filename)
    {
        return System.IO.File.ReadAllLines(filename);
    }

    public static long Sol1()
    {
        var lines = ReadInputFile("Inputs/input3.txt");
        string gamma = "";
        int length = lines.First().Length;
        for (int i = 0; i < length; i++)
        {
            gamma += GetGammaDigit(lines, i);
        }
        var longGamma = Convert.ToInt64(gamma, 2);
        var max = (long)Math.Pow(2, length) - 1;
        var eps = max - longGamma;
        return longGamma * eps;
    }

    private static char GetGammaDigit(string[] lines, int i)
    {
        var nums = lines.Length;
        var ones = lines.Select(x => x.Skip(i).Take(1).First()).Where(x => x.Equals('1')).Count();
        var zeros = nums - ones;
        if (ones >= zeros)
        {
            return '1';
        }
        else
        {
            return '0';
        }
    }

    public static long Sol2()
    {
        var lines = ReadInputFile("Inputs/input3.txt");
        int digits = lines.First().Length;
        var oxygen = FilterLines(lines, digits, true);
        var c02 = FilterLines(lines, digits, false);
        return oxygen * c02;

    }

    private static long FilterLines(string[] lines, int digits, bool oxygen)
    {
        while (lines.Length > 1)
        {
            for (int i = 0; i < digits && lines.Length > 1; i++)
            {
                var mostCommonBit = GetGammaDigit(lines, i);
                if(!oxygen){
                    mostCommonBit = mostCommonBit == '1' ? '0' : '1';
                }
                lines = lines.Where(x => x[i].Equals(mostCommonBit)).ToArray();
            }
        }
        return Convert.ToInt64(lines.First(), 2);
    }
}
