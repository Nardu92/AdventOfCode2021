
using System.Drawing;

public class Day19
{


    public static List<Scanner> ReadInputFile(string filename)
    {
        using System.IO.StreamReader file = new System.IO.StreamReader(filename);
        string? line;
        List<Scanner> scanners = new List<Scanner>();
        var eof = false;
        do
        {
            var beacons = new List<Point3D>();
            line = file.ReadLine();
            var scannerId = Convert.ToInt32(line!.Split(' ')[2]);
            var sameScanner = true;
            while (sameScanner)
            {
                line = file.ReadLine();
                if (string.Empty.Equals(line))
                {
                    sameScanner = false;
                    break;
                }
                if (string.IsNullOrEmpty(line))
                {
                    eof = true;
                    break;
                }
                var pos = line.Split(",").Select(int.Parse).ToArray();
                var beaconPosition = new Point3D(pos[0], pos[1], pos[2]);
                beacons.Add(beaconPosition);
            }
            scanners.Add(new Scanner(scannerId, beacons));
        } while (!eof);
        return scanners;
    }

    public static long Sol1()
    {
        return 0;
    }


    public static long Sol2()
    {
        return 0;
    }

}

public class Scanner
{

    public List<Point3D> Beacons => BeaconsById.Values.ToList();

    public Dictionary<Guid, Point3D> BeaconsById { get; }

    public int Id { get; }

    public Dictionary<(Guid, Guid), double> DistancesById { get; }

    public Scanner(int id, List<Point3D> beacons)
    {
        BeaconsById = beacons.ToDictionary(b => b.Id);
        Id = id;
        DistancesById = new Dictionary<(Guid, Guid), double>();
    }

    public void CalculateDistancesBetweenPoints()
    {
        foreach (var beacon in Beacons)
        {
            foreach (var otherBeacon in Beacons)
            {
                if (otherBeacon == beacon)
                {
                    continue;
                }
                if (DistancesById.ContainsKey((beacon.Id, otherBeacon.Id))
                    || DistancesById.ContainsKey((otherBeacon.Id, beacon.Id)))
                {
                    continue;
                }
                else
                {
                    var distance = beacon.GetDistance(otherBeacon);
                    DistancesById.Add((beacon.Id, otherBeacon.Id), distance);
                }
            }
        }
    }


}
public class Point3D
{
    private static readonly int[,] x90matrix = new int[,] { { 1, 0, 0 }, { 0, 0, -1 }, { 0, 1, 0 } };
    private static readonly int[,] x180matrix = new int[,] { { 1, 0, 0 }, { 0, -1, 0 }, { 0, 0, -1 } };
    private static readonly int[,] x270matrix = new int[,] { { 1, 0, 0 }, { 0, 0, 1 }, { 0, -1, 0 } };

    private static readonly int[,] y90matrix = new int[,] { { 0, 0, 1 }, { 0, 1, 0 }, { -1, 0, 0 } };

    private static readonly int[,] y180matrix = new int[,] { { -1, 0, 0 }, { 0, 1, 0 }, { 0, 0, -1 } };

    private static readonly int[,] y270matrix = new int[,] { { 0, 0, -1 }, { 0, 1, 0 }, { 1, 0, 0 } };

    private static readonly int[,] z90matrix = new int[,] { { 0, -1, 0 }, { 1, 0, 0 }, { 0, 0, 1 } };

    private static readonly int[,] z180matrix = new int[,] { { -1, 0, 0 }, { 0, -1, 0 }, { 0, 0, 1 } };
    private static readonly int[,] z270matrix = new int[,] { { 0, 1, 0 }, { -1, 0, 0 }, { 0, 0, 1 } };


    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }

    public Guid Id { get; }

    public Point3D()
    {
        Id = Guid.NewGuid();
    }
    public Point3D(int x, int y, int z) : this()
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Point3D(string input) : this()
    {
        var i = input.Split(",").Select(int.Parse).ToArray();
        X = i[0];
        Y = i[1];
        Z = i[2];
    }

    public Point3D(int[] input) : this()
    {
        X = input[0];
        Y = input[1];
        Z = input[2];
    }

    public Point3D(Point3D p) : this()
    {
        X = p.X;
        Y = p.Y;
        Z = p.Z;
    }
    private void MultiplyForRotationMatrix(int[,] rotationMatrix)
    {
        var newX = X * rotationMatrix[0, 0] + Y * rotationMatrix[0, 1] + Z * rotationMatrix[0, 2];
        var newY = X * rotationMatrix[1, 0] + Y * rotationMatrix[1, 1] + Z * rotationMatrix[1, 2];
        var newZ = X * rotationMatrix[2, 0] + Y * rotationMatrix[2, 1] + Z * rotationMatrix[2, 2];
        X = newX;
        Y = newY;
        Z = newZ;
    }
    public void RotateX(int rotations90)
    {
        int[,] rotationMatrix;
        if (rotations90 == 1)
        {
            rotationMatrix = x90matrix;
        }
        else if (rotations90 == 2)
        {
            rotationMatrix = x180matrix;
        }
        else if (rotations90 == 3)
        {
            rotationMatrix = x270matrix;
        }
        else throw new System.Exception("Invalid rotation");

        MultiplyForRotationMatrix(rotationMatrix);
    }

    public void RotateY(int rotation90)
    {
        int[,] rotationMatrix;
        if (rotation90 == 1)
        {
            rotationMatrix = y90matrix;
        }
        else if (rotation90 == 2)
        {
            rotationMatrix = y180matrix;
        }
        else if (rotation90 == 3)
        {
            rotationMatrix = y270matrix;
        }
        else throw new System.Exception("Invalid rotation");

        MultiplyForRotationMatrix(rotationMatrix);
    }

    public void RotateZ(int rotation90)
    {
        int[,] rotationMatrix;
        if (rotation90 == 1)
        {
            rotationMatrix = z90matrix;
        }
        else if (rotation90 == 2)
        {
            rotationMatrix = z180matrix;
        }
        else if (rotation90 == 3)
        {
            rotationMatrix = z270matrix;
        }
        else throw new System.Exception("Invalid rotation");

        MultiplyForRotationMatrix(rotationMatrix);
    }

    public void Rotate(int axis, int rotations90)
    {
        switch (axis)
        {
            case 0:
                RotateX(rotations90);
                break;
            case 1:
                RotateY(rotations90);
                break;
            case 2:
                RotateZ(rotations90);
                break;
            default:
                throw new System.Exception("Invalid axis");
        }
    }

    public (int xr, int yr, int zr) FindRotation(Point3D point)
    {
        if (Equals(point))
        {
            return (0, 0, 0);
        }

        //y1, y2, y3
        for (int yrotations = 1; yrotations < 4; yrotations++)
        {
            var p = new Point3D(X, Y, Z);
            p.Rotate(1, yrotations);
            if (point.Equals(p))
            {
                return (0, yrotations, 0);
            }
        }
        // z1, z1y1, z1y2, z1y3
        var pz1 = new Point3D(X, Y, Z);
        pz1.Rotate(2, 1);
        if (point.Equals(pz1))
        {
            return (0, 0, 1);
        }

        for (int yrotations = 1; yrotations < 4; yrotations++)
        {
            var p = new Point3D(X, Y, Z);
            p.Rotate(2, 1);
            p.Rotate(1, yrotations);
            if (point.Equals(p))
            {
                return (0, yrotations, 1);
            }
        }
        // z3, z3y1, z3y2, z3y3
        var pz3 = new Point3D(X, Y, Z);
        pz3.Rotate(2, 3);
        if (point.Equals(pz1))
        {
            return (0, 0, 3);
        }
        for (int yrotations = 1; yrotations < 4; yrotations++)
        {
            var p = new Point3D(X, Y, Z);

            p.Rotate(2, 3);
            p.Rotate(1, yrotations);
            if (point.Equals(p))
            {
                return (0, yrotations, 3);
            }
        }
        // x1, x1y1, x1y2, x1y3
        var px1 = new Point3D(X, Y, Z);
        px1.Rotate(0, 1);
        if (point.Equals(px1))
        {
            return (1, 0, 0);
        }

        for (int yrotations = 1; yrotations < 4; yrotations++)
        {
            var p = new Point3D(X, Y, Z);
            p.Rotate(0, 1);
            p.Rotate(1, yrotations);
            if (point.Equals(p))
            {
                return (1, yrotations, 0);
            }
        }

        //x2, x2y1, x2y2, x2y3
        var px2 = new Point3D(X, Y, Z);
        px2.Rotate(0, 2);
        if (point.Equals(px2))
        {
            return (2, 0, 0);
        }

        for (int yrotations = 1; yrotations < 4; yrotations++)
        {
            var p = new Point3D(X, Y, Z);
            p.Rotate(0, 2);
            p.Rotate(1, yrotations);
            if (point.Equals(p))
            {
                return (2, yrotations, 0);
            }
        }

        //x3, x3y1, x3y2, x3y3
        var px3 = new Point3D(X, Y, Z);
        px3.Rotate(0, 3);
        if (point.Equals(px3))
        {
            return (3, 0, 0);
        }

        for (int yrotations = 1; yrotations < 4; yrotations++)
        {
            var p = new Point3D(X, Y, Z);
            p.Rotate(0, 3);
            p.Rotate(1, yrotations);
            if (point.Equals(p))
            {
                return (3, yrotations, 0);
            }
        }
        return (-1, -1, -1);
    }

    public double GetDistance(Point3D point)
    {
        return Math.Sqrt(Math.Pow(point.X - X, 2) + Math.Pow(point.Y - Y, 2) + Math.Pow(point.Z - Z, 2));
    }

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Point3D p)
        {
            return X == p.X && Y == p.Y && Z == p.Z;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
    }
}