using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
                (!radioButton3.Checked && !LSB.Checked))
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
                        if (LSB.Checked)
                            result = LsbMachine.LsbEncodeImage(toExecute, textBox1.Text);

                        if (radioButton3.Checked)
                            result = mach.PvdEncodeImage(toExecute, textBox1.Text);

                        pictureBox1.Image = new Bitmap(result);
                        toSave = new Bitmap(result);
                        DownloadImg.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Bits Overflow")
                            MessageBox.Show("Сообщение слишком большое. Сократите его либо\nвозьмите картинку более высокого разрешения.");
                    }
                }
                else if (Extract.Checked)
                {
                    String resmsg = null;

                    if (LSB.Checked)
                        resmsg = LsbMachine.LsbDecodeImage(toExecute);

                    if (radioButton3.Checked)
                        resmsg = mach.PvdDecodeImage(toExecute);

                    textBox1.Clear();
                    textBox1.Text = "Сообщение в изображении: " + resmsg;
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
    public class PVD
    {
        public Bitmap PvdEncodeImage(Bitmap img, String msg)
        {
            Bitmap ans = new Bitmap(img);
            Random rng = new Random();

            for (int i = 0; i < img.Width * img.Height - 100; i++)
            //for (int i = 0; i < 1; i++)
            {
                int x0 = (3 * i) % (img.Width - 1);
                int y0 = (2 * i) / (img.Height - 2);
                Color toEmbed = Color.FromArgb(rng.Next(0, 256), rng.Next(0, 256), rng.Next(0, 256));
                ans.SetPixel(x0, y0, toEmbed);
                ans.SetPixel(x0 + 1, y0, toEmbed);
                ans.SetPixel(x0 + 2, y0, toEmbed);

                ans.SetPixel(x0, y0 + 1, toEmbed);
                ans.SetPixel(x0 + 1, y0 + 1, toEmbed);
                ans.SetPixel(x0 + 2, y0 + 1, toEmbed);

            }

            return ans;
        }
        public String PvdDecodeImage(Bitmap img)
        {
            String ans = null;

            return ans;
        }
    }
}
