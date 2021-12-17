using System.Drawing;
using System.Text.RegularExpressions;

public class Day17
{
    public static ((int Start, int End), (int Start, int End)) ReadInputFile(string filename)
    {
        using System.IO.StreamReader file = new System.IO.StreamReader(filename);
        string line;
        string text = file.ReadLine()!;

        var regexX = new Regex(@"x=(?<xrange>.*\.\..*), y=(?<yrange>.*\.\..*)");
        var groups = regexX.Match(text).Groups;
        var captureGroupX = groups["xrange"].Value.Split(".", System.StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var captureGroupY = groups["yrange"].Value.Split(".", System.StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        return ((captureGroupX[0], captureGroupX[1]), (captureGroupY[1], captureGroupY[0]));
    }


    public static long Sol1()
    {
        var ranges = ReadInputFile("Inputs/input17.txt");
        var speeds = GetValidStartingSpeeds(ranges.Item1, ranges.Item2, 1000, 0);
        return speeds.Select(x => GetHighestPoint(x.Y)).Max();
    }

    public static List<Point> GetValidStartingSpeeds((int Start, int End) xRange, (int Start, int End) yRange, int minY, int maxY)
    {
        var minX = CalculateMinXSpeed(xRange.Start);
        var maxX = minX * 10;

        List<Point> validSpeeds = new List<Point>();
        for (int xSpeed = minX; xSpeed < maxX; xSpeed++)
        {
            for (int ySpeed = minY; ySpeed > maxY; ySpeed--)
            {
                Point startingSpeed = new Point(xSpeed, ySpeed);
                var isValid = IsTrajectoryValid(startingSpeed, xRange, yRange);
                if (isValid)
                {
                    validSpeeds.Add(startingSpeed);
                }
            }
        }
        return validSpeeds;
    }

    public static int CalculateMinXSpeed(int xStart)
    {
        var min = 0;
        var totalX = 0;
        do
        {
            min++;
            totalX = min * (min + 1) / 2;
        } while (totalX < xStart);
        return min;
    }

    public static long Sol2()
    {
        var ranges = ReadInputFile("Inputs/input17.txt");
        return GetValidStartingSpeeds(ranges.Item1, ranges.Item2, 1000, -1000).Count;
    }

    public static bool IsTrajectoryValid(Point startingSpeed, (int Start, int End) xRange, (int Start, int End) yRange)
    {
        var currentPosition = new Point(0, 0);
        var currentSpeed = startingSpeed;
        while (true)
        {
            currentPosition.X += currentSpeed.X;
            currentPosition.Y += currentSpeed.Y;
            if (currentSpeed.X > 0)
            {
                currentSpeed.X--;
            }
            else if (currentSpeed.X < 0)
            {
                currentSpeed.X++;
            }
            currentSpeed.Y--;
            if (IsPositionWithinRange(currentPosition, xRange, yRange))
                return true;
            if (currentPosition.Y < yRange.End)
                return false;
            if (currentSpeed.X == 0 && currentPosition.X < xRange.Start)
                return false;
            if (currentSpeed.X > 0 && currentPosition.X > xRange.End)
                return false;
        }
    }

    public static long GetHighestPoint(int startingVerticalSpeed)
    {
        var verticalPosition = 0;
        var verticalSpeed = startingVerticalSpeed;
        var maxY = 0;
        while (true)
        {
            verticalPosition += verticalSpeed;
            if (maxY < verticalPosition)
            {
                maxY = verticalPosition;
            }
            verticalSpeed--;
            if(verticalSpeed < 0)
            {
                return maxY;
            }
        }
    }

    public static bool IsPositionWithinRange(Point position, (int Start, int End) xRange, (int Start, int End) yRange)
    {
        return position.X >= xRange.Start && position.X <= xRange.End && position.Y <= yRange.Start && position.Y >= yRange.End;
    }
}