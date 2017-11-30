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
        }
    }
}
