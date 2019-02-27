using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1_4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("请输入第一个数字：");
                double a = double.Parse(Console.ReadLine());
                Console.Write("请输入第二个数字：");
                double b = double.Parse(Console.ReadLine());
                Console.WriteLine("两个数的积是{0}", a * b);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("输入有误！");
                Console.ReadKey();
            }
        }
    }
}
