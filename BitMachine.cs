using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3
{
    static public class BitMachine
    {
        const int BITS_TO_SYMBOL = 16;

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        static public String IntToBit(int val, int bits)
        {
            String ans = null;
            for (int i = 0; i < bits; i++)
            {
                ans += (val % 2 == 0 ? "0" : "1");
                val = val / 2;
            }
            ans = Reverse(ans);
            return ans;
        }
        static public int BitToInt(String bit)
        {
            int t = 0;
            int plow = 1;
            for (int i = bit.Length - 1; i > 0; i--)
            {
                t += (int)(bit[i] == '0' ? 0 : 1) * plow;
                plow = plow * 2;
            }
            return (int)t;
        }
        static public String MsgToBit(String msg)
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
                ans += BitMachine.Reverse(asd);
            }

            return ans;
        }
        static public String BitToMsg(String bit)
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
    }

}
