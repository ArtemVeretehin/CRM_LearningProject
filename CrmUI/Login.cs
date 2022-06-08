using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUI
{
    public partial class Login : Form
    {
        CrmContext db;
        public Customer Customer { get; set; }
        public Login(CrmContext db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer = new Customer()
            {
                Name = textBox1.Text
            };
            var tempCustomer = db.Customers.FirstOrDefault(c => c.Name == Customer.Name);
            if (tempCustomer != null)
            { 
                Customer = tempCustomer;
                this.DialogResult = DialogResult.OK;
            }
            else
            { 
                LoginFailed loginFailed = new LoginFailed();
                loginFailed.ShowDialog();
                if (loginFailed.DialogResult == DialogResult.OK) loginFailed.Close();
            }
        }

    }
}
