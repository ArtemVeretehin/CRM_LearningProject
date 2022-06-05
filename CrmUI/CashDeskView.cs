using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUI
{
    public class CashDeskView
    {
        CashDesk cashDesk;
        public Label Label_cashDesk { get; set; }
        public Label Label_QueueWorkload { get; set; }
        public Label Label_CustomersGone { get; set; }
        public NumericUpDown NumericUpDown_Price { get; set; }
        public NumericUpDown NumericUpDown_CustomersGone { get; set; }
        public ProgressBar ProgressBar { get; set; }
        public ShopComputerModel_ForThreads model { get; set; }

        public CashDeskView(CashDesk cashDesk, int number, int x, int y)
        {
            this.model = model;
            this.cashDesk = cashDesk;
            Label_cashDesk = new System.Windows.Forms.Label();
            Label_QueueWorkload = new System.Windows.Forms.Label();
            Label_CustomersGone = new System.Windows.Forms.Label();
            NumericUpDown_Price = new System.Windows.Forms.NumericUpDown();
            NumericUpDown_CustomersGone = new System.Windows.Forms.NumericUpDown();
            ProgressBar = new System.Windows.Forms.ProgressBar();

            Label_cashDesk.AutoSize = true;
            Label_cashDesk.Location = new System.Drawing.Point(x, y + 30 * number);
            Label_cashDesk.Name = "label" + number;
            Label_cashDesk.Size = new System.Drawing.Size(44, 16);
            Label_cashDesk.TabIndex = number;
            Label_cashDesk.Text =  cashDesk.ToString();

            Label_QueueWorkload.AutoSize = true;
            Label_QueueWorkload.Location = new System.Drawing.Point(x + 200, y + 30 * number);
            Label_QueueWorkload.Name = "label" + number;
            Label_QueueWorkload.Size = new System.Drawing.Size(70, 16);
            Label_QueueWorkload.TabIndex = number;
            Label_QueueWorkload.Text = $"Загруженность кассы";

            Label_CustomersGone.AutoSize = true;
            Label_CustomersGone.Location = new System.Drawing.Point(x + 450, y + 30 * number);
            Label_CustomersGone.Name = "label" + number;
            Label_CustomersGone.Size = new System.Drawing.Size(90, 16);
            Label_CustomersGone.TabIndex = number;
            Label_CustomersGone.Text = $"Количество ушедших клиентов";

            NumericUpDown_Price.Location = new System.Drawing.Point(x + 75, (y - 2) + 30 * number);
            NumericUpDown_Price.Name = "numericUpDown" + number;
            NumericUpDown_Price.Size = new System.Drawing.Size(120, 22);
            NumericUpDown_Price.TabIndex = number;
            NumericUpDown_Price.Maximum = 10000000000000000000;

            NumericUpDown_CustomersGone.Location = new System.Drawing.Point(x + 630, (y - 2) + 30 * number);
            NumericUpDown_CustomersGone.Name = "numericUpDown" + number;
            NumericUpDown_CustomersGone.Size = new System.Drawing.Size(50, 22);
            NumericUpDown_CustomersGone.TabIndex = number;
            NumericUpDown_CustomersGone.Maximum = 10000000000000000000;

            ProgressBar.Location = new System.Drawing.Point(x + 330, (y - 2) + 30 * number);
            ProgressBar.Maximum = cashDesk.MaxQueueLenght;
            ProgressBar.Name = "progressBar1";
            ProgressBar.Size = new System.Drawing.Size(100, 20);
            ProgressBar.TabIndex = 1;
            ProgressBar.Value = 0;

            cashDesk.CheckClosed += CashDesk_CheckClosed;
            cashDesk.CardQueueUpdated += Card_QueueUpdated;
            cashDesk.CustomerGone += Customer_Gone;
        }
  

        //Данный метод позволяет пробросить действия из другого потока в текущий поток
        //Все три конструкции в метод равносильны и выполняют:
        //1.Вызов метода Invoke элемента типа Control
        //2.Создание делегата Action в качестве аргумента метода Invoke
        private void CashDesk_CheckClosed(object sender, Check e)
        {
            NumericUpDown_Price?.Invoke((Action)delegate { NumericUpDown_Price.Value += e.Price; });
            //NumericUpDown?.Invoke(new Action(() => { NumericUpDown.Value += e.Price; }));
            //NumericUpDown?.Invoke(new Action(delegate { NumericUpDown.Value += e.Price; }));
        }

        private void Card_QueueUpdated(CashDesk obj)
        {
            ProgressBar?.Invoke((Action)delegate { ProgressBar.Value = obj.Count; });
        }

        private void Customer_Gone(CashDesk obj)
        {
            NumericUpDown_CustomersGone?.Invoke((Action)delegate { NumericUpDown_CustomersGone.Value = obj.ExitCustomer; });
        }
    }
}
