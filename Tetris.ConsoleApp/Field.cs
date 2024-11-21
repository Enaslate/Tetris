namespace Tetris.ConsoleApp;

public class Field
{
    public bool[,] Cells { get; set; }
    public int LineCount { get; set; } = 12;
    public int ColumnCount { get; set; } = 10;

    public Field(int lineCount, int columnCount)
    {
        LineCount = lineCount;
        ColumnCount = columnCount;

        Cells = new bool[LineCount, ColumnCount];

        Clear();
    }

    public void Clear()
    {
        for (int i = 0; i < Cells.GetLength(0); i++)
        {
            for (int j = 0; j < Cells.GetLength(1); j++)
            {
                Cells[i, j] = false;
            }
        }
    }

    public int ClearFullLines()
    {
        var fullLineIndexes = GetFullLineIndexes();

        if (fullLineIndexes.Any())
        {
            foreach (var index in fullLineIndexes.OrderBy(i => i))
            {
                for (int i = index; i > 0; i--)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        Cells[i, j] = Cells[i - 1, j];
                    }
                }

                for (int j = 0; j < ColumnCount; j++)
                {
                    Cells[0, j] = false;
                }
            }
        }

        return fullLineIndexes.Count;
    }

    private List<int> GetFullLineIndexes()
    {
        var fullLineIndexes = new List<int>();
        for (int i = 0; i < LineCount; i++)
        {
            var isFull = true;
            for (int j = 0; j < ColumnCount; j++)
            {
                if (!Cells[i, j])
                {
                    isFull = false;
                    break;
                }
            }

            if (isFull)
                fullLineIndexes.Add(i);
        }

        return fullLineIndexes;
    }
}
