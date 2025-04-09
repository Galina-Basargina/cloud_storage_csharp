using coursework3.Classes;
using System;
using Npgsql;
using coursework3.Class;
using System.Collections.ObjectModel;
using System.Globalization;

namespace coursework3.Model
{
    public class UserFromDb
    {
        // Проверка авторизации администратора через базу данных
        public bool AuthAdmin(string login, string password)
        {
            NpgsqlConnection connection = DbConnection.getConnection();
            if (connection.FullState == System.Data.ConnectionState.Closed)
                connection.Open();
            string sqlQuery = 
                "select login, password_checksum from users " +
                "where id = 0 and login = @login";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.Parameters.AddWithValue("login", login);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                string password_checksum = Verification.HashPassword(password);
                if (password_checksum != reader[1].ToString())
                {
                    connection.Close();
                    return false;
                }
                connection.Close();
                return true;
            }
            connection.Close();
            return false;
        }

        // Получение списка всех пользователей
        public ObservableCollection<User> GetUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            NpgsqlConnection connection = DbConnection.getConnection();
            if (connection.FullState == System.Data.ConnectionState.Closed)
                connection.Open();
            string sqlQuery = 
                "select " +
                " u.id,u.login, " +
                " count(f.filesize),sum(f.filesize), " +
                " (select logged_at" +
                "  from auth a where a.logged_in = u.id " +
                "  order by logged_at limit 1) " +
                "from users u " +
                " left join files f on (f.\"owner\" = u.id) " +
                "group by u.id " +
                "order by u.id;";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, connection);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    users.Add(new User(
                        int.Parse(reader[0].ToString()),
                        reader[1].ToString(),
                        int.Parse(reader[2].ToString()),
                        Math.Round(
                            double.Parse(
                                reader[3].ToString() != "" ? reader[3].ToString() : "0") / 1024, 3),
                        getUserDateTime(reader[4].ToString())));
                }
            }
            connection.Close();
            return users;
        }

        private DateTime getUserDateTime(string date)
        {
            if (DateTime.TryParseExact(
                date,
                "dd.MM.yyyy HH:mm:ss.fff",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime result))
                return result;
            return new DateTime();
        }
    }
}



