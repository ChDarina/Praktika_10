using System;
using System.IO;
using System.Text.RegularExpressions;

namespace praktika_10
{
    class ListEntry
    {
        public int Degree { get; set; }
        public int A { get; set; }
        public ListEntry Next { get; set; }
        public ListEntry()
        {
            Degree = 0;
            A = 0;
            Next = null;
        }
        public ListEntry(int degree, int a)
        {
            Degree = degree;
            A = a;
            Next = null;
        }
        public override string ToString()
        {
            return Degree + " " + A;
        }
    }
    class Program
    {
        public static ListEntry ReadFromFile(string file_name)
        {
            ListEntry ans = new ListEntry();
            ListEntry list = ans;
            using (FileStream sf = new FileStream(file_name, FileMode.OpenOrCreate)) { }
            using (StreamReader reader = new StreamReader(file_name))
            {
                Console.WriteLine("\n"+ file_name.ToUpper() + "\n");
                while (reader.Peek() > -1)
                {
                    string str = reader.ReadLine();
                    str = Regex.Replace(str.Trim(), @"\s+", " ");
                    Console.WriteLine(str);
                    string[] str_arr = str.Split(new char[] { ' ' });
                    try
                    {
                        if (Convert.ToInt32(str_arr[1]) != 0)
                        {
                            list.Next = new ListEntry(Convert.ToInt32(str_arr[0]), Convert.ToInt32(str_arr[1]));
                            list = list.Next;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\n\nФайл заполнен неверно. Проверьте заполнение файла и запустите программу еще раз");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
            }
            return ans.Next;
        }
        public static ListEntry Addition(ListEntry pol_1, ListEntry pol_2)
        {
            if (pol_1 != null && pol_2 != null)
            {
                ListEntry beg = new ListEntry();
                ListEntry list = beg;
                ListEntry beg1 = pol_1;
                ListEntry temp1 = pol_1;
                ListEntry temp2 = pol_2;
                int slag = 0;
                while (pol_1 != null)
                {
                    slag =0;            
                    while (temp1!=null)
                    {
                        if (pol_1.Degree == temp1.Degree) slag += temp1.A;
                        temp1 = temp1.Next;
                    }
                    while (temp2 != null)
                    {
                        if (pol_1.Degree == temp2.Degree)
                        {
                            slag += temp2.A;
                        }
                        temp2 = temp2.Next;
                    }
                    if (slag != 0)
                    {
                        list.Next = new ListEntry(pol_1.Degree, slag);
                        list = list.Next;
                    }
                    temp2 = pol_2;
                    pol_1 = pol_1.Next;
                    temp1 = beg1;
                }
                list = beg.Next;
                temp1 = beg.Next;
                while (list!=null) //проверка на одинаковые слагаемые
                {
                    while (temp1!= null)
                    {
                        if (list.A == temp1.A && list.Degree == temp1.Degree)
                        {
                            list.Next = list.Next.Next;
                        }
                        temp1 = temp1.Next;
                    }
                    list = list.Next;
                }
                return beg.Next;
            }
            else return null;
        }

        static void Main(string[] args)
        {
            string input_1_f = "input_1.txt", input_2_f = "input_2.txt", output_f = "output.txt";
            ListEntry list_1 = ReadFromFile(input_1_f);
            ListEntry list_2 = ReadFromFile(input_2_f);
            using (FileStream sf = new FileStream(output_f, FileMode.OpenOrCreate)) { }
            using (StreamWriter writer = new StreamWriter(output_f))
            {
                Console.WriteLine("\nOUTPUT.TXT\n");
                ListEntry list = Addition(list_1, list_2);
                while (list != null)
                {
                    Console.WriteLine(list.ToString());
                    writer.WriteLine(list.ToString());
                    list = list.Next;
                }
            }
            Console.ReadKey();
        }
    }
}
