using System;
using System.Drawing;
using steganography;
using System.IO;

namespace steganography
{
    public class PVD
    {
        //кодируем блоками по 2 пикселя
        //АЛЬФА КАНАЛ НЕ КОДИРУЕМ
        const int BITS_BY_PIXEL = 3;
        const int BITS_TO_SIZE = 32;
        const int BITS_TO_FLAG = 16;
        const int BITS_TO_SYMBOL = 16;
        const String FLAG = "1111111111111111";
        private void LogOn(String path, String data, bool append)
        {
            using (StreamWriter writer = new StreamWriter(path, append))
            {
                writer.WriteLine(data);
            }
        }
        private int GetGreyLevel(Color col)
        {
            return (col.R + col.G + col.B) / 3;
        }
        private void SetGreyLevel(Color col, int greylevel, out Color ans)
        {
            int prevgrey = GetGreyLevel(col);
            int delta = (greylevel - prevgrey);
            int r = col.R + delta;
            int g = col.G + delta;
            int b = col.B + delta;

            if (r < 0 || r > 255)
            {
                if (g + delta > 0 && g + delta < 256)
                    g -= delta;
                else if (b + delta > 0 && b + delta < 256)
                    b -= delta;
                r -= delta;
            }
            if (g < 0 || g > 255)
            {
                if (r + delta > 0 && r + delta < 256)
                    r -= delta;
                else if (b + delta > 0 && b + delta < 256)
                    b -= delta;
                g -= delta;
            }
            if (b < 0 || b > 255)
            {
                if (g + delta > 0 && g + delta < 256)
                    g -= delta;
                else if (r + delta > 0 && r + delta < 256)
                    r -= delta;
                b -= delta;
            }

            ans = Color.FromArgb(r, g, b);
        }
        private bool Encode2Pixels(String  msg, Color col1, Color col2, out Color o1, out Color o2, out int qwe)
        {
            int g1 = GetGreyLevel(col1),
                g2 = GetGreyLevel(col2),
                delta = (g2 - g1),
                bin = 1, i = 0;

            while (bin <= Math.Abs(delta)) 
            {
                bin <<= 1;
                i++;
            }
            int upper = bin;
            int lower = bin / 2;
            double n = Math.Truncate(Math.Log(upper - lower + 1) / Math.Log(2));
            if (Math.Abs(delta)<=1)
            {
                //if (msg[0] == '0')
                //{
                //    n = 1;
                //    upper = 1;
                //    lower = 0;
                //}
                //else
                {
                    qwe = 0;
                    o1 = new Color();
                    o2 = new Color();
                    return false;
                }
            }
            
            String submsg = msg.Substring(0, Math.Min(msg.Length, (int)n));
            while (submsg.Length != n)
            {   //для последней вставки в случае необхродимости удлинняем строку
                submsg += "1";
            }
            int toembed = BitMachine.BitToInt(submsg);
            int newdelta;
            newdelta = Math.Sign(delta) * (lower + toembed);

            int mid = (newdelta - delta);
            //if ((g1 - mid / 2) < 256 &&
            //    (g1 - mid / 2) > 0 &&
            //    (g2 + mid / 2) + (g2 - mid % 2) < 256 &&
            //    (g2 + mid / 2) + (g2 - mid % 2) > 0)
            {
                int a1, a2;
                int low = (mid) / 2;
                int up = mid - low;

                a1 = g1 - low;
                a2 = g2 + up;

                SetGreyLevel(col1, a1, out o1);
                SetGreyLevel(col2, a2, out o2);
                qwe = (int)n;
                int asd = Math.Abs(GetGreyLevel(o1) - GetGreyLevel(o2));

                if (asd != Math.Abs(newdelta)) 
                {
                    int a = 1 / 1;
                }
#if DEBUG
                LogOn("logEncode.txt", $"True: ({GetGreyLevel(col1)})({GetGreyLevel(col2)})" +
                                    $"({GetGreyLevel(o1)})({GetGreyLevel(o2)})" +
                                    $"({upper})({lower})" +
                                    $"({toembed})" + o1.ToString()
                                    + " --- " + o2.ToString(), true);
#endif 
                return true;
            }
            o1 = new Color();
            o2 = new Color();
            qwe = 0;
#if DEBUG
            LogOn("logEncode.txt", "False", true);
#endif
            return false;
        }
        private int Decode2Pixels(Color col1, Color col2, out int bits, int onsize)
        {
            bits = 0;
            int g1 = GetGreyLevel(col1),
                g2 = GetGreyLevel(col2),
                delta = (g2 - g1),
                bin = 1, i = 0;

            while (bin <= Math.Abs(delta))
            {
                bin <<= 1;
                i++;
            }
            int upper = bin;
            int lower = bin / 2;
            bits = (int)Math.Truncate(Math.Log(upper - lower + 1) / Math.Log(2));
            if (Math.Abs(delta) <= 1)
            {
                bits = 0;
                return 0;
            }

            int b = Math.Abs(Math.Abs(delta) - lower);

            return b;
        }
        public Bitmap PvdEncodeImage(Bitmap sourceImage, String msg)
        {
            //Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height, sourceImage.PixelFormat);
            Bitmap resultImage = new Bitmap(sourceImage);
            String log = "logEncode.txt";
            String bitmsg = FLAG + 
                BitMachine.IntToBit(msg.Length, BITS_TO_SIZE) + 
                BitMachine.MsgToBit(msg);
#if DEBUG
            LogOn(log, " ", false);
#endif
            int i = 0, p = 0;
            int width = sourceImage.Width - (sourceImage.Width % 2);
            int height = sourceImage.Height - (sourceImage.Height % 2);
            while (bitmsg.Length!=0)
            {
                //обрабатываем i и i+1 пиксЭлб
                try
                {
                    Color c1 = sourceImage.GetPixel((2 * i) % width, (2 * i) / height);
                    Color c2 = sourceImage.GetPixel((2 * i + 1) % width, (2 * i + 1) / height);
                    Color o1, o2;
                    int qwe = 0;
                    if (Encode2Pixels(bitmsg, c1, c2, out o1, out o2, out qwe))
                    {

                        resultImage.SetPixel((2 * i) % width, (2 * i) / height, o1);
                        resultImage.SetPixel((2 * i + 1) % width, (2 * i + 1) / height, o2);
                        if (qwe <= bitmsg.Length) bitmsg = bitmsg.Substring(qwe);
                        else bitmsg = "";
                        p++;
                    }
                    i++;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return resultImage;
        }
        public String PvdDecodeImage(Bitmap sourceImage)
        {
            String ans = "";
            String log = "logDecode.txt";
            int i = 0, p = 0;
            int width = sourceImage.Width - (sourceImage.Width % 2);
            int height = sourceImage.Height - (sourceImage.Height % 2);
#if DEBUG
            LogOn(log, " ", false);
#endif

            while (ans.Length < BITS_TO_FLAG)
            {
                Color c1 = sourceImage.GetPixel((2 * i) % width, (2 * i) / height);
                Color c2 = sourceImage.GetPixel((2 * i + 1) % width, (2 * i + 1) / height);
#if DEBUG
                LogOn(log, "True: "+
                        $"({GetGreyLevel(c1)})({GetGreyLevel(c2)})"   
                        + c1.ToString()
                        + " --- " + c2.ToString(), true);
#endif
                int n = 0;
                int a = Decode2Pixels(c1, c2, out n, 1);
                if (n > 0)
                {
                    ans += BitMachine.IntToBit(a, n);
                }
                i++;
            }
#if DEBUG
            LogOn(log, ans, true);
#endif
            int cnt = 0;
            foreach (var asd in ans)
            {
                if (asd == '0')
                    cnt++;
            }
            //if (ans.Substring(0, BITS_TO_FLAG) != FLAG)
            if (cnt > 2)
                return "Сообщения нет.";

            ans = ans.Substring(BITS_TO_FLAG, ans.Length - BITS_TO_FLAG);
            while (ans.Length < BITS_TO_SIZE)
            {
                Color c1 = sourceImage.GetPixel((2 * i) % width, (2 * i) / height);
                Color c2 = sourceImage.GetPixel((2 * i + 1) % width, (2 * i + 1) / height);
#if DEBUG
                LogOn(log, "True: " + $"({2 * i})" + c1.ToString()
                        + " --- " + $"({2 * i + 1})" + c2.ToString(), true);
#endif
                int n = 0;
                int a = Decode2Pixels(c1, c2, out n, 2);
                if (n > 0)
                {
                    ans += BitMachine.IntToBit(a, n);
                }
                i++;
            }

            int count = BitMachine.BitToInt(ans.Substring(0, BITS_TO_SIZE)) * BITS_TO_SYMBOL;
            ans = ans.Substring(BITS_TO_SIZE, ans.Length - BITS_TO_SIZE);

            while (ans.Length < count)
            {
                Color c1 = sourceImage.GetPixel((2 * i) % width, (2 * i) / height);
                Color c2 = sourceImage.GetPixel((2 * i + 1) % width, (2 * i + 1) / height);
#if DEBUG
                LogOn(log, "True: " + $"({2 * i})" + c1.ToString()
                        + " --- " + $"({2 * i + 1})" + c2.ToString(), true);
#endif
                int n = 0;
                int a = Decode2Pixels(c1, c2, out n, 3);
                if (n > 0)
                {
                    ans += BitMachine.IntToBit(a, n);
                }
                i++;
            }

            ans = ans.Substring(0, count);

            ans = BitMachine.BitToMsg(ans);
            ans = Clean(ans);
            return ans;
        }
        private String Clean(String s)
        {
            String ans = "";
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == '\0')
                    ans += " ";
                else
                    ans += s[i]; 
            }
            return ans;
        }
    }

}
