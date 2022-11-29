using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    internal class Program
    {
        const int g = 10;
        static byte n, m;

        static Data[,] data;

        static int count = 0;

        static void Go(int r, int h)
        {
            for (int i = r + 1; i < m; i++)//горизонталь
            {
                for (int j = h; j < i + 1 && j <= n; j++)//вертикаль
                {
                    double speed = data[r, h].Speed;
                    double time = CalculateTime(new Point(j, i), new Point(h, r), ref speed);
                    if (data[i, j].Time > time + data[r, h].Time)
                    {
                        data[i, j].Time = time + data[r, h].Time;
                        data[i, j].Speed = speed;
                        Go(i, j);
                    }
                }
            }

            double lastTime = CalculateTime(new Point(n, m), new Point(h, r), data[r, h].Speed);
            if (data[m, n].Time > lastTime + data[r, h].Time)
            {
                data[m, n].Time = lastTime + data[r, h].Time;
            }
        }

        static void Main(string[] args)
        {
            string[] str = Console.ReadLine().Split(' ');
            n = byte.Parse(str[0]);
            m = byte.Parse(str[1]);
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
            Console.WriteLine("count: " + count);
            Console.WriteLine("answer: " + data[n, m].Time);
            PrintData();
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

        static double CalculateTime(Point current, Point prev, ref double speed)
        {
            count++;
            double y1 = current.Y - prev.Y;
            double plank_length = Math.Sqrt(Math.Pow(current.X - prev.X, 2) + Math.Pow(y1, 2));
            double cos_a = y1 / plank_length;
            double g1 = g * cos_a;
            double speed1 = (Math.Sqrt(Math.Pow(speed, 2) + 2 * g1 * plank_length) - speed);
            speed += speed1;
            return speed1 / g1;
        }

        static double CalculateTime(Point current, Point prev, double speed)
        {
            count++;
            double y1 = current.Y - prev.Y;
            double plank_length = Math.Sqrt(Math.Pow(current.X - prev.X, 2) + Math.Pow(y1, 2));
            double cos_a = y1 / plank_length;
            double g1 = g * cos_a;
            return (Math.Sqrt(Math.Pow(speed, 2) + 2 * g1 * plank_length) - speed) / g1;
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