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
            GitHelper.Checkout("master", "https://github.com/DimaT1/gg.git");
            List<Commit> commits = LogParser.GetAllCommits();
            foreach (Commit commit in commits)
            {
                Console.WriteLine(commit.User);
                Console.WriteLine(commit.Email);
                Console.WriteLine(commit.Date);
                Console.WriteLine();
            }
            //Process process = Process.Start("log.bat");
            
            //Console.WriteLine(_out);
            //GitHelper.Checkout("dev", true);
            //process.
            //string s = process.StandardOutput.ReadToEnd();
            //Console.WriteLine(s);

            //Console.Read();
        }
    }
}