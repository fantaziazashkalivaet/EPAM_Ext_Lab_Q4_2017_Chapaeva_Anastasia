// На основе класса User(см.задание 3 из предыдущей темы),
// создать класс Employee, описывающий сотрудника фирмы.
// В дополнение к полям пользователя добавить поля «стаж работы» и «должность».
// Обеспечить нахождение класса в заведомо корректном состоянии.

namespace Task01
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var a = new Employee();
            Console.WriteLine("{0}, {1}", a.Age, a.Post);
            a = new Employee("lol", "kek", "mom", DateTime.Parse("22/12/1999"), Post.director, 2);
            Console.WriteLine("{0}, {1}", a.Age, a.Post);
        }
    }
}
