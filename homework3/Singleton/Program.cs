using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            EventHandler e1;
            EventHandler e2;

            //单例测试
            e1 = EventHandler.GetInstance();
            e2 = EventHandler.GetInstance();

            if (ReferenceEquals(e1,e2))
                Console.WriteLine("二者是同一对象！");
            else Console.WriteLine("二者不是同一对象！");
        }
    }


    //定义单例类
    class EventHandler
    {
        //私有构造方法
        private EventHandler() { }

        private static class EventHandlerInstance{
           public readonly static EventHandler instance = new EventHandler();
        }

        public static EventHandler GetInstance()
        {
            return EventHandlerInstance.instance;
        }
    }
}
