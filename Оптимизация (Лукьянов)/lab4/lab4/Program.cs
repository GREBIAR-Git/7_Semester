using System;
using System.Collections.Generic;

namespace lab4
{
    class Point : Point1, IComparable<Point>
    {
        public Point(int x, int y) : base(x, y)
        {
            this.x = x;
            this.y = y;
        }

        public virtual int CompareTo(Point other)
        {

            if (x == other.x && y == other.y)
            {
                return 0;
            }
            else if (y > other.y)
            {
                return 1;
            }
            else if (y == other.y && x > other.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

    }

    class Point1 : IComparable<Point1>
    {
        public int x;
        public int y;
        public Point1(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public virtual int CompareTo(Point1 other)
        {
            if (x == other.x && y == other.y)
            {
                return 0;
            }
            else if (x == other.x && y > other.y)
            {
                return 1;
            }
            else if (x > other.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Program
    {
        static Point[] BubbleSort(Point[] mas)
        {
            Point temp;
            for (int i = 0; i < mas.Length; i++)
            {
                for (int j = i + 1; j < mas.Length; j++)
                {
                    if (mas[i].y > mas[j].y)
                    {
                        temp = mas[i];
                        mas[i] = mas[j];
                        mas[j] = temp;
                    }
                    else if (mas[i].y == mas[j].y && mas[i].x > mas[j].x)
                    {
                        temp = mas[i];
                        mas[i] = mas[j];
                        mas[j] = temp;
                    }
                }
            }
            return mas;
        }

        static Point1[] BubbleSort(Point1[] mas)
        {
            Point1 temp;
            for (int i = 0; i < mas.Length; i++)
            {
                for (int j = i + 1; j < mas.Length; j++)
                {
                    if (mas[i].x > mas[j].x)
                    {
                        temp = mas[i];
                        mas[i] = mas[j];
                        mas[j] = temp;
                    }
                    else if (mas[i].x == mas[j].x && mas[i].y > mas[j].y)
                    {
                        temp = mas[i];
                        mas[i] = mas[j];
                        mas[j] = temp;
                    }
                }
            }
            return mas;
        }

        static int NumberShips(int totalDistance, int shipDecks)
        {
            if (totalDistance >= shipDecks)
            {
                return totalDistance - (shipDecks - 1);
            }
            else
            {
                return 0;
            }
        }
        static void AddUnique(List<Point> points, int x, int y)
        {
            if (!FindShip(points, x, y))
            {
                points.Add(new Point(x, y));
            }
        }

        static bool FindShip(List<Point> ships, int x, int y)
        {
            for (int i = 0; i < ships.Count; i++)
            {
                if (ships[i].x == x && ships[i].y == y)
                {
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            string[] firstLine = Console.ReadLine().Split(' ');
            short vertical = short.Parse(firstLine[0]);
            short horizontal = short.Parse(firstLine[1]);
            byte numberOfShips = byte.Parse(firstLine[2]);
            List<Point> points = new List<Point>();
            for (byte i = 0; i < numberOfShips; i++)
            {
                string[] nextLine = Console.ReadLine().Split(' ');
                int x = (int.Parse(nextLine[0]) - 1);//горизонталь
                int y = (int.Parse(nextLine[1]) - 1);//вертикаль
                byte numberDecks = byte.Parse(nextLine[2]);
                if (nextLine[3] == "V")
                {
                    int up = y > 0 ? -1 : 0;
                    int down = y + numberDecks < vertical ? 1 : 0;
                    for (int f = up; f < numberDecks + down; f++)
                    {
                        AddUnique(points, x, y + f);
                    }
                    if (x > 0)
                    {
                        for (int f = up; f < numberDecks + down; f++)
                        {
                            AddUnique(points, x - 1, y + f);
                        }
                    }
                    if (x + 1 < horizontal)
                    {
                        for (int f = up; f < numberDecks + down; f++)
                        {
                            AddUnique(points, x + 1, y + f);
                        }
                    }
                }
                else
                {
                    int left = x > 0 ? -1 : 0;
                    int right = x + numberDecks < horizontal ? 1 : 0;
                    for (int f = left; f < numberDecks + right; f++)
                    {
                        AddUnique(points, x + f, y);
                    }
                    if (y > 0)
                    {
                        for (int f = left; f < numberDecks + right; f++)
                        {
                            AddUnique(points, x + f, y - 1);
                        }
                    }
                    if (y + 1 < vertical)
                    {
                        for (int f = left; f < numberDecks + right; f++)
                        {
                            AddUnique(points, x + f, y + 1);
                        }
                    }
                }
            }
            byte shipDecks = byte.Parse(Console.ReadLine());
            Point[] horPoints = points.ToArray();
            Point1[] verPoints = points.ToArray();
            Array.Sort(verPoints);
            Array.Sort(horPoints);
            int number = 0;
            if (verPoints.Length > 0)
            {
                if (shipDecks == 1)
                {
                    number = horizontal * vertical - points.Count;
                }
                else
                {
                    int lastX = -1;
                    int column = 0;
                    bool first = true;
                    //вертикальный проход по кораблям
                    for (int i = 0; i < verPoints.Length; i++)
                    {
                        if (column != verPoints[i].x)
                        {
                            if (first)
                            {
                                number += (verPoints[i].x) * NumberShips(vertical, shipDecks);
                                first = false;
                            }
                            else
                            {
                                if (lastX != -1)
                                {
                                    number += NumberShips((vertical - (lastX + 1)), shipDecks);
                                }
                                if (column + 1 != verPoints[i].x)
                                {
                                    number += (verPoints[i].x - (column + 1)) * NumberShips(vertical, shipDecks);
                                }
                            }
                            column = verPoints[i].x;
                            lastX = -1;
                        }
                        first = false;
                        number += NumberShips((verPoints[i].y - (lastX + 1)), shipDecks);
                        lastX = verPoints[i].y;
                    }
                    if (column + 1 != horizontal)
                    {
                        number += (horizontal - (column + 1)) * NumberShips(vertical, shipDecks);
                    }
                    if (lastX != -1)
                    {
                        number += NumberShips((vertical - (lastX + 1)), shipDecks);
                    }


                    int lastY = -1;
                    int row = 0;
                    first = true;
                    //горизонтальный проход по кораблям
                    for (int i = 0; i < horPoints.Length; i++)
                    {
                        if (row != horPoints[i].y)
                        {
                            if (first)
                            {
                                number += (horPoints[i].y) * NumberShips(horizontal, shipDecks);
                                first = false;
                            }
                            else
                            {
                                if (lastY != -1)
                                {
                                    number += NumberShips((horizontal - (lastY + 1)), shipDecks);
                                }
                                if (row + 1 != horPoints[i].y)
                                {
                                    number += (horPoints[i].y - (row + 1)) * NumberShips(horizontal, shipDecks);//row+1
                                }
                            }
                            row = horPoints[i].y;
                            lastY = -1;
                        }
                        first = false;
                        number += NumberShips((horPoints[i].x - (lastY + 1)), shipDecks);//lastY+1
                        lastY = horPoints[i].x;
                    }
                    if (row + 1 != vertical)
                    {
                        number += (vertical - (row + 1)) * (NumberShips(horizontal, shipDecks));
                    }
                    if (lastY != -1)
                    {
                        number += NumberShips(horizontal - (lastY + 1), shipDecks);
                    }
                }
            }
            else
            {
                if (shipDecks == 1)
                {
                    number = vertical * horizontal;
                }
                else
                {
                    if (vertical - (shipDecks - 1) > 0)
                    {
                        number += (vertical - (shipDecks - 1)) * horizontal;
                    }
                    if (horizontal - (shipDecks - 1) > 0)
                    {
                        number += vertical * (horizontal - (shipDecks - 1));
                    }
                }
            }
            Console.WriteLine(number);
        }
    }
}
