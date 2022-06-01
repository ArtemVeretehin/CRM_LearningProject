using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBL.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public virtual ICollection<Sell> Sells { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public override int GetHashCode()
        {
            return ProductID;
        }
        public override bool Equals(object obj)
        {
            Product product = obj as Product;
            return this.Name == product.Name;
            //return this.ProductID == product.ProductID; //- неверное сравнение (ProductId генерится в базу автоматически и мы не задаем его, поэтому у всех объектов Product это свойство равно 0)
        }

    }
}
