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
Run("Day12- 1", Day12.Sol1);
Run("Day12- 2", Day12.Sol2);
Run("Day13- 1", Day13.Sol1);
Run("Day13- 2", Day13.Sol2);
Run("Day14- 1", Day14.Sol1);
Run("Day14- 2", Day14.Sol2);
Run("Day15- 1", Day15.Sol1);
Run("Day15- 2", Day15.Sol2);
Run("Day16- 1", Day16.Sol1);
Run("Day16- 2", Day16.Sol2);
Run("Day17- 1", Day17.Sol1);
Run("Day17- 2", Day17.Sol2);
Run("Day18- 1", Day18.Sol1);
Run("Day18- 2", Day18.Sol2);
Run("Day19- 1", Day19.Sol1);
Run("Day19- 2", Day19.Sol2);
Run("Day20- 1", Day20.Sol1);
Run("Day20- 2", Day20.Sol2);
Run("Day21- 1", Day21.Sol1);
Run("Day21- 2", Day21.Sol2);
Run("Day22- 1", Day22.Sol1);
Run("Day22- 2", Day22.Sol2);
// Run("Day23- 1", Day23.Sol1);
// Run("Day23- 2", Day23.Sol2);
// Run("Day24- 1", Day24.Sol1);
// Run("Day24- 2", Day24.Sol2);
Run("Day25- 1", Day25.Sol1);

static void Run(string name, Func<long> sol){
    var stopWatch = new Stopwatch();
    stopWatch.Start();
    var v = sol().ToString();
    stopWatch.Stop();
    Console.WriteLine($"{name.PadRight(8,' ')}: {v.PadRight(13,' ')} in {stopWatch.ElapsedMilliseconds.ToString().PadRight(2, ' ')} ms");
}

