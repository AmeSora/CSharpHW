using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw2_9
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] s = new int[101];
            for (int i = 0; i < 101; i++) s[i] = i;
            s[0] = s[1] = 0;

            //筛选至sqrt(n)
            for (int i = 2; i <= Math.Sqrt(100);i++)
            {
                if (s[i] != 0)
                {
                    for (int j = i + 1; j < s.Length; j++)
                        //若不是素数则置0
                        if (s[j] % s[i] == 0) s[j] = 0;
                }
            }

            Console.Write("2-100间的素数有：");
            foreach (int i in s)
                if (i != 0) Console.Write(i+" ");
            Console.ReadKey();
        }
    }
}
