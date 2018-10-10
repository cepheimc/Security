using System;
using System.Collections.Generic;


namespace GeneticAlgorithm
{
    class Decrypt
    {
        public static string DecryptText(string t, string key)
        {
            string normalizedText = t.ToUpper();
            // string key = "phqgiumeaylnofdxjkrcvstzwb";
            // char[] k = key.ToUpper().ToCharArray();
            string decryptedText = "";
            Dictionary<char, int> indexInKey = new Dictionary<char, int>();
            for (int i = 0; i < key.Length; i++)
            {
                indexInKey[key[i]] = i;
            }

            foreach (char c in normalizedText)
            {

                decryptedText += (char)('A' + indexInKey[c]);

            }

            return decryptedText;
        }
    }
}
