using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace CrmBL.Model
{
    public class Cart : IEnumerable
    {
        public Customer Customer { get; set; }
        public Dictionary<Product,int> Products { get; set; }
        public decimal Price
        {
            get
            {
                return GetAll().Sum(x => x.Price);
            }
            set
            {
                Price = value;
            }
        }


        public Cart(Customer customer)
        {
            Customer = customer;
            Products = new Dictionary<Product, int>();
        }
        public void Add(Product product)
        {
            if(Products.TryGetValue(product,out int count))
            {
                Products[product] = ++count;
            }
            else
            {
                Products.Add(product, 1);
            }
            
        }

        public void Remove(Product product)
        {
            if (Products.TryGetValue(product, out int count))
            {
                if (count > 1) Products[product]--;
                else if (count == 1) Products.Remove(product);
            }
        }
        
        public IEnumerator GetEnumerator()
        {
            foreach (var product in Products.Keys)
            {
                for (int i = 0; i < Products[product]; i++)
                {
                    yield return product;
                }
            }
        }
        public List<Product> GetAll()
        {
            var result = new List<Product>();
            foreach(Product i in this)
            {
                result.Add(i);
            }
            return result;
        }

    }
}
