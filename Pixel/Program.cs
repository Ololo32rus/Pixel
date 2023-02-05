// массив (задаваемый/пользователем). Премещение курсора пользователем. Заполнение структура(цветом (enum Console.Color)/символом) . Сохранение, серриализация, дессириализация.

/* 1. Задать массив спросив у пользлвателя кол-во строк и столбцов +
2. Генерация стен у пользлвателя +
3. Передвижение курсора по полю +
4. Выбор и заполнение пикселя цветом и символом
*/
using System;
using System.Drawing;

namespace PixelArt
{
    public enum Cell
    {
        Empty = '*', // Белый
        Bound = '#', // Коричневый
        PixSkin = '֍' // 1421 U+058D D6 8D Navy
    }

    public struct Pixel
    {
        public Cell symbol;
        public ConsoleColor color;
    }

    public class Program
    {
        static Pixel[,] CreateField(int yRows, int xCols) // задаём двумерный массив
        {
            return new Pixel[yRows, xCols];
        }
        static int InitRows() // Спрашиваем сколько строк + возвращаем их
        {
            Console.WriteLine($"Введите количество строк по оси Y: ");
            int yRows = int.Parse(Console.ReadLine());

            return yRows;
        }

        static int InitCols() // Спрашиваем сколько столбцов + возвращаем их
        {
            Console.WriteLine($"Введите количество столбцов по оси X: ");
            int xCols = int.Parse(Console.ReadLine());

            return xCols;
        }

        static void InitField(Pixel[,] field) // задаём поле, границы поля по X(i) , Y (j)
        {
            int yRows = field.GetLength(0);
            int xCols = field.GetLength(1);

            for (int i = 0; i < yRows; i++)
            {
                for (int j = 0; j < xCols; j++)
                {
                    field[i, j] = Cell.Empty;
                }
            }
            for (int i = 0; i < yRows; i++)
            {
                field[i, 0] = Cell.Bound;
                field[i, xCols - 1] = Cell.Bound;
            }
            for (int j = 0; j < xCols; j++)
            {
                field[0, j] = Cell.Bound;
                field[yRows - 1, j] = Cell.Bound;
            }
        }

        static int InitXPix() // Старт координаты пикселя по X
        {
            int xPix = 1;
            return xPix;
        }

        static int InitYPix() // Старт координаты пикселя по Y

        {
            int yPix = 1;
            return yPix;
        }


        static void PrintObjects(Pixel[,] field, int xPix, int yPix) // отрисовка поля, пикселя, стен, пустот
        {

            int yRows = field.GetLength(0);
            int xCols = field.GetLength(1);

            Console.Clear();
            Console.ResetColor();


            for (int i = 0; i < yRows; i++)
            {
                for (int j = 0; j < xCols; j++)
                {
                    if (i == xPix && j == yPix)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((char)Cell.PixSkin);
                    }
                    else
                    {
                        Console.ForegroundColor = field[i, j].color;
                        Console.Write((char)field[i, j].symbol);
                    }
                }
                Console.WriteLine();
            }

        }

        static void MovePix(Pixel[,] field, ConsoleKey key, ref int xPix, ref int yPix)// движение пикселя WASD + Space
        {
            int yRows = field.GetLength(0);
            int xCols = field.GetLength(1);

            switch (key)
            {
                case System.ConsoleKey.A:
                    if (yPix > 1)
                    {
                        yPix--;
                    }
                    break;

                case System.ConsoleKey.W:
                    if (xPix > 1)
                    {
                        xPix--;
                    }
                    break;

                case System.ConsoleKey.D:
                    if (yPix < xCols - 2)
                    {
                        yPix++;
                    }
                    break;

                case System.ConsoleKey.S:
                    if (xPix < yRows - 2)
                    {
                        xPix++;
                    }
                    break;
                    /*
                case System.ConsoleKey.Spacebar:
                    field[xPix, yPix] = Pixel.symbol && Pixel.symbol = Pixel.color;
                    break;

                case System.ConsoleKey.Backspace:
                    field[xPix, yPix] = Cell.Empty;
                    break;
                    */
            }
        }



        static void PrintMessage()
        {
            Console.WriteLine($"Для добавления цвета и символа нажмите *пробел*: ");
            // Console.Clear();
        }

        static void PrintMenuColors() // меню выбора цвета
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("1. Красный ");


            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("2. Синий ");


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("3. Зеленый ");
        }
        /*
        static void ColorPixel(ConsoleColor color) // Раскрашивание по выбраному цвету
        {
            ConsoleKey key = Console.ReadKey(false).Key;
            switch (key)
            {
                case System.ConsoleKey.1:                       {
                        Console.ForegroundColor = ConsoleColor.Red;
                }
                    break;

                case System.ConsoleKey.2:
                    { 
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    break;

                case System.ConsoleKey.3:
{
                        Console.Write("Введите позицию: ");
                        int newColor = int.Parse(Console.ReadLine());
                        color = Color.Green;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    break;
            }

        }
        */

        //----------------------------—

        static void Main()
        {

            int yRows = InitRows();
            int xCols = InitCols();

            InitField(field);

            int xPix = InitXPix();
            int yPix = InitYPix();


            Pixel[,] field = CreateField(yRows, xCols);

            PrintMenuColors();

            bool play = true;

            while (play)
            {
                PrintMenuColors();
                PrintObjects(field, xPix, yPix);
                ConsoleKey key = Console.ReadKey(false).Key;
                MovePix(field, key, ref xPix, ref yPix);
            }
        }
    }
}