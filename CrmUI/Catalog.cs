using System;
using System.Windows.Forms;
using System.Data.Entity;
using CrmBL.Model;

namespace CrmUI
{
    public partial class Catalog<T> : Form
        where T:class
    {
        CrmContext db;
        DbSet set;
        public Catalog(DbSet<T> set, CrmContext db)
        {
            InitializeComponent();
            this.db = db;
            this.set = set;
            set.Load();
            dataGridView.DataSource = set.Local.ToBindingList();
        }

        

        private void Catalog_Load(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (typeof(T) == typeof(Product))
            {
                var form = new ProductForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    db.Products.Add(form.product);
                    db.SaveChanges();
                    dataGridView.Update();
                }
            }
            else if (typeof(T) == typeof(Seller))
            {
                var form = new SellerForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    db.Sellers.Add(form.seller);
                    db.SaveChanges();
                    dataGridView.Update();
                }
            }
            else if(typeof(T) == typeof(Customer))
            {
                var form = new CustomerForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    db.Customers.Add(form.customer);
                    db.SaveChanges();
                    dataGridView.Update();
                }
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            var id = dataGridView.SelectedRows[0].Cells[0].Value;
           
            if (typeof(T) == typeof(Product))
            {
                var product_set = this.set.Find(id) as Product;
                if (product_set!= null)
                {
                    ProductForm form = new ProductForm(product_set);
                    if (form.ShowDialog()==DialogResult.OK)
                    {    
                        db.SaveChanges();
                        dataGridView.Update();
                    }
                }
            }

            else if (typeof(T) == typeof(Seller))
            {
                var seller_set = this.set.Find(id) as Seller;
                if (seller_set != null)
                {
                    SellerForm form = new SellerForm(seller_set);
                    if (form.ShowDialog() == DialogResult.OK)
                    {   
                        db.SaveChanges();
                        dataGridView.Update();
                    }
                }
            }

            else if (typeof(T) == typeof(Customer))
            {
                var customer_set = this.set.Find(id) as Customer;
                if (customer_set != null)
                {
                    CustomerForm form = new CustomerForm(customer_set);
                    if (form.ShowDialog() == DialogResult.OK)
                    {   
                        db.SaveChanges();
                        dataGridView.Update();
                    }
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            var id = dataGridView.SelectedRows[0].Cells[0].Value;
            if (typeof(T) == typeof(Product))
            {
                var product_set = this.set.Find(id) as Product;
                if (product_set != null)
                {
                    db.Products.Remove(product_set);
                    db.SaveChanges();
                }
            }

            else if (typeof(T) == typeof(Seller))
            {
                var seller_set = this.set.Find(id) as Seller;
                if (seller_set != null)
                {
                    db.Sellers.Remove(seller_set);
                    db.SaveChanges();
                }
            }

            else if (typeof(T) == typeof(Customer))
            {
                var customer_set = this.set.Find(id) as Customer;
                if (customer_set != null)
                {
                    db.Customers.Remove(customer_set);
                    db.SaveChanges();
                }
            }
        }
    }
}
