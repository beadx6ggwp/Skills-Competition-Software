namespace _104_5
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mse_label = new System.Windows.Forms.Label();
            this.mae_label = new System.Windows.Forms.Label();
            this.psnr_label = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mse_label
            // 
            this.mse_label.AutoSize = true;
            this.mse_label.Location = new System.Drawing.Point(390, 283);
            this.mse_label.Name = "mse_label";
            this.mse_label.Size = new System.Drawing.Size(28, 12);
            this.mse_label.TabIndex = 0;
            this.mse_label.Text = "MSE";
            // 
            // mae_label
            // 
            this.mae_label.AutoSize = true;
            this.mae_label.Location = new System.Drawing.Point(390, 315);
            this.mae_label.Name = "mae_label";
            this.mae_label.Size = new System.Drawing.Size(30, 12);
            this.mae_label.TabIndex = 1;
            this.mae_label.Text = "MAE";
            // 
            // psnr_label
            // 
            this.psnr_label.AutoSize = true;
            this.psnr_label.Location = new System.Drawing.Point(390, 347);
            this.psnr_label.Name = "psnr_label";
            this.psnr_label.Size = new System.Drawing.Size(33, 12);
            this.psnr_label.TabIndex = 2;
            this.psnr_label.Text = "PSNR";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(424, 280);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(133, 22);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(424, 312);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(133, 22);
            this.textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(424, 344);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(133, 22);
            this.textBox3.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(392, 234);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "Calc";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 395);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.psnr_label);
            this.Controls.Add(this.mae_label);
            this.Controls.Add(this.mse_label);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mse_label;
        private System.Windows.Forms.Label mae_label;
        private System.Windows.Forms.Label psnr_label;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
    }
}

