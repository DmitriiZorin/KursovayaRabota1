using System;
using System.Drawing;

namespace steganography
{
    public class LSB
    {
        //АЛЬФА КАНАЛ НЕ КОДИРУЕМ
        const int BITS_BY_PIXEL = 3;
        const int BITS_TO_SIZE = 32;
        const int BITS_TO_FLAG = 16;
        const int BITS_TO_SYMBOL = 16;
        const String FLAG = "1111111111111111";

        private Color EncodePixel(String msg, Color col)
        {

            int red = (col.R >> 1 << 1) + (msg[0] == '0' ? 0 : 1);
            int green = (col.G >> 1 << 1) + (msg[1] == '0' ? 0 : 1);
            int blue = (col.B >> 1 << 1) + (msg[2] == '0' ? 0 : 1);
            Color a = Color.FromArgb(red, green, blue);
            return a;
        }
        private int DecodePixel(Color col)
        {
            int ans = 0;
            ans += ((col.R) % 2);
            ans *= 2;
            ans += ((col.G) % 2);
            ans *= 2;
            ans += ((col.B) % 2);
            return ans;
        }
        public Bitmap LsbEncodeImage(Bitmap sourceImage, String msg)
        {
            Bitmap resultImage = new Bitmap(sourceImage);

            if (resultImage.Width * resultImage.Height < (BITS_TO_FLAG + BITS_TO_SIZE + msg.Length * BITS_TO_SYMBOL / BITS_BY_PIXEL + 1))
                throw new Exception("Bits Overflow");

            //исходное сообщение переделываем в последовательность бит
            String bitmsg = FLAG + BitMachine.IntToBit(msg.Length, BITS_TO_SIZE) + BitMachine.MsgToBit(msg);
            while (bitmsg.Length % 3 != 0)
                bitmsg += "1";

            int i = 0;
            while (i * BITS_BY_PIXEL < BITS_TO_FLAG + BITS_TO_SIZE)
            {
                Color ext = sourceImage.GetPixel(i % sourceImage.Width, i / sourceImage.Height);
                Color ins = EncodePixel(bitmsg.Substring(i * BITS_BY_PIXEL, BITS_BY_PIXEL), ext);
                resultImage.SetPixel(i % sourceImage.Width, i / sourceImage.Height, ins);
                i++;
            } 
            while (i * BITS_BY_PIXEL < bitmsg.Length)
            {
                Color ext = sourceImage.GetPixel(i % sourceImage.Width, i / sourceImage.Height);
                Color ins = EncodePixel(bitmsg.Substring(i * BITS_BY_PIXEL, BITS_BY_PIXEL), ext);
                resultImage.SetPixel(i % sourceImage.Width, i / sourceImage.Height, ins);
                i++;
            }

            return resultImage;
        }
        public String LsbDecodeImage(Bitmap img)
        {
            Bitmap boat = new Bitmap(img);

            int resultLen = 0;
            int i = 0;
            String bitdata = null;
            while (i * BITS_BY_PIXEL < BITS_TO_FLAG)
            {
                Color t = boat.GetPixel(i % boat.Width, i / boat.Height);
                bitdata += (BitMachine.IntToBit(DecodePixel(t), BITS_BY_PIXEL));
                i++;
            }

            if (bitdata.Substring(0, BITS_TO_FLAG) != FLAG)
                return "Сообщения нет.";

            while (i * BITS_BY_PIXEL < BITS_TO_SIZE + BITS_TO_FLAG)
            {
                Color t = boat.GetPixel(i % boat.Width, i / boat.Height);
                bitdata += (BitMachine.IntToBit(DecodePixel(t), BITS_BY_PIXEL));
                i++;
            }

            resultLen = BitMachine.BitToInt(bitdata.Substring(16, BITS_TO_SIZE));

            int off = (BITS_TO_SIZE + BITS_TO_FLAG);
            while ((i - off) * BITS_BY_PIXEL <= resultLen * BITS_TO_SYMBOL) 
            {
                Color t = boat.GetPixel((i) % boat.Width, (i) / boat.Height);
                bitdata += (BitMachine.IntToBit(DecodePixel(t), BITS_BY_PIXEL));
                i++;
            }
            bitdata = bitdata.Substring(BITS_TO_SIZE + BITS_TO_FLAG, resultLen * BITS_TO_SYMBOL);

            if (bitdata != null) bitdata = BitMachine.BitToMsg(bitdata);
            else bitdata = "Сообщения нет.";

            return bitdata;
        }
    }
}
