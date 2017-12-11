namespace Task03
{
    using System;
    using System.Globalization;

    public class ForConsole
    {
        public static void PrintUser(User user)
        {
            Console.WriteLine("User:");
            Console.WriteLine("\t{0} {1} {2}, {3}({4} years old).", user.FirstName, user.Patronymic, user.SecondName, user.DateOfBirth.ToString("dd.MM.yyyy"), user.Age);
        }

        public static DateTime ReadAndCheckDate()
        {
            var str = Console.ReadLine();
            DateTime date;
            try
            {
                date = DateTime.ParseExact(str, "d", CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Invalid date format. Enter again:");
                date = ReadAndCheckDate();
            }

            return date;
        }
    }
}
