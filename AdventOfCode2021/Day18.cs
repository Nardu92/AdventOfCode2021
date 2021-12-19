
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

public class SFMTuple{

    public SFMTuple Parent {get; private set;}
    public SFMTuple Left {get; private set;}
    public SFMTuple Right {get; private set;}

    public int Value {get; private set;}

    public SFMTuple(string input, SFMTuple parent){
        Parent = parent;
        if(!input.Contains("[")){
            var i = input.Split(",").Select(int.Parse).ToArray();
            Left = new SFMTuple(i[0], this);
            Right = new SFMTuple(i[1], this);
        }else{
            
        }
    }

    public SFMTuple(int value, SFMTuple parent){
        Parent = parent;
        Value = value;
        Left = null;
        Right = null;
    }

    public void ParseInput(string input){

    }

    public static void ParseLine(string line)
    {
        var depth = 0;
        var dictionary = new Dictionary<int,string>();
        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];
            if(c == '['){
                depth++;
            }
            else if (c == ']'){
                var t = new SFMTuple(dictionary[depth], null);
                dictionary.Remove(depth);
                depth--;
            }
            else{
                dictionary[depth] = dictionary.GetValueOrDefault(depth, "") + c;
            }
        }
    }

    

    public void ExplodeOne(){

    }

    public void SplitOne()
    {
    }
}