using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab3
{
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
            Go(new Point(0, 0), new Point[(n + m) * 2], 0, 0, 0);


            Console.WriteLine("\n\n\n");
            // x и y - координаты искомой точки
            // (x+a) ^ 2 / a ^ 2 + y ^ 2 / b ^ 2 = 1; эллипс   a = n, b = m
            // y = sqrt(b^2-x^2*b^2/a^2) = (sqrt(b^2 * (1 - x^2/a^2)))
            int a = n;
            int b = m;
            List<Point> ellipsePoints = new List<Point>();
            if (a>b)
            {
                // поделить отрезок на например (n+m)*2 частей и искать ближайшую точку каждый раз? и удалить дубликаты
                double parts_number = (n + m) * 2; // n = parts_number / 2 - m
                for (int i = 0; i < parts_number; i++)
                {
                    double x = Math.Ceiling(i / Math.Ceiling(parts_number / (double)n));
                    double y = Math.Sqrt(Math.Pow(b, 2) * (1 - Math.Pow(x-a, 2) / Math.Pow(a, 2)));
                    Console.WriteLine(x + " " + y);
                    if (Math.Abs(y-Math.Round(y)) < 0.05)
                    {
                        ellipsePoints.Add(new Point((int)Math.Round(x), (int)Math.Round(y)));
                    }
                }
                ellipsePoints = ellipsePoints.Distinct().ToList();
                Console.WriteLine("\n\n\n");
            }
            else if (a == b)
            {
                // круг
            }
            else
            {
                // перевернуть эллипс
                // поделить отрезок на например (n+m)*2 частей и искать ближайшую точку каждый раз? и удалить дубликаты
                double parts_number = (n + m) * 2; // n = parts_number / 2 - m
                for (int i = 0; i < parts_number; i++)
                {
                    double x = i * (n / (double)parts_number);
                    double y = Math.Sqrt(Math.Pow(b, 2) - Math.Pow(x, 2) * Math.Pow(b, 2) / Math.Pow(a, 2));
                    Console.WriteLine(x + " " + y);
                    ellipsePoints.Add(new Point((int)Math.Round(x), (int)Math.Round(y)));
                }
                ellipsePoints = ellipsePoints.Distinct().ToList();
                Console.WriteLine("\n\n\n");
            }


            // ax ^ 2 + bx + c = 0; парабола  откуда брать a b и с хз
            // D = b^2 - 4 * a * c
            // x = (-b + sqrt(D)) / (2 * a)



            foreach (Point point in points)
            {
                Console.WriteLine(point.X + " " + point.Y);
            }
            Console.WriteLine(minTime);

            foreach (Point point in ellipsePoints)
            {
                Console.WriteLine(point.X + " " + point.Y);
            }
        }

        static Point[] points;

        static void Go(Point point, Point[] planks, int size, double speed, double time)
        {
            if (point.X == n && point.Y == m)
            {
                points = planks;
                minTime = time;
            }
            else
            {
                for (int i = point.X; i <= n; i++)//горизонталь
                {
                    for (int j = point.Y + 1; j <= m; j++)//вертикаль
                    {
                        if (j != m || i == n)
                        {
                            Next(new Point(i, j), point, planks, size, speed, time);
                        }
                    }
                }
            }
        }

        static void Next(Point newPoint, Point point, Point[] planks, int size, double speed, double time)
        {
            if (size == 0)
            {
                time += CalculateTime(newPoint, point, ref speed);
                if (time > minTime)
                {
                    return;
                }
            }
            else
            {
                time += CalculateTime(newPoint, planks[size - 1], ref speed);
                if (time > minTime)
                {
                    return;
                }
            }
            Point[] newPlanks = new Point[planks.Length];
            Array.Copy(planks, newPlanks, planks.Length);
            newPlanks[size] = newPoint;
            Go(newPoint, newPlanks, size + 1, speed, time);
        }

        static double CalculateTime(Point current, Point prev, ref double speed)
        {
            double plank_length = Math.Sqrt(Math.Pow(current.X - prev.X, 2) + Math.Pow(current.Y - prev.Y, 2));
            double cos_a = (current.Y - prev.Y) / (plank_length);
            double plank_time = (Math.Sqrt(Math.Pow(speed, 2) + 2 * g * cos_a * plank_length) - speed) / (g * cos_a);
            speed += g * cos_a * plank_time;
            return plank_time;
        }

        /*
        static void CalculateAllTime(Point[] planks, int size)
        {
            double time = 0;
            double speed = 0;
            time += CalculateTime(planks[0], new Point(0, 0), ref speed);
            if (time > minTime)
            {
                return;
            }
            for (int i = 1; i < size; i++)
            {
                time += CalculateTime(planks[i], planks[i - 1], ref speed);
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

        
        
        static void CalculateAllTime(Point[] planks, int size, double time1)
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
            if(time==time1)
            {
                Console.WriteLine("1"+" time="+time+" time1="+time1+" count pl="+size);
            }
            else
            {
                Console.WriteLine("0" + " time=" + time + " time1=" + time1 + " count pl=" + size);
            }

            if (time < minTime)
            {
                minTime = time;
            }
        }

        /*
        static void Go(Point point, Point[] planks, int size, double speed, double time)
        {
            if (point.X == n && point.Y == m)
            {
                CalculateAllTime(planks,size,time);
            }
            else
            {
                for (int i = point.X; i <= n; i++)//вертикаль
                {
                    for (int j = point.Y + 1; j < m; j++)//горизонталь
                    {
                        if (j!=m || i==n)
                        {
                            Point newPoint = new Point(i, j);
                            double newSpeed = speed;
                            double tempTime = time;
                            if(size>0)
                            {

                                tempTime += CalculateTime(newPoint, planks[size - 1], ref newSpeed);
                                Console.WriteLine("#" + size + " time - " + tempTime);
                                if(tempTime > minTime)
                                {
                                    return;
                                }
                            }
                            Point[] newPlanks = new Point[planks.Length];
                            Array.Copy(planks, newPlanks, planks.Length);
                            newPlanks[size]= newPoint;
                            Go(newPoint, newPlanks, size+1, newSpeed, tempTime);
                        }
                    }
                }
            }
        }

        /*static void Go(Point point, Point[] planks, int size, double speed, double time)
        {
            if (point.X == n && point.Y == m)
            {
                if (size > 1)
                {
                    if (!(point.Y == m && point.X != n))
                    {
                        minTime = time;
                    }
                }
                else
                {
                    CalculateAllTime(planks, size);
                }
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
                            if (size > 0)
                            {
                                time += CalculateTime(newPoint, planks[size - 1], ref speed);
                                if (time > minTime)
                                {
                                    return;
                                }
                            }

                            Point[] newPlanks = new Point[planks.Length];
                            Array.Copy(planks, newPlanks, planks.Length);
                            newPlanks[size] = newPoint;

                            Go(newPoint, newPlanks, size + 1, speed, time);
                        }
                    }
                }
            }
        }*/
    }
}
/*
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

            Point[] planks = new Point[n * m * 2];
            planks[0] = new Point(0, 0);


            Go(planks, 0, 0, 0);
            Console.WriteLine(minTime);
        }

        static void Go(Point[] planks, int size, double speed, double time)
        {
            if (planks[size].X == n && planks[size].Y == m)
            {
                minTime = time;
            }
            else
            {
                for (int i = planks[size].X; i <= n; i++)//вертикаль
                {
                    for (int j = planks[size].Y + 1; j <= m; j++)//горизонталь
                    {
                        if (j != m || i == n)
                        {
                            Point newPoint = new Point(i, j);
                            double speed1 = speed;
                            double time1 = time;

                            time1 += CalculateTime(newPoint, planks[size], ref speed1);
                            if (time1 > minTime)
                            {
                                return;
                            }

                            Point[] newPlanks = new Point[planks.Length];
                            Array.Copy(planks, newPlanks, planks.Length);
                            newPlanks[size + 1] = newPoint;
                            Go(newPlanks, size + 1, speed1, time1);
                        }
                    }
                }
            }
        }

        static void CalculateAllTime(Point[] planks, int size)
        {
            double time = 0;
            double speed = 0;
            time += CalculateTime(planks[0], new Point(0, 0), ref speed);
            if (time > minTime)
            {
                return;
            }
            for (int i = 1; i < size; i++)
            {
                time += CalculateTime(planks[i], planks[i - 1], ref speed);
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

        static double CalculateTime(Point current, Point prev, ref double speed)
        {
            double plank_length = Math.Sqrt(Math.Pow(current.X - prev.X, 2) + Math.Pow(current.Y - prev.Y, 2));
            double cos_a = (double)(current.Y - prev.Y) / (plank_length);
            double plank_time = (Math.Sqrt(Math.Pow(speed, 2) + 2 * g * cos_a * plank_length) - speed) / (g * cos_a);
            speed += g * cos_a * plank_time;
            return plank_time;
        }

        static void CalculateAllTime(Point[] planks, int size, double time1)
        {
            double time = 0;
            double speed = 0;
            time += CalculateTime(planks[0], new Point(0, 0), ref speed);
            if (time > minTime)
            {
                return;
            }
            for (int i = 1; i < size; i++)
            {
                time += CalculateTime(planks[i], planks[i - 1], ref speed);
                if (time > minTime)
                {
                    return;
                }
            }
            if (time == time1)
            {
                Console.WriteLine("1" + " time=" + time + " time1=" + time1 + " count pl=" + size);
            }
            else
            {
                Console.WriteLine("0" + " time=" + time + " time1=" + time1 + " count pl=" + size);
            }

            if (time < minTime)
            {
                minTime = time;
            }
        }

        /*
        static void Go(Point point, Point[] planks, int size, double speed, double time)
        {
            if (point.X == n && point.Y == m)
            {
                CalculateAllTime(planks,size,time);
            }
            else
            {
                for (int i = point.X; i <= n; i++)//вертикаль
                {
                    for (int j = point.Y + 1; j < m; j++)//горизонталь
                    {
                        if (j!=m || i==n)
                        {
                            Point newPoint = new Point(i, j);
                            double newSpeed = speed;
                            double tempTime = time;
                            if(size>0)
                            {

                                tempTime += CalculateTime(newPoint, planks[size - 1], ref newSpeed);
                                Console.WriteLine("#" + size + " time - " + tempTime);
                                if(tempTime > minTime)
                                {
                                    return;
                                }
                            }
                            Point[] newPlanks = new Point[planks.Length];
                            Array.Copy(planks, newPlanks, planks.Length);
                            newPlanks[size]= newPoint;
                            Go(newPoint, newPlanks, size+1, newSpeed, tempTime);
                        }
                    }
                }
            }
        }

        /*static void Go(Point point, Point[] planks, int size, double speed, double time)
        {
            if (point.X == n && point.Y == m)
            {
                if (size > 1)
                {
                    if (!(point.Y == m && point.X != n))
                    {
                        minTime = time;
                    }
                }
                else
                {
                    CalculateAllTime(planks, size);
                }
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
                            if (size > 0)
                            {
                                time += CalculateTime(newPoint, planks[size - 1], ref speed);
                                if (time > minTime)
                                {
                                    return;
                                }
                            }

                            Point[] newPlanks = new Point[planks.Length];
                            Array.Copy(planks, newPlanks, planks.Length);
                            newPlanks[size] = newPoint;

                            Go(newPoint, newPlanks, size + 1, speed, time);
                        }
                    }
                }
            }
        }
    }
}*/
