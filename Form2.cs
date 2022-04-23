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
        Random blessrng;
        LSB megamachine1;
        Image toSave;

        public Form2()
        {
            InitializeComponent();
            blessrng = new Random();
            blessrng.Next();
            megamachine1 = new LSB();
            DownloadImg.Visible = false;
            toSave = null;
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
                            result = megamachine1.LsbEncodeImage(pictureBox1.Image, textBox1.Text);

                        pictureBox1.Image = result;
                        toSave = result;
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
                        resmsg = megamachine1.LsbDecodeImage(pictureBox1.Image);
                    textBox1.Clear();
                    textBox1.Text = resmsg;
                }

                DownloadImg.Visible = true;
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

            foreach(string i in path)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(i);
                System.IO.FileStream fileStream = fileInfo.OpenRead();
                pictureBox1.Image = System.Drawing.Image.FromStream(fileStream);
                Application.DoEvents();
                fileStream.Close();
            }
        }

        private void DownloadImg_MouseClick(object sender, MouseEventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            toSave.Save(saveFileDialog1.FileName);
            DownloadImg.Visible = false;
            toSave = null;
        }
    }
    public class LSB
    {
        const int BITS_BY_PIXEL = 4;
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        private Color EncodePixel(String msg, Color col)
        {
            //msg = {0,1,2,3}

            //int t = col.ToArgb();
            //t = t / 4;
            //t += msg;
            //return Color.FromArgb(t);

            Color a = Color.FromArgb( (col.A >> 1 << 1) + (msg[0] == '0' ? 0 : 1),
                                      (col.R >> 1 << 1) + (msg[1] == '0' ? 0 : 1),
                                      (col.G >> 1 << 1) + (msg[2] == '0' ? 0 : 1),
                                      (col.B >> 1 << 1) + (msg[3] == '0' ? 0 : 1));
            return a;
        }
        private int DecodePixel(Color col)
        {
            //retval = {0,1,2,3}
            int ans = 0;
            ans += ((col.A) % 2);
            ans += ((col.R) % 2);
            ans += ((col.G) % 2);
            ans += ((col.B) % 2);

            return ans;
        }
        private String MsgToBit(String msg)
        {
            String ans = null;

            for (int i = 0; i < msg.Length; i++) 
            {
                byte a = ((byte)msg[i]);
                string asd = null;
                while (a != 0)
                {
                    asd += (a % 2 == 0 ? "0" : "1");
                    a /= 2;
                }
                while (asd.Length != 8)
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

            for (int i = 0; i*8 < bit.Length; i++)
            {
                String s = bit.Substring(i, 8);
                int t = 0;
                for (int j = 0; j < 8; j++)
                {
                    t += (s[i] == '0' ? 0 : 1) * (int)Math.Pow(2, 8 - i - 1);
                }
                ans += (char)t;
            }

            return ans;
        }
        public Bitmap LsbEncodeImage(Image img, String msg)
        {
            Bitmap boat = new Bitmap(img);

            if (boat.Width * boat.Height < (msg.Length * 2))
                throw new Exception("Overflow");

            String bitmsg = MsgToBit(msg); //исходное сообщение переделываем в последовательность бит


            int i = 0;
            while (i + 2 < bitmsg.Length)
            {
                Color t = boat.GetPixel(i / boat.Width, i % boat.Height);
                Color ins = EncodePixel(bitmsg.Substring(i, BITS_BY_PIXEL), t);
                boat.SetPixel(i / boat.Width, i % boat.Height, ins);
                i += BITS_BY_PIXEL;
            }
            
            return boat;
        }
        public String LsbDecodeImage(Image img)
        {
            String ans = null;


            return ans;
        }
    }
    class PVD
    {

    }
}
