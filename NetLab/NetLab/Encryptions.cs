using System;
using System.Collections.Generic;
using System.Text;


/**
* @author HariLab
* @date - 7/17/2021 3:26:07 PM 
*/

namespace NetLab
{
    public static class Encryptions
    {
        const int MATRIX_VOLUME = 16;

        /// <summary>
        /// Merupakan fungsi untuk pesan enkripsi.
        /// </summary>
        /// <param name="text">parameter pesan yang akan dienkripsi</param>
        public static string MessageEncryption(this string text)
        {
            if (text.Length > MATRIX_VOLUME)
                return "Unable process with length more than max volume";

            // make sure data yang kita dapat bersih dari karakter whitespace.
            text = text.Trim();

            // menyimpan data sementara per character
            var tempChar = text.ToCharArray();

            // define kebutuhan matrix that stored encryption data.
            int maxColumn = 4;
            int maxRow = 4;

            // dibutuhkan ketika ingin array menjadi fleksible
            int matrixSpace = maxRow * maxColumn - text.Length;

            // define postion untuk melakukan tracking pada saat looping.
            int position = 0;

            #region Before the message is encrypted

            Console.WriteLine("Before:");
            // init matrix untuk menyimpan pesan yang akan di enkripsi
            char[,] arr = new char[maxColumn, maxRow];

            // main logic (before the message is encrypted)
            for (int index = 0; index < maxColumn; index++)
            {
                for (int inner = 0; inner < maxRow; inner++)
                {
                    if (position == tempChar.Length)
                    {
                        arr[index, inner] = '*';
                    }
                    else
                    {
                        arr[index, inner] = tempChar[position];
                        position++;
                    }
                }
            }

            // Display current message
            for (int index = 0; index < maxColumn; index++)
            {
                for (int inner = 0; inner < maxRow; inner++)
                {
                    Console.Write($"{arr[index, inner]} ");
                }

                Console.WriteLine();
            }

            #endregion

            Console.WriteLine("\n");



            #region After the message is encrypted

            Console.WriteLine("After:");
            // init matrix untuk menyimpan pesan yang telah di enkripsi
            char[,] encryptedMsg = new char[maxColumn, maxRow];

            // reset current position
            position = maxRow - 1;

            // main logic (after the message is encrypted)
            for (int index = 0; index < maxColumn; index++)
            {
                for (int inner = 0; inner < maxRow; inner++)
                {
                    encryptedMsg[index, inner] = arr[position, index];
                    position--;
                }

                position = 3;
            }

            // Display encryted message
            for (int index = 0; index < maxColumn; index++)
            {
                for (int inner = 0; inner < maxRow; inner++)
                {
                    Console.Write($"{encryptedMsg[index, inner]} ");
                }

                Console.WriteLine();
            }

            Console.Write("\nThe result is: ");
            string result = string.Empty;

            // Display a result
            for (int index = 0; index < maxColumn; index++)
            {
                for (int inner = 0; inner < maxRow; inner++)
                {
                    if (encryptedMsg[index, inner] != '*')
                    {
                        result += encryptedMsg[index, inner];
                    }
                }
            }

            #endregion

            return result;
        }
    }
}
