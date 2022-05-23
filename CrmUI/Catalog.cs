using System;
using System.Windows.Forms;
using System.Data.Entity;

namespace CrmUI
{
    public partial class Catalog<T> : Form
        where T:class
    {
        public Catalog(DbSet<T> set)
        {
            InitializeComponent();
            dataGridView.DataSource = set.Local.ToBindingList();
        }

        

        private void Catalog_Load(object sender, EventArgs e)
        {

        }
    }
}
