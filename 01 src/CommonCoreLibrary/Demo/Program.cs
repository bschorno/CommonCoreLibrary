using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using CommonCoreLibrary.Algorithm;
using CommonCoreLibrary.Serialisation;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            if (input == "1")
            {
                SrlCompound var1 = new SrlCompound();

                var1.Add(new SrlByte(123));
                var1.Add(new SrlShort(3456));
                var1.Add(new SrlInt(242393487));
                var1.Add(new SrlLong(65421541514545));
                var1.Add(new SrlFloat(1154.101f));
                var1.Add(new SrlDouble(123.55555d));
                var1.Add(new SrlString("Hallo liebe Welt"));
                var1.Add(new SrlBoolean(true));
                var1.Add(new SrlChar('&'));

                SrlSerializer var2 = new SrlSerializer("test.igw");
                var2.Serializer(var1);
            }
            else
            {
                SrlSerializer var2 = new SrlSerializer("test.igw");
                SrlCompound var1 = (SrlCompound)var2.Deserialize();

                Console.WriteLine(var1.GetByte().Value);
                Console.WriteLine(var1.GetShort().Value);
                Console.WriteLine(var1.GetInt().Value);
                Console.WriteLine(var1.GetLong().Value);
                Console.WriteLine(var1.GetFloat().Value);
                Console.WriteLine(var1.GetDouble().Value);
                Console.WriteLine(var1.GetString().Value);
                Console.WriteLine(var1.GetBoolean().Value);
                Console.WriteLine(var1.GetChar().Value);
            }
            //Type var2 = var1.GetType();
            //foreach (FieldInfo var3 in var2.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            //{
            //    Console.WriteLine("{0}", var3.FieldType.FullName);
            //}


            Console.ReadKey();
        }
    }


    public class Test
    {
        private byte _byte;
        private short _short;
        private int _int;
        private long _long;
        private ushort _ushort;
        private uint _uint;
        private ulong _ulong;
        private double _double;
        private float _float;
        private string _string;
        private char _char;
        private bool _bool;
        private short[] _array;
        private List<short> _list;

        public Test()
        {
            
        }
    }
}
