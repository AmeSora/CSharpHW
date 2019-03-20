using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderService orderService = new OrderService();

            Order o1 = new Order(1, "联想", "thinkpad", new OrderDetails(10, 80000, "清华"));
            Order o2 = new Order(2, "Apple", "MacBook", new OrderDetails(10, 120000, "北大"));
            Order o3 = new Order(3, "Dell", "XPS15", new OrderDetails(10, 80000, "人大"));
            Order o4 = new Order(4, "华为", "Matebook", new OrderDetails(10, 70000, "北航"));

            orderService.AddOrder(o1);
            orderService.AddOrder(o2);
            orderService.AddOrder(o3);
            orderService.AddOrder(o4);


            Console.WriteLine("\n\t欢迎进入订单管理系统\n");
            Console.WriteLine("\t   1、添加订单\n");
            Console.WriteLine("\t   2、删除订单\n");
            Console.WriteLine("\t   3、修改订单\n");
            Console.WriteLine("\t   4、根据订单编号查询订单\n");
            Console.WriteLine("\t   5、根据产品名称查询订单\n");
            Console.WriteLine("\t   6、根据顾客名称查询订单\n");
            Console.WriteLine("\t   输入-1以退出系统\n");
            Console.Write("\n\t请输入编号以选择功能：");
            try
            {
                int select = int.Parse(Console.ReadLine());
                if (select == -1) return;

                while (select != 1 && select != 2 && select != 3 && select != 4 && select != 5 && select != 6)
                {
                    Console.Write("\n\t请输入正确的选项！");
                    select = int.Parse(Console.ReadLine());
                }

                do
                {
                    switch (select)
                    {
                        case 1:
                            orderService.AddOrder();
                            break;
                        case 2:
                            Console.Write("\n\t请输入要删除的订单编号：");
                            int deleteId = int.Parse(Console.ReadLine());
                            orderService.DeleteOrderById(deleteId);
                            break;
                        case 3:
                            Console.Write("\n\t请输入要修改的订单编号：");
                            int modifyId = int.Parse(Console.ReadLine());
                            orderService.ModifyOrder(modifyId);
                            break;
                        case 4:
                            Console.Write("\n\t请输入要查询的订单编号：");
                            orderService.GetOrderById(int.Parse(Console.ReadLine()));
                            break;
                        case 5:
                            Console.Write("\n\t请输入要查询的产品名称：");
                            orderService.GetOrdersByProductName(Console.ReadLine());
                            break;
                        case 6:
                            Console.Write("\n\t请输入要查询的顾客名称：");
                            orderService.GetOrdersByCustomerName(Console.ReadLine());
                            break;
                    }
                    Console.Write("\n\t请继续输入以选择功能：");
                    select = int.Parse(Console.ReadLine());
                } while (select != -1);
                
            } catch (Exception e)
            {
                Console.WriteLine("您的输入有误！");
            }

        }
    }
}


class Order
{
    public Order()
    {
        OrderDetails = new OrderDetails();
    }

    public Order(int orderId, string producer, string productName, OrderDetails orderDetails)
    {
        OrderId = orderId;
        Producer = producer;
        ProductName = productName;
        OrderDetails = orderDetails;
    }

    public int OrderId { get; set; }
    public String Producer { get; set; }
    public String ProductName { get; set; }
    public OrderDetails OrderDetails { get; set; }


}

class OrderDetails
{

    public OrderDetails() { }

    public OrderDetails(int quantity, int totalPrice, string customerName)
    {
        Quantity = quantity;
        TotalPrice = totalPrice;
        CustomerName = customerName;
    }

    public int Quantity { get; set; }
    public int TotalPrice { get; set; }
    public String CustomerName { get; set; }

}

class OrderService
{
    private Dictionary<int,Order> orders;

    public OrderService()
    {
        orders = new Dictionary<int, Order>();
    }

    public void AddOrder()
    {

        try
        {
            Order order = new Order();

            Console.Write("\n\t请输入订单编号:");
            int orderId = int.Parse(Console.ReadLine());
            if (orders.ContainsKey(orderId))
            {
                Console.WriteLine("\n\t您添加的订单已存在！");
                return;
            }
            order.OrderId = orderId;

            Console.Write("\n\t请输入商品名称：");
            order.ProductName = Console.ReadLine();

            Console.Write("\n\t请输入生产厂商：");
            order.Producer = Console.ReadLine();
            
            Console.Write("\n\t请输入需求数量：");
            order.OrderDetails.Quantity = int.Parse(Console.ReadLine());

            Console.Write("\n\t请输入订单总价："); 
            order.OrderDetails.TotalPrice = int.Parse(Console.ReadLine());

            Console.Write("\n\t请输入顾客名称：");
            order.OrderDetails.CustomerName = Console.ReadLine();

            orders.Add(orderId, order);

            Console.WriteLine("\n\t订单添加成功！");

        }catch(Exception e)
        {
            Console.WriteLine("\n\t您的输入有误！");
        }
    }

    public void AddOrder(Order order)  
    {
        int OrderId = order.OrderId;
        try
        {
            orders.Add(OrderId, order);
        }
        catch(ArgumentException)
        {
            Console.WriteLine("\n\t您添加的订单已存在！");
        }
    }

    public void DeleteOrderById(int OrderId)
    {
        try
        {
            if (orders.Remove(OrderId))
                Console.WriteLine("\n\t删除成功！");
            else Console.WriteLine("\n\t目标删除订单不存在！");
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine("\n\t您未提供要删除的订单编号！");
        }
    }

    public void ModifyOrder(int OrderId)
    {
        if (!orders.ContainsKey(OrderId))
        {
            Console.WriteLine("\n\t您要修改的订单不存在！");
            return;
        }

        orders.TryGetValue(OrderId, out Order order);
        orders.Remove(OrderId);

        Console.Write("\n\t请输入要修改的项：");
        Console.Write("\n\t1、商品种类");
        Console.Write("\n\t2、生产厂商");
        Console.Write("\n\t3、商品数量");
        Console.Write("\n\t4、顾客姓名");
        Console.Write("\n\t5、订单编号");
        Console.Write("\n\t6、订单总价");

        try
        {
            int select = int.Parse(Console.ReadLine());
            while (select != 1 && select != 2 && select != 3 && select != 4 && select != 5 && select != 6)
            {
                Console.WriteLine("\n\t请输入正确的选项编号！");
                select = int.Parse(Console.ReadLine());
            }

            switch (select)
            {
                case 1:
                    Console.Write("\n\t请输入新的商品种类名称：");
                    String type = Console.ReadLine();
                    order.ProductName = type;
                    break;
                case 2:
                    Console.Write("\n\t请输入新的生产商：");
                    String fac = Console.ReadLine();
                    order.Producer = fac;
                    break;
                case 3:
                    Console.Write("\n\t请输入商品数量：");
                    int quantity = int.Parse(Console.ReadLine());
                    order.OrderDetails.Quantity = quantity;
                    break;
                case 4:
                    Console.Write("\n\t请输入顾客姓名：");
                    String name = Console.ReadLine();
                    order.OrderDetails.CustomerName = name;
                    break;
                case 5:
                    Console.Write("\n\t请输入新的订单编号：");
                    int id = int.Parse(Console.ReadLine());
                    if (orders.ContainsKey(id))
                    {
                        Console.WriteLine("\n\t您输入的编号已存在！");
                        return;
                    }
                    else
                    {
                        order.OrderId = id;
                    }
                    break;
                case 6:
                    Console.Write("\n\t请输入商品总价：");
                    int price = int.Parse(Console.ReadLine());
                    order.OrderDetails.TotalPrice = price;
                    break;
            }

            AddOrder(order);
        }
        catch (Exception e){
            Console.WriteLine("\n\t您输入的数字有误！");
        }
    }

    public void GetOrderById(int orderId)
    {
        
        if (!orders.ContainsKey(orderId))
        {
            Console.WriteLine("\n\t输入的订单编号不存在！");
            return;
        }

        ShowOrder(orders[orderId]);
    }

    public void GetOrdersByProductName(String name)
    {
        bool flag = false;
        foreach (KeyValuePair<int,Order> pair in orders)
        {
            if(pair.Value.ProductName.Equals(name))
            {
                ShowOrder(pair.Value);
                flag = true;
            }
        }

        if (!flag)
        {
            Console.WriteLine("\n\t输入的商品名称对应订单不存在！");
        }

    }

    public void GetOrdersByCustomerName(String name)
    {
        bool flag = false;
        foreach (KeyValuePair<int, Order> pair in orders)
        {
            if (pair.Value.OrderDetails.CustomerName.Equals(name))
            {
                ShowOrder(pair.Value);
                flag = true;
            }
        }

        if (!flag)
        {
            Console.WriteLine("\n\t输入的顾客名称对应订单不存在！");
        }

    }

    public void ShowOrder(Order order)
    {
        Console.WriteLine("\n\t订单详细信息如下：\n");
        Console.WriteLine("\t   订单编号：{0}", order.OrderId);
        Console.WriteLine("\t   产品名称：{0}", order.ProductName);
        Console.WriteLine("\t   生产厂商：{0}", order.Producer);
        Console.WriteLine("\t   需求数量：{0}", order.OrderDetails.Quantity);
        Console.WriteLine("\t   顾客名称：{0}", order.OrderDetails.CustomerName);
        Console.WriteLine("\t   订单总价：{0}\n", order.OrderDetails.TotalPrice);
    }
}
