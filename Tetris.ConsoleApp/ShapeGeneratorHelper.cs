namespace Tetris.ConsoleApp;

public static class ShapeGeneratorHelper
{
    public static bool[,] CreateShape()
    {
        var random = new Random();
        
        var shapeCase = random.Next(0, 7);

        return shapeCase switch
        {
            0 => GetLine(),
            1 => GetSquare(),
            2 => GetLeftGun(),
            3 => GetRightGun(),
            4 => GetLeftSnake(),
            5 => GetRightSnake(),
            6 => GetT(),
            _ => throw new Exception("Произошла ошибка при создании фигуры")
        };
    }

    private static bool[,] GetLine()
    {
        var shape = new bool[1,4];

        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
                shape[i, j] = true;
        };

        return shape;
    }

    private static bool[,] GetSquare()
    {
        var shape = new bool[2,2];

        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
                shape[i, j] = true;
        };

        return shape;
    }

    private static bool[,] GetLeftGun()
    {
        var shape = new bool[3,2];

        for (int i = 0; i < shape.GetLength(0); i++)
        {
            var line = new List<bool>();

            for (int j = 0; j < shape.GetLength(1); j++)
            {
                if (j == 0 && (i == 0 || i == 1))
                    shape[i, j] = false;
                else
                    shape[i, j] = true;
            }
        };

        return shape;
    }

    private static bool[,] GetRightGun()
    {
        var shape = new bool[3,2];

        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
            {
                if (j == 1 && (i == 0 || i == 1))
                    shape[i, j] = false;
                else
                    shape[i, j] = true;
            }
        };

        return shape;
    }

    private static bool[,] GetLeftSnake()
    {
        var shape = new bool[3,2];

        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
            {
                if ((i == 0 && j == 0) || (i == 2 && j == 1))
                    shape[i, j] = false;
                else
                    shape[i, j] = true;
            }
        };

        return shape;
    }

    private static bool[,] GetRightSnake()
    {
        var shape = new bool[3,2];

        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
            {
                if ((i == 0 && j == 1) || (i == 2 && j == 0))
                    shape[i, j] = false;
                else
                    shape[i, j] = true;
            }
        };

        return shape;
    }

    private static bool[,] GetT()
    {
        var shape = new bool[2,3];

        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
            {
                if (i == 1 && (j == 0 || j == 2))
                    shape[i, j] = false;
                else
                    shape[i, j] = true;
            }
        };

        return shape;
    }
}
