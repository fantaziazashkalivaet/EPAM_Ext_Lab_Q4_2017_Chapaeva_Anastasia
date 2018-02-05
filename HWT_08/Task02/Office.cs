namespace Task02
{
    using System;
    using System.Collections.Generic;

    public class Office
    {
        private List<Person> workers;

        /// <summary>
        /// Инициализирует новый объект класса Office
        /// </summary>
        public Office()
        {
            workers = new List<Person>();
        }

        /// <summary>
        /// Все сотрудники в офисе приветствуют того, кто пришел
        /// </summary>
        /// <param name="newPerson">Пришедший сотрудник</param>
        /// <param name="time">Время прихода</param>
        public void WorkerCome(Person newPerson, DateTime time)
        {
            ForConsole.Write(string.Empty);
            ForConsole.Write(String.Format("[На работу пришел {0}]", newPerson.Name));//todo pn почему здесь в константы не вынесла?
            foreach (var worker in workers)
            {
                newPerson.Come += worker.Greeting;
                newPerson.Out += worker.Goodbye;
                worker.Out += newPerson.Goodbye;
            }

            workers.Add(newPerson);
            newPerson.ComeToOffice(DateTime.Now);
        }

        /// <summary>
        /// Все сотрудники в офисе прощаются с тем, кто ушел
        /// </summary>
        /// <param name="outPerson">Ушедший сотрудник</param>
        public void WorkerOut(Person outPerson)
        {
            ForConsole.Write(string.Empty);
            ForConsole.Write(String.Format("[{0} ушел домой]", outPerson.Name));//todo pn почему здесь в константы не вынесла?
			outPerson.OutOffice();
            foreach (var worker in workers)
            {
                outPerson.Come -= worker.Greeting;
                outPerson.Out -= worker.Goodbye;
                worker.Out -= outPerson.Goodbye;
            }

            workers.Remove(outPerson);
        }
    }
}
