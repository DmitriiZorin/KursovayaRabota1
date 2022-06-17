using System;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using steganography;

namespace LAB3
{
    public partial class Form2 : Form
    {
        Bitmap toExecute;
        Bitmap toSave;
        LSB LsbMachine;
        PVD mach;

        public Form2()
        {
            InitializeComponent();
            LsbMachine = new LSB();
            mach = new PVD();
            DownloadImg.Visible = false;
            toSave = null;
            toExecute = null;
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if ((!Insert.Checked && !Extract.Checked) ||
                (!PVD.Checked && !LSB.Checked))
            {
                MessageBox.Show("Выберите метод стеганографии и действие.");
                return;
            }
            else
            {
                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Загрузите картинку.");
                    return;
                }

                if (Insert.Checked)
                {
                    Bitmap result = null;
                    try
                    {
                        Stopwatch tim = new Stopwatch();
                        if (LSB.Checked)
                        {
                            tim.Start();
                            result = LsbMachine.LsbEncodeImage(toExecute, textBox1.Text);
                            tim.Stop();
                        }
                        if (PVD.Checked)
                                result = mach.PvdEncodeImage(toExecute, textBox1.Text);

                        pictureBox1.Image = new Bitmap(result);
                        toSave = new Bitmap(result);
                        DownloadImg.Visible = true;
                        using (StreamWriter s = new StreamWriter("logResearch.txt", true))
                        {
                            String typ = "";
                            if (LSB.Checked) typ += "LSB";
                            else typ += "LSB";
                            s.WriteLine($"{typ} Msg: \"{textBox1.Text}\" Time(ms): {tim.ElapsedMilliseconds}"); 
                        }

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        if (ex.Message == "Bits Overflow")
                            MessageBox.Show("Сообщение слишком большое. Сократите его либо\nвозьмите картинку более высокого разрешения.");
                    }
                }
                else if (Extract.Checked)
                {
                    String resmsg = null;

                    if (LSB.Checked)
                        resmsg = LsbMachine.LsbDecodeImage(toExecute);

                    if (PVD.Checked)
                        resmsg = mach.PvdDecodeImage(toExecute);

                    textBox1.Clear();
                    if (resmsg != "Сообщения нет.") textBox1.Text = "Сообщение в изображении: " + resmsg;
                    else textBox1.Text = resmsg;
                }
            }
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            DownloadImg.Visible = false;
            toSave = null;
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string[] path = openFileDialog1.FileNames;

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path[0]);
            System.IO.FileStream fileStream = fileInfo.OpenRead();
            Bitmap rawImage = (Bitmap)Bitmap.FromStream(fileStream);
            fileStream.Close();

            toExecute = rawImage;
            pictureBox1.Image = rawImage;
        }

        private void DownloadImg_MouseClick(object sender, MouseEventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            toSave.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            DownloadImg.Visible = false;
            toSave = null;
            pictureBox1.Image = null;
        }

        private void Extract_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = null;
            textBox1.Text = "";
            DownloadImg.Visible = false;
        }

        private void Insert_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = null;
            textBox1.Text = ""; 
            DownloadImg.Visible = false;
        }

    }
}
