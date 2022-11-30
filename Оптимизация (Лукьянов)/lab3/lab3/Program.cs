using System;
using System.Collections.Generic;

namespace lab3
{
    internal class Program
    {
        const int g = 10;
        static byte n, m;

        static Data[,] data;

        static Dictionary<int, double> speeds = new Dictionary<int, double>();
        static Dictionary<Point, double> times = new Dictionary<Point, double>();


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
                        for (int b = a; b < j; b++) // b-я строка, < т.к. нельзя горизонтально идти (по той же строке)
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
            data[0, 0] = new Data(0, 0);
            for (int i = 1; i < m; i++)//горизонталь
            {
                for (int j = 0; j < i + 1 && j <= n; j++)//вертикаль
                {
                    data[i, j] = new Data();
                }
            }
            data[m, n] = new Data();
            Go(0, 0);
            Console.WriteLine(times[new Point(n, m)]);
        }

        static void CalculateTime(Point current, Point prev)
        {
            if (!speeds.ContainsKey(prev.Y))
            {
                speeds.Add(prev.Y, Math.Sqrt(2 * g * prev.Y));
            }
            double speed = speeds[prev.Y];

            double y1 = current.Y - prev.Y;
            double plank_length = Math.Sqrt(Math.Pow(current.X - prev.X, 2) + Math.Pow(y1, 2));
            double cos_a = y1 / plank_length;
            double g1 = g * cos_a;
            double speed1 = (Math.Sqrt(Math.Pow(speed, 2) + 2 * g1 * plank_length) - speed);
            double time = times[prev] + speed1 / g1;

            if (!times.ContainsKey(current) || time < times[current])
            {
                times[current] = time;
            }
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