using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            byte[] f = new byte[5];
            for (int i = 0; i < 5; i++)
            {
                f[i] = b[i];
                //Console.WriteLine($"{f[i]}");
            }

            int[] key = new[]
            {
                (int) 'e', (int) 't', (int) 'a', (int) 'o',
                (int) 'i', (int) 'n', (int) 's', (int) 'h', (int) 'r'
            };

            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < f.Length; j++)
                {
                    Console.Write($" letter:{f[j]}\t char: {(char)(f[j]^key[i])}\t");
                }
                Console.WriteLine();
            }
         
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
