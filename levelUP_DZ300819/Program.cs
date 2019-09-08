using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace levelUP_DZ300819
{
    class Program
    {
        static void Main(string[] args)
        {
            



            Console.ReadKey();
            
        }


        static void DZ1() // подсчет 1 и 0 в байте 
        {
            Console.Write("Введите число: ");
            string myString = Console.ReadLine();

            byte variableChech = 0;

            bool checkParse = byte.TryParse(myString, out variableChech);

            while (checkParse == false)
            {
                Console.WriteLine("Введите число в диапазоне 0 - 255");
                myString = Console.ReadLine();

                if (checkParse = byte.TryParse(myString, out variableChech))
                {
                    break;
                }
                else
                {
                    continue;
                }

            }

            int count0 = 0;
            int count1 = 0;

            for (int i = 0; i < 8; i++)
            {
                if ((variableChech & 0x01) == 1)
                {
                    count1++;
                }
                else
                {
                    count0++;
                }
                variableChech >>= 1;
            }

            Console.WriteLine($"Количество нулей в байте: {count0}\nколичество едениц: {count1}");
        }

        static void DZ2() // умножение двух переменных использую битовый сдвиг
        {
            Console.Write("Введите первое число:");

            string myyString = Console.ReadLine();
            int firstNumber = 0;

            bool chek = int.TryParse(myyString, out firstNumber);

            while (chek == false)
            {
                Console.WriteLine("Введите число!");
                myyString = Console.ReadLine();
                if (chek = int.TryParse(myyString, out firstNumber))
                {
                    break;
                }

            }

            Console.WriteLine($"Ваше первое число: {firstNumber}");

            Console.Write("Введите второе число:");

            myyString = Console.ReadLine();

            int secondNumber = 0;
            chek = int.TryParse(myyString, out secondNumber);

            while (chek == false)
            {
                Console.WriteLine("Введите число!");
                myyString = Console.ReadLine();
                if (chek = int.TryParse(myyString, out secondNumber))
                {
                    break;
                }

            }
            Console.WriteLine($"Ваше второе число: {secondNumber}");

            int resMultiplication = firstNumber * secondNumber;

            int resBitShift = 0;
            while (secondNumber != 0)
            {
                if ((secondNumber & 0x1) == 1)
                    resBitShift += firstNumber;

                secondNumber >>= 1;
                firstNumber <<= 1;
            }

            Console.WriteLine($"{resBitShift}\n{resMultiplication}");
        }
    }
}
