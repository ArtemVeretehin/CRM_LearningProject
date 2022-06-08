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
        //Номер кассы (уникальный идентификатор)
        public int Number { get; set; }

        //Продавец
        public Seller Seller { get; set; }

        //Очередь корзин
        public Queue<Cart> Queue { get; set; }

        //Максимальный размер очереди
        public int MaxQueueLenght { get; set; }

        //Количество клиентов, ушедших из-за переполнения очереди
        public int ExitCustomer { get; set; }

        //Флаг-признак виртуальности
        public bool IsModel { get; set; }

        //Количество корзин в очереди
        public int Count => Queue.Count;

        public event EventHandler<Check> CheckClosed;
        public event Action<CashDesk> CardQueueUpdated;
        public event Action<CashDesk> CustomerGone;

        private delegate void SellEvent_Handler(Check check);
        private SellEvent_Handler notify;
        private event SellEvent_Handler Notify
        {
            add
            {
                notify += value;
            }
            remove
            {
                notify -= value;
            }
        }

        public CashDesk(int number, Seller seller)
        {
            Number = number;
            Seller = seller;
            Queue = new Queue<Cart>();
            IsModel = true;
            MaxQueueLenght = 10;
        }

        //Функция установки корзины в очередь
        public void Enqueue(Cart cart)
        {
            if (Queue.Count < MaxQueueLenght)
            {
                Queue.Enqueue(cart);
                CardQueueUpdated?.Invoke(this);
            }
            else
            {
                ExitCustomer++;
                CustomerGone?.Invoke(this);
            }
        }

        //Функция реализующая логику продажи и формирования соответствующей транзакции
        public decimal Dequeue()
        {
            decimal sum = 0;
            var card = Queue.Dequeue();

            //Если в очереди есть корзины с товаром
            if (card != null)
            {
                //Формирование чека
                var check = new Check()
                {
                    SellerId = Seller.SellerID,
                    SellerName = Seller.Name,
                    CustomerId = card.Customer.CustomerId,
                    CustomerName = card.Customer.Name,
                    Created = DateTime.Now
                };

                //Если продажа в реальном времени - чек сохраняется в базе; Если продажа виртуальная, то в чек добавляется CheckId = 0
                if (!IsModel)
                {
                    //Redact!
                    //Notify += (Check Check) => db.Checks.Add(Check);
                    //notify?.Invoke(check);
                    
                    db.Checks.Add(check);

                }
                else
                {
                    check.CheckId = 0;
                }

                var Sells = new List<Sell>();
                
                //Перебор продуктов в текущей корзине
                foreach (Product product in card)
                {
                    //Если продукт присутствует на складе - формируется информация по продаже, заносится в БД (если необходимо); определяется общая цена продуктов в корзине; актуализируется количество продукта на складе
                    if (product.Count > 0)
                    {
                        var sell = new Sell()
                        {
                            ProductId = product.ProductID,
                            ProductName = product.Name,
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
                        var db_product = db.Products.FirstOrDefault(p => p.Name == product.Name);
                        db_product.Count = product.Count;
                    }
                }

                check.Price = sum;

                if (!IsModel)
                {
                    db.SaveChanges();
                }

                CheckClosed?.Invoke(this, check);
            }
            return sum;
        }

        public override string ToString()
        {
            return $"Касса №{Number}";
        }
    }
}
