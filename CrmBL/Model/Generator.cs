using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBL.Model
{
    //Создает виртуальные объекты
    public class Generator
    {
        Random rnd = new Random();
        //public List<Cart> Carts { get; set; }
        //public List<Check> Checks { get; set; }
        //public List<CashDesk> CashDesks { get; set; }   
        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
        public List<Seller> Sellers { get; set; }

        public List<Customer> GetNewCustomers(int count)
        {
            var result = new List<Customer>();

            for (int i = 0; i < count; i++)
            {
                var customer = new Customer()
                {
                    CustomerId = Customers.Count,
                    Name = GetRandomText()
                };
                Customers.Add(customer);
                result.Add(customer);
            }
            return result;
        }

        public List<Seller> GetNewSellers(int count)
        {
            var result = new List<Seller>();

            for (int i = 0; i < count; i++)
            {
                var seller = new Seller()
                {
                    SellerID = Sellers.Count,
                    Name = GetRandomText()
                };
                Sellers.Add(seller);
                result.Add(seller);
            }
            return result;
        }

        public List<Product> GetNewProducts(int count)
        {
            var result = new List<Product>();

            for (int i = 0; i < count; i++)
            {
                var product = new Product()
                {
                    ProductID = Products.Count,
                    Name = GetRandomText(),
                    Count = rnd.Next(10, 1000),
                    Price = Convert.ToDecimal(rnd.Next(5,100000) + rnd.NextDouble())
                };
                Products.Add(product);
                result.Add(product);
            }
            return result;
        }

   
        private static string GetRandomText()
        {
            return Guid.NewGuid().ToString().Substring(0, 5);
        }
    }
}
