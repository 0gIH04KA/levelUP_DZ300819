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
            Print();

            Console.ReadKey();
        }

        static void DZ1() // подсчет 1 и 0 в байте 
        {
            Console.Write("Введите число: ");
            string myString = Console.ReadLine();

            byte variableByte;

            bool checkParse = byte.TryParse(myString, out variableByte);

            while (checkParse == false)
            {
                Console.WriteLine("Введите число в диапазоне 0 - 255");
                myString = Console.ReadLine();

                if (checkParse = byte.TryParse(myString, out variableByte))
                {
                    break;
                }
                else
                {
                    continue;
                }

            }

            Console.WriteLine($"Ваше число {variableByte}, НЕХ: {Convert.ToString(variableByte, 16)}");

            int count0 = 0;
            int count1 = 0;

            const int amountBitInByte = 7;

            for (int i = 0; i <= amountBitInByte; i++)
            {
                if ((variableByte & 0x01) == 1)
                {
                    count1++;
                }
                else
                {
                    count0++;
                }
                variableByte >>= 1;
            }

            Console.WriteLine($"Количество нулей в байте: {count0}\nКоличество едениц в байте: {count1}");
        }

        static void DZ2() // умножение двух переменных использую битовый сдвиг
        {
            Console.Write("Введите первое число:");

            string myyString = Console.ReadLine();

            int firstNumber;

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

            Console.Write("Введите второе число:");

            myyString = Console.ReadLine();

            int secondNumber;

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

            int resMultiplication = firstNumber * secondNumber;

            int resBitShift = 0;
            while (secondNumber != 0)
            {
                if ((secondNumber & 0x1) == 1)
                    resBitShift += firstNumber;

                secondNumber >>= 1;
                firstNumber <<= 1;
            }

            Console.WriteLine($"Результат умножения сдвигом: {resBitShift}\nРезультат стандартного умножения: {resMultiplication}");
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

одиночный выбор - устанавливает напоминание, повторный - снимает");

            bool check = true;
            byte scheduleInOneByte = 0x0;

            schedule(check, scheduleInOneByte);

            Console.WriteLine("Для дальнейшего редактирования введите Пароль:");

            passwordEncrypt();

            string passwordEncod = passwordEnc();
            string usserEncod = usserEncrypt();

            if (passwordEncod == usserEncod)
            {
                Console.WriteLine("Пароль введен верно продолжайте редактирование");
                schedule(check, scheduleInOneByte);
            }
            else
            {
                Console.WriteLine("Пароль не верный!");
            }

        }

        static void schedule(bool check, byte scheduleInOneByte) // функция редактирования расписания
        {

            while (check == true)
            {

                Console.WriteLine("{0,8}", Convert.ToString(scheduleInOneByte, 2));

                int dayWeek;

                check = int.TryParse((Console.ReadLine()), out dayWeek);
                switch (dayWeek)
                {
                    case 1: //понедельник
                        Console.Clear();
                        scheduleInOneByte = (byte)(scheduleInOneByte ^ 0x80);

                        break;

                    case 2: //вторник
                        Console.Clear();
                        scheduleInOneByte = (byte)(scheduleInOneByte ^ 0x40);

                        break;

                    case 3: //среда
                        Console.Clear();
                        scheduleInOneByte = (byte)(scheduleInOneByte ^ 0x20);

                        break;

                    case 4: //четверг
                        Console.Clear();
                        scheduleInOneByte = (byte)(scheduleInOneByte ^ 0x10);

                        break;

                    case 5: //пятница
                        Console.Clear();
                        scheduleInOneByte = (byte)(scheduleInOneByte ^ 0x08);

                        break;

                    case 6: //суббота
                        Console.Clear();
                        scheduleInOneByte = (byte)(scheduleInOneByte ^ 0x04);

                        break;

                    case 7: //воскресенье
                        Console.Clear();
                        scheduleInOneByte = (byte)(scheduleInOneByte ^ 0x02);

                        break;

                    default:
                        check = false;
                        Console.Clear();
                        Console.WriteLine("{0,8}", Convert.ToString(scheduleInOneByte, 2));

                        break;

                }

            }

        }

        static void passwordEncrypt()   // создание файла пароля
        {
            string Pass = "Пароль";

            FileStream file1 = new FileStream(@"C:\Users\Vitaliy\Desktop\mask.txt", FileMode.Open); //создаем файловый поток
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

            FileStream file2 = new FileStream(@"C:\Users\Vitaliy\Desktop\passwordEncrypt.txt", FileMode.Create); //создаем файловый поток
            StreamWriter writerPasswordEncrypt = new StreamWriter(file2); //создаем «потоковый писатель» и связываем его с файловым потоком
            writerPasswordEncrypt.Write($"{destination}"); //записываем в файл
            writerPasswordEncrypt.Close();

        }

        static string passwordEnc() // чтение файла
        {
            FileStream file = new FileStream("C:\\Users\\Vitaliy\\Desktop\\passwordEncrypt.txt", FileMode.Open); //создаем файловый поток
            StreamReader readerPass = new StreamReader(file); // создаем «потоковый читатель» и связываем его с файловым потоком
            string Pass = readerPass.ReadToEnd(); //считываем все данные с потока
            readerPass.Close(); //закрываем поток

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

        static void DZ4()
        {

        }

        static void Print()
        {

            Console.WriteLine("Первое задание: Ввывести на экран количество единиц и нулей в байте");
            DZ1();
            Console.WriteLine("\nДля продолжения нажмите клавишу :)");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Второе задание: Выполнить умножение двух целых числе используя битовый сдвиг");
            DZ2();
            Console.WriteLine("\nДля продолжения нажмите клавишу :)");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Третье задание: Создать расписание с редактированием и паролем ");
            DZ3();

        }
    }
}
