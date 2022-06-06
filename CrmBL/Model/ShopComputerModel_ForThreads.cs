using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrmBL.Model
{
    public class ShopComputerModel_ForThreads
    {
        Generator Generator = new Generator();
        bool isWorking = false;      

        public List<CashDesk> CashDesks { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Check> Checks { get; set; }
        public List<Sell> Sell { get; set; }
        public Queue<Seller> Sellers { get; set; }
        public int CustomerSpeed = 1000;
        public int CashDeskSpeed { get; set; } = 1000;
        

        public ShopComputerModel_ForThreads()
        {
            CashDesks = new List<CashDesk>();
            Carts = new List<Cart>();
            Checks = new List<Check>();
            Sell = new List<Sell>();
            Sellers = new Queue<Seller>();

            //Генерируем 20 продавцов (Seller) и ставим их в очередь Sellers
            var sellers = Generator.GetNewSellers(20);
            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }

            //Генерируем 20 продуктов
            Generator.GetNewProducts(1000);

            //В список касс добавляются три новых кассы, продавцами на которых являются три первых продавца в сгенерированной очереди рандомных продавцов
            for (int i = 0; i < 3; i++)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue()));
            }
        }

        public void Start()
        {
            isWorking = true;

            //Создание задачи для генерации корзин и распределения корзин по кассам
            Task.Run(() => CreateCarts(10, ref CustomerSpeed));
            
            //Создание задач на обработку покупок в кассах
            var cashDeskTasks = CashDesks.Select(c => new Task(() => CashDeskWork(c)));

            //Запуск задач
            foreach (var task in cashDeskTasks) task.Start();
        }

        //Функция генерации корзин (создание клиента, создание на основе него корзины, добавление продуктов в корзину) и распределение созданных корзин по кассам.
        private void CreateCarts(int customersCount, ref int sleep)
        {
            while (isWorking)
            {
                //Создание 10 рандомных клиентов
                var customers = Generator.GetNewCustomers(customersCount);

                //Для каждого рандомного клиента создается корзина, которая забивается случайным количеством случайных продуктов. Созданная корзина ставится на кассу с наименьшей загруженностью
                foreach (var customer in customers)
                {      
                    var cart = new Cart(customer);

                    foreach (var prod in Generator.GetRandomProducts(10, 30))
                    {
                        cart.Add(prod);
                    }

                    //Выбор кассы с наименьшим количеством корзин в очереди
                    var cash = CashDesks.FirstOrDefault(x => x.Count == CashDesks.Min(c => c.Count));

                    //Постановка корзины в выбранную кассу
                    cash.Enqueue(cart);
                    //Вызов обработчика события "Произошла обработка корзины (добавлена в очередь/выкинута на мороз)"                    
                }
                
                Thread.Sleep(sleep);
           }
        }

        private void CashDeskWork(CashDesk cashDesk)
        {
            int sleep;
            while (isWorking)
            {
                sleep = CashDeskSpeed;
                if (cashDesk.Count > 0)
                {
                    cashDesk.Dequeue();
                    Thread.Sleep(sleep);
                }
            }
        }

        public void Stop()
        {
            isWorking = false;
        }
    }
}
