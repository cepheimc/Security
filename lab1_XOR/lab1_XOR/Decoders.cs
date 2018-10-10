using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_XOR
{
    class Decoders
    {
        public void XORDecoder(string input)
        {
            int key = 19;
            char[] output = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (char)(input[i] ^ key);
                Console.Write(output[i]);
            }
        }

        public string ToHexString(string text)
        {
            string hex = "";
            byte[] inputBytes = Encoding.Default.GetBytes(text);
            foreach (byte b in inputBytes)
            {
                hex += String.Format("{0:x2}", b);
            }
            Console.WriteLine($"hex: {hex}");
            return hex;
        }

        public byte[] FromHexString(string text, int start)
        {
            byte[] bytes = new byte[text.Length / 2 - start];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(text.Substring((i + start) * 2, 2), 16);
            }

            return bytes;
        }

        public int HammingDistance(byte[] b1, byte[] b2)
        {
            int distance = 0;

            for (int i = 0; i < b2.Length; i++)
            {
                if (b1[i] != b2[i])
                {
                    distance++;
                }
            }

            return distance;
        }

        public byte[] Devision(byte[] b, int start)
        {
            byte[] answer = new byte[b.Length / 3];
            int j = start;
            for (int i = 0; i < answer.Length; i++)
            {
                answer[i] = b[j];
                j += 3;
            }

            return answer;
        }

        public Dictionary<byte, int> LetterFrequancy(byte[] b)
        {
            Dictionary<byte, int> freq = new Dictionary<byte, int>();
            freq = b.GroupBy(x => x).OrderByDescending(x => x.Count()).ToDictionary(x => x.Key, x => x.Count());
            foreach (var c in freq)
            {
                // Console.WriteLine($" {c.Key}      {c.Value}");
            }

            return freq;
        }

        public void Decoder2(byte[] b)
        {
            byte[] f = new byte[9];
            for (int i = 0; i < 9; i++)
            {
                f[i] = b[i];
                //Console.WriteLine($"{f[i]}");
            }

            int f1 = f[0];
            int f2 = f[1];
            int f3 = f[2];
            int f4 = f[3];
            int f5 = f[4];
            int f6 = f[5];
            int f7 = f[6];
            int f8 = f[7];
            int f9 = f[8];
            int k1 = f1 ^ 32;
            int k2 = f2 ^ (int)'e';
            int k3 = f3 ^ (int)'o';
            int k4 = f4 ^ (int)'i';
            int k5 = f5 ^ (int)'i';
            int k6 = f6 ^ (int)'s';
            int k7 = f7 ^ (int)'5';
            int k8 = f8 ^ (int)'1';
            int k9 = f9 ^ (int)'u';
            Console.WriteLine($"b1: {b[0]} k1: {k1} char: {(char)k1}");
            Console.WriteLine($"b1: {b[1]} k2: {k2} char: {(char)k2}");
            Console.WriteLine($"b1: {b[2]} k3: {k3} char: {(char)k3}");
            Console.WriteLine($"b1: {b[3]} k4: {k4} char: {(char)k4}");
            Console.WriteLine($"b1: {b[4]} k5: {k5} char: {(char)k5}");
            Console.WriteLine($"b1: {b[5]} k6: {k6} char: {(char)k6}");
            Console.WriteLine($"b1: {b[6]} k7: {k7} char: {(char)k7}");
            Console.WriteLine($"b1: {b[7]} k8: {k8} char: {(char)k8}");
            Console.WriteLine($"b1: {b[8]} k9: {k9} char: {(char)k9}");
        }

        public void Vigener(byte[] b, string key)
        {
            int[] answer = new int[b.Length];
            char[] c = key.ToCharArray();
            int j = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (j == 3)
                {
                    j = 0;
                }
                answer[i] = (int)b[i] ^ (int)c[j];
                Console.Write($" {(char)answer[i]}");

                j++;
            }
        }
    }
}
