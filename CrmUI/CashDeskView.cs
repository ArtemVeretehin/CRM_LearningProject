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
        public Label Label { get; set; }
        public NumericUpDown NumericUpDown { get; set; }

        public CashDeskView(CashDesk cashDesk, int number, int x, int y)
        {
            this.cashDesk = cashDesk;
            Label = new System.Windows.Forms.Label();
            NumericUpDown = new System.Windows.Forms.NumericUpDown();
            
            Label.AutoSize = true;
            Label.Location = new System.Drawing.Point(x, y + 30 * number);
            Label.Name = "label" + number;
            Label.Size = new System.Drawing.Size(44, 16);
            Label.TabIndex = number;
            Label.Text =  cashDesk.ToString();

            NumericUpDown.Location = new System.Drawing.Point(x + 75, (y - 2) + 30 * number);
            NumericUpDown.Name = "numericUpDown" + number;
            NumericUpDown.Size = new System.Drawing.Size(120, 22);
            NumericUpDown.TabIndex = number;
            
        }
    }
}
