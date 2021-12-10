using System.Text;

public class Day10
{
    public static List<string> ReadInputFile(string filename)
    {
        return System.IO.File.ReadAllLines(filename).ToList();
    }

    public static long Sol1()
    {
        return  ReadInputFile("Inputs/input10.txt").Select(x=> IsLineCorrupted(x)).Where(x=> x.Item1).Sum(x=> ScoreByCloser[x.Item2]);
    }

    public static long Sol2()
    {
        var scores = ReadInputFile("Inputs/input10.txt").Where(x=> !IsLineCorrupted(x).Item1).Select(x=> CalculateScore(GetCompletionString(x))).ToList();
        scores.Sort();
        return scores[scores.Count/2];
    }

    public static (bool, char) IsLineCorrupted(string line)
    {
        var depth = 0;
        var dictionary = new Dictionary<int,char>();
        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];
            if(Openers.Contains(c)){
                depth++;
                dictionary.Add(depth, c);
            }
            else{
                if(dictionary.TryGetValue(depth, out var opener))
                {
                    if (opener != OpenerByCloser[c])
                    {
                        return (true, c);
                    }else{
                        dictionary.Remove(depth);
                        depth--;
                    }
                }
                else{
                    return (true, c);
                }
            }
        }
        return (false, ' ');
    }
    
    public static string GetCompletionString(string line)
    {
        var depth = 0;
        var dictionary = new Dictionary<int,char>();
        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];
            if(Openers.Contains(c)){
                depth++;
                dictionary.Add(depth, c);
            }
            else{
                if(dictionary.TryGetValue(depth, out var opener))
                {
                    if (opener != OpenerByCloser[c])
                    {
                        throw new Exception("Invalid input");
                    }else{
                        dictionary.Remove(depth);
                        depth--;
                    }
                }
                else{
                    throw new Exception("Invalid input");
                }
            }
        }
        var keys = dictionary.Keys.ToList();
        keys.Sort();
        keys.Reverse();
        var sb = new StringBuilder();
        foreach (var key in keys)
        {
            sb.Append(CloserByOpener[dictionary[key]]);
        }
        return sb.ToString();
    }
    
    public static long CalculateScore(string line){
        long score = 0;
        foreach (var c in line){
            score *= 5;
            score += ScoreByCloserAutocomplete[c];
        }
        return score;
    }
    public static char RoundOpen = '('; 
    public static char RoundClosed = ')'; 
    public static char SquareOpen = '['; 
    public static char SquareClosed = ']'; 
    public static char CurlyOpen = '{'; 
    public static char CurlyClosed = '}'; 
    public static char AngularOpen = '<'; 
    public static char AngularClosed = '>'; 

    public static HashSet<char> Openers = new HashSet<char>() { RoundOpen, SquareOpen, CurlyOpen, AngularOpen };
    public static HashSet<char> Closers = new HashSet<char>() { RoundClosed, SquareClosed, CurlyClosed, AngularClosed };
    public static Dictionary<char, char> CloserByOpener = new Dictionary<char, char>()
    {
        {RoundOpen, RoundClosed},
        {SquareOpen, SquareClosed},
        {CurlyOpen, CurlyClosed},
        {AngularOpen, AngularClosed}
    };

    public static Dictionary<char, char> OpenerByCloser = new Dictionary<char, char>()
    {
        {RoundClosed, RoundOpen},
        {SquareClosed, SquareOpen},
        {CurlyClosed, CurlyOpen},
        {AngularClosed, AngularOpen}
    };

    public static Dictionary<char, long> ScoreByCloser = new Dictionary<char, long>()
    {
        {RoundClosed, 3},
        {SquareClosed, 57},
        {CurlyClosed, 1197},
        {AngularClosed, 25137},
    };

    public static Dictionary<char, long> ScoreByCloserAutocomplete = new Dictionary<char, long>()
    {
        {RoundClosed, 1},
        {SquareClosed, 2},
        {CurlyClosed, 3},
        {AngularClosed, 4},
    };
}