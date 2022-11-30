using System;
using System.Collections.Generic;

namespace lab3
{
    internal class Program
    {
        const int g = 10;
        static byte n, m;

        static Data[,] data;

        static void Go(int r, int h)
        {
            // перебираем все точки
            for (int i = 0; i <= n; i++) // i-й столбец
            {
                for (int j = 1; j <= m; j++) // j-я строка
                {
                    // для каждой точки ищем минимальное время, за которое в нее можно дойти
                    // перебираем все точки до нее (предыдущие)
                    for (int a = 0; a <= i; a++) // a-й столбец, <= т.к. можно ровно вниз
                    {
                        for (int b = 0; b < j; b++) // b-я строка, < т.к. нельзя горизонтально идти (по той же строке)
                        {
                            // считаем время
                            CalculateTime(new Point(i, j), new Point(a, b));
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            string[] str = Console.ReadLine().Split(' ');
            n = byte.Parse(str[0]);
            m = byte.Parse(str[1]);
            //times[new Point(0, 0)] = 0;
            data = new Data[n + 1, m + 1];
            for (int i = 0; i <= n; i++) // i-й столбец
            {
                for (int j = 0; j <= m; j++) // j-я строка
                {
                    data[i, j] = new Data();
                }
            }
            data[0, 0] = new Data(0, 0);
            data[n, m] = new Data();
            Go(0, 0);
            Console.WriteLine(data[n, m].Time);
        }

        static void CalculateTime(Point current, Point prev)
        {
            if (data[prev.X, prev.Y].Speed == double.MaxValue)
            {
                data[prev.X, prev.Y].Speed = Math.Sqrt(2 * g * prev.Y);
            }
            double speed = data[prev.X, prev.Y].Speed;

            double y1 = current.Y - prev.Y;
            double plank_length = Math.Sqrt(Math.Pow(current.X - prev.X, 2) + Math.Pow(y1, 2));
            double cos_a = y1 / plank_length;
            double g1 = g * cos_a;
            double speed1 = (Math.Sqrt(Math.Pow(speed, 2) + 2 * g1 * plank_length) - speed);
            double time = data[prev.X, prev.Y].Time + speed1 / g1;

            if (time < data[current.X, current.Y].Time)
            {
                data[current.X, current.Y].Time = time;
            }
        }

        static void PrintData()
        {
            Console.WriteLine();
            for (int i = 0; i <= n; i++)
            {
                string str = string.Empty;
                for (int j = 0; j <= m; j++)
                {
                    if (data[i, j] == null || data[i, j].Time == double.MaxValue)
                    {
                        str = "0,000 " + str;
                    }
                    else
                    {
                        str = string.Format("{0:0.000}", Math.Round(data[i, j].Time, 3)) + " " + str;
                    }
                }
                Console.WriteLine(str);
            }
            Console.WriteLine();
        }
    }

    struct Point
    {
        public int X;
        public int Y;

        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    class Data
    {
        public double Time { get; set; }
        public double Speed { get; set; }

        public Data(double time = double.MaxValue, double speed = double.MaxValue)
        {
            Time = time;
            Speed = speed;
        }
    }
}