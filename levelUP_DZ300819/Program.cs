using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


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

        static void DZ3() // расписание + пароль
        {
            Console.WriteLine(@"Расписание
Для корректироовки воспользуйтесь цифровым блоком :)

1- понедельник
2- вторник
3- среда
4- четверг
5- пятница
6- суббота
7- воскресенье
некорректный ввод (символ) или выход за представленный диапазон (1-7) приводит к окончанию редактированию

одиночный выбор дня недели устанавливает напоминание, повторный - снимает");

            bool BB = true;
            byte B = 0x0;

            schedule(BB, B);

            Console.WriteLine("Для дальнейшего редактирования введите Пароль:");

            passwordEncrypt();

            string passwordEncod = passwordEnc();
            string usserEncod = usserEncrypt();

            if (passwordEncod == usserEncod)
            {
                Console.WriteLine("Пароль введен верно продолжайте редактирование");
                schedule(BB, B);
            }
            else
            {
                Console.WriteLine("Паротль не верный!");
            }


        }

        static void schedule(bool BB, byte B) // функция редактирования расписания
        {

            while (BB == true)
            {

                Console.WriteLine("{0,8}", Convert.ToString(B, 2));

                int dayWeek;
                BB = int.TryParse((Console.ReadLine()), out dayWeek);
                switch (dayWeek)
                {
                    case 1: //понедельник
                        Console.Clear();
                        B = (byte)(B ^ 0x80);


                        break;
                    case 2: //вторник
                        Console.Clear();
                        B = (byte)(B ^ 0x40);


                        break;
                    case 3: //среда
                        Console.Clear();
                        B = (byte)(B ^ 0x20);

                        break;
                    case 4: //четверг
                        Console.Clear();
                        B = (byte)(B ^ 0x10);

                        break;
                    case 5: //пятница
                        Console.Clear();
                        B = (byte)(B ^ 0x08);

                        break;
                    case 6: //суббота
                        Console.Clear();
                        B = (byte)(B ^ 0x04);

                        break;
                    case 7: //воскресенье
                        Console.Clear();
                        B = (byte)(B ^ 0x02);



                        break;

                    default:
                        BB = false;
                        Console.Clear();
                        Console.WriteLine("{0,8}", Convert.ToString(B, 2));

                        break;

                }

            }


        }

        static void passwordEncrypt()   // создание файла пароля
        {
            string Pass = "Пароль";

            FileStream file1 = new FileStream("C:\\Users\\Vitaliy\\Desktop\\mask.txt", FileMode.Open); //создаем файловый поток
            StreamReader readerMask = new StreamReader(file1); // создаем «потоковый читатель» и связываем его с файловым потоком
            string mask = readerMask.ReadToEnd(); //считываем все данные с потока
            readerMask.Close(); //закрываем поток
            int maskk = Convert.ToInt32(mask); // 1010 1010 1010 1010

            string destination = "";

            for (int i = 0; i < Pass.Length; i++)
            {
                char ch_ = (char)(Pass[i] ^ maskk);

                destination += ch_;
            }

            FileStream file2 = new FileStream("C:\\Users\\Vitaliy\\Desktop\\passwordEncrypt.txt", FileMode.Create); //создаем файловый поток
            StreamWriter writerPasswordEncrypt = new StreamWriter(file2); //создаем «потоковый писатель» и связываем его с файловым потоком
            writerPasswordEncrypt.Write($"{destination}"); //записываем в файл
            writerPasswordEncrypt.Close();

        }

        static string passwordEnc() // чтение файла
        {
            FileStream file = new FileStream("C:\\Users\\Vitaliy\\Desktop\\passwordEncrypt.txt", FileMode.Open); //создаем файловый поток
            StreamReader readerMask = new StreamReader(file); // создаем «потоковый читатель» и связываем его с файловым потоком
            string Pass = readerMask.ReadToEnd(); //считываем все данные с потока
            readerMask.Close(); //закрываем поток

            return Pass;

        }

        static string usserEncrypt() //шифрование пароля от пользователя
        {
            string usserPass = Console.ReadLine();

            FileStream file3 = new FileStream("C:\\Users\\Vitaliy\\Desktop\\mask.txt", FileMode.Open); //создаем файловый поток
            StreamReader readerMask = new StreamReader(file3); // создаем «потоковый читатель» и связываем его с файловым потоком
            string mask = readerMask.ReadToEnd(); //считываем все данные с потока и выводим на экран
            readerMask.Close(); //закрываем поток
            int maskk = Convert.ToInt32(mask); // 1010 1010 1010 1010

            string destination = "";

            for (int i = 0; i < usserPass.Length; i++)
            {
                char ch_ = (char)(usserPass[i] ^ maskk);

                destination += ch_;

            }

            return destination;

        }
    }
}
