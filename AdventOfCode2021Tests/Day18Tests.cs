using System.Drawing;
using Xunit;

namespace AdventOfCode2021Tests
{
    public class Day18Tests
    {

        [InlineData("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
        [InlineData("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
        [InlineData("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
        [InlineData("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
        [InlineData("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
        public void ExplodeOneTest(string input, string expected){
            var tupla = new SFMTuple(input, null);
            tupla.ExplodeOne();
            Assert.Equal(expected, tupla.ToString());
        }

        [InlineData("10", "[5,5]")]
        [InlineData("11", "[5,6]")]
        [InlineData("12", "[6,6]")]
        public void SplitOneTest(string input, string expected){
            var tupla = new SFMTuple(input, null);
            tupla.SplitOne();
            Assert.Equal(expected, tupla.ToString());
        }

    }
}