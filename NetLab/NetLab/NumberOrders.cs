using System;
using System.Collections.Generic;
using System.Text;


/**
* @author HariLab
* @date - 7/17/2021 3:29:05 PM 
*/

namespace NetLab
{
    public static class NumberOrders
    {
        /// <summary>
        /// Merupakan fungsi dengan input interger positif dan menghasilkan 
        /// susunan angka yang jika dijumlahkan sesuai/sama dengan nilai input.
        /// </summary>
        /// <param name="value">parameter nilai input</param>
        public static void NumberOrder(int value)
        {
            for (int inOuter = 1; inOuter <= value; inOuter++)
            {
                int total = 0;

                for (int inInner = 1; inInner <= inOuter; inInner++)
                {
                    total++;
                    Console.Write("1,");
                }

                if((value - total) != 0)
                {
                    Console.Write(value - total);
                }

                Console.WriteLine();
            }

        }
    }
}
