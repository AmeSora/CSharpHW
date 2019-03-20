using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Alarm
{
    /* 闹钟委托 */
    public delegate void AlarmDelegate(int left);
    class Program
    {
        static void Main(string[] args)
        {
            int h,m,s,left;
            Console.WriteLine("\n请设定闹钟剩余时间：");
            Console.WriteLine("\n小时：");

            try
            {
                h = int.Parse(Console.ReadLine());
                if (h < 0) throw new Exception();
                Console.WriteLine("\n分钟：");
                m = int.Parse(Console.ReadLine());
                if (m < 0) throw new Exception();
                Console.WriteLine("\n秒：");
                s = int.Parse(Console.ReadLine());

                left = h * 60 * 60 + m * 60 + s;

                Alarm alarm = new Alarm(left);

                alarm.alarmTime += ShowCurrentTime;
                alarm.start();


            } catch (Exception e)
            {
                Console.WriteLine("请输入正确的时间设置！");
                Console.ReadKey();
            }
 
        }

        static void ShowCurrentTime(int left)
        {
            
            Console.WriteLine("\n当前时间："+DateTime.Now.ToString("hh:mm:ss")+"\n剩余时间："+
                left+"s");
        }
    }

    class Alarm
    {
        /* 剩余时间（s） */
        public int timeLeft { set; get; }

        public Alarm (int left)
        {
            timeLeft = left;
        }

        /* 闹钟委托 */
        public event AlarmDelegate alarmTime;

        public void start()
        {
            for (int i = 1; i <= timeLeft; i++)
            {
                Console.Clear();
                alarmTime(timeLeft-i);
                Thread.Sleep(1000);
            }
            System.Media.SystemSounds.Beep.Play();
            Console.WriteLine("\n您设定的时间已到！");
            Console.ReadKey();
        }
    }
}
