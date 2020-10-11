using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_DZ_11
{
    class Customer
    {
        public int Money { get; set; }
        public int Assets { get; set; }
        public string Name { get; set; }
        public Customer(string name, int money = 100000)
        {
            Name = name;
            if (money>0)
            {
                Money = money;
            }
            else
            {
                Money = 100000;
                
            }
            Assets = 0;
        }
        public void Sell(int course)
        {
            int assets;
            Random rand = new Random();
            if (Money < course)
            {
                Console.WriteLine("No money");
                return;
            }
            assets = rand.Next(1, (Money / course) + 1);
            Assets += assets;
            Money -= assets * course;
            Console.WriteLine(Name+" buy "+assets+" by course "+course);
        }
        public void Show()
        {
            Console.WriteLine($"Name: {Name}\nAssets: {Assets}\nMoney: {Money}\n");
        }
    }

    class Seller
    {
        public int Money { get; set; }
        public int Assets { get; set; }
        public string Name { get; set; }

        public Seller(string name, int assets = 15)
        {
            Name = name;
            if (assets > 0)
            {
                Assets = assets;
            }
            else
            {
                Assets = 15;
            }
        }
        public void Buy(int course)
        {
            Random rand = new Random();
            int assets;
            if (Assets < 0)
            {
                Console.WriteLine("No assets that you can sell");
                return;
            }
            assets = rand.Next(1, Assets + 1);
            Assets -= assets;
            Money += course * assets;
            Console.WriteLine(Name+" sell "+assets+" by course "+course);
        }
        public void Show()
        {
            Console.WriteLine($"Name: {Name}\nAssets: {Assets}\nMoney: {Money}\n");
        }
    }

    class Exchange
    {
        public delegate void CourseDelegate(int course);
        public event CourseDelegate CourseChangeUp;
        public event CourseDelegate CourseChangeDown;
        public string Name { get; set; }
        private int course;
        public void SetCourse()
        {
            Random rand = new Random();
            course = rand.Next(-10, 11);
        }
        public int Course
        {
            get { return course; }
            set
            {
                if (course+value > course)
                {
                    course += value;
                    CourseChangeUp?.Invoke(course);
                }
                else if (course + value < course)
                {
                    course += value;
                    CourseChangeDown?.Invoke(course);
                }
                else
                {
                    course = value;
                }
            }
        }
        public Exchange(string name, int course)
        {
            Name = name;
            this.course = 0;
            Course = course;
        }

        public void Show()
        {
            Console.WriteLine($"Name: {Name}\nCourse: {Course}\n");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Seller s1 = new Seller("Seller 1", 12), s2 = new Seller("Seller 2"), s3 = new Seller("Seller 3", 10);
            Customer c1 = new Customer("Customer 1"), c2 = new Customer("Customer 2", 180000000), c3 = new Customer("Customer 3");
            Exchange exc = new Exchange("Kraken", 250);
            exc.CourseChangeUp += s1.Buy;
            exc.CourseChangeUp += s2.Buy;
            exc.CourseChangeUp += s3.Buy;
            exc.CourseChangeDown += c1.Sell;
            exc.CourseChangeDown += c2.Sell;
            exc.CourseChangeDown += c3.Sell;
            for (int i = 0; i < 10; i++)
            {
                Console.Clear();
                exc.Show();
                exc.SetCourse();
                Console.WriteLine(new string('~', 30));
                s1.Show();
                s2.Show();
                s3.Show();
                Console.WriteLine(new string('~', 30));
                c1.Show();
                c2.Show();
                c3.Show();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Press any key...");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }
        }
    }
}
