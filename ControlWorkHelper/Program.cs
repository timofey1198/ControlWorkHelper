using System;
using System.Collections.Generic;


namespace ControlWorkHelper
{
    class Program:Helper
    {
        static void Main(string[] args)
        {
            // Получаем таблицу штрафных баллов
            List<List<string>> penaltyPointsStr = CSVHelper.ReadAll("PenaltyPoints.csv");
            Dictionary<int, int> penaltyPoints = new Dictionary<int, int>();
            int currentMinute = 0;
            int lastMinute = 0;
            foreach (List<string> str in penaltyPointsStr)
            {
                lastMinute = currentMinute;
                currentMinute = int.Parse(str[0]);
                for (int i = lastMinute; i < currentMinute; i++)
                {
                    penaltyPoints.Add(i, int.Parse(str[1]));
                }
            }

            // Максимальная задержка, после которой работа аннулируется
            TimeSpan maxDelay = new TimeSpan(0, currentMinute, 0);

            // Ввод названия репозитория контрольной
            string repos = "";
            string confirm = "n";
            while (confirm == "n")
            {
                Console.WriteLine("Введите название работы (имя репозитория):");
                repos = Console.ReadLine();
                Console.WriteLine("Вы уверены?(y/n)");
                confirm = Console.ReadLine();
            }

            // Ввод времени начала работы
            Console.WriteLine("Введите информацию о времени начала работы:");
            int year = IntInput("Введите год:", 2017, 2021);
            int month = IntInput("Введите месяц:", 1, 12);
            int day = IntInput("Введите число:", 1, 31);
            int hour = IntInput("Введите час:", 0, 23);
            int minute = IntInput("Введите минуту:", 0, 59);
            // Время начала работы
            DateTime startTime = new DateTime(year, month, day, hour, minute, 0);

            // Ввод времени отведенного на работу
            Console.WriteLine();
            int limit = IntInput("Введите время на работу (в минутах):");
            // Время на работу
            TimeSpan timeLimit = new TimeSpan(0, limit, 0);

            // Считывание данных о студентах
            List<List<string>> db = CSVHelper.ReadAll("DataBase.csv");

            // Основные используемые переменные
            string url, email, githubName, name, surname;
            Commit lastCommit; // Объект последнего коммита
            TimeSpan workTime; // Время работы студента
            TimeSpan delay; // Опоздание отправки

            // Список студентов
            List<Person> students = new List<Person>();
            foreach (List<string> user in db)
            {
                // Генерация уникальной ссылки на репозиторий
                // студента с решением контрольной
                githubName = user[0];
                email = user[1];
                url = string.Format("https://github.com/{0}/{1}.git", githubName, repos);

                // Объект студента
                Person person = new Person(email, githubName);

                // Сохранение репозитория студента
                GitHelper.Clone(url, email);
                // Список всех коммитов студента
                List<Commit> commits = LogParser.GetAllCommits(email);

                // Расчет штрафа для студента
                if (commits.Count != 0)
                {
                    lastCommit = commits[0]; // Последний коммит
                    person.LastCommit = lastCommit;
                    workTime = lastCommit.Date.Subtract(startTime); // Время работы
                    delay = workTime.Subtract(timeLimit); // Опоздание
                    
                    if (delay > TimeSpan.Zero)
                    {
                        if (delay >= maxDelay)
                        {
                            person.PenaltyPoints = 10;
                        }
                        else
                        {
                            person.PenaltyPoints = penaltyPoints[delay.Minutes];
                        }
                        person.Comment = "Превышено время работы на " + delay;
                    }
                    else if (workTime < TimeSpan.Zero)
                    {
                        person.Comment = "Работа не была начата.";
                        person.PenaltyPoints = 10;
                    }
                }
                else
                {
                    person.LastCommit = null;
                    person.Comment = "Работа не была начата.";
                    person.PenaltyPoints = 10;
                }
                // Добавляем объект сдудента в общий список
                students.Add(person);
            }

            // Вывод всех студентов в консоль
            foreach (Person person in students)
            {
                Console.WriteLine(person);
            }
        }
    }
}