using System.Diagnostics;



Run("Day1 - 1", Day1.Sol1);
Run("Day1 - 2", Day1.Sol2);
Run("Day2 - 1", Day2.Sol1);
Run("Day2 - 2", Day2.Sol2);
Run("Day3 - 1", Day3.Sol1);
Run("Day3 - 2", Day3.Sol2);
Run("Day4 - 1", Day4.Sol1);
Run("Day4 - 2", Day4.Sol2);
Run("Day5 - 1", Day5.Sol1);
Run("Day5 - 2", Day5.Sol2);
Run("Day6 - 1", Day6.Sol1);
Run("Day6 - 2", Day6.Sol2);
Run("Day7 - 1", Day7.Sol1);
Run("Day7 - 2", Day7.Sol2);
Run("Day8 - 1", Day8.Sol1);
Run("Day8 - 2", Day8.Sol2);
Run("Day9 - 1", Day9.Sol1);
Run("Day9 - 2", Day9.Sol2);
Run("Day10- 1", Day10.Sol1);
Run("Day10- 2", Day10.Sol2);
Run("Day11- 1", Day11.Sol1);
Run("Day11- 2", Day11.Sol2);

static void Run(string name, Func<long> sol){
    var stopWatch = new Stopwatch();
    stopWatch.Start();
    var v = sol().ToString();
    stopWatch.Stop();
    Console.WriteLine($"{name.PadRight(8,' ')}: {v.PadRight(13,' ')} in {stopWatch.ElapsedMilliseconds.ToString().PadRight(2, ' ')} ms");
}

