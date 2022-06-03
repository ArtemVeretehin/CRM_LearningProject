﻿using System;
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
            if (Queue.Count <= MaxQueueLenght)
            {
                Queue.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
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
                    Seller = Seller,
                    CustomerId = card.Customer.CustomerId,
                    Customer = card.Customer,
                    Created = DateTime.Now
                };

                //Если продажа в реальном времени - чек сохраняется в базе; Если продажа виртуальная, то в чек добавляется CheckId = 0
                if (!IsModel)
                {
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
