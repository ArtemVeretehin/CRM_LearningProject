using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBL.Model
{
    public class Check
    {
        public int CheckId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public virtual Customer Customer { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public virtual Seller Seller { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Sell> Sells {get;set;}
        public decimal Price { get; set; }
        public override string ToString()
        {
            return $"{CheckId} от {Created.ToString("hh:mm:ss:yyyy:MM:dd")}";
        }
    }
}
