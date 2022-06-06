using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrmBL.Model;

namespace CrmUI
{
    public partial class Main : Form
    {
        CrmContext db;
        Cart cart;
        Customer customer;
        CashDesk cashDesk;

        public Main()
        {
            InitializeComponent();
            db = new CrmContext();
            cart = new Cart(customer);
            cashDesk = new CashDesk(1, db.Sellers.FirstOrDefault())
            {
                IsModel = false
            };
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogProduct = new Catalog<Product>(db.Products,db);
            catalogProduct.Show();
        }

        private void sellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogSeller = new Catalog<Seller>(db.Sellers,db);
            catalogSeller.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCustomer = new Catalog<Customer>(db.Customers, db);
            catalogCustomer.Show();
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCheck = new Catalog<Check>(db.Checks, db);
            catalogCheck.Show();
        }

        private void customerAddToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm();
            //label1.Text = "Test";
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Customers.Add(form.customer);
                db.SaveChanges();
                //label1.Text = db.Customers.ToList()[0].Name;
            }
        }

        private void sellerAddToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new SellerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Sellers.Add(form.seller);
                db.SaveChanges();
            }        
        }

        private void productAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Products.Add(form.product);
                db.SaveChanges();
            }
        }

        private void ModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ModelForm = new ModelForm();
            ModelForm.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Task Products_Load = Task.Run(() =>
            {
                var products = db.Products.ToArray();
                listBox1.Invoke((Action)delegate { listBox1.Items.AddRange(products); });
            });
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Product product)
            { 
                listBox2.Items.Add(product);
                cart.Add(product);
                label3.Text = System.Convert.ToString(cart.Price);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            if (login.ShowDialog() == DialogResult.OK)
            {
                //if (db.Customers.Where(c => c.Name == login.Customer.Name).Count() > 0)
                
                var tempCustomer = db.Customers.FirstOrDefault(c => c.Name.Equals(login.Customer.Name));
                if (tempCustomer != null)
                { 
                    customer = tempCustomer;
                    cart.Customer = customer;
                    linkLabel1.Text = $"Здравствуй, {tempCustomer.Name}";
                }
                else
                {
                    //db.Customers.Add(login.Customer);
                    //db.SaveChanges();
                    //customer = login.Customer;
                    LoginFailed loginfailedform = new LoginFailed();
                    if (loginfailedform.ShowDialog() == DialogResult.OK)
                    {
                        loginfailedform.Close();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.Dispose();
            if(customer !=null)
            {
                cashDesk.Enqueue(cart);
                cashDesk.Dequeue();
                listBox2.Items.Clear();
                cart = new Cart(customer);

                MessageBox.Show("Покупка выполнена успешно","Успех",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Требуется авторизация!");
            }
        }
    }
}
