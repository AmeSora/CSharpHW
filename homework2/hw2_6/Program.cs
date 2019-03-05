using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw2_6
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("作业2.6：计算素数因子");
                Console.Write("请输入一个整数:");
                int a = int.Parse(Console.ReadLine());
                Console.Write("所有素数因子为：");
                for (int i = 2; i <= a; i++)
                {
                    while (a % i == 0)
                    {
                        Console.Write(i + " ");
                        a /= i;
                    }
                }
                Console.ReadKey();

            } catch (Exception e)
            {
                Console.WriteLine("请输入整数！");
                Console.ReadKey();
            }
        }
    }
}
