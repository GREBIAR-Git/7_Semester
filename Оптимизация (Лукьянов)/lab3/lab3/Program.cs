using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    internal class Program
    {
        public struct Point
        {
            public int X;
            public int Y;

            public Point(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            string str = Console.ReadLine();
            int n = int.Parse(str.Split(' ')[0]);
            int m = int.Parse(str.Split(' ')[1]);
            List<Point> planks = new List<Point>();
            if (false) // ручной ввод досок
            {
                planks = CustomPath();
                double time = CalculateTime(planks);
                Console.WriteLine("Result: " + time.ToString() + "\n\n\n");
                return;
            }
            double minTime = double.MaxValue;
            Go(new Point(0, 0), n, m, ref minTime, planks);
            Console.WriteLine(minTime);
        }

        public static List<Point> ClonePlanks(List<Point> planks)
        {
            List<Point> newPlanks = new List<Point>();
            foreach (Point plank in planks)
            {
                newPlanks.Add(plank);
            }
            return newPlanks;
        }

        public static void Go(Point point, int n, int m, ref double minTime, List<Point> planks)
        {
            if (point.X == n && point.Y == m)
            {
                double time = CalculateTime(planks);
                if (time < minTime)
                {
                    minTime = time;
                }
            }
            else
            {
                for(int i = point.X; i <= n; i++)
                {
                    if (point.Y < m && !(point.Y + 1 == m && i != n))
                    {
                        Point newPoint = new Point(i, point.Y + 1);
                        List<Point> newPlanks = ClonePlanks(planks);
                        newPlanks.Add(newPoint);
                        Go(newPoint, n, m, ref minTime, newPlanks);
                    }
                }
            }
        }

        public static double CalculateTime(List<Point> planks)
        {
            int g = 10;
            double time = 0;
            double speed = 0; // скорость не падает на поворотах
            for (int i = 0; i < planks.Count; i++)
            {
                Point plank = planks[i];
                Console.WriteLine($"Plank[{i}] = ({plank.X},{plank.Y})");
                Point prev_plank = i > 0 ? planks[i - 1] : new Point(0, 0);
                double plank_length = Math.Sqrt(Math.Pow(plank.X - prev_plank.X, 2) + Math.Pow(plank.Y - prev_plank.Y, 2));
                double cos_a = (double)(plank.Y - prev_plank.Y) / (plank_length);
                //Console.WriteLine("Plank " + (i + 1).ToString() + " length == " + plank_length.ToString());
                //Console.WriteLine("Plank " + (i + 1).ToString() + " angle == " + (Math.Acos(cos_a) * (180 / Math.PI)).ToString());
                double plank_time = cos_a > 0 ? (Math.Sqrt(Math.Pow(speed, 2) + 2 * g * cos_a * plank_length) - speed) / (g * cos_a) : plank_length / speed;

                speed += g * cos_a * plank_time;
                time += plank_time;
            }
            Console.WriteLine($"time = {time}");

            return time;
        }

        public static List<Point> CustomPath()
        {
            Console.WriteLine("Planks:");
            List<Point> planks = new List<Point>();
            while (true)
            {
                try
                {
                    planks.Add(new Point(Int32.Parse(Console.ReadLine()), Int32.Parse(Console.ReadLine())));
                }
                catch (Exception e)
                {
                    break; // нецелое число = конец ввода, построение пути завершено
                }
            }
            return planks;
        }
    }
}
