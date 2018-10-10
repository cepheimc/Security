using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GeneticAlgorithm
{
    class DNA
    {
        static Dictionary<string, double> monogramD = ReadWriteFrequency("E:\\Security\\GeneticAlgorithm\\GeneticAlgorithm\\monograms.txt");
        static Dictionary<string, double> bigramD = ReadWriteFrequency("E:\\Security\\GeneticAlgorithm\\GeneticAlgorithm\\bigrams.txt");
        static Dictionary<string, double> trigramD = ReadWriteFrequency("E:\\Security\\GeneticAlgorithm\\GeneticAlgorithm\\trigrams.txt");

        static List<string> wordsD = ReadWriteTopWord("E:\\Security\\GeneticAlgorithm\\GeneticAlgorithm\\words.txt");

        private string genes;
        private double fitness;

        public double Fit
        {
            get { return fitness; }
        }

        public DNA()
        {

        }

        /*   public void DeleteRepeat(char[] t)
           {
               string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
               char[] a= new char[t.Length];
               List<char> notInAlph = new List<char>();

               List<char> alph = new List<char>(alphabet.ToCharArray());
               List<char> t1 = new List<char>(t);
               a = t;
               Console.WriteLine($"alphabet: ");
               foreach (var c in alph)
               {
                   Console.Write($" {c} ");
               }
               Console.WriteLine($"\n t: ");
               foreach (var c in t)
               {
                   Console.Write($" {c} ");
               }

               for (int i = 0; i < alphabet.Length; i++)
               {
                   if (!a.Contains(alph[i]))
                   {
                       notInAlph.Add(alph[i]);
                   }

               }
               Console.WriteLine($"\nnot in t: ");
               foreach (var c in notInAlph)
               {
                   Console.Write($" {c} ");
               }

               for (int i = 0; i < t.Length; i++)
               {
                   if (t1.Count(x => x == t[i]) > 1)
                   {
                       t1[i] = notInAlph[0];
                       notInAlph.RemoveAt(0);
                   }
               }

               Console.WriteLine($"\nnew t: ");
               foreach (var c in t1)
               {
                   Console.Write($" {c} ");
               }

           }*/

        public DNA(string genes, string text)
        {
            this.genes = genes;
            // this.text = text;
            this.fitness = CalculateFitness(text, genes);
        }

        public string Genes()
        {
            return genes;
        }

        public double CalculateFitness(string text, string genes)
        {
            string decr = Decrypt.DecryptText(text, genes);

            Dictionary<string, double> monogramM = LetterFrequancy(decr, 1);
            Dictionary<string, double> bigramM = LetterFrequancy(decr, 2);
            Dictionary<string, double> trigramM = LetterFrequancy(decr, 3);


            double diff1, diff2, diff3, f;
            int w = WordFrequancy(decr);
            diff1 = CompareDictionaries(monogramD, monogramM);
            diff2 = CompareDictionaries(bigramD, bigramM);
            diff3 = CompareDictionaries(trigramD, trigramM);

            f = -(diff1 + diff2 * 0.6 + diff3 * 0.5) + (double)w / 20; //+ count of words / 20

            return f;
        }

        public Dictionary<string, double> LetterFrequancy(string t, int n)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            Dictionary<string, double> freq1 = new Dictionary<string, double>();
            string[] f = new string[t.Length - n + 1];

            for (int i = 0; i < f.Length; i++)
            {
                f[i] = t.Substring(i, n);
            }

            freq = f.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            int sum = freq.Values.Sum();

            foreach (var c in freq)
            {
                freq1.Add(c.Key, (double)c.Value / (double)sum);
            }

            // foreach (var c in freq1)
            // {
            //eq1.Add(c.Key, c.Value / sum);
            // Console.WriteLine($" {c.Key}      {c.Value}");
            //}

            return freq1;
        }

        static Dictionary<string, double> ReadWriteFrequency(string file)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            Dictionary<string, double> freq1 = new Dictionary<string, double>();

            var reader = new StreamReader(File.OpenRead(file));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    var values = line.Split(' ');
                    string key = values[0].ToUpper();
                    int value = Convert.ToInt32(values[1]);

                    dictionary.Add(key, value);
                }
            }

            long sum = 0;//dictionary.Values.Sum();

            foreach (int v in dictionary.Values)
            {
                sum += v;
            }

            foreach (var d in dictionary)
            {
                freq1.Add(d.Key, (double)d.Value / (double)sum);
                // Console.WriteLine($"key: {d.Key}, value: {(float)d.Value / (float)sum}");
            }

            return freq1;
        }

        static List<string> ReadWriteTopWord(string file)
        {
            List<string> list = new List<string>();

            var reader = new StreamReader(File.OpenRead(file));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line != null)
                    list.Add(line.ToUpper());
            }

            foreach (var d in list)
            {
                //  Console.WriteLine($"word: {d}");
            }

            return list;
        }

        public int WordFrequancy(string t)
        {
            int count = 0;

            //List<string> text = new List<string>();
            // t.CopyTo(0,text,t.Length,);
            int l = wordsD.Count / 2;

            for (int i = 0; i < l; i++)
            {
                int j = 0;
                //if (wordsD[i].Length > 2)
                // {
                while ((j = t.IndexOf(wordsD[i], j)) != -1)
                {
                    count++;
                    break;
                }

                // Console.WriteLine($" {wordsD[i]}      {r}");
                // r = 0;
                // }
            }

            // foreach (var c in freq1)
            // {
            //eq1.Add(c.Key, c.Value / sum);
            // Console.WriteLine($" {c.Key}      {c.Value}");
            //}

            return count;
        }

        public double CompareDictionaries(Dictionary<string, double> d1, Dictionary<string, double> d2)
        {
            double diff = 0;

            foreach (var d in d1)
            {
                double v;
                v = d2.TryGetValue(d.Key, out v) ? v : 0.0;
                //v = d2.TryGetValue(d.Key, out v)) ? v:0.0;
                //{
                diff += Math.Abs(v - d.Value);
                // Console.WriteLine($"{diff}");
                //}
            }

            return diff;
        }
        /*
        public string DecryptText()
        {
            string normalizedText = text.ToUpper();
           // string key = "phqgiumeaylnofdxjkrcvstzwb";
           // char[] k = key.ToUpper().ToCharArray();
            string decryptedText = "";
            Dictionary<char, int> indexInKey = new Dictionary<char, int>();
            for (int i = 0; i < genes.Length; i++)
            {
                indexInKey[genes[i]] = i;
            }
            foreach (char c in normalizedText)
            {
                // if (Char.IsLetter(c))
               // {
                    decryptedText += (char)('A' + indexInKey[c]);
                //}
                //else
               // {
                 //   decryptedText += c;
               // }
            }
            return decryptedText;
        }*/
    }
}
