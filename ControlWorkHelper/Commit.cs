using System;
using System.Globalization;


namespace ControlWorkHelper
{
    public class Commit
    {
        public DateTime Date { get; }
        public string Email { get; }
        public string User { get; }
        public string CommitText { get; set; }
        public Commit(string date, string email, string user, string commit_text)
        {
            Date = DateTime.ParseExact(date, "dd HH:mm:ss yyyy zz", CultureInfo.InvariantCulture);
            Email = email;
            User = user;
            CommitText = commit_text;
        }

        public Commit(string date, string email, string user)
        {
            Date = DateTime.ParseExact(date, "dd HH:mm:ss yyyy zz", CultureInfo.InvariantCulture);
            Email = email;
            User = user;
            CommitText = "";
        }

        public Commit() { }
    }
}
