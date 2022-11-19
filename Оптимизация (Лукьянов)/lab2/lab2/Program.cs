using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace lab2
{
    class lab
    {
        const double speedCar = 20 / 3.6d;
        static void Main(string[] args)
        {
            List<long> carsList = InitCar();
            List<long> rDistance = InitCar();
            for (int i = 0; i < rDistance.Count; i++)
            {
                carsList.Add(rDistance[i] - 8);
            }
            carsList.Add(-5);
            carsList.Add(10100);
            long[] cars = carsList.ToArray();
            Array.Sort(cars);

            for (int f = 0; f < cars.Length - 1; f++)
            {
                if (cars[f] + 5 + 8 <= cars[f + 1])
                {
                    Answer(cars[f] + 5);
                    break;
                }
            }
        }

        static List<long> InitCar()
        {
            Console.ReadLine();
            List<long> distance = new List<long>();
            distance = Array.ConvertAll(Console.ReadLine().Split(' '), s => long.Parse(s)).ToList();
            return distance;
        }

        static void Answer(long i)
        {
            Console.WriteLine(i / speedCar);
        }
    }
}