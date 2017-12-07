using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorkHelper
{
    class Person
    {
        public string GithubName { get;}
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; }
        public Commit LastCommit { get; set; }
        public int PenaltyPoints { get; set; }
        public string Comment { get; set; }


        public Person() {}

        public Person(string email, string githubName)
        {
            Email = email;
            GithubName = githubName;
            Name = "";
            Surname = "";
            PenaltyPoints = 0;
            Comment = "";
            LastCommit = new Commit();
        }

        public override string ToString()
        {
            string str = string.Format("Имя: {0}\n Фамилия: {1}\n Почта: {2}\n Время отправки: {3}\n Штраф: {4}\n Комментарий: {5}",
                Name, Surname, Email, LastCommit.Date, PenaltyPoints, Comment);
            return str;
        }
    }
}
