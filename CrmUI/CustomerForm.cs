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
    public partial class CustomerForm : Form
    {
        public Customer customer { get; set; }

        public CustomerForm()
        {
            InitializeComponent();
        }

        public CustomerForm(Customer customer): this()
        {
            this.customer = customer;
            textBox1.Text = this.customer.Name;
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (customer == null)
            { 
                customer = new Customer { Name = textBox1.Text };
            }
            else
            {
                customer.Name = textBox1.Text;
            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
