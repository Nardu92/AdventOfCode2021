using System.Drawing;

public class Day4
{
    public static (List<int>, List<BingoBoard>) ReadInputFile(string filename)
    {
        List<BingoBoard> boards = new List<BingoBoard>();
        using System.IO.StreamReader file = new System.IO.StreamReader(filename);
        string line;

        var numbers = file.ReadLine().Split(',').Select(x => Convert.ToInt32(x)).ToList();

        do
        {
            List<List<string>> listOfLinesForBoard = new List<List<string>>();
            while ((line = file.ReadLine()) != null && line != string.Empty)
            {
                listOfLinesForBoard.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList());
            }
            if (listOfLinesForBoard.Any())
            {
                boards.Add(new BingoBoard(listOfLinesForBoard));

            }
        } while (!file.EndOfStream);

        return (numbers, boards);
    }


    public static long Sol1()
    {
        var (numbers, boards) = ReadInputFile("Inputs/input4.txt");
        BingoGame game = new BingoGame(numbers, boards);

        return game.GetFirstWinning();
    }

    public static long Sol2()
    {
        var (numbers, boards) = ReadInputFile("Inputs/input4.txt");
        BingoGame game = new BingoGame(numbers, boards);

        return game.GetLastWinning();
    }
}

public class BingoGame
{
    public List<BingoBoard> Boards { get; private set; }
    public List<int> Numbers { get; private set; }

    public BingoBoard WinningBoard { get; private set; }

    public BingoGame(List<int> numbers, List<BingoBoard> boards)
    {
        Boards = boards;
        Numbers = numbers;
    }

    public bool PlayRound(int value)
    {
        var winningBoards = Boards.Where(x => x.PlayRound(value)).ToList();
        if (winningBoards.Any())
        {
            WinningBoard = winningBoards.First();
            return true;
        }
        return false;
    }

    public long GetFirstWinning()
    {
        foreach (var number in Numbers)
        {
            if (PlayRound(number))
            {
                return WinningBoard.Score;
            };
        }
        return 0;
    }
    public long GetLastWinning()
    {
        foreach (var number in Numbers)
        {
            if (Boards.Where(x => !x.IsComplete).Any())
            {
                PlayRound(number);
            }else{
                break;
            }
        }
        return WinningBoard.Score;
    }
}
public class BingoBoard
{
    public BingoBoardValue[,] BoardArray { get; private set; }

    public bool IsComplete { get; private set; }
    public long Score { get; private set; }

    public BingoBoard(List<List<string>> inputlines)
    {
        BoardArray = new BingoBoardValue[inputlines.Count, inputlines[0].Count];
        IsComplete = false;
        Score = 0;
        for (int i = 0; i < inputlines.Count; i++)
        {
            for (int j = 0; j < inputlines[0].Count; j++)
            {
                BoardArray[i, j] = new BingoBoardValue(Convert.ToInt32(inputlines[i][j]));
            }
        }
    }

    public bool CheckNumber(int number)
    {
        for (int i = 0; i < BoardArray.GetLength(0); i++)
        {
            for (int j = 0; j < BoardArray.GetLength(1); j++)
            {
                if (BoardArray[i, j].Value == number)
                {
                    BoardArray[i, j].Check = true;
                    return true;
                }
            }
        }
        return false;
    }

    public bool PlayRound(int number)
    {
        if (!IsComplete)
        {
            if (CheckNumber(number))
            {
                CheckForWin(number);
                return IsComplete;
            }
        }
        return false;
    }

    private void CheckForWin(int value)
    {
        CheckRowWin(value);
        CheckColumnWin(value);
    }

    private void CheckColumnWin(int value)
    {
        for (int i = 0; i < BoardArray.GetLength(0); i++)
        {
            var columnWin = true;
            for (int j = 0; j < BoardArray.GetLength(1); j++)
            {
                if (!BoardArray[j, i].Check)
                {
                    columnWin = false;
                    break;
                }
            }
            if (columnWin)
            {
                IsComplete = true;
                CalculateScore(value);
                return;
            }
        }
    }

    private void CheckRowWin(int value)
    {
        for (int i = 0; i < BoardArray.GetLength(0); i++)
        {
            var rowWin = true;
            for (int j = 0; j < BoardArray.GetLength(1); j++)
            {
                if (!BoardArray[i, j].Check)
                {
                    rowWin = false;
                    break;
                }
            }
            if (rowWin)
            {
                IsComplete = true;
                CalculateScore(value);
                return;
            }
        }
    }

    private void CalculateScore(int value)
    {
        long sum = 0;
        for (int i = 0; i < BoardArray.GetLength(0); i++)
        {
            for (int j = 0; j < BoardArray.GetLength(1); j++)
            {
                if (!BoardArray[i, j].Check)
                {
                    sum += BoardArray[i, j].Value;
                }
            }
        }
        Score = sum * value;
    }

}

public class BingoBoardValue
{
    public int Value { get; private set; }
    public bool Check { get; set; }
    public BingoBoardValue(int value)
    {
        Value = value;
        Check = false;
    }
}
