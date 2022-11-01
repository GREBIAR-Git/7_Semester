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
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            int g = 10;
            int n = Int32.Parse(Console.ReadLine());
            int m = Int32.Parse(Console.ReadLine());
            List<Point> planks = new List<Point>();
            while(true)
            {
                Console.WriteLine("Planks:");
                while(true)
                {
                    try
                    {
                        planks.Add(new Point(Int32.Parse(Console.ReadLine()), Int32.Parse(Console.ReadLine())));
                    }
                    catch(Exception e)
                    {
                        break;
                    }
                }
                double time = 0;
                double speed = 0; // скорость не падает на поворотах
                for(int i = 0; i < planks.Count; i++)
                {
                    Point plank = planks[i];
                    Point prev_plank = i > 0 ? planks[i - 1] : new Point(0, 0);
                    double plank_length = Math.Sqrt(Math.Pow(plank.X - prev_plank.X, 2) + Math.Pow(plank.Y - prev_plank.Y, 2));
                    double cos_a = (double)(plank.Y - prev_plank.Y) / (plank_length);
                    Console.WriteLine("Plank " + (i+1).ToString() + " length == " + plank_length.ToString());
                    Console.WriteLine("Plank " + (i + 1).ToString() + " angle == " + (Math.Acos(cos_a) * (180 / Math.PI)).ToString());
                    double plank_time = cos_a > 0 ? (Math.Sqrt(Math.Pow(speed, 2) + 2 * g * cos_a * plank_length) - speed) / (g * cos_a) : plank_length/ speed;

                    speed += g * cos_a * plank_time;
                    time += plank_time;
                }
                Console.WriteLine("Result: " + time.ToString() + "\n\n\n");
                planks.Clear();
            }
        }
    }
}
