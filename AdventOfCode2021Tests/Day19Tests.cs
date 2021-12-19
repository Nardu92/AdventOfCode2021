using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Tests
{
    public class Day19Tests
    {
        [Theory]
        [InlineData(1, 1, 1, 1, 1, -1, 1)]
        [InlineData(10, -1, 6, 1, 10, -6, -1)]
        [InlineData(10, -1, 6, 2, 10, 1, -6)]
        [InlineData(10, -1, 6, 3, 10, 6, 1)]

        public void TestXRotations(int x, int y, int z, int rotations, int expectedX, int expectedY, int expectedZ)
        {
            var p = new Point3D(x, y, z);
            p.RotateX(rotations);
            Assert.Equal(expectedX, p.X);
            Assert.Equal(expectedY, p.Y);
            Assert.Equal(expectedZ, p.Z);
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 1, 1, -1)]
        [InlineData(10, -1, 6, 1, 6, -1, -10)]
        [InlineData(10, -1, 6, 2, -10, -1, -6)]
        [InlineData(10, -1, 6, 3, -6, -1, 10)]

        public void TestYRotations(int x, int y, int z, int rotations, int expectedX, int expectedY, int expectedZ)
        {
            var p = new Point3D(x, y, z);
            p.RotateY(rotations);
            Assert.Equal(expectedX, p.X);
            Assert.Equal(expectedY, p.Y);
            Assert.Equal(expectedZ, p.Z);
        }


        [Theory]
        [InlineData(1, 1, 1, 1, -1, 1, 1)]
        [InlineData(10, -1, 6, 1, 1, 10, 6)]
        [InlineData(10, -1, 6, 2, -10, 1, 6)]
        [InlineData(10, -1, 6, 3, -1, -10, 6)]

        public void TestZRotations(int x, int y, int z, int rotations, int expectedX, int expectedY, int expectedZ)
        {
            var p = new Point3D(x, y, z);
            p.RotateZ(rotations);
            Assert.Equal(expectedX, p.X);
            Assert.Equal(expectedY, p.Y);
            Assert.Equal(expectedZ, p.Z);
        }

        [Theory]
        [InlineData(10, -1, 6, 0, 1, 10, -6, -1)]
        [InlineData(10, -1, 6, 0, 2, 10, 1, -6)]
        [InlineData(10, -1, 6, 0, 3, 10, 6, 1)]
        [InlineData(10, -1, 6, 1, 1, 6, -1, -10)]
        [InlineData(10, -1, 6, 1, 2, -10, -1, -6)]
        [InlineData(10, -1, 6, 1, 3, -6, -1, 10)]
        [InlineData(10, -1, 6, 2, 1, 1, 10, 6)]
        [InlineData(10, -1, 6, 2, 2, -10, 1, 6)]
        [InlineData(10, -1, 6, 2, 3, -1, -10, 6)]

        public void TestRotations(int x, int y, int z, int axes, int rotations, int expectedX, int expectedY, int expectedZ)
        {
            var p = new Point3D(x, y, z);
            p.Rotate(axes, rotations);
            Assert.Equal(expectedX, p.X);
            Assert.Equal(expectedY, p.Y);
            Assert.Equal(expectedZ, p.Z);
        }

        [Theory]
        [InlineData(10, -1, 6, 2, 1, 1, 1, 6, 10, -1)]
        [InlineData(10, -1, 6, 2, 1, 1, 2, -1, 10, -6)]
        [InlineData(10, -1, 6, 2, 1, 1, 3, -6, 10, 1)]

        [InlineData(10, -1, 6, 2, 3, 1, 1, 6, -10, 1)]
        [InlineData(10, -1, 6, 2, 3, 1, 2, 1, -10, -6)]
        [InlineData(10, -1, 6, 2, 3, 1, 3, -6, -10, -1)]

        [InlineData(10, -1, 6, 0, 1, 1, 1, -1, -6, -10)]
        [InlineData(10, -1, 6, 0, 1, 1, 2, -10, -6, 1)]
        [InlineData(10, -1, 6, 0, 1, 1, 3, 1, -6, 10)]

        [InlineData(10, -1, 6, 0, 2, 1, 1, -6, 1, -10)]
        [InlineData(10, -1, 6, 0, 2, 1, 2, -10, 1, 6)]
        [InlineData(10, -1, 6, 0, 2, 1, 3, 6, 1, 10)]

        [InlineData(10, -1, 6, 0, 3, 1, 1, 1, 6, -10)]
        [InlineData(10, -1, 6, 0, 3, 1, 2, -10, 6, -1)]
        [InlineData(10, -1, 6, 0, 3, 1, 3, -1, 6, 10)]
        public void TestMultipleRotation(int x, int y, int z, int axes1, int rotations1, int axes2, int rotations2, int expectedX, int expectedY, int expectedZ)
        {
            var p = new Point3D(x, y, z);
            p.Rotate(axes1, rotations1);
            p.Rotate(axes2, rotations2);
            Assert.Equal(expectedX, p.X);
            Assert.Equal(expectedY, p.Y);
            Assert.Equal(expectedZ, p.Z);
        }

        [Fact]
        public void TestAllRotations()
        {
            int x = 5;
            int y = 6;
            int z = -4;
            var allRotations = new List<Point3D>();
            //y0, y1, y2, y3
            allRotations.Add(new Point3D(x, y, z));
            for (int yrotations = 1; yrotations < 4; yrotations++)
            {
                var p = new Point3D(x, y, z);
                p.Rotate(1, yrotations);
                allRotations.Add(p);
            }
            // z1, z1y1, z1y2, z1y3
            var pz1 = new Point3D(x, y, z);
            pz1.Rotate(2, 1);
            allRotations.Add(pz1);
            for (int yrotations = 1; yrotations < 4; yrotations++)
            {
                var p = new Point3D(x, y, z);
                p.Rotate(1, yrotations);
                p.Rotate(2, 1);
                allRotations.Add(p);
            }
            // z3, z3y1, z3y2, z3y3
            var pz3 = new Point3D(x, y, z);
            pz3.Rotate(2, 3);
            allRotations.Add(pz3);
            for (int yrotations = 1; yrotations < 4; yrotations++)
            {
                var p = new Point3D(x, y, z);
                p.Rotate(1, yrotations);
                p.Rotate(2, 3);
                allRotations.Add(p);
            }
            // x1, x1y1, x1y2, x1y3
            var px1 = new Point3D(x, y, z);
            px1.Rotate(0, 1);
            allRotations.Add(px1);
            for (int yrotations = 1; yrotations < 4; yrotations++)
            {
                var p = new Point3D(x, y, z);
                p.Rotate(1, yrotations);
                p.Rotate(0, 1);
                allRotations.Add(p);
            }
            //x2, x2y1, x2y2, x2y3
            var px2 = new Point3D(x, y, z);
            px2.Rotate(0, 2);
            allRotations.Add(px2);
            for (int yrotations = 1; yrotations < 4; yrotations++)
            {
                var p = new Point3D(x, y, z);
                p.Rotate(1, yrotations);
                p.Rotate(0, 2);
                allRotations.Add(p);
            }
            //x3, x3y1, x3y2, x3y3
            var px3 = new Point3D(x, y, z);
            px3.Rotate(0, 3);
            allRotations.Add(px3);
            for (int yrotations = 1; yrotations < 4; yrotations++)
            {
                var p = new Point3D(x, y, z);
                p.Rotate(1, yrotations);
                p.Rotate(0, 3);
                allRotations.Add(p);
            }

            Assert.Equal(24, allRotations.Count);
            Assert.Contains(new Point3D(-5, 4, -6), allRotations);
            Assert.Contains(new Point3D(4, 6, 5), allRotations);
            Assert.Contains(new Point3D(-4, -6, 5), allRotations);
            Assert.Contains(new Point3D(-6, -4, -5), allRotations);
        }

        [Theory]
        [InlineData(10, -1, 6, 10, 6, 1, 3, 0, 0)]
        [InlineData(10, -1, 6, 1, 6, -10, 3, 1, 0)]
        [InlineData(1, 1, 1, 1, 1, 1, 0, 0, 0)]
        [InlineData(1, 1, 1, 2, 2, 2, -1, -1, -1)]
        public void FindCorrectRotation(int xi, int yi, int zi, int xf, int yf, int zf, int xr, int xy, int xz)
        {
            var initialPoint = new Point3D(xi, yi, zi);
            var finalPoint = new Point3D(xf, yf, zf);

            var rotation = initialPoint.FindRotation(finalPoint);
            Assert.Equal(xr, rotation.xr);
            Assert.Equal(xy, rotation.yr);
            Assert.Equal(xz, rotation.zr);
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 1, 1, true)]
        [InlineData(1, 1, 1, 1, 2, 1, false)]
        public void EqualsTest(int xi, int yi, int zi, int xf, int yf, int zf, bool expected)
        {
            var initialPoint = new Point3D(xi, yi, zi);
            var finalPoint = new Point3D(xf, yf, zf);

            var result = initialPoint.Equals(finalPoint);
            Assert.Equal(expected, result);

        }

        [Fact]
        public void ReadInputTest()
        {
            var input = Day19.ReadInputFile("Inputs/input19e.txt");
            Assert.Equal(5, input.Count);
            Assert.Equal(6, input[0].Beacons.Count);
        }


        [Fact]
        public void CalculateDistancesTest()
        {
            var scanners = Day19.ReadInputFile("Inputs/input19e2.txt");
            var s = scanners.First();
            s.CalculateDistancesBetweenPoints();

            var otherScanner = scanners[1];
            otherScanner.CalculateDistancesBetweenPoints();

            List<(Guid,Guid,Guid,Guid)> pointsToVerify = new List<(Guid,Guid,Guid,Guid)>();

            foreach (var ((id1, id2), distance1) in s.DistancesById)
            {
                foreach (var ((id3, id4), distance2) in otherScanner.DistancesById)
                {
                    if(System.Math.Abs(distance1 - distance2) < 0.00001)
                    {
                        pointsToVerify.Add((id1, id2, id3, id4));
                    }
                }
            }

            foreach(var pointToVerify in pointsToVerify){
                var point1 = s.BeaconsById[pointToVerify.Item1];
                var point2 = s.BeaconsById[pointToVerify.Item2];
                var point3 = otherScanner.BeaconsById[pointToVerify.Item3];
                var point4 = otherScanner.BeaconsById[pointToVerify.Item4];

            }
        }
    }
}