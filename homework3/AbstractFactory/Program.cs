using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            GameFactory gameFactory;
            InterfaceController interfaceController;
            OperationController operationController;

            Console.WriteLine("AndriodFactory示例：");
            gameFactory = new AndriodFactory();

            interfaceController = gameFactory.CreateInterfaceController();
            operationController = gameFactory.CreateOperationController();

            interfaceController.InitInterfaceController();
            operationController.InitOperationController();

            Console.WriteLine("-----------------------------------");

            Console.WriteLine("SymbianFactory示例：");
            gameFactory = new SymbianFactory();

            interfaceController = gameFactory.CreateInterfaceController();
            operationController = gameFactory.CreateOperationController();

            interfaceController.InitInterfaceController();
            operationController.InitOperationController();
        }
    }

    //初始化游戏各组件的工厂接口
    interface GameFactory
    {
        OperationController CreateOperationController();
        InterfaceController CreateInterfaceController();
    }

    //操作控制接口
    interface OperationController
    {
        void InitOperationController();
    }

    //界面控制接口
    interface InterfaceController
    {
        void InitInterfaceController();
    }


    //Symbian系统实现
    class SymbianOperationController : OperationController
    {
        public void InitOperationController()
        {
            Console.WriteLine("初始化Symbian系统的Operation Controller");
        }
    }

    class SymbianInterfaceController : InterfaceController
    {
        public void InitInterfaceController()
        {
            Console.WriteLine("初始化Symbian系统的Interface Controller");
        }
    }

    class SymbianFactory : GameFactory
    {
        public SymbianFactory()
        {
            Console.WriteLine("在Symbian系统上启动游戏");
        }
        public InterfaceController CreateInterfaceController()
        {
            return new SymbianInterfaceController();
        }

        public OperationController CreateOperationController()
        {
            return new SymbianOperationController();
        }

    }

    //Andriod系统实现
    class AndriodInterfaceController : InterfaceController
    {
        public void InitInterfaceController()
        {
            Console.WriteLine("初始化Andriod系统的Interface Controller");
        }
    }

    class AndriodOperationController : OperationController
    {
        public void InitOperationController()
        {
            Console.WriteLine("初始化Andriod系统的Operation Controller");
        }
    }

    class AndriodFactory : GameFactory
    {
        public AndriodFactory()
        {
            Console.WriteLine("在Andriod系统上启动游戏");
        }
        public InterfaceController CreateInterfaceController()
        {
            return new AndriodInterfaceController();
        }

        public OperationController CreateOperationController()
        {
            return new AndriodOperationController();
        }
    }


}
