using System;
using Xunit;
 
namespace AdventOfCode2021Tests
{
    public class Day16Tests
    {
        [Fact]
        public void ReadInputTest()
        {
            var input = Day16.ReadInputFile("Inputs/input16e.txt");
            Assert.Equal(input, "D2FE28");
        }

        [Theory]
        [InlineData("8", "1000")]
        [InlineData("FA", "11111010")]
        [InlineData("D2FE28", "110100101111111000101000")]
        [InlineData("38006F45291200", "00111000000000000110111101000101001010010001001000000000")]
        public void ConvertToBinaryTest(string input, string expectedOutput)
        {
            var binary = Day16.ConvertToBinary(input);
            Assert.Equal(expectedOutput, binary);
        }

        [Theory]
        [InlineData("110100101111111000101000", 6, 4, 2021, 21)]
        public void CreateLiteralPacketTest(string input, int expectedVersion,int expectedTypeId, long expectedLiteralValue, int transmissionLength)
        {
            var packet = new Packet(input);
            Assert.Equal(expectedVersion, packet.Version);
            Assert.Equal(expectedTypeId, packet.TypeId);
            Assert.Equal(true, packet.IsLiteralType);
            Assert.Equal(expectedLiteralValue, packet.LiteralValue);
            Assert.Equal(transmissionLength, packet.TransmissionLength);
            Assert.Equal(input, packet.Transmission);
        }

        [Theory]
        [InlineData("00111000000000000110111101000101001010010001001000000000", 1, 6, false, 27, 49)]
        public void CreateComplexPacketTest(string input, int expectedVersion,int expectedTypeId, bool lengthTypeId, int totalLengthInBits, int transmissionLength )
        {
            var packet = new Packet(input);
            Assert.Equal(expectedVersion, packet.Version);
            Assert.Equal(expectedTypeId, packet.TypeId);
            Assert.Equal(false, packet.IsLiteralType);
            Assert.Equal(0, packet.LiteralValue);
            Assert.Equal(totalLengthInBits, packet.TotalLengthInBits);
            Assert.Equal(transmissionLength, packet.TransmissionLength);
            Assert.Equal(input, packet.Transmission);
            packet.GetSubPackets();
        }

        [Fact]
        public void Sol1Test()
        {
            var input = Day16.ReadInputFile("Inputs/input16e.txt");
            Assert.Equal(Day16.Sol1(), 27);
        }
    }
}
