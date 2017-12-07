using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ControlWorkHelper
{
    class Program:Helper
    {
        static void Main(string[] args)
        {
            do
            {
                // Проверка на гит
                try
                {
                    Process.Start("git", "help");
                }
                catch
                {
                    Console.WriteLine("У вас не установлен Git!");
                    Console.WriteLine("Установите его и нажмите любую клавишу.");
                    Console.Read();
                    continue;
                }

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

                //// Ввод времени начала работы
                DateTime startTime = DateInput("Введите информацию о времени начала работы:");

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

                    List<Commit> commits = new List<Commit>();

                    try
                    {
                        // Список всех коммитов студента
                        commits = LogParser.GetAllCommits(email);
                    }
                    catch
                    {
                        person.PenaltyPoints = 10;
                        person.Comment = "Не найден рапозиторий с работой";
                        students.Add(person);
                        continue;
                    }

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
                Console.WriteLine("Для выхода нажмите любую клавишу");
                Console.Read();
                break;
            }
            while (true);
        }
    }
}