namespace Tetris.ConsoleApp;

public class Figure
{
    public bool[,]? Shape { get; set; }
    public int Line { get; set; }
    public int Column { get; set; }

    public Field Field { get; set; }

    public Figure(bool[,]? shape, Field field, int column = 0)
    {
        Shape = shape;
        Field = field;
        Line = 0;
        Column = column;
    }

    public int MoveDown()
    {
        var canMove = true;

        var lineCount = Shape.GetLength(0);
        var columnCount = Shape.GetLength(1);

        for (int i = 0; i < lineCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                if (i + Line + 1 >= Field.LineCount)
                {
                    canMove = false;
                    break;
                }

                if (Shape[i, j] && Field.Cells[i + Line + 1, j + Column])
                {
                    canMove = false;
                    break;
                }
            }

            if (!canMove)
            {
                break;
            }
        }

        if (canMove)
            return Line + 1;

        return Line;
    }

    public int MoveRight()
    {
        var canMove = true;

        var lineCount = Shape?.GetLength(0);
        var columnCount = Shape?.GetLength(1);

        for (int i = 0; i < lineCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                if (i + 1 < lineCount &&
                    j + 1 < columnCount)
                {
                    if (Shape[i, j + 1])
                        continue;
                }

                if (Column + Shape?.GetLength(1) >= Field.ColumnCount ||
                    Field.Cells[Line + Shape.GetLength(0), Column + Shape.GetLength(1)])
                    canMove = false;
            }
        }

        if (canMove)
            return Column + 1;

        return Column;
    }

    public int MoveLeft()
    {
        var canMove = true;

        var lineCount = Shape?.GetLength(0);
        var columnCount = Shape?.GetLength(1);

        for (int i = 0; i < lineCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                if (i + 1 < lineCount &&
                    j - 1 > columnCount)
                {
                    if (Shape[i, j - 1])
                        continue;
                }

                if (Column <= 0 ||
                    Field.Cells[Line + Shape.GetLength(0), Column + Shape.GetLength(1) - 1])
                    canMove = false;
            }
        }

        if (canMove)
           return Column - 1;

        return Column;
    }

    public void TurnOver()
    {
        if (Shape == null)
            return;

        var lineCount = Shape.GetLength(0);
        var columnCount = Shape.GetLength(1);

        if (Line + 1 + Shape.GetLength(1) > Field.LineCount ||
            (Field.Cells[Line + Shape.GetLength(1), Column + 1]))
            return;

        if (Column + Shape.GetLength(0) >= Field.ColumnCount ||
            Field.Cells[Line + 1, Column + Shape.GetLength(0)])
            return;

        var tempShape = new bool[columnCount, lineCount];

        for (int j = Shape.GetLength(1) -1; j >= 0; j--)
        {
            for (int i = Shape.GetLength(0) - 1; i >= 0; i--)
            {
                tempShape[j, i] = Shape[i, j];
            }
        }

        Shape = tempShape;
    }
}
