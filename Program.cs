// See https://aka.ms/new-console-template for more information
// See https://aka.ms/new-console-template for more information
class Board
{
    public int RowSize { get; set; }
    public int ColumnSize { get; set; }
    public int SubmarineRow { get; set; }
    public int SubmarineCol { get; set; }

    public Board(int row ,int col)
    {
        CellState[,] cells;
        RowSize = row;
        ColumnSize = col;
        SubmarineRow = 5;
        SubmarineCol = 5;
        cells = new CellState[RowSize, ColumnSize];
        for (int i = 0; i < RowSize; i++)
        {
            for (int j = 0; j < ColumnSize; j++)
            {
                cells[i, j] = CellState.Unselected;
            }
        }
    }
    enum CellState
    {
        Unselected,
        Selected,
        Sunk,
    }

    public int inputRow;
    public int inputCol;
    public void Input()
    {
        Console.Write($"縦軸の値を入力してください[1 - {RowSize}] >");
        int.TryParse(Console.ReadLine(), out inputRow);
        Console.Write($"横軸の値を入力してください[A - {(char)('A' + ColumnSize - 1)}] >");
        string ? colnumber = Console.ReadLine();
        if (colnumber == "A")
        {
            inputCol = 1;
        }
        else if (colnumber == "B")
        {
            inputCol = 2;
        }
        else if (colnumber == "C")
        {
            inputCol = 3;
        }
        else if (colnumber == "D")
        {
            inputCol = 4;
        }
        else if (colnumber == "E")
        {
            inputCol = 5;
        }
        else if (colnumber == "F")
        {
            inputCol = 6;
        }
        else if (colnumber == "G")
        {
            inputCol = 7;
        }
        else if (colnumber == "H")
        {
            inputCol = 8;
        }
        else if (colnumber == "I")
        {
            inputCol = 9;
        }
        else
        {
            inputCol = 0;
        }

            if (inputRow < 1 || inputRow > RowSize || inputCol < 1 || inputCol > ColumnSize)
        {
            Console.WriteLine("入力値が不正です。");
        }
        else
        {
            Console.WriteLine($"入力した位置: {inputRow}, {colnumber}");
        }
    }
    public void Print()
    {
        int hitCount = 0;
        if (inputRow == SubmarineRow && inputCol == SubmarineCol)
        {
            Console.WriteLine($"潜水艦の位置は{SubmarineRow},{(char)('A' + SubmarineCol)}");
            hitCount = 1;
        }
        else
        {
            Console.WriteLine("潜水艦は見つかりませんでした。");
            int dist = Math.Abs(inputRow - SubmarineRow) + Math.Abs(inputCol - SubmarineCol);
            Console.WriteLine($"潜水艦との距離は{dist}です。");
        }
        Console.WriteLine("   A B C D E F G H I");
        for (int i = 1; i <= RowSize; i++)
        {
            Console.WriteLine();
            Console.Write($"{i} ");
            for (int j = 1; j <= ColumnSize; j++)
            {
                Console.Write("|");
                if(i == SubmarineRow && j == SubmarineCol && hitCount == 1)
                {
                    Console.Write("X"); ;
                }
                else if (i == inputRow && j == inputCol)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine("|");
        }
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
            if (board.inputRow == board.SubmarineRow && board.inputCol == board.SubmarineCol)
            {
                break;
            }
        }
    }
}
