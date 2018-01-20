 // Написать программу, описывающую небольшой офис,
 // в котором работают сотрудники – объекты класса Person,тобладающие полем имя(Name).
 // Каждый из сотрудников содержит пару методов:
 // приветствие сотрудника, пришедшего на работу
 // (принимает в качестве аргументов объект сотрудника и время его прихода)
 // и прощание с ним(принимает только объект сотрудника).
 // В зависимости от времени суток, приветствие может быть различным:
 // до 12 часов – «Доброе утро», с 12 до 17 – «Добрый день», начиная с 17 часов – «Добрый вечер».
 // Каждый раз при входе очередного сотрудника в офис, все пришедшие ранее его приветствуют.
 // При уходе сотрудника домой с ним также прощаются все присутствующие.
 // Вызов процедуры приветствия/прощания производить через групповые делегаты.
 // Факт прихода и ухода сотрудника отслеживается через генерируемые им события.
 // Событие прихода описывается делегатом, передающим в числе параметров наследника EventArgs,
 // явно содержащего поле с временем прихода.
 // Продемонстрировать работу офиса при последовательном приходе и уходе сотрудников.

namespace Task02
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKeyInfo cki;

            do
            {
                Console.Clear();

                var office = new Office();
                var workers = new List<Person>();

                Console.WriteLine("Введите имена работников через Enter:");

                string newLine;
                while ((newLine = Console.ReadLine()) != string.Empty)
                {
                    workers.Add(new Person(newLine));
                }

                for (var i = 0; i < workers.Count; i++)
                {
                    office.WorkerCome(workers[i], DateTime.Now);
                }

                for (var i = workers.Count - 1; i >= 0 && workers.Count != 0; i--)
                {
                    office.WorkerOut(workers[i]);
                }

                Console.WriteLine("Press Escape (Esc) key to quit:");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);
        }
    }
}
