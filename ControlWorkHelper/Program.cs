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
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Введите расположение репозитория:");
            //string path = Console.ReadLine();
            //Process p = Process.Start("cd", path);
            //GitHelper.Clone("https://github.com/DimaT1/gg.git");
            //Console.WriteLine(GitHelper.Log("xxx"));
            

            string repos = "gg";
            List<List<string>> db = CSVHelper.ReadAll("Книга1.csv");
            foreach (List<string> user in db)
            {
                string url = string.Format("https://github.com/{0}/{1}.git", user[0], repos);
                GitHelper.Clone(url, user[1]);
                List<Commit> commits = LogParser.GetAllCommits(user[1]);
                foreach (Commit commit in commits)
                {
                    Console.WriteLine(commit.User);
                    Console.WriteLine(commit.Email);
                    Console.WriteLine(commit.Date);
                    Console.WriteLine();
                }
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