using System.Text;

public class Day21
{
    public static (int p1, int p2) ReadInputFile(string filename)
    {
        using var file = new System.IO.StreamReader(filename);
        var p1 = Convert.ToInt32(file.ReadLine()!.Split(' ').Last());
        var p2 = Convert.ToInt32(file.ReadLine()!.Split(' ').Last());
        return (p1, p2);
    }

    public static long Sol1()
    {
        var (pos1, pos2) = ReadInputFile("Inputs\\input21.txt");

        var dice = new DeterministicDice();
        var player1 = new PlayerPosition(pos1, dice);
        var player2 = new PlayerPosition(pos2, dice);

        while (true)
        {
            player1.PlayRound();
            if (player1.Victory)
            {
                break;
            }
            player2.PlayRound();
            if (player2.Victory)
            {
                break;
            }
        }
        return player1.Victory ? player2.Score * dice.Rolls : player1.Score * dice.Rolls;
    }
    public static long Sol2()
    {
        var (pos1, pos2) = ReadInputFile("Inputs\\input21e.txt");

        // var positionsP1 = new Dictionary<int, Dictionary<long, long>>();
        // var positionsP2 = new Dictionary<int, Dictionary<long, long>>();
        // positionsP1[pos1] = new Dictionary<long, long>();
        // positionsP1[pos1][0] = 1;
        // positionsP2[pos2] = new Dictionary<long, long>();
        // positionsP2[pos2][0] = 1;
        // while (true)
        // {
        //     var newPositionP1 = new Dictionary<int, Dictionary<long, long>>();
        //     foreach (var (pos, countByScore) in positionsP1)
        //     {
        //         newPositionP1[pos + 1] = new Dictionary<long, long>();
        //         newPositionP1[pos + 2] = new Dictionary<long, long>();
        //         newPositionP1[pos + 3] = new Dictionary<long, long>();
        //         foreach (var (score, count) in countByScore)
        //         {
        //             newPositionP1[pos + 1][pos + 1] = count + positionsP1.GetValueOrDefault(pos + 1, new Dictionary<long, long>()).GetValueOrDefault(score + 1, 0);
        //             newPositionP1[pos + 2][pos + 2] = count + positionsP1.GetValueOrDefault(pos + 2, new Dictionary<long, long>()).GetValueOrDefault(score + 2, 0);
        //             newPositionP1[pos + 3][pos + 3] = count + positionsP1.GetValueOrDefault(pos + 3, new Dictionary<long, long>()).GetValueOrDefault(score + 3, 0);
        //         }
        //     }
        //     positionsP1 = newPositionP1;
        // }
        return 0;

    }
}

public class DeterministicDice
{
    private int _nextValue;

    private int _faces;
    public int Rolls { get; private set; }

    public DeterministicDice()
    {
        _nextValue = 1;
        _faces = 100;
    }

    public int Roll(int times)
    {
        var v = _nextValue + times / 2;
        var result = v * times;
        _nextValue = (_nextValue + times) % _faces;
        Rolls += times;
        return result;
    }
}

public class PlayerPosition
{
    private int _position;

    private DeterministicDice _dice;
    public int Score { get; private set; }

    public bool Victory => Score >= 1000;

    public PlayerPosition(int startingPos, DeterministicDice dice)
    {
        _dice = dice;
        _position = startingPos;
        Score = 0;
    }

    public void PlayRound()
    {
        var diceValue = _dice.Roll(3);
        _position = (_position + diceValue - 1) % 10 + 1;
        Score += _position;
    }
}