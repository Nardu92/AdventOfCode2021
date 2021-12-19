using System.Drawing;
using Xunit;

namespace AdventOfCode2021Tests
{
    public class Day17Tests
    {
        [Theory]
        [InlineData(7,2, 20, 30, -5,-10, true)]
        [InlineData(6,3, 20, 30, -5,-10, true)]
        [InlineData(9,0, 20, 30, -5,-10, true)]
        [InlineData(17,-4, 20, 30, -5,-10, false)]
        public void TrajectoryTest(int ssx ,int ssy, int xRange1,int xRange2, int yRange1 ,int yRange2, bool expected){
            var t = Day17.IsTrajectoryValid(new Point(ssx, ssy), (xRange1,xRange2), (yRange1,yRange2));
            Assert.Equal(expected, t);
        }

        [Theory]
        [InlineData(7,2, 20, 30, -5,-10, false)]
        [InlineData(20,-5, 20, 30, -5,-10, true)]
        [InlineData(30,-10, 20, 30, -5,-10, true)]
        [InlineData(30,-5, 20, 30, -5,-10, true)]
        [InlineData(20,-10, 20, 30, -5,-10, true)]
        [InlineData(25,-10, 20, 30, -5,-10, true)]
        [InlineData(25,-7, 20, 30, -5,-10, true)]
        [InlineData(20,-7, 20, 30, -5,-10, true)]
        [InlineData(35,-7, 20, 30, -5,-10, false)]
        [InlineData(15,-7, 20, 30, -5,-10, false)]
        [InlineData(25,-3, 20, 30, -5,-10, false)]
        [InlineData(25,-13, 20, 30, -5,-10, false)]

        public void IsPositionInRange(int x, int y, int xRange1,int xRange2, int yRange1 ,int yRange2, bool expected){
            var t = Day17.IsPositionWithinRange(new Point(x, y), (xRange1,xRange2), (yRange1,yRange2));
            Assert.Equal(expected, t);
        }

        [Theory]
        [InlineData(2, 3)]
        [InlineData(4, 10)]
        [InlineData(6, 20)]
        [InlineData(11, 56)]
        [InlineData(100, 5050)]
        public void CalculateMinXSpeedTest(int expectedMin, int xStart ){
            var min = Day17.CalculateMinXSpeed(xStart);
            Assert.Equal(expectedMin, min);
        }
    }
}