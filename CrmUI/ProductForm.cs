using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrmBL.Model;

namespace CrmUI
{
    public partial class ProductForm : Form
    {
        public Product product { get; set; }
        public ProductForm()
        {
            InitializeComponent();
        }

        public ProductForm(Product product) : this()
        {
            this.product = product;
            textBox1.Text = product.Name;
            numericUpDown1.Value = product.Price;
            numericUpDown2.Value = product.Count;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (product == null)
            {
                product = new Product() {Name = textBox1.Text, Price = numericUpDown1.Value, Count = (int)numericUpDown2.Value };
            }
            else
            {
                product.Name = textBox1.Text;
                product.Price = numericUpDown1.Value;
                product.Count = (int)numericUpDown2.Value;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
