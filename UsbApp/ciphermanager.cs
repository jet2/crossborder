using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kppApp
{
    public class CipherManager
    {
        private string xfile = "";

        public CipherManager(string pluginfile)
        {
            xfile = pluginfile;
        }

        public void UpdatePStorage(string pword)
        {
            File.WriteAllBytes(xfile, seedPassword(pword));
        }

        private static byte[] seedPassword(string pass)
        {
            Random r = new Random();
            byte[] buffer = new byte[98765];
            byte[] asciiBytes = Encoding.ASCII.GetBytes(pass);
            int glob_pointer = 110;
            byte rnd_offset;
            r.NextBytes(buffer);

            for (int i = 0; i < asciiBytes.Length; i++)
            {
                glob_pointer += 3; // сдвигаем указатель сеяния на позицию хранения сдвига до сл элемента
                rnd_offset = (byte)(r.Next(10, 250)); // вычисляем сдвиг
                buffer[glob_pointer] = rnd_offset; // записываем сдвиг в позицию указателя сеяния
                glob_pointer += rnd_offset; // сдвигаем указателя сеяния в позицию хранения элемента пароля 
                buffer[glob_pointer] = asciiBytes[i]; // записываем код символа элемента в позицию указателя сеяния
            //    Console.Write(asciiBytes[i]);
            //    Console.Write(" ");
            }
           // Console.WriteLine("1<<<<<<<<<<<<<<");
            // пароль посеян, сеем завершение.
            glob_pointer += 3; // сдвигаем указатель сеяния на позицию хранения сдвига до сл элемента
            rnd_offset = (byte)(r.Next(10, 250)); // вычисляем сдвиг
            buffer[glob_pointer] = rnd_offset; // записываем сдвиг в позицию указателя сеяния
            glob_pointer += rnd_offset; // сдвигаем указателя сеяния в позицию хранения элемента пароля 
            buffer[glob_pointer] = 255; // записываем код завершения пароля в позицию указателя сеяния
            return buffer;
        }

        private static string unseedPassword(byte[] asciiBytes)
        {
            Random r = new Random();
            string unhidden = "";
            byte aschar;
            int glob_pointer = 110;

            while (glob_pointer < asciiBytes.Length)
            {
                glob_pointer += 3; // сдвигаем указатель сеяния на позицию хранения сдвига до сл элемента
                var el_pos = asciiBytes[glob_pointer];
                glob_pointer += el_pos;
                //Console.Write(asciiBytes[glob_pointer]);
                //Console.Write(" ");
                aschar = asciiBytes[glob_pointer]; // записываем сдвиг в позицию указателя сеяния
                if (aschar == 255)
                {
                    break;
                }
                unhidden += (char)aschar;
            };
            return unhidden;
        }

        private string getStoredPword(string pfile)
        {
            string result = "";
            try
            {
                var hiddenpass2 = File.ReadAllBytes(pfile);

                result += unseedPassword(hiddenpass2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось прочитать {this.xfile} [{ex.Message}]");
            }
            //Console.WriteLine(unseedPassword(hiddenpass2));
            return result;
        }


        public string getFullPword(string RightPart)
        {
            string result = getStoredPword(this.xfile) + RightPart;

            return result;
        }
    }
}
