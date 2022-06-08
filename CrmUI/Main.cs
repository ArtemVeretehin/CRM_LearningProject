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

        //Блок кода создания и вызова формы каталога для различных сущностей
        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogProduct = new Catalog<Product>(db.Products, db);
            catalogProduct.Show();
        }

        private void sellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogSeller = new Catalog<Seller>(db.Sellers, db);
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
        //Конец блока

        //Блок кода для вызова форм добавления данных в каталоге
        private void customerAddToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Customers.Add(form.customer);
                db.SaveChanges();
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
        //Конец блока

        //Вызов формы управления компьютерной моделью
        private void ModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ModelForm = new ModelForm();
            ModelForm.Show();
        }

        //Вывод списка продуктов из БД на главную форму
        private void Main_Load(object sender, EventArgs e)
        {
            Task Products_Load = Task.Run(() =>
            {
                var products = db.Products.ToArray();
                listBox1.Invoke((Action)delegate { listBox1.Items.AddRange(products); });
            });
        }

        //Добавление продуктов в корзину покупателя
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Product product)
            {
                //Если количество продукта больше 0 (смотрим по форме - считаем что в момент формирования заказа данные по ней актуальны)
                if (product.Count > 0)
                {
                    //Добавляем продукты в список заказа, корзину, обновляем общую цену заказа
                    listBox2.Items.Add(product);
                    cart.Add(product);
                    label3.Text = System.Convert.ToString(cart.Price);

                    //Обновляем количества продукта в списке продуктов
                    int selectedIndex = listBox1.Items.IndexOf(product);
                    listBox1.Items.RemoveAt(selectedIndex);
                    Product productToList = new Product()
                    {
                        ProductID = product.ProductID,
                        Name = product.Name,
                        Count = product.Count,
                        Price = product.Price
                    };
                    productToList.Count--;
                    listBox1.Items.Insert(selectedIndex, productToList);
                }
                else
                {
                    MessageBox.Show("Нет такого количества выбранного продукта", "Ошибка корзины", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem is Product selectedProduct)
            {
                //Если в списке продуктов есть продукт который удаляется из корзины   
                if (listBox1.Items.Contains(listBox2.SelectedItem))
                {
                    //Определяем индекс этого продукта в списке продуктов
                    int indexListBox1 = listBox1.Items.IndexOf(listBox2.SelectedItem);

                    //Получаем продукт из списка продуктов
                    var product = listBox1.Items[indexListBox1] as Product;
                    //Удаляем продукт из корзины и обновляем общую стоимость заказа
                    cart.Remove(product);
                    label3.Text = System.Convert.ToString(cart.Price);

                    //Изменяем данные в списке товаров
                    listBox1.Items.RemoveAt(indexListBox1);
                    product.Count++;
                    listBox1.Items.Insert(indexListBox1, product);
                    //Удаляем продукт из списка заказанного
                    listBox2.Items.Remove(selectedProduct);
                }
            }
        }

        //Авторизация покупателя
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login(db);
            if (login.ShowDialog() == DialogResult.OK)
            {
                customer = login.Customer;
                cart.Customer = customer;
                linkLabel1.Text = $"Здравствуй, {cart.Customer}";
            }
        }

        //Проведение транзакции
        private void button1_Click(object sender, EventArgs e)
        {
            //Если произвели авторизацию, то производим транзакцию и сбрасываем ненужные данные
            if ((customer != null) && (cart.Products.Count > 0))
            {
                //Удаляем контекст
                db.Dispose();
                //Производим транзакцию
                cashDesk.Enqueue(cart);
                cashDesk.Dequeue();

                //Производим сброс данных и обновление списка продуктов
                db = new CrmContext();
                cart = new Cart(customer);
                listBox1.Items.Clear();
                listBox1.Items.AddRange(db.Products.ToArray());
                label3.Text = "0";
                listBox2.Items.Clear();
                

                MessageBox.Show("Покупка выполнена успешно", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (customer == null) MessageBox.Show("Требуется авторизация!");
                else if (cart.Products.Count == 0) MessageBox.Show("Добавьте продукты в корзину!");
            }
        }

        //Разлогинивание пользователя
        private void button2_Click(object sender, EventArgs e)
        {
            if (customer != null)
            {
                customer = null;
                linkLabel1.Text = $"Здравствуй, гость";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            db.Dispose();
            db = new CrmContext();
            //var prods = db.Products.Select(p => p);
            //var entityEntry = db.Entry(prods);
            //db.Entry(db.Products).Reload();
            listBox1.Items.Clear();
            Product[] products = db.Products.ToArray();
            listBox1.Items.AddRange(products);
        }
    }
}
