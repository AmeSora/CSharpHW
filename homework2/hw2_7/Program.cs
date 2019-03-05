using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw2_7
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int[] x;
                Console.Write("请输入数组大小：");
                int n = int.Parse(Console.ReadLine());
                x = new int[n];
                Console.Write("请输入整数：");
                for (int i = 0; i < n; i++)
                {
                    x[i] = int.Parse(Console.ReadLine());
                }
                Find(x, out int max, out int min, out float ave, out int sum);
                Console.WriteLine($"最大值:{max},最小值:{min},总和:{sum},平均值:{ave}");
                Console.ReadKey();
            } catch (Exception e)
            {
                Console.WriteLine("请输入数字！");
                Console.ReadKey();
            }

            
        }

        public static void Find(int[] x, out int max, out int min, out float ave, out int sum)
        {
            max = min = x[0];
            sum = 0;
            foreach (int i in x)
            {
                if (max < i) max = i;
                if (min > i) min = i;
                sum += i;
            }
            ave = (float)sum / x.Length;
        }
    }
}
