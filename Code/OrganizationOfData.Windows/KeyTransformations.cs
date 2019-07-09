using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationOfData.Data
{
    public static class KeyTransformations
    {
        public static int p = 7;
        public static int v = 10;

        public static int ResidualSplitting(int key, int numberOfBuckets)
        {
            return key % numberOfBuckets;
        }

        public static int CentralKeyDigits(int key, int numberOfBuckets)
        {
            int n = (int)Math.Ceiling(Math.Log10(numberOfBuckets));
            int t = (int)Math.Floor((double)p - n / 2);
            key *= key;

            int r = (int)Math.Floor(key / Math.Pow(v, t)) % (int)Math.Floor(Math.Pow(v, n));
            r = (int)Math.Floor((r * numberOfBuckets) / Math.Pow(v, n));

            return r;
        }

        public static int Overlap(int key, int numberOfBuckets)
        {
            int sum = 0;
            int n = (int)Math.Ceiling(Math.Log10(numberOfBuckets));
            int max = p / n;

            for (int i = 0; i < max; i++)
            {
                if (i % 2 == 0)
                {
                    sum += SplitDigits(n * i + 1, n * (i + 1), key);
                }
                else
                {
                    for (int j = 0; j < n; j++)
                    {
                        sum += SplitDigits(n * i + 1 + j, n * i + 1 + j, key) * (int)Math.Pow(10, n - j - 1);
                    }
                }
            }

            return (int)Math.Floor(sum % (int)Math.Pow(10, n) * numberOfBuckets / Math.Pow(10, n));
        }

        private static int SplitDigits(int start, int end, int number)
        {
            return (int)Math.Floor((number % (int)Math.Pow(v, end)) / Math.Pow(v, start - 1));
        }
    }
}
