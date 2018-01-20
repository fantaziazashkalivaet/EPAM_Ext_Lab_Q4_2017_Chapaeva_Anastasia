namespace Task03
{
    using System;

    public class User
    {
        private const int MinYearOfBirth = 1900;//todo pn вот год в константы вынесла, а остальные значения по умолчанию нет.
        private string firstName;
        private string secondName;
        private string patronymic;
        private DateTime dateOfBirth;
        private int age;

        public User()
        {
            firstName = "Petr";//todo pn хардкод
			secondName = "Ivanov";//todo pn хардкод
			patronymic = "Vladimirovich";//todo pn хардкод
			dateOfBirth = new DateTime(MinYearOfBirth, 01, 01);//todo pn хардкод
            age = DateTime.Now.Year - dateOfBirth.Year;//todo pn дублирование кода
		}

        public User(string firstName, string secondName, string patronimic, DateTime dateOfBirth)
        {
            FirstName = firstName;
            SecondName = secondName;
            Patronymic = patronimic;
            DateOfBirth = dateOfBirth;
            age = DateTime.Now.Year - dateOfBirth.Year;//todo pn некорректная логика вычисления возраста
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
                        firstName = "Petr";//todo pn хардкод
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
                        secondName = "Ivanov";//todo pn хардкод
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
                        patronymic = "Vladimirovich";//todo pn хардкод
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
                    if (dateOfBirth == null) //todo pn атата проверяешь значимый тип на null)
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
