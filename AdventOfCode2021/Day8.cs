public class Day8
{
    public static List<string> ReadInputFile(string filename)
    {
        return System.IO.File.ReadAllLines(filename).ToList();
    }

    public static long Sol1()
    {
        var input = ReadInputFile("Inputs\\input8.txt");
        return input.Select(x => x.Split('|', StringSplitOptions.RemoveEmptyEntries).Last())
                    .SelectMany(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    .Where(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7).Count();
    }

    public static long Sol2()
    {
        return ReadInputFile("Inputs\\input8.txt").Select(GetOutputFromLine).Sum();
    }

    private static long GetOutputFromLine(string line)
    {
        var map = MapSegmentsToDigit(line);
        var output = line.Split('|', StringSplitOptions.RemoveEmptyEntries).Last()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => String.Concat(x.OrderBy(c => c))).ToList();

        long result = 0;
        for (int i = 0; i < output.Count; i++)
        {
            var d = output[i];
            var pos = (int)Math.Pow(10, output.Count - i - 1);
            result += map[d] * pos;
        }
        return result;
    }

    private static char[] GetDiff(string a, string b)
    {
        var result = a;
        foreach (var c in b)
        {
            result = result.Replace(c.ToString(), "");
        }
        return result.ToCharArray();
    }

    private static string RemoveSegments(string a, char[] segment)
    {
        var result = a;
        foreach (var c in segment)
        {
            result = result.Replace(c.ToString(), "");
        }
        return result;
    }

    private static Dictionary<string, int> MapSegmentsToDigit(string line)
    {
        var digits = line.Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .SelectMany(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => String.Concat(x.OrderBy(c => c))).Distinct().ToList();

        var one = digits.Where(x => x.Length == 2).Single();
        var seven = digits.Where(x => x.Length == 3).Single();
        var aSegment = GetDiff(seven, one).Single();
        var four = digits.Where(x => x.Length == 4).Single();
        var eight = digits.Where(x => x.Length == 7).Single();
        var zeroSixNine = digits.Where(x => x.Length == 6).ToList();
        var twoThreeFive = digits.Where(x => x.Length == 5).ToList();
        var six = zeroSixNine.Where(x => !x.Contains(one.First()) || !x.Contains(one.Last())).First();
        var cSegment = GetDiff(one, six).Single();
        var zeroOrNine = zeroSixNine.Where(x => !x.Equals(six)).ToList();
        var nine = zeroOrNine.Where(x => GetDiff(four, x).Length == 0).Single();
        var eSegment = GetDiff(eight, nine).Single();
        var zero = zeroOrNine.Where(x => !x.Equals(nine)).Single();
        var dSegment = GetDiff(eight, zero).Single();
        var someSegments = new char[] { aSegment, cSegment, dSegment, eSegment };
        var two = twoThreeFive.Where(x => RemoveSegments(x, someSegments).Length == 1).Single();
        var gSegment = two.Where(x => !someSegments.Contains(x)).Single();
        var threeFive = twoThreeFive.Where(x => !x.Equals(two)).ToList();
        var segmentsOfOneAndTwo = two.ToCharArray().ToList();
        segmentsOfOneAndTwo.AddRange(one.ToCharArray());
        var five = threeFive.Where(x => RemoveSegments(x, segmentsOfOneAndTwo.ToArray()).Length == 1).Single();
        var three = threeFive.Where(x => !x.Equals(five)).Single();

        return new Dictionary<string, int>() { { one, 1 }, { two, 2 }, { three, 3 }, { four, 4 }, { five, 5 }, 
                { six, 6 }, { seven, 7 }, { eight, 8 }, { nine, 9 }, { zero, 0 } };
    }
}