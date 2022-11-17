using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    // можно без рекурсии сразу по формуле овала попробовать
    internal class Program
    {
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

        static byte n, m;
        static double minTime = double.MaxValue;
        const int g = 10;

        static void Main(string[] args)
        {
            string str = Console.ReadLine();
            n = byte.Parse(str.Split(' ')[0]);
            m = byte.Parse(str.Split(' ')[1]);
            Go(new Point(0, 0), new Point[(n+m)*2],0);
            Console.WriteLine(minTime);
        }

        static List<Point> ClonePlanks(List<Point> planks)
        {
            List<Point> newPlanks = new List<Point>();
            foreach (Point plank in planks)
            {
                newPlanks.Add(plank);
            }
            return newPlanks;
        }

        static void Go(Point point, Point[] planks, int size)
        {
            if (point.X == n && point.Y == m)
            {
                CalculateAllTime(planks,size);
            }
            else
            {
                for (int i = point.X; i <= n; i++)
                {
                    for (int j = point.Y + 1; j <= m; j++)
                    {
                        if (!(j == m && i != n))
                        {
                            Point newPoint = new Point(i, j);
                            Point[] newPlanks = new Point[planks.Length];
                            Array.Copy(planks, newPlanks, planks.Length);
                            newPlanks[size]= newPoint;
                            if(size>1)
                            {

                            }
                            Go(newPoint, newPlanks, size+1);
                        }
                    }
                }
            }
        }

        static void CalculateAllTime(Point[] planks, int size)
        {
            double time = 0;
            double speed = 0;
            time += CalculateTime(planks[0], new Point(0,0), ref speed);
            if(time>minTime)
            {
                return;
            }
            for (int i = 1; i < size; i++)
            {
                time += CalculateTime(planks[i], planks[i-1], ref speed);
                if (time > minTime)
                {
                    return;
                }
            }
            if (time < minTime)
            {
                minTime = time;
            }
        }

        static double CalculateTime(Point current, Point prev,ref double speed)
        {
            double plank_length = Math.Sqrt(Math.Pow(current.X - prev.X, 2) + Math.Pow(current.Y - prev.Y, 2));
            double cos_a = (double)(current.Y - prev.Y) / (plank_length);
            double plank_time = (Math.Sqrt(Math.Pow(speed, 2) + 2 * g * cos_a * plank_length) - speed) / (g * cos_a);
            speed += g * cos_a * plank_time;
            return plank_time;
        }
    }
}
