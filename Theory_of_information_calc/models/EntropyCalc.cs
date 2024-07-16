using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theory_of_information_calc
{
    public static class EntropyCalc
    {
        private static int CountLetters(string str)
        {
            int countLetters = 0;

            foreach (char c in str)
                if (char.IsLetter(c) && !char.IsWhiteSpace(c)) countLetters++;


            return countLetters;
        }

        private static void CalculationProbabilityLetters(Dictionary<char, double> probabilityLetters, string str, int countLetters)
        {
            int countLetter = 0;

            foreach (char c in str)
            {
                if (!char.IsLetter(c) || probabilityLetters.ContainsKey(c)) continue;

                foreach (char c_s in str)
                    if (c == c_s) countLetter++;

                probabilityLetters.Add(c, countLetter / (double)countLetters);
                countLetter = 0;
            }
        }

        public static double CalculationEntropy(string str)
        {
            Dictionary<char, double> probabilityLetters = new Dictionary<char, double>();
            double entropy = 0;

            int countLetters = CountLetters(str);
            CalculationProbabilityLetters(probabilityLetters, str, countLetters);

            foreach (double p in probabilityLetters.Values)
                entropy += p * Math.Log(1 / p, 2);

            return entropy;
        }

        public static double CalculateMaxEntropy(int powerOfAlphabet)
        {
            return Math.Log(powerOfAlphabet, 2);
        }

        public static double CalculateAbsoluteRedundancy(double maxEntropy, double entropy)
        {
            return maxEntropy - entropy;
        }

        public static double CalculateRelativeRedundancy(double maxEntropy, double absRedundancy)
        {
            return (absRedundancy / maxEntropy) * 100;
        }
    }
}
