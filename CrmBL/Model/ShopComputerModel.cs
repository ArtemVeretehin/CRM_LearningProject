using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBL.Model
{
    public class ShopComputerModel
    {
        Generator Generator = new Generator();
        public List<CashDesk> CashDesks { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Check> Checks { get; set; }
        public List<Sell> Sell { get; set; }
        public Queue<Seller> Sellers { get; set; }

        public ShopComputerModel()
        {
            CashDesks = new List<CashDesk>();
            Carts = new List<Cart>();
            Checks = new List<Check>();
            Sell = new List<Sell>();
            Sellers = new Queue<Seller>();

            //Генерируем 20 продавцов (Seller) и ставим их в очередь Sellers
            var sellers = Generator.GetNewSellers(20);
            
            //Генерируем 20 продуктов
            Generator.GetNewProducts(1000);

            //Generator.GetNewCustomers(100);
            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }
            
            //В список касс добавляются три новых кассы, продавцами на которых являются три первых продавца в сгенерированной очереди рандомных продавцов
            for (int i = 0; i < 3; i++)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue()));
            }
        }

        public void Start()
        {
            //Создание 10 рандомных клиентов
            var customers = Generator.GetNewCustomers(10);

            //Список корзин
            var carts = new Queue<Cart>();

            //Для каждого рандомного клиента создается корзина, которая забивается случайным количеством случайных продуктов. Созданная корзина добавляется в список корзин
            foreach(var customer in customers)
            {
                var cart = new Cart(customer);

                foreach (var prod in Generator.GetRandomProducts(10,30))
                {
                    cart.Add(prod);
                }

                carts.Enqueue(cart);   
            }


            //Выбираем кассу.Ставим корзину в очередь на выбранной кассе. Выбранную корзину удаляем из списка
            while (carts.Count > 0)
            {
                //Выбор кассы с наименьшим количеством корзин в очереди
                var cash = CashDesks.FirstOrDefault(x => x.Count == CashDesks.Min(c => c.Count));
                cash.Enqueue(carts.Dequeue());
            }

            while (CashDesks.Where(c=>c.Count > 0).Count() > 0)
            {
                var cash = CashDesks.FirstOrDefault(x => x.Count > 0);
                cash.Dequeue();
            }

        }
    }
}
