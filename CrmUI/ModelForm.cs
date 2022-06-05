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
    public partial class ModelForm : Form
    {
        ShopComputerModel_ForThreads Thread_model = new ShopComputerModel_ForThreads();
        ShopComputerModel Simple_model = new ShopComputerModel();
        public ModelForm()
        {
            InitializeComponent();
            CustomerSpeed.Value = Thread_model.CustomerSpeed;
            CashDeskSpeed.Value = Thread_model.CashDeskSpeed;
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            var cashDeskViews = new List<CashDeskView>();

            //Для каждой кассы в компьютерной модели создаем свои графические элементы на экранной форме (пары лэйбл-числовое поле)
            for(int i = 0; i < Thread_model.CashDesks.Count; i++)
            {
                cashDeskViews.Add(new CashDeskView(Thread_model.CashDesks[i], i, 12, 9));
            }
            foreach (var CashDeskView in cashDeskViews)
            {
                Controls.Add(CashDeskView.Label_cashDesk);
                Controls.Add(CashDeskView.Label_QueueWorkload);
                Controls.Add(CashDeskView.Label_CustomersGone);
                Controls.Add(CashDeskView.NumericUpDown_Price);
                Controls.Add(CashDeskView.NumericUpDown_CustomersGone);
                Controls.Add(CashDeskView.ProgressBar);
            }

            Thread_model.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var cashDeskViews = new List<CashDeskView>();

            //Для каждой кассы в компьютерной модели создаем свои графические элементы на экранной форме(пары лэйбл - числовое поле)
            for (int i = 0; i < Simple_model.CashDesks.Count; i++)
            {
                cashDeskViews.Add(new CashDeskView(Simple_model.CashDesks[i], i, 12, 9));
            }
            foreach (var CashDeskView in cashDeskViews)
            {
                Controls.Add(CashDeskView.Label_cashDesk);
                Controls.Add(CashDeskView.Label_QueueWorkload);
                Controls.Add(CashDeskView.Label_CustomersGone);
                Controls.Add(CashDeskView.NumericUpDown_Price);
                Controls.Add(CashDeskView.NumericUpDown_CustomersGone);
                Controls.Add(CashDeskView.ProgressBar);
            }

            Simple_model.Start();

        }

        private void ModelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Thread_model.Stop();
        }

        private void CustomerSpeed_ValueChanged(object sender, EventArgs e)
        {
            Thread_model.CustomerSpeed = (int)CustomerSpeed.Value;      
        }

        private void CashDeskSpeed_ValueChanged(object sender, EventArgs e)
        {
            Thread_model.CashDeskSpeed = (int)CashDeskSpeed.Value;
        }

    }
}
