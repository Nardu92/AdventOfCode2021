using System;
using Xunit;

namespace AdventOfCode2021Tests
{
    public class Day16Tests
    {
        [Theory]
        [InlineData("8", "1000")]
        [InlineData("FA", "11111010")]
        [InlineData("D2FE28", "110100101111111000101000")]
        [InlineData("38006F45291200", "00111000000000000110111101000101001010010001001000000000")]
        [InlineData("EE00D40C823060", "11101110000000001101010000001100100000100011000001100000")]
        public void ConvertToBinaryTest(string input, string expectedOutput)
        {
            var binary = Day16.ConvertToBinary(input);
            Assert.Equal(expectedOutput, binary);
        }

        [Theory]
        [InlineData("110100101111111000101000", 6, 4, 2021, 21, "110100101111111000101")]
        [InlineData("11010001010", 6, 4, 10, 11, "11010001010")]
        [InlineData("0101001000100100", 2, 4, 20, 16, "0101001000100100")]
        [InlineData("01010000001", 2, 4, 1, 11, "01010000001")]
        [InlineData("10010000010", 4, 4, 2, 11, "10010000010")]
        [InlineData("00110000011", 1, 4, 3, 11, "00110000011")]
        public void CreateLiteralPacketTest(string input, int expectedVersion, int expectedTypeId, long expectedLiteralValue, int transmissionLength, string transmissionWithoutTrailingZeros)
        {
            var packet = new Packet(input);
            Assert.Equal(expectedVersion, packet.Version);
            Assert.Equal(expectedTypeId, packet.TypeId);
            Assert.Equal(expectedLiteralValue, packet.LiteralValue);
            Assert.Equal(transmissionLength, packet.Transmission.Length);
            Assert.Equal(transmissionWithoutTrailingZeros, packet.Transmission);
        }


        [Fact]
        public void CreateComplexPacketAndSubPacketsLenghtTypeZero()
        {
            string input = "00111000000000000110111101000101001010010001001000000000";
            int expectedVersion = 1;
            int expectedTypeId = 6;
            bool lengthTypeId = false;
            int transmissionLength = 49;
            string transmissionWithoutTrailingZeros = "0011100000000000011011110100010100101001000100100";

            var packet = new Packet(input);
            Assert.Equal(expectedVersion, packet.Version);
            Assert.Equal(expectedTypeId, packet.TypeId);
            Assert.Equal(lengthTypeId, packet.LengthTypeID);
            Assert.Equal(0, packet.LiteralValue);
            Assert.Equal(transmissionLength, packet.Transmission.Length);
            Assert.Equal(transmissionWithoutTrailingZeros, packet.Transmission);

            Assert.Equal(2, packet.SubPackets.Count);
            Assert.Equal(10, packet.SubPackets[0].LiteralValue);
            Assert.Equal(20, packet.SubPackets[1].LiteralValue);
            Assert.Equal("11010001010", packet.SubPackets[0].Transmission);
            Assert.Equal("0101001000100100", packet.SubPackets[1].Transmission);
        }

        [Fact]
        public void CreateComplexPacketAndSubPacketsLenghtTypeOne()
        {
            string input = "11101110000000001101010000001100100000100011000001100000";
            int expectedVersion = 7;
            int expectedTypeId = 3;
            bool lengthTypeId = true;
            int transmissionLength = 51;
            string transmissionWithoutTrailingZeros = "111011100000000011010100000011001000001000110000011";

            var packet = new Packet(input);
            Assert.Equal(expectedVersion, packet.Version);
            Assert.Equal(expectedTypeId, packet.TypeId);
            Assert.Equal(lengthTypeId, packet.LengthTypeID);
            Assert.Equal(0, packet.LiteralValue);
            Assert.Equal(3, packet.NumberOfSubPackets);
            Assert.Equal(transmissionLength, packet.Transmission.Length);
            Assert.Equal(transmissionWithoutTrailingZeros, packet.Transmission);

            Assert.Equal(3, packet.SubPackets.Count);
            Assert.Equal(1, packet.SubPackets[0].LiteralValue);
            Assert.Equal(2, packet.SubPackets[1].LiteralValue);
            Assert.Equal(3, packet.SubPackets[2].LiteralValue);
            Assert.Equal("01010000001", packet.SubPackets[0].Transmission);
            Assert.Equal("10010000010", packet.SubPackets[1].Transmission);
            Assert.Equal("00110000011", packet.SubPackets[2].Transmission);
        }

        [Theory]
        [InlineData("8A004A801A8002F478", 16)]
        [InlineData("620080001611562C8802118E34", 12)]
        [InlineData("C0015000016115A2E0802F182340", 23)]
        [InlineData("A0016C880162017C3686B18A3D4780", 31)]
        public void GetVersionsTest(string input, int expectedVersion)
        {
            var version = Day16.GetVersions(input);
            Assert.Equal(expectedVersion, version);
        }

        [Theory]
        [InlineData("C200B40A82", 3, 0)]
        [InlineData("04005AC33890", 54, 1)]
        [InlineData("880086C3E88112", 7, 2)]
        [InlineData("CE00C43D881120", 9, 3)]
        [InlineData("D8005AC2A8F0", 1, 6)]
        [InlineData("F600BC2D8F", 0, 5)]
        [InlineData("9C005AC2F8F0", 0, 7)]
        [InlineData("9C0141080250320F1802104A08", 1, 7)]
        public void GetValueTest(string input, long value, int expectedTypeId)
        {
            var binary = Day16.ConvertToBinary(input);
            var packet = new Packet(binary);
            Assert.Equal(expectedTypeId, packet.TypeId);
            Assert.Equal(value, packet.GetValue());
        }
    }
}
