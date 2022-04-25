
namespace LAB3
{
    partial class Form2
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
            this.Insert = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Extract = new System.Windows.Forms.RadioButton();
            this.executeStego = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.LSB = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.loadImg = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.DownloadImg = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Insert
            // 
            this.Insert.AutoSize = true;
            this.Insert.Checked = true;
            this.Insert.Location = new System.Drawing.Point(6, 19);
            this.Insert.Name = "Insert";
            this.Insert.Size = new System.Drawing.Size(103, 17);
            this.Insert.TabIndex = 0;
            this.Insert.TabStop = true;
            this.Insert.Text = "Встроить текст";
            this.Insert.UseVisualStyleBackColor = true;
            this.Insert.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Insert_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Extract);
            this.groupBox1.Controls.Add(this.Insert);
            this.groupBox1.Location = new System.Drawing.Point(572, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 69);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Что делать с изображением?";
            // 
            // Extract
            // 
            this.Extract.AutoSize = true;
            this.Extract.Location = new System.Drawing.Point(6, 42);
            this.Extract.Name = "Extract";
            this.Extract.Size = new System.Drawing.Size(99, 17);
            this.Extract.TabIndex = 1;
            this.Extract.TabStop = true;
            this.Extract.Text = "Извлечь текст";
            this.Extract.UseVisualStyleBackColor = true;
            this.Extract.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Extract_MouseClick);
            // 
            // executeStego
            // 
            this.executeStego.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.executeStego.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.executeStego.Location = new System.Drawing.Point(578, 377);
            this.executeStego.Name = "executeStego";
            this.executeStego.Size = new System.Drawing.Size(194, 50);
            this.executeStego.TabIndex = 2;
            this.executeStego.Text = "Выполнить";
            this.executeStego.UseVisualStyleBackColor = true;
            this.executeStego.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.LSB);
            this.groupBox2.Location = new System.Drawing.Point(572, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Выберите метод стеганографии";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 42);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(88, 17);
            this.radioButton3.TabIndex = 1;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "PVD???GLM";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // LSB
            // 
            this.LSB.AutoSize = true;
            this.LSB.Checked = true;
            this.LSB.Location = new System.Drawing.Point(6, 19);
            this.LSB.Name = "LSB";
            this.LSB.Size = new System.Drawing.Size(45, 17);
            this.LSB.TabIndex = 0;
            this.LSB.TabStop = true;
            this.LSB.Text = "LSB";
            this.LSB.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(37, 381);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(376, 50);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 365);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Введите сообщение";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(554, 312);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // loadImg
            // 
            this.loadImg.Location = new System.Drawing.Point(407, 340);
            this.loadImg.Name = "loadImg";
            this.loadImg.Size = new System.Drawing.Size(159, 23);
            this.loadImg.TabIndex = 5;
            this.loadImg.Text = "Загрузить изображение";
            this.loadImg.UseVisualStyleBackColor = true;
            this.loadImg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button2_MouseClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "*.bmp|*.bmp";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // DownloadImg
            // 
            this.DownloadImg.Location = new System.Drawing.Point(242, 340);
            this.DownloadImg.Name = "DownloadImg";
            this.DownloadImg.Size = new System.Drawing.Size(159, 23);
            this.DownloadImg.TabIndex = 6;
            this.DownloadImg.Text = "Сохранить изображение";
            this.DownloadImg.UseVisualStyleBackColor = true;
            this.DownloadImg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DownloadImg_MouseClick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.DownloadImg);
            this.Controls.Add(this.loadImg);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.executeStego);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form2";
            this.Text = "Steganography";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton Insert;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Extract;
        private System.Windows.Forms.Button executeStego;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton LSB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button loadImg;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button DownloadImg;
    }
}