namespace CrmUI
{
    partial class ModelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CustomerSpeed = new System.Windows.Forms.NumericUpDown();
            this.CashDeskSpeed = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashDeskSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 63);
            this.button1.TabIndex = 0;
            this.button1.Text = "Запуск магазина в многопоточном режиме";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(229, 350);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 63);
            this.button2.TabIndex = 2;
            this.button2.Text = "Запуск магазина в однопоточном режиме";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(654, 377);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Скорость генерации клиентов";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(654, 409);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Скорость обработки корзин клиентов";
            // 
            // CustomerSpeed
            // 
            this.CustomerSpeed.Location = new System.Drawing.Point(929, 375);
            this.CustomerSpeed.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.CustomerSpeed.Name = "CustomerSpeed";
            this.CustomerSpeed.Size = new System.Drawing.Size(120, 22);
            this.CustomerSpeed.TabIndex = 5;
            this.CustomerSpeed.ValueChanged += new System.EventHandler(this.CustomerSpeed_ValueChanged);
            // 
            // CashDeskSpeed
            // 
            this.CashDeskSpeed.Location = new System.Drawing.Point(929, 407);
            this.CashDeskSpeed.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.CashDeskSpeed.Name = "CashDeskSpeed";
            this.CashDeskSpeed.Size = new System.Drawing.Size(120, 22);
            this.CashDeskSpeed.TabIndex = 6;
            this.CashDeskSpeed.ValueChanged += new System.EventHandler(this.CashDeskSpeed_ValueChanged);
            // 
            // ModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 450);
            this.Controls.Add(this.CashDeskSpeed);
            this.Controls.Add(this.CustomerSpeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ModelForm";
            this.Text = "ModelForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.CustomerSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashDeskSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown CustomerSpeed;
        private System.Windows.Forms.NumericUpDown CashDeskSpeed;
    }
}