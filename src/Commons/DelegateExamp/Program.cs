using System;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace DelegateExamp
{
    public delegate void MyDelegate();
    public delegate void MyDelegate2(string text);

    public delegate int MyDelegate3(int a, int b);

    public delegate void StockControl();
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.SendMessage();
            MyDelegate myDelegate = customerManager.SendMessage;
            myDelegate += customerManager.ShowAlert;
            myDelegate();
            myDelegate -= customerManager.ShowAlert;
            myDelegate();

            MyDelegate2 myDelegate2 = customerManager.SendMessage2;
            myDelegate2 += customerManager.ShowAlert2;
            myDelegate2("text");

            ///////////
            ///


            Func<int, int, int> add = Add;
            Console.WriteLine(add(3, 5));

            //Func<int> getRandomm = delegate() { return new Random().Next(1, 10); };

            Func<int> getRandom = () => new Random().Next(1, 100); //parametre almıyor sadece int dönürüyor.
            Console.WriteLine(getRandom());

            // Event

            Product gsm = new Product(50);
            gsm.ProductName = "GSM";
            Product hardDisk = new Product(50);
            hardDisk.ProductName = "Hard Disk";
            gsm.StockControlEvent += Gsm_StockControlEvent;

            for (int i = 0; i < 15; i++)
            {
                gsm.Sell(10);
                hardDisk.Sell(10);
                Console.ReadLine();
            }


            Console.Read();
        }

        private static void Gsm_StockControlEvent()
        {
            Console.WriteLine("Gsm about to finish...");
        }

        static int Add(int a, int b)
        {
            return a + b;
        }
    }

    public class Product
    {
        private int _stock;

        public Product(int stock)
        {
            _stock = stock;
        }

        public event StockControl StockControlEvent;
        public string ProductName { get; set; }

        public int Stock
        {
            get => _stock;
            set
            {
                _stock = value;
                if (value <= 15 && StockControlEvent != null)
                {
                    StockControlEvent();
                }

            }
        }

        public void Sell(int amount)
        {
            Stock -= amount;
            Console.WriteLine($"{ProductName} Stock : {Stock}");
        }
    }

    public class CustomerManager
    {
        public void SendMessage()
        {
            Console.WriteLine("Hello.");
        }
        public void SendMessage2(string text)
        {
            Console.WriteLine($"Hello {text}.");
        }

        public void ShowAlert()
        {
            Console.WriteLine("Warning.");
        }
        public void ShowAlert2(string text)
        {
            Console.WriteLine($"Warning {text}.");
        }
    }
}
