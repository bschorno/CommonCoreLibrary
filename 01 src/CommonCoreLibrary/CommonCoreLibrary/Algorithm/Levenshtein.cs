using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Algorithm
{
    public static class Levenshtein
    {
        /// <summary>
        /// Compare two strings with the levenshtein distance
        /// </summary>
        /// <param name="a">String A</param>
        /// <param name="b">String B</param>
        /// <returns>Levenshtein distance</returns>
        public static int Compare(string a, string b)
        {
            if(string.IsNullOrEmpty(a))
            {
                if (string.IsNullOrEmpty(b))
                    return 0;
                return b.Length;
            }
            if (string.IsNullOrEmpty(b))
                return a.Length;
            if (a == b)
                return 0;

            if (a.Length > b.Length)
            {
                string var1 = b;
                b = a;
                a = var1;
            }

            int var2 = b.Length;
            int var3 = a.Length;

            int[,] var4 = new int[2, var2 + 1];

            for (int var5 = 1; var5 <= var2; var5++)
                var4[0, var5] = var5;

            int var6 = 0;
            for (int var5 = 1; var5 <= var3; var5++)
            {
                var6 = var5 & 1;
                var4[var6, 0] = var5;
                int var7 = var6 ^ 1;

                for (int var8 = 1; var8 <= var2; var8++)
                {
                    int var9 = (b[var8 - 1] == a[var5 - 1] ? 0 : 1);
                    var4[var6, var8] = Math.Min(Math.Min(var4[var7, var8],
                                                         var4[var6, var8 - 1] + 1),
                                                var4[var7, var8 - 1] + var9);
                }
            }
            return var4[var6, var2];
        }
    }
}
