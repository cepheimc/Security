using System;
using System.Collections.Generic;
using System.Linq;


namespace GeneticAlgorithm
{
    class Generation
    {
        private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private int numberOfGeneration;
        private int populationSize;
        private Random r;
        private List<DNA> population;
        private double mutation;
        private string encrText;

        public List<DNA> Population
        {
            get { return this.population; }
        }

        public Generation(int populationSize, double mutation, string text)
        {
            this.populationSize = populationSize;
            this.mutation = mutation;
            this.population = new List<DNA>();
            r = new Random();
            encrText = text.ToUpper();

            CreatePopulation();
            population.Sort((x, y) => y.Fit.CompareTo(x.Fit));
        }

        /* private int CompareDNA(DNA a, DNA b)
         {
             return a.Fitness().CompareTo(b.Fitness());
         }*/

        private int RouletteSelect(List<DNA> f)
        {
            double fitnessSum = 0;
            for (int i = 0; i < f.Count; i++)
            {
                fitnessSum += f[i].Fit;
            }

            
            double current = 0.0;
            for (int i = 0; i < f.Count; i++)
            {
                current += f[i].Fit;
                if (current > fitnessSum)
                    return i;
            }

            return f.Count - 1;
        }

        public void CreatePopulation()
        {
            int size = populationSize;
            int swapPosition;

            char[] genes = new char[alphabet.Length];

            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < alphabet.Length; i++)
                {
                    char c = alphabet[i];
                    genes[i] = c;
                }

                for (int i = 0; i < alphabet.Length; i++)
                {
                    swapPosition = r.Next(0, alphabet.Length);

                    char oldValue = genes[i];
                    genes[i] = genes[swapPosition];
                    genes[swapPosition] = oldValue;
                }

                population.Add(new DNA(new String(genes), encrText));
                // Console.WriteLine($"p: {population[j].Genes()} f: {population[j].Fit}");
            }

        }

        public void CreateNewPopulation()
        {
            List<DNA> newPopulation = new List<DNA>();

            //population.Sort((x, y) => y.Fit.CompareTo(x.Fit));
            //foreach (var c in population)
            //{
            //    Console.WriteLine($"new p: {c.Genes()} f: {c.Fit}");
            //}
            for (int i = 0; i < populationSize / 2; i++)
            {
                int n1 = RouletteSelect(population);
                int n2 = RouletteSelect(population);
                //  Console.WriteLine($"n1: {n1} n2: {n2}");
                DNA winner1 = population[n1];
                DNA winner2 = population[n2];

                DNA[] children = Crossover(winner1.Genes(), winner2.Genes());
                newPopulation.Add(children[0]);


                newPopulation.Add(children[1]);


                //newPopulation.Sort((x, y) => y.Fit.CompareTo(x.Fit));

            }
            population = newPopulation;
            population.Sort((x, y) => y.Fit.CompareTo(x.Fit));

            //{
            //    Console.WriteLine($"new p: {c.Genes()} f: {c.Fit}");
            //}



        }

        private char[] DeleteRepeat(char[] t)
        {
            //string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] a = new char[t.Length];
            List<char> notInAlph = new List<char>();

            List<char> alph = new List<char>(alphabet.ToCharArray());
            List<char> t1 = new List<char>(t);
            a = t;
            /* Console.WriteLine($"\nalphabet: ");
             foreach (var c in alph)
             {
                 Console.Write($" {c} ");
             }
             Console.WriteLine($"\n t: ");
             foreach (var c in t)
             {
                 Console.Write($" {c} ");
             }*/
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (!a.Contains(alph[i]))
                {
                    notInAlph.Add(alph[i]);
                }

            }

            /* Console.WriteLine($"\nnot in t: ");
             foreach (var c in notInAlph)
             {
                 Console.Write($" {c} ");
             }*/

            for (int i = 0; i < t.Length; i++)
            {
                if (t1.Count(x => x == t[i]) > 1)
                {
                    t1[i] = notInAlph[0];
                    notInAlph.RemoveAt(0);
                }
            }

            // t = t1.ToArray();

            /* Console.WriteLine($"\nnew t: ");
             foreach (var c in t1)
             {
                 Console.Write($" {c} ");
             }*/

            return t1.ToArray();
        }

        private DNA[] Crossover(string dna1, string dna2)
        {
            DNA[] children = new DNA[2];
            int len = dna1.Length;

            char[] child1 = new char[len];
            char[] child2 = new char[len];

            var transposingChar1 = dna1.ToCharArray();
            var transposingChar2 = dna2.ToCharArray();

            int randomPosition = r.Next(0, len);

            for (int i = 0; i < randomPosition; i++)
            {
                child1[i] = transposingChar1[i];
                child2[i] = transposingChar2[i];
            }

            for (int i = randomPosition; i < len; i++)
            {
                child1[i] = transposingChar2[i];
                child2[i] = transposingChar1[i];
            }

            child1 = DeleteRepeat(child1);
            child2 = DeleteRepeat(child2);

            int oldValue, newValue;
            char oldChar;

            /*  Console.WriteLine($"\before mutation child1");
              for (int i = 0; i < child1.Length; i++)
              {
                  Console.Write($" {child1[i]} ");
              }

              Console.WriteLine($"before mutation child2");
              for (int i = 0; i < child2.Length; i++)
              {
                  Console.Write($" {child2[i]} ");
              }*/

            for (int i = 0; i < len; i++)
            {
                int m = r.Next(0, 100);
                if (m < mutation)
                {
                    oldValue = r.Next(0, len);

                    do
                    {
                        newValue = r.Next(0, len);

                    } while (oldValue == newValue);

                    // Console.WriteLine($"old 1 : {oldValue} new 1 : {newValue}");
                    oldChar = child1[oldValue];
                    // Console.WriteLine($"oldchar 1 : {oldChar} ");
                    child1[oldValue] = child1[newValue];
                    //  Console.WriteLine($"changeold 1 : {child1[oldValue]} ");
                    child1[newValue] = oldChar;
                    //  Console.WriteLine($"changenew 1 : {child1[newValue]} ");
                }
            }

            for (int i = 0; i < len; i++)
            {
                int m = r.Next(0, 100);
                if (m < mutation)
                {
                    oldValue = r.Next(0, len);
                    // int newValue;
                    do
                    {
                        newValue = r.Next(0, len);

                    } while (oldValue == newValue);

                    // Console.WriteLine($"old 2 : {oldValue} new 2 : {newValue}");
                    oldChar = child2[oldValue];
                    // Console.WriteLine($"oldchar 2 : {oldChar} ");
                    child2[oldValue] = child2[newValue];
                    // Console.WriteLine($"changeold 2 : {child2[oldValue]} ");
                    child2[newValue] = oldChar;
                    //Console.WriteLine($"changenew 2 : {child2[newValue]} ");
                }
            }
            /* Console.WriteLine($"\nafter mutation child1");
             for (int i = 0; i < child1.Length; i++)
             {
                 Console.Write($" {child1[i]} ");
             }

             Console.WriteLine($"after mutation child2");
             for (int i = 0; i < child2.Length; i++)
             {
                 Console.Write($" {child2[i]} ");
             }*/

            children[0] = new DNA(new String(child1), encrText);
            children[1] = new DNA(new String(child2), encrText);
            return children;
        }
    }
}
