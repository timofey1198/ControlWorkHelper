using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace ControlWorkHelper
{
    class Program:Helper
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Введите расположение репозитория:");
            //string path = Console.ReadLine();
            //Process p = Process.Start("cd", path);
            //GitHelper.Clone("https://github.com/DimaT1/gg.git");
            //Console.WriteLine(GitHelper.Log("xxx"));
            

            string repos = "";
            string confirm = "n";
            while (confirm == "n")
            {
                Console.WriteLine("Введите название работы (имя репозитория):");
                repos = Console.ReadLine();
                Console.WriteLine("Вы уверены?(y/n)");
                confirm = Console.ReadLine();
            }

            Console.WriteLine("Введите информацию о времени начала работы:");
            int year = IntInput("Введите год:");
            int month = IntInput("Введите месяц:");
            int day = IntInput("Введите число:");
            int hour = IntInput("Введите час:");
            int minute = IntInput("Введите минуту:");
            DateTime startTime = new DateTime(year, month, day, hour, minute, 0);

            Console.WriteLine();
            int limit = IntInput("Введите время на работу (в минутах):");
            //startTime.Subtract
            TimeSpan timeLimit = new TimeSpan(0, limit, 0);
            //DateTime endTime = new DateTime(year, month, day, hour, minute, 0);

            List<List<string>> db = CSVHelper.ReadAll("Книга1.csv");
            string url, email, githubName, name, surname;
            Commit lastCommit;
            TimeSpan workTime;
            // Список студентов
            List<Person> students = new List<Person>();
            foreach (List<string> user in db)
            {
                githubName = user[0];
                email = user[1];
                url = string.Format("https://github.com/{0}/{1}.git", githubName, repos);

                Person person = new Person(email, githubName);

                GitHelper.Clone(url, email);
                List<Commit> commits = LogParser.GetAllCommits(email);
                if (commits.Count != 0)
                {
                    lastCommit = commits[0];
                    person.LastCommit = lastCommit;
                    workTime = lastCommit.Date.Subtract(startTime);
                    Console.WriteLine(workTime);
                    if (workTime > timeLimit)
                    {
                        Console.WriteLine("Превышено время работы.");
                        Console.WriteLine(workTime - timeLimit);
                    }
                }
                else { person.LastCommit = null; }
                students.Add(person);
            }

            foreach (Person person in students)
            {
                Console.WriteLine(person);
            }

            //ProcessStartInfo psi = new ProcessStartInfo();
            //psi.FileName = "git";
            //psi.Arguments = "log";
            //psi.UseShellExecute = false;
            //psi.RedirectStandardOutput = true;
            //psi.RedirectStandardInput = true;
            //psi.WorkingDirectory = "xxx";
            //Process process = Process.Start(psi);
            //StreamReader stream = process.StandardOutput;
            //Console.WriteLine(stream.ReadToEnd());

            //Console.WriteLine(_out);
            //GitHelper.Checkout("dev", true);
            //process.
            //string s = process.StandardOutput.ReadToEnd();
            //Console.WriteLine(s);

            //Console.Read();
        }
    }
}