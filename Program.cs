enum CellState
{
    Unselected,
    Selected,
    Sunk
}

struct Position
{
    public int Row;
    public int Column;

    public Position(int row, int col)
    {
        Row = row;
        Column = col;
    }

    public bool Equals(Position other)
    {
        return Row == other.Row && Column == other.Column;
    }

    public bool IsValid()
    {
        return 0 <= Row && Row < Program.ROW_SIZE
            && 0 <= Column && Column < Program.COLUMN_SIZE;
    }

    public int Distance(Position other)
    {
        return Math.Abs(Row - other.Row) + Math.Abs(Column - other.Column);
    }
}

class Board
{
    private CellState[,] cells;
    private Position submarine;
    private Position input;

    public Board()
    {
        cells = new CellState[Program.ROW_SIZE, Program.COLUMN_SIZE];
        submarine = new Position(4, 4);

        for (int i = 0; i < Program.ROW_SIZE; i++)
        {
            for (int j = 0; j < Program.COLUMN_SIZE; j++)
            {
                cells[i, j] = CellState.Unselected;
            }
        }
    }

    public void Input()
    {
        Console.Write($"座標を入力してください> ");
        string? input_string = Console.ReadLine();

        if (string.IsNullOrEmpty(input_string) || input_string.Length != 2)
        {
            Console.WriteLine("入力は2文字で行ってください。（例: 1A , A1　など）");
            return;
        }

        input_string = input_string.ToUpper();
        char first = input_string[0];
        char second = input_string[1];

        int row = -1;
        int col = -1;

        if (char.IsDigit(first) && char.IsLetter(second))
        {
            row = first - '0' - 1;
            col = second - 'A';
        }
        else if (char.IsLetter(first) && char.IsDigit(second))
        {
            row = second - '0' - 1;
            col = first - 'A';
        }
        else
        {
            Console.WriteLine("1桁の数字とアルファベット1文字を入力してください（例: 1A , A1　など）");
            return;
        }

        Position pos = new Position(row, col);

        if (!pos.IsValid())
        {
            Console.WriteLine("入力値が範囲外です。");
            return;
        }

        if (cells[pos.Row, pos.Column] != CellState.Unselected)
        {
            Console.WriteLine("その位置はすでに選択済みです。");
            return;
        }

        input = pos;

        Console.WriteLine($"入力した位置: {input.Row + 1}, {(char)('A' + input.Column)}");

        if (input.Equals(submarine))
        {
            cells[input.Row, input.Column] = CellState.Sunk;
        }
        else
        {
            cells[input.Row, input.Column] = CellState.Selected;
        }
    }

    public void Print()
    {
        Console.Write("   ");
        for (int i = 0; i < Program.COLUMN_SIZE; i++)
        {
            Console.Write($" {((char)('A' + i)),1}");
        }
        Console.WriteLine();

        for (int i = 0; i < Program.ROW_SIZE; i++)
        {
            Console.Write($"{i + 1,2} ");
            for (int j = 0; j < Program.COLUMN_SIZE; j++)
            {
                Console.Write("|");
                switch (cells[i, j])
                {
                    case CellState.Unselected:
                        Console.Write(" ");
                        break;
                    case CellState.Selected:
                        Console.Write("*");
                        break;
                    case CellState.Sunk:
                        Console.Write("X");
                        break;
                }
            }
            Console.WriteLine("|");
        }

        if (cells[submarine.Row, submarine.Column] == CellState.Sunk)
        {
            Console.WriteLine($"潜水艦の位置は {submarine.Row + 1}, {(char)('A' + submarine.Column)} にあります。撃沈しました。");
        }
        else
        {
            int dist = input.Distance(submarine);
            Console.WriteLine($"潜水艦は見つかりませんでした。潜水艦との距離は {dist} です。");
        }
    }

    public bool IsFinished()
    {
        return cells[submarine.Row, submarine.Column] == CellState.Sunk;
    }
}

class Program
{
    public const int ROW_SIZE = 9;
    public const int COLUMN_SIZE = 9;

    static void Main(string[] args)
    {
        Board board = new Board();

        while (true)
        {
            board.Input();
            board.Print();
            if (board.IsFinished())
            {
                Console.WriteLine("ゲームが終了しました。お疲れさまでした。");
                break;
            }
        }
    }
}