using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBL.Model
{
    //Виртуальный объект для моделирования
    //По сути выполняет функции контроллера
    //Реализует логику продажи
    public class CashDesk
    {
        CrmContext db = new CrmContext();
        public int Number { get; set; }
        public Seller Seller { get; set; }

        public Queue<Cart> Queue { get; set; }
        public int MaxQueueLenght { get; set; }
        public int ExitCustomer { get; set; }
        public bool IsModel { get; set; }
        public CashDesk(int number, Seller seller)
        {
            Number = number;
            Seller = seller;
            Queue = new Queue<Cart>();
            IsModel = true;
            MaxQueueLenght = 10;
        }

        public void Enqueue(Cart cart)
        {
            if (Queue.Count <= MaxQueueLenght)
            {
                Queue.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
            }
        }

        public decimal Dequeue()
        {
            decimal sum = 0;
            var card = Queue.Dequeue();
            if (card != null)
            {
                var check = new Check()
                {
                    SellerId = Seller.SellerID,
                    Seller = Seller,
                    CustomerId = card.Customer.CustomerId,
                    Customer = card.Customer,
                    Created = DateTime.Now
                };

                if (!IsModel)
                {
                    db.Checks.Add(check);

                }
                else
                {
                    check.CheckId = 0;
                }

                var Sells = new List<Sell>();

                foreach (Product product in card)
                {
                    if (product.Count > 0)
                    {
                        var sell = new Sell()
                        {
                            ProductId = product.ProductID,
                            //Product = product,
                            CheckId = check.CheckId,
                            //Check = check
                        };

                        Sells.Add(sell);

                        if (!IsModel)
                        {
                            db.Sells.Add(sell);
                        }
                        sum += product.Price;
                        product.Count--;
                    }
                }
                if (!IsModel)
                {
                    db.SaveChanges();
                }
            }
            return sum;
        }
    }
}
