using System;
using System.Text;
using System.IO;

namespace Person1
{
    class Person
    {
        string name;
        int birth_year;
        double pay;

        public Person()
        {
            name = "Anonimous";
            birth_year = 0;
            pay = 0;
        }

        public Person(string s)
        {
            var parts = s.Split(' '); // Розбиття рядка на частини
            name = parts[0] + " " + parts[1]; // Прізвище і ініціали
            birth_year = Convert.ToInt32(parts[2]); // Рік народження
            pay = Convert.ToDouble(parts[3]); // Оклад

            if (birth_year < 0) throw new FormatException();
            if (pay < 0) throw new FormatException();
        }

        public override string ToString()
        {
            return string.Format("Name: {0,30} birth: {1} pay: {2:F2}", name, birth_year, pay);
        }

        public int Compare(string name)
        {
            return string.Compare(this.name, 0, name + " ", 0, name.Length + 1, StringComparison.OrdinalIgnoreCase);
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Birth_year
        {
            get { return birth_year; }
            set
            {
                if (value > 0) birth_year = value;
                else throw new FormatException();
            }
        }

        public double Pay
        {
            get { return pay; }
            set
            {
                if (value > 0) pay = value;
                else throw new FormatException();
            }
        }

        public static double operator +(Person pers, double a)
        {
            pers.pay += a;
            return pers.pay;
        }

        public static double operator +(double a, Person pers)
        {
            pers.pay += a;
            return pers.pay;
        }

        public static double operator -(Person pers, double a)
        {
            pers.pay -= a;
            if (pers.pay < 0) throw new FormatException();
            return pers.pay;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person[] dbase = new Person[100];
            int n = 0;
            try
            {
                StreamReader f = new StreamReader("d:\\Persons.txt");
                string s;
                int i = 0;

                while ((s = f.ReadLine()) != null)
                {
                    dbase[i] = new Person(s);
                    Console.WriteLine(dbase[i]);
                    ++i;
                }
                n = i;
                f.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Перевірте правильність імені і шляху до файлу!");
                return;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Дуже великий файл!");
                return;
            }
            catch (FormatException)
            {
                Console.WriteLine("Неприпустима дата народження або оклад");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("Помилка: " + e.Message);
                return;
            }

            int n_pers = 0;
            double mean_pay = 0;
            Console.WriteLine("Введіть прізвище співробітника");
            string name;
            while ((name = Console.ReadLine()) != "")
            {
                bool not_found = true;
                for (int k = 0; k < n; ++k)
                {
                    Person pers = dbase[k];
                    if (pers.Compare(name) == 0)
                    {
                        Console.WriteLine(pers);
                        not_found = false;
                        break;
                    }
                }
                if (not_found)
                {
                    Console.WriteLine("Співробітника не знайдено");
                }
            }
        }
    }
}
