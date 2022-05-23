﻿using System;
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

        private void button1_Click(object sender, EventArgs e)
        {
            product = new Product()
            {
                Name = textBox1.Text
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
