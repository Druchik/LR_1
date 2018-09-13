using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR_1
{
    class Program
    {
        static int i = 0;
        static Stack<int> stack = new Stack<int>();
        static string lenta;
        static string word;
        static char[] ch;
        static char[] ch1 = { 'A','B','X'};
        static char charr;
        static string e = "Error";
        static readonly string[,] controlTable =  {
            {"o",e,e,e,"S2",e,e,e,e,e},//1 
            {e,"S3","S4","S6",e,"S8",e,e,"S9",e},//2
            {e,e,e,e,e,e,e,e,e,"R1"},//3
            {e,e,e,e,e,e,e,"S5",e,e},//4
            {e,e,e,e,e,e,e,e,e,"R2"},//5
            {e,e,e,e,e,e,"S7",e,e,e},//6
            {e,e,e,e,e,e,e,e,e,"R3"},//7
            {e,e,"S9",e,e,"S8",e,"R5",e,"R5"},//8
            {e,e,e,"S10",e,e,"R7","R4","S9","R4"},//9
            {e,e,e,e,e,e,"R6",e,e,"R6"},//10
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку: ");
            lenta = Console.ReadLine() + "$";

            stack.Push(0);
            Console.WriteLine();

            while (true)
            {
                try
                {
                    Console.WriteLine("Lenta = " + lenta);
                    Console.Write("Stack = ");
                    foreach (var item in stack)
                    {
                        Console.Write(item + 1 + " ");
                    }
                    
                    string command = controlTable[stack.Peek(), GetCol(Convert.ToString(lenta[i]))];
                    
                    Console.WriteLine("\ntable[" + (stack.Peek() + 1) + "][" + lenta[i] + "] = " + command);

                    if (command.StartsWith("S"))
                    {
                        // Сдвиг
                        charr = lenta[i];
                        Shift(int.Parse(command.Substring(1)));
                        if (ch1.Contains(charr))
                            lenta =Replace(lenta, charr);
                    }
                    else if (command.StartsWith("R"))
                    {
                        // Приведение 
                        Reduction(int.Parse(command.Substring(1)));
                    }
                    else if (command == "o")
                    {
                        // Допуск
                        Console.WriteLine("Accept");
                        break;
                    }
                    else if (command == e)
                    {
                        // Ошибка
                        Console.WriteLine(e);
                        break;
                    }
                    else
                    { // never
                        Console.WriteLine("index of table is not correct");
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("\nСлово содержит нераспознаваемые символы!");
                    break;
                }
            }
            Console.WriteLine("Нажмите любую клавишу для завершения...");
            Console.ReadLine();
        }
        /* Получить номер колонки */
        static int GetCol(String chr)
        {
            return "SXABabcdf$".IndexOf(chr);
        }

        /* Сдвиг */
        static void Shift(int rule)
        {
            Console.WriteLine("Shift -> " + rule);
            word += lenta[i]; Console.WriteLine("\nWord: " + word);
            stack.Push(rule - 1);
            i++;
        }

        /* Приведение */
        static void Reduction(int rule)
        {
            Console.WriteLine("Reduction -> " + rule + "\n");
            switch (rule - 1)
            {
                case 0:
                    Drop(2);
                    PushAt("S");
                    ch = new char[] { 'a', 'X' };
                    Del(ch);
                    Console.WriteLine("Word: " + word);
                    break;
                case 1:
                    Drop(2);
                    PushAt("X");
                    ch = new char[] { 'A', 'd' };
                    Del(ch);
                    Console.WriteLine("Word: " + word);
                    break;
                case 2:
                    Drop(2);
                    PushAt("X");
                    ch = new char[] { 'B', 'c' };
                    Del(ch);
                    Console.WriteLine("Word: " + word);
                    break;
                case 3:
                    Drop(2);
                    PushAt("A");
                    ch = new char[] { 'b', 'A' };
                    Del(ch);
                    Console.WriteLine("Word: " + word);
                    break;
                case 4:
                    PushAt("A");
                    break;
                case 5:
                    Drop(2);
                    PushAt("B");
                    ch = new char[] { 'f', 'B' };
                    Del(ch);
                    Console.WriteLine("Word: " + word);
                    break;
                case 6:
                    PushAt("B");
                    break;
            }
        }
        /* Удалить N символов из стека */
        static void Drop(int n)
        {
            for (int k = 0; k < n; k++)
                stack.Pop();
        }
        
        /* поместить в ленту  */
        static void PushAt(String chr)
        {
            lenta = lenta.Substring(0, i) + chr + lenta.Substring(i);
        }

        static void Del(char[] ch)
        {
            word = word.Trim(ch);
        }

        static string Replace(string lenta, char charr)
        {
            if(lenta.Contains(charr))
            {
                lenta = lenta.Remove(lenta.IndexOf(charr), 1);
                i--;
            }
            return lenta;
        }
    }
}