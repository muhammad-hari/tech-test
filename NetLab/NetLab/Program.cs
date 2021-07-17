using System;
using System.Linq;

namespace NetLab
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("===> Pesan Enkripsi <==");
            Console.WriteLine("akucintakamu".MessageEncryption());
            Console.WriteLine();
            Console.WriteLine("maafAkuenggak".MessageEncryption());

            Console.WriteLine("\n\n ===> Susunan Angka Terjumlah Berdasarkan Input <==");
            NumberOrders.NumberOrder(4);
        }

    }
}
