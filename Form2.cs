﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace LAB3
{
    public partial class Form2 : Form
    {
        LSB megamachine1;
        Bitmap toExecute;
        Bitmap toSave;


        public Form2()
        {
            InitializeComponent();
            megamachine1 = new LSB();
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
                if (toExecute == null)
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
                            result = megamachine1.LsbEncodeImage(toExecute, textBox1.Text);

                        pictureBox1.Image = result;
                        toSave = result;
                        DownloadImg.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Overflow")
                            MessageBox.Show("Сообщение слишком большое. Сократите его либо\nвозьмите картинку более высокого разрешения.");
                    }
                }
                else if (Extract.Checked)
                {
                    String resmsg = null;

                    if (Extract.Checked && LSB.Checked)
                        resmsg = megamachine1.LsbDecodeImage(toExecute);
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
            Image rawImage = System.Drawing.Image.FromStream(fileStream);
            Application.DoEvents();
            fileStream.Close();


            toExecute = new Bitmap(rawImage);
            pictureBox1.Image = toExecute;
        }

        private void DownloadImg_MouseClick(object sender, MouseEventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            toSave.Save(saveFileDialog1.FileName + ".bmp");
            DownloadImg.Visible = false;
            toSave = null;
        }

        private void Extract_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = null;
            textBox1.Text = "";
        }

        private void Insert_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = null;
            textBox1.Text = "";
        }
    }
    public class LSB
    {
        //АЛЬФА КАНАЛ НЕ КОДИРУЕМ
        const int BITS_BY_PIXEL = 4;
        const int BITS_TO_SIZE = 32;
        const int BITS_TO_SYMBOL = 16;
        
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        private String IntToBit(int val, int bits)
        {
            String ans = null;
            for (int i = 0; i < bits; i++)
            {
                ans += (val % 2 == 0 ? "0" : "1");
                val >>= 1;
            }
            ans = Reverse(ans);
            return ans;
        }
        private int BitToInt(String bit)
        {
            int t = 0;
            for (int i = 0; i < bit.Length; i++)
            {
                t += (bit[i] == '0' ? 0 : 1) * (int)Math.Pow(2, bit.Length - i - 1);
            }
            return t;
        }
        private Color EncodePixel(String msg, Color col)
        {
            //Color a = Color.FromArgb((col.A >> 1 << 1) + (msg[0] == '0' ? 0 : 1),
            //                          (col.R >> 1 << 1) + (msg[1] == '0' ? 0 : 1),
            //                          (col.G >> 1 << 1) + (msg[2] == '0' ? 0 : 1),
            //                          (col.B >> 1 << 1) + (msg[3] == '0' ? 0 : 1));

            int g = (col.G >> 2 << 2) + (msg[0] == '0' ? 0 : 2) + (msg[1] == '0' ? 0 : 1);
            int b = (col.B >> 2 << 2) + (msg[2] == '0' ? 0 : 2) + (msg[3] == '0' ? 0 : 1);

            Color a = Color.FromArgb(col.A, col.R, g, b);
            return a;
        }
        private int DecodePixel(Color col)
        {
            int ans = 0;
            ans += ((col.G) % 4);
            ans <<= 2;
            ans += ((col.B) % 4);
            return ans;
        }
        private String MsgToBit(String msg)
        {
            String ans = null;

            for (int i = 0; i < msg.Length; i++)
            {
                int a = ((int)msg[i]);
                string asd = null;
                while (a != 0)
                {
                    asd += (a % 2 == 0 ? "0" : "1");
                    a /= 2;
                }
                while (asd.Length != BITS_TO_SYMBOL)
                {
                    asd += "0";
                }
                ans += Reverse(asd);
            }

            return ans;
        }
        private String BitToMsg(String bit)
        {
            String ans = null;

            for (int i = 0; i * BITS_TO_SYMBOL < bit.Length; i++)
            {
                String s = bit.Substring(i * BITS_TO_SYMBOL, BITS_TO_SYMBOL);
                int t = 0;
                for (int j = 0; j < BITS_TO_SYMBOL; j++)
                {
                    t += (s[j] == '0' ? 0 : 1) * (int)Math.Pow(2, BITS_TO_SYMBOL - 1 - j);
                }
                ans += (char)t;
            }

            return ans;
        }
        public Bitmap LsbEncodeImage(Bitmap img, String msg)
        {
            Bitmap boat = new Bitmap(img);

            if (boat.Width * boat.Height < (BITS_TO_SIZE + msg.Length * BITS_TO_SYMBOL / BITS_BY_PIXEL))
                throw new Exception("Overflow");

            //исходное сообщение переделываем в последовательность бит
            int t = (msg.Length * BITS_TO_SYMBOL / BITS_BY_PIXEL);
            String bitmsg = IntToBit(t, BITS_TO_SIZE);
            bitmsg += MsgToBit(msg);

            int i = 0;
            while (i * BITS_BY_PIXEL < bitmsg.Length)
            {
                Color ext = boat.GetPixel(i % boat.Width, i / boat.Height);
                Color ins = EncodePixel(bitmsg.Substring(i * BITS_BY_PIXEL, BITS_BY_PIXEL), ext);
                boat.SetPixel(i % boat.Width, i / boat.Height, ins);
                i++;
            }

            return boat;
        }
        public String LsbDecodeImage(Bitmap img)
        {
            String ans = null;
            Bitmap boat = new Bitmap(img);

            int num = 0;
            int i = 1;
            String bitnum = null;

            while (i * BITS_BY_PIXEL < BITS_TO_SIZE)
            {
                Color t = boat.GetPixel(i % boat.Width, i / boat.Height);
                bitnum += (IntToBit(DecodePixel(t), BITS_BY_PIXEL));
                i++;
            }
            num = BitToInt(bitnum);

            //num пикселей обрабатываем
            int off = (BITS_TO_SIZE / BITS_BY_PIXEL);
            i = 0;
            while (i  < num)
            {
                Color t = boat.GetPixel((i+off) % boat.Width, (i + off) / boat.Height);
                ans += (IntToBit(DecodePixel(t), BITS_BY_PIXEL));
                i++;
            }

            if (ans != null) ans = BitToMsg(ans);
            else ans = "Сообщения нет.";

            return ans;
        }
    }
    class PVD
    {

    }
}
