namespace Task02
{
    using System;

    public class Person
    {
        private const string StandartName = "Джо";
        private const string StandartGreetingMorging = "Доброе утро";
        private const string StandartGreetingDay = "Добрый день";
        private const string StandartGreetingEvening = "Добрый вечер";
        private const string StandartGoodbye = "До свидания";
        private const int MorningHour = 12;
        private const int EveningHour = 17;

        /// <summary>
        /// Инициализирует новый экземпляр класса Person со значением Name по умолчанию
        /// </summary>
        public Person() : this(StandartName)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса Person с указанным значением Name
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        public Person(string name)
        {
            Name = name;
        }

        public delegate void ComeEventArgs(Person person, DateTime time);

        public delegate void OutEventArgs(Person person);

        public event ComeEventArgs Come;

        public event OutEventArgs Out;

        /// <summary>
        /// Получает Имя сотрудника
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Вызывает событие Come
        /// </summary>
        /// <param name="time">Время прихода сотрудника в офис</param>
        public void ComeToOffice(DateTime time)
        {
            Come?.Invoke(this, time);
        }

        /// <summary>
        /// Вызывает событие Out
        /// </summary>
        public void OutOffice()
        {
            Out?.Invoke(this);
        }

        /// <summary>
        /// Приветствует пришедшего
        /// </summary>
        /// <param name="worker">Пришедший отрудник</param>
        /// <param name="time">Время прихода</param>
        public void Greeting(Person worker, DateTime time)
        {
            string greeting = StandartGreetingDay;
            if (time.Hour < MorningHour)
            {
                greeting = StandartGreetingMorging;
            }
            else
            {
                if (time.Hour >= EveningHour)
                {
                    greeting = StandartGreetingEvening;
                }
            }

            Say(String.Format("{0}, {1}!", greeting, worker.Name));
        }

        /// <summary>
        /// Говорит фразу от своего имени
        /// </summary>
        /// <param name="s">Фраза</param>
        public void Say(string s)
        {
            ForConsole.Write(String.Format("\"{0}\", - сказал {1}", s, Name));
        }

        /// <summary>
        /// Прощается с ушедшим
        /// </summary>
        /// <param name="worker">Ушедший сотрудник</param>
        public void Goodbye(Person worker)
        {
            Say(String.Format("{0}, {1}!", StandartGoodbye, worker.Name));
        }
    }
}
