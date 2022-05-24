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
    public partial class SellerForm : Form
    {
        public Seller seller { get; set; }
        public SellerForm()
        {
            InitializeComponent();
        }

        public SellerForm(Seller seller): this()
        {
            this.seller = seller;
            textBox1.Text = this.seller.Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            if (seller == null)
            { 
                seller = new Seller() { Name = textBox1.Text };
            }
            else
            {
                seller.Name = textBox1.Text;
            }*/
            seller = this.seller ?? new Seller();
            seller.Name = textBox1.Text;
           
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
