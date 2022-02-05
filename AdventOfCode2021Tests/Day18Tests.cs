using System.Drawing;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Tests
{
    public class Day18Tests
    {
        [Theory]
        [InlineData("[1,2]", 2)]
        [InlineData("[[1,2],3]", 6)]
        [InlineData("[9,[8,7]]", 2)]
        [InlineData("[[1,9],[8,5]]", 6)]
        [InlineData("[[[[1,2],[3,4]],[[5,6],[7,8]]],9]", 30)]
        [InlineData("[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]", 22)]
        [InlineData("[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]", 30)]
        [InlineData("[[[[1,3],[5,3]],[[1,3],[8,7]]][[[4,9],[6,9]],[[8,2],[7,3]]]]", -1)]
        public void FindCommaTest(string input, int expectedIndex)
        {
            var index = SnailMath.FindComma(input);
            Assert.Equal(expectedIndex, index);
        }

        [Fact]
        public void SFMTupleSimpleConstrTest()
        {
            var t = new SnailMath("[1,2]");
            Assert.Equal(1, t.Left!.Value);
            Assert.Equal(2, t.Right!.Value);
        }


        [Fact]
        public void SFMTupleSimpleConstrTest2()
        {
            var t = new SnailMath("[[1,2],3]");
            Assert.Equal(1, t.Left!.Left!.Value);
            Assert.Equal(2, t.Left!.Right!.Value);
            Assert.Equal(3, t.Right!.Value);
        }

        [Fact]
        public void SFMTupleSimpleConstrTest3()
        {
            var t = new SnailMath("[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]");
            Assert.Equal(1, t.Left!.Left!.Left!.Left!.Value);
            Assert.Equal(3, t.Left!.Left!.Left!.Right!.Value);
            Assert.Equal(5, t.Left!.Left!.Right!.Left!.Value);
            Assert.Equal(3, t.Left!.Left!.Right!.Right!.Value);
            Assert.Equal(3, t.Right!.Right!.Right!.Right!.Value);
        }


        [Theory]
        [InlineData("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
        [InlineData("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
        [InlineData("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
        [InlineData("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
        [InlineData("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
        [InlineData("[[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]", "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]")]
        [InlineData("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
        [InlineData("[[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]],[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]]", "[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]")]
        [InlineData("[[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]],[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]]", "[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]]")]
        [InlineData("[[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]", "[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]")]
        [InlineData("[[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]],[[2,[2,2]],[8,[8,1]]]]", "[[[[6,6],[6,6]],[[6,0],[6,7]]],[[[7,7],[8,9]],[8,[8,1]]]]")]
        [InlineData("[[[[[6,6],[6,6]],[[6,0],[6,7]]],[[[7,7],[8,9]],[8,[8,1]]]],[2,9]]", "[[[[6,6],[7,7]],[[0,7],[7,7]]],[[[5,5],[5,6]],9]]")]
        [InlineData("[[[[[6,6],[7,7]],[[0,7],[7,7]]],[[[5,5],[5,6]],9]],[1,[[[9,3],9],[[9,0],[0,7]]]]]", "[[[[7,8],[6,7]],[[6,8],[0,8]]],[[[7,7],[5,0]],[[5,5],[5,6]]]]")]
        [InlineData("[[[[[7,8],[6,7]],[[6,8],[0,8]]],[[[7,7],[5,0]],[[5,5],[5,6]]]],[[[5,[7,4]],7],1]]", "[[[[7,7],[7,7]],[[8,7],[8,7]]],[[[7,0],[7,7]],9]]")]
        [InlineData("[[[[[7,7],[7,7]],[[8,7],[8,7]]],[[[7,0],[7,7]],9]],[[[[4,2],2],6],[8,7]]]", "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")]
        public void SnailTreeExplodeTest(string input, string expected)
        {
            var snailTree = new SnailTree(input);
            snailTree.Reduce();
            var reduced = new SnailTree(expected);
            Assert.Equal(reduced.ToString(), snailTree.ToString());

        }

        [Fact]
        public void SnailTreeSplitTest()
        {
            var snailTree = new SnailTree("[[[[0,7],4],[15,[0,13]]],[1,1]]");
            snailTree.Split(snailTree.Root.Left.Right.Left);
            Assert.Equal(7, snailTree.Root.Left.Right.Left.Left.Value);
            Assert.Equal(8, snailTree.Root.Left.Right.Left.Right.Value);
        }

        [Fact]
        public void SnailMathToSplitTest()
        {
            var snailNode = new SnailMath("[[[[0,7],4],[15,[0,13]]],[1,1]]");
            Assert.Equal(true, snailNode.Left.Right.Left.ToSplit);
        }

        [Fact]
        public void SnailMathDepthTest()
        {
            var snailMath = new SnailMath("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
            Assert.Equal(4, snailMath.Right.Right.Right.Right.Depth);
            Assert.Equal(true, snailMath.Right.Right.Right.Right.Right.ToExplode);
        }

        [Fact]
        public void SnailMathToExplodePropTest()
        {
            var snailMath = new SnailMath("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
        }

        [Fact]
        public void AddTest()
        {
            var snailTree = new SnailTree("[1,1]");
            snailTree.Add("[2,2]");
            snailTree.Add("[3,3]");
            snailTree.Add("[4,4]");
            var reduced = new SnailTree("[[[[1,1],[2,2]],[3,3]],[4,4]]");
            Assert.Equal(reduced.OrderedSnailMath.Select(x => x.Value), snailTree.OrderedSnailMath.Select(x => x.Value));
        }

        [Fact]
        public void AddTest2()
        {
            var snailTree = new SnailTree("[1,1]");
            snailTree.Add("[2,2]");
            snailTree.Add("[3,3]");
            snailTree.Add("[4,4]");
            snailTree.Add("[5,5]");
            snailTree.Add("[6,6]");
            var reduced = new SnailTree("[[[[5,0],[7,4]],[5,5]],[6,6]]");
            Assert.Equal(reduced.OrderedSnailMath.Select(x => x.Value), snailTree.OrderedSnailMath.Select(x => x.Value));
        }


        [Fact]
        public void AddTest3()
        {
            var snailTree = new SnailTree("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]");
            snailTree.Add("[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]");
            var reduced = "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]";
            Assert.Equal(reduced, snailTree.ToString());

            snailTree.Add("[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]");
            reduced = "[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]";
            Assert.Equal(reduced, snailTree.ToString());

            snailTree.Add("[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]");
            reduced = "[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]]";
            Assert.Equal(reduced, snailTree.ToString());

            snailTree.Add("[7,[5,[[3,8],[1,4]]]]");
            reduced = "[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]";
            Assert.Equal(reduced, snailTree.ToString());

            snailTree.Add("[[2,[2,2]],[8,[8,1]]]");
            reduced = "[[[[6,6],[6,6]],[[6,0],[6,7]]],[[[7,7],[8,9]],[8,[8,1]]]]";
            Assert.Equal(reduced, snailTree.ToString());

            snailTree.Add("[2,9]");
            reduced = "[[[[6,6],[7,7]],[[0,7],[7,7]]],[[[5,5],[5,6]],9]]";
            Assert.Equal(reduced, snailTree.ToString());

            snailTree.Add("[1,[[[9,3],9],[[9,0],[0,7]]]]");
            reduced = "[[[[7,8],[6,7]],[[6,8],[0,8]]],[[[7,7],[5,0]],[[5,5],[5,6]]]]";
            Assert.Equal(reduced, snailTree.ToString());

            snailTree.Add("[[[5,[7,4]],7],1]");
            reduced = "[[[[7,7],[7,7]],[[8,7],[8,7]]],[[[7,0],[7,7]],9]]";
            Assert.Equal(reduced, snailTree.ToString());

            snailTree.Add("[[[[4,2],2],6],[8,7]]");
            reduced = "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]";
            Assert.Equal(reduced, snailTree.ToString());
        }


        [Theory]
        [InlineData("[[1,2],[[3,4],5]]", 143)]
        [InlineData("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
        [InlineData("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
        [InlineData("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
        [InlineData("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
        [InlineData("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
        [InlineData("[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]", 4140)]
        public void MagnitudeTest(string input, int expected)
        {
            var snailMath = new SnailMath(input);
            Assert.Equal(expected, snailMath.GetMagnitude());
        }

        
        [Theory]
        [InlineData("input18e.txt", "[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]")]
        public void SumAllTest(string inputFile, string expectedResult){
            var inputs = Day18.ReadInputFile("Inputs\\" + inputFile);
            var result = Day18.SumAllMath(inputs);
            Assert.Equal(expectedResult, result.ToString());
        }
        
        [Theory]
        [InlineData("input18e.txt", 3993)]
        public void FindMaxMagnitudeTest(string inputFile, long expectedResult){
            var inputs = Day18.ReadInputFile("Inputs\\" + inputFile);
            var result = Day18.FindHighestPair(inputs);
            Assert.Equal(expectedResult, result);
        }
    }
}