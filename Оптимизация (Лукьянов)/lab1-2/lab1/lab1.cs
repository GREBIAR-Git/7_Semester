using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace labs
{
    class lab
    {
        static void Main(string[] args)
        {
            ushort N = ushort.Parse(Console.ReadLine());
            StringBuilder str = new StringBuilder();
            Dictionary<uint, bool> edinichki = new Dictionary<uint, bool>();
            for (uint i = 0, f = 0; i < 2147483648; i += f)
            {
                edinichki.Add(i, true);
                f++;
            }

            for (uint i = 0; i < N; i++)
            {
                if (edinichki.ContainsKey(uint.Parse(Console.ReadLine()) - 1))
                {
                    str.Append("1 ");
                }
                else
                {
                    str.Append("0 ");
                }
            }
            Console.WriteLine(str);
        }
    }
}
