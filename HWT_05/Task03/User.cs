namespace Task03
{
    using System;

    public class User
    {
        private const int MinYearOfBirth = 1900;
        private string firstName;
        private string secondName;
        private string patronymic;
        private DateTime dateOfBirth;
        private int age;

        public User()
        {
            firstName = "Petr";
            secondName = "Ivanov";
            patronymic = "Vladimirovich";
            dateOfBirth = new DateTime(MinYearOfBirth, 01, 01);
            age = DateTime.Now.Year - dateOfBirth.Year;
        }

        public User(string firstName, string secondName, string patronimic, DateTime dateOfBirth)
        {
            FirstName = firstName;
            SecondName = secondName;
            Patronymic = patronimic;
            DateOfBirth = dateOfBirth;
            age = DateTime.Now.Year - dateOfBirth.Year;
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                if (CheckName(value))
                {
                    firstName = value;
                }
                else
                {
                    if (firstName == string.Empty)
                    {
                        firstName = "Petr";
                    }
                }
            }
        }

        public string SecondName
        {
            get
            {
                return secondName;
            }

            set
            {
                if (CheckName(value))
                {
                    secondName = value;
                }
                else
                {
                    if (secondName == string.Empty)
                    {
                        secondName = "Ivanov";
                    }
                }
            }
        }

        public string Patronymic
        {
            get
            {
                return patronymic;
            }

            set
            {
                if (CheckName(value))
                {
                    patronymic = value;
                }
                else
                {
                    if (patronymic == string.Empty)
                    {
                        patronymic = "Vladimirovich";
                    }
                }
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }

            set
            {
                if (CheckDateOfBirth(value))
                {
                    dateOfBirth = value;
                }
                else
                {
                    if (dateOfBirth == null)
                    {
                        dateOfBirth = new DateTime(MinYearOfBirth, 01, 01);
                    }
                }
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
        }

        private bool CheckDateOfBirth(DateTime date)
        {
            return date.Year >= MinYearOfBirth;
        }

        private bool CheckName(string name)
        {
            return name != string.Empty;
        }
    }
}
