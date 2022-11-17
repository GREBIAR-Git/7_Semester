using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string[]> names = new List<string[]>();
            byte n = byte.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                names.Add(Console.ReadLine().Split(' '));
            }
            byte[] numbers = Array.ConvertAll(Console.ReadLine().Split(' '), s => byte.Parse(s));
            string bestName = FindBestName(names[numbers[0] - 1]);
            StringBuilder str = new StringBuilder(bestName + '\n');
            for (int i = 1; i < numbers.Length; i++)
            {
                string[] fixNames = FixNames(names[numbers[i] - 1], bestName);
                if (fixNames.Length == 0)
                {
                    Console.WriteLine("IMPOSSIBLE");
                    return;
                }
                bestName = FindBestName(fixNames);
                str.AppendLine(bestName);
            }
            Console.WriteLine(str);
        }

        static string[] FixNames(string[] names, string beforeName)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < names.Length; i++)
            {
                for (int f = 0; f < names[i].Length; f++)
                {
                    if (f < beforeName.Length)
                    {
                        if (beforeName[f] < names[i][f])
                        {
                            result.Add(names[i]);
                            break;
                        }
                        else if (beforeName[f] > names[i][f])
                        {
                            break;
                        }
                    }
                    else
                    {
                        result.Add(names[i]);
                        break;
                    }
                }
            }
            return result.ToArray();
        }

        static string FindBestName(string[] names)
        {
            string bestName = names[0];
            for (int i = 1; i < names.Length; i++)
            {
                for (int f = 0; f < bestName.Length; f++)
                {
                    if (f < names[i].Length)
                    {
                        if (bestName[f] > names[i][f])
                        {
                            bestName = names[i];
                            break;
                        }
                        else if (bestName[f] < names[i][f])
                        {
                            break;
                        }
                    }
                    else
                    {
                        bestName = names[i];
                        break;
                    }
                }
            }
            return bestName;
        }
    }
}
