using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Tests
{
    public class Day24Tests
    {
        [Theory]
        [InlineData(3, 0, 0, 1, 1)]
        [InlineData(7, 0, 1, 1, 1)]
        [InlineData(8, 1, 0, 0, 0)]
        [InlineData(15, 1, 1, 1, 1)]
        [InlineData(12, 1, 1, 0, 0)]

        public void ToBinary(int input, int w, int x, int y, int z)
        {
            var commands = Day24.ReadInputFile("Inputs/input24e.txt");
            var inputCommand = commands.First();
            inputCommand.B = input.ToString();
            var memory = new Dictionary<string, long>();
            foreach (var command in commands)
            {
                command.Execute(memory);
            }
            Assert.Equal(memory["z"], z);
            Assert.Equal(memory["y"], y);
            Assert.Equal(memory["x"], x);
            Assert.Equal(memory["w"], w);
        }
    }
}