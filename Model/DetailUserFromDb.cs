using System;
using System.Collections.ObjectModel;
using System.Globalization;
using coursework3.Classes;
using Npgsql;

namespace coursework3.Model
{
    class DetailUserFromDb
    {
        // Получение дат авторизации пользователя из базы данных
        public ObservableCollection<Auth> GetAuths(int user)
        {
            NpgsqlConnection connection = DbConnection.getConnection();
            if (connection.FullState == System.Data.ConnectionState.Closed)
                connection.Open();

            ObservableCollection<Auth> auths = new ObservableCollection<Auth>();

            string sqlQuery = 
                "select logged_at from auth where logged_in = @logged_in " +
                "order by logged_in desc;";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.Parameters.AddWithValue("owner", user);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    auths.Add(new Auth(getUserDateTime(reader[0].ToString())));

            connection.Close();
            return auths;
        }

        // Удаление сохраненных дат авторизации пользователя из базы данных
        public void DeleteAuth(int user)
        {
            NpgsqlConnection connection = DbConnection.getConnection();
            if (connection.FullState == System.Data.ConnectionState.Closed)
                connection.Open();

            string sqlQuery = "delete from auth where logged_in = @user";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public string getDateTimeString(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return string.Empty;

            return dateTime.ToString(
                "yyyy-MM-dd HH:mm:ss.fff",
                CultureInfo.InvariantCulture);
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

        // Получение файлов пользователя из базы данных
        public ObservableCollection<File> GetFiles(int user)
        {
            NpgsqlConnection connection = DbConnection.getConnection();
            if (connection.FullState == System.Data.ConnectionState.Closed)
                connection.Open();

            ObservableCollection<File> files = new ObservableCollection<File>();

            string sqlQuery = 
                "select id,original_filename,filesize,content_type,upload_date " +
                "from files where owner = @owner;";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.Parameters.AddWithValue("owner", user);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                    files.Add(new File(
                        int.Parse(reader[0].ToString()),
                        reader[1].ToString(),
                        double.Parse(reader[2].ToString()) / 1024,
                        reader[3].ToString(),
                        getUserDateTime(reader[4].ToString())));
            }
            connection.Close();
            return files;
        }
    }
}
