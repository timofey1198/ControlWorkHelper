using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace ControlWorkHelper
{
    public class LogParser
    {
        private static string logPath = "E://log.txt";
        private static string commitPattern = 
            "commit ([a-z,A-Z,0-9,\\s,:]){30,100}Author: ";
        private static string datePattern = 
            "[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2} 20[0-9]{2} [\\+, -]{0,1}[0-9]{2}";
        public static List<Commit> GetAllCommits()
        {
            string[] strCommits = ParseLog();
            int len = strCommits.Length;
            //Commit[] commits = new Commit[len];
            List<Commit> commits = new List<Commit>();

            for (int i = 0; i < len; i++)
            {
                string commit = strCommits[i];
                if (commit.Length > 40)
                {
                    string[] xxx = commit.Split();
                    string userName = xxx[0];
                    string email = xxx[1].Substring(1, xxx[1].Length - 2);
                    Regex regex = new Regex(datePattern);
                    Match match = regex.Match(commit);
                    string strDate = match.Groups[0].Value;
                    commits.Add(new Commit(strDate, email, userName));
                }
            }

            return commits;
        }

        private static string[] ParseLog()
        {
            StreamReader fileIn = new StreamReader(logPath);
            string log = fileIn.ReadToEnd();
            fileIn.Close();
            string[] Commits = Regex.Split(log, commitPattern);
            return Commits;
        }
    }
}
