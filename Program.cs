
enum CellState
{
    Unselected,
    Selected,
    Sunk
}

class Board
{
    public int RowSize { get; set; }
    public int ColumnSize { get; set; }
    public int SubmarineRow { get; set; }
    public int SubmarineCol { get; set; }
    public int inputRow;
    public int inputCol;
    private CellState[,] cells;

    public Board(int row, int col)
    {
        RowSize = row;
        ColumnSize = col;
        SubmarineRow = 5;
        SubmarineCol = 5;
        cells = new CellState[RowSize, ColumnSize];
        // 初期化
        for (int i = 0; i < RowSize; i++)
        {
            for (int j = 0; j < ColumnSize; j++)
            {
                cells[i, j] = CellState.Unselected;
            }
        }
    }

    public void Input()
    {
        Console.Write($"縦軸の値を入力してください[1 - {RowSize}] > ");
        int.TryParse(Console.ReadLine(), out inputRow);

        Console.Write($"横軸の値を入力してください[A - {(char)('A' + ColumnSize - 1)}] >");
        string? colnumber = Console.ReadLine();
        inputCol = colnumber?.ToUpper()[0] - 'A' + 1 ?? 0;

        if (inputRow < 1 || inputRow > RowSize || inputCol < 1 || inputCol > ColumnSize)
        {
            Console.WriteLine("入力値が不正です。");
        }
        else
        {
            Console.WriteLine($"入力した位置: {inputRow}, {colnumber}");

            int r = inputRow - 1;
            int c = inputCol - 1;

            if (inputRow == SubmarineRow && inputCol == SubmarineCol)
            {
                cells[r, c] = CellState.Sunk;
            }
            else if (cells[r, c] == CellState.Unselected)
            {
                cells[r, c] = CellState.Selected;
            }
        }
    }

    public void Print()
    {
        Console.Write("   ");
        for (int i = 0; i < ColumnSize; i++)
        {
            Console.Write($" {((char)('A' + i)),1}");
        }
        Console.WriteLine();
        for (int i = 0; i < RowSize; i++)
        {
            Console.Write($"{i + 1,2} ");
            for (int j = 0; j < ColumnSize; j++)
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

        if (cells[SubmarineRow - 1, SubmarineCol - 1] == CellState.Sunk)
        {
            Console.WriteLine($"潜水艦の位置は {SubmarineRow}, {(char)('A' + SubmarineCol - 1)} にあります。撃沈しました！");
        }
        else
        {
            int dist = Math.Abs(inputRow - SubmarineRow) + Math.Abs(inputCol - SubmarineCol);
            Console.WriteLine($"潜水艦は見つかりませんでした。潜水艦との距離は {dist} です。");
        }
    }

    public bool IsFinished()
    {
        return cells[SubmarineRow - 1, SubmarineCol - 1] == CellState.Sunk;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int row = 9, col = 9;
        Board board = new Board(row, col);

        while (true)
        {
            board.Input();
            board.Print();
            if (board.IsFinished())
            {
                Console.WriteLine("ゲーム終了！");
                break;
            }
        }
    }
}
