using System.Text;

public class Day14
{
    private static (string Template, Dictionary<string, char> InsertionRules) ReadInputFile(string filename)
    {
        using System.IO.StreamReader file = new System.IO.StreamReader(filename);
        string template = file.ReadLine()!;

        string? line = file.ReadLine();
        var insertionRules = new Dictionary<string, char>();
        while ((line = file.ReadLine()) != null)
        {
            insertionRules.Add(line[0..2], line[^1]);
        }

        return (template, insertionRules);
    }


    public static long Sol1()
    {
        var input = ReadInputFile("Inputs/input14e.txt");
        for (int step = 0; step < 10; step++)
        {
            var insertionByIndex = new Dictionary<int, char>();
            for (int i = 0; i < input.Template.Length - 1; i++)
            {
                var match = input.Template[i..(i + 2)];
                if (input.InsertionRules.TryGetValue(match, out char replacement))
                {
                    insertionByIndex.Add(i + 1, replacement);
                }
            }
            var newString = new StringBuilder();

            for (int i = 0; i < input.Template.Length; i++)
            {
                if (insertionByIndex.TryGetValue(i, out char replacement))
                {
                    newString.Append(replacement);
                }
                newString.Append(input.Template[i]);
            }
            input.Template = newString.ToString();
        }
        var groups = input.Template.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        return groups.Values.Max() - groups.Values.Min();
    }

    public static long Sol2()
    {
        var input = ReadInputFile("Inputs/input14e.txt");
        var countByMatch = new Dictionary<string, long>();
        for (int i = 0; i < input.Template.Length - 1; i++)
        {
            var match = input.Template[i..(i + 2)];
            countByMatch[match] = countByMatch.GetValueOrDefault(match, 0) + 1;
        }

        for (int step = 0; step < 10; step++)
        {
            var newCountByMatch = countByMatch.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var (match, amount) in countByMatch)
            {
                if (input.InsertionRules.TryGetValue(match, out char replacement))
                {
                    var a = match[0];
                    var b = match[1];

                    string newPair1 = a + replacement.ToString();
                    string newPair2 = replacement.ToString() + b;
                    newCountByMatch.Remove(match);
                    newCountByMatch[newPair1] = countByMatch.GetValueOrDefault(newPair1, 0) + amount;
                    newCountByMatch[newPair2] = countByMatch.GetValueOrDefault(newPair2, 0) + amount;
                }
            }
            countByMatch = newCountByMatch;
        }
        var charByCount = new Dictionary<char, long>();
        foreach (var (match, amount) in countByMatch)
        {
            var a = match[0];
            var b = match[1];
            charByCount[a] = charByCount.GetValueOrDefault(a, 0) + amount;
            charByCount[b] = charByCount.GetValueOrDefault(b, 0) + amount;
        }
        var min = charByCount.Values.Min();
        var max = charByCount.Values.Max();
        return 0;
    }
}
