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
        ShopComputerModel model = new ShopComputerModel();
        public ModelForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //model.Start();

            var cashDeskViews = new List<CashDeskView>();

            //Для каждой кассы в компьютерной модели создаем свои графические элементы на экранной форме (пары лэйбл-числовое поле)
            for(int i = 0; i < model.CashDesks.Count; i++)
            {
                cashDeskViews.Add(new CashDeskView(model.CashDesks[i], i, 12, 9));
                //Controls.Add(cashDeskViews[cashDeskViews.Count - 1].Label);
                //Controls.Add(cashDeskViews[cashDeskViews.Count - 1].NumericUpDown);
            }
            foreach (var CashDeskView in cashDeskViews)
            {
                Controls.Add(CashDeskView.Label);
                Controls.Add(CashDeskView.NumericUpDown);
            }

        }


    }
}
