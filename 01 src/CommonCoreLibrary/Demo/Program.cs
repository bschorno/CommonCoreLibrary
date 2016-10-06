using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCoreLibrary.Algorithm;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Insert string 1: ");
            string a = Console.ReadLine();
            Console.Write("Insert string 2: ");
            string b = Console.ReadLine();

            int l = Levenshtein.Compare(a, b);
            Console.WriteLine("Levenshtein-Distance: {0}", l);

            Console.ReadKey();
        }
    }
}
