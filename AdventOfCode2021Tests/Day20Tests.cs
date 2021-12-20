using Xunit;

namespace AdventOfCode2021Tests
{
    public class Day20Tests
    {
        [Theory]
        [InlineData(2,2, 34, true)]
        [InlineData(3,2, 275, false)]
        [InlineData(2,1, 305, true)]
       
        public void GetPixelValueTest(int y ,int x, int expectedValue,bool enhancedValueExpected){
            y +=4;
            x +=4;
            var enahncer= Day20.ReadInputFile("Inputs/input20e.txt");
            var value = enahncer.GetPixelValue(x, y);
            Assert.Equal(expectedValue, value);
            var enhanced = enahncer.Decoder[value];
            Assert.Equal(enhancedValueExpected, enhanced);
        }
    }
}