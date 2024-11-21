using Tetris.ConsoleApp;

var delayToUpdate = 500;

var linesCount = 12;
var columnCount = 10;

var nextLine = 0;
var nextColumn = 0;

var isShapeMoving = false;

var lastMoveKey = ConsoleKey.NoName;
var isMove = false;

var startLinePosition = 0;
var startColumnPosition = 0;

var field = new Field(linesCount, columnCount);
var figure = new Figure(null, field, startColumnPosition);

while (true)
{
    figure.Line = nextLine;
    figure.Column = nextColumn;

    if (isShapeMoving)
    {
        HandleMove();
    }
    else
    {
        field.ClearFullLines();

        figure.Shape = ShapeGeneratorHelper.CreateShape();
        PlaceShape();
    }

    Render(field.Cells);
    Thread.Sleep(delayToUpdate);

    isMove = false;
}

static void Render(bool[,] cells)
{
    Console.Clear();

    for (int i = 0; i < cells.GetLength(0); i++)
    {
        Console.Write('|');
        for (int j = 0; j < cells.GetLength(1); j++)
        {
            Console.Write(cells[i, j] ? '=' : ' ');
            Console.Write('|');
        }
        Console.WriteLine();
    }
}

void PlaceShape()
{
    if (figure.Shape != null)
        startColumnPosition = (columnCount - figure.Shape.GetLength(1)) / 2;

    nextLine = startLinePosition;
    nextColumn = startColumnPosition;

    RenderShape();

    isShapeMoving = true;
}

void HandleMove()
{
    RenderShape(clearMode: true);

    nextLine = figure.MoveDown();
    MoveGorizontal();

    isShapeMoving = figure.Line != nextLine;    
    RenderShape();
}

void MoveGorizontal()
{
    if (Console.KeyAvailable && isShapeMoving)
    {
        var info = Console.ReadKey(intercept: true);
        lastMoveKey = info.Key;

        if (figure.Line + figure.Shape?.GetLength(0) + 1 <= field.LineCount && 
            lastMoveKey != ConsoleKey.NoName)
            isMove = true;

        while (Console.KeyAvailable)
            info = Console.ReadKey(intercept: true);
    }

    if (isMove)
    {
        switch (lastMoveKey)
        {
            case ConsoleKey.LeftArrow:
                nextColumn = figure.MoveLeft();
                break;
            case ConsoleKey.RightArrow:
                nextColumn = figure.MoveRight();
                break;
            case ConsoleKey.UpArrow:
                figure.TurnOver();
                break;
            case ConsoleKey.DownArrow:
                figure.TurnOver();
                break;
        }

        lastMoveKey = ConsoleKey.NoName;
    }
}

void RenderShape(bool clearMode = false)
{
    for (int i = 0; i < figure.Shape?.GetLength(0); i++)
    {
        for (int j = 0; j < figure.Shape?.GetLength(1); j++)
        {
            if (clearMode)
            {
                if (figure.Shape[i, j])
                    field.Cells[i + figure.Line, j + figure.Column] = false;
            }
            else
            {
                if (figure.Shape[i, j])
                    field.Cells[i + nextLine, j + nextColumn] = figure.Shape[i, j];
            }
        }
    }
}