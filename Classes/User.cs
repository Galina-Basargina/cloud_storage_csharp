using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coursework3.Class
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public int FileCount { get; set; }
        public double AllFileSize { get; set; }
        public DateTime LastSession { get; set; }

        public User(int userId, string login, int fileCount, double allFileSize,  DateTime lastSession)
        {
            this.UserId = userId;
            this.Login = login;
            FileCount = fileCount;
            LastSession = lastSession;
            AllFileSize = allFileSize;
        }
    }
}
