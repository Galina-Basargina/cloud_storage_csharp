using coursework3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using coursework3.Class;
using System.Collections.ObjectModel;
using System.Globalization;

namespace coursework3.Model
{
    public class UserFromDb
    {
        public ObservableCollection<User> GetUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            using (NpgsqlConnection connect = DbConnection.getConnection())
            {
                connect.Open();
                string sqlQuery = "select id, login, password_checksum from users;";
                NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, connect);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    //while (reader.Read())
                        //users.Add(new User(
                        //    int.Parse(reader[0].ToString()),
                        //    reader[1].ToString(),
                        //    reader[2].ToString(), 0));

                }
                return users;
            }
        }


        private DateTime? getUserDateTime(string date)
        {
            if (DateTime.TryParseExact(
                date,
                "yyyy-MM-dd HH:mm:ss.fff",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private double getAllFileSize(List<File> files)
        {
            return files.Sum(f => f.FileSize) / (1024 * 1024 * 1024); // из байтов в мегабайты
        }


        //public User GetUser(string login, string password)
        //{
        //    User user = null;

        //    try
        //    {
        //        using (NpgsqlConnection connect = new NpgsqlConnection(Connection.connectionStr))
        //        {
        //            connect.Open();
        //            string sqlQuery = $"Select * from Users where Login = @login";
        //            NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, connect);
        //            cmd.Parameters.AddWithValue("login", login);
        //            NpgsqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                reader.Read();

        //                if (password != "")
        //                {
        //                    if (false)
        //                        //(Verification.VerifySHA512Hash(password, reader[6].ToString()))
        //                    {
        //                        DateTime birthday = DateTime.Now;
        //                        if (!(reader[4] is DBNull))
        //                            birthday = Convert.ToDateTime(reader[4]);
        //                        //user = new User(
        //                        //    Convert.ToInt32(reader[0]),
        //                        //    reader[1].ToString(),
        //                        //    reader[2].ToString(),
        //                        //    reader[3].ToString(),
        //                        //    birthday,
        //                        //    reader[5].ToString(),
        //                        //    reader[6].ToString(),
        //                        //    reader[8].ToString(),
        //                        //    Convert.ToInt32(reader[9]),
        //                        //    reader[7].ToString());
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("неверный пароль");
        //                    }
        //                }
        //            }
        //            else MessageBox.Show("нет такого пользователя");
        //            return user;
        //        }
        //    }
        //    catch (NpgsqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return user;
        //    }
        //}

        //public static bool CheckPassword(string password, string passRepeat)
        //{
        //    if (password.Length < 6)
        //    {
        //        MessageBox.Show("Длина пароля не может быть меньше 6 символов");
        //        return false;
        //    }
        //    else
        //    {
        //        bool f, f1, f2;
        //        f = f1 = f2 = false;
        //        for (int i = 0; i < password.Length; i++)
        //        {
        //            if (Char.IsDigit(password[i])) f1 = true;
        //            if (Char.IsUpper(password[i])) f2 = true;
        //            if (f1 && f2) break;

        //        }
        //        if (!(f1 && f2))
        //        {
        //            MessageBox.Show("Пароль должен содержать хотя бы одну цифру и одну заглавную букву!");
        //            return f1 && f2;
        //        }
        //        else
        //        {
        //            string symbols = "!@#$%^";
        //            for (int i = 0; i < password.Length; i++)
        //            {
        //                for (int j = 0; j < symbols.Length; j++)
        //                {
        //                    if (password[i] == symbols[j]) { f = true; break; }
        //                }
        //            }

        //            if (!f) MessageBox.Show("Пароль должен содержать один из символов !@#$%^");

        //            if ((password == passRepeat) && f) return true;
        //            else
        //            {
        //                MessageBox.Show("Неверно подтверждаем пароль");
        //                return false;
        //            }

        //        }

        //    }
        //}

        //public static bool UserCheck(string login)
        //{
        //    try
        //    {
        //        using (NpgsqlConnection connect = new NpgsqlConnection(Connection.connectionStr))
        //        {
        //            connect.Open();
        //            string sqlExp = $"SELECT Login from Users where Login=@login";
        //            NpgsqlCommand cmd = new NpgsqlCommand(sqlExp, connect);
        //            cmd.Parameters.AddWithValue("@login", login);
        //            NpgsqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                MessageBox.Show("Такой логин уже есть"); 
        //                return false;
        //            }
        //            else
        //            {
        //                reader.Close();
        //                return true;
        //            }
        //        }
        //    }
        //    catch (NpgsqlException ex)
        //    {
        //        MessageBox.Show(ex.Message); return false;
        //    }
        //}

        //public static void UserAdd(string login, string password, string firstName, string lastName)
        //{
        //    NpgsqlConnection sqlConnection = new NpgsqlConnection(Connection.connectionStr);
        //    try
        //    {
        //        sqlConnection.Open();
        //        string sqlQuery = $"Insert into users (Login, Password, FirsName, LastName) Values (@login, @password, @firstName, @lastName)";
        //        //string encrypt_password = Verification.GetSHA512Hash(password);
        //        string encrypt_password = "";
        //        NpgsqlCommand command = new NpgsqlCommand(sqlQuery, sqlConnection);
        //        command.Parameters.AddWithValue("login", login);
        //        command.Parameters.AddWithValue("password", encrypt_password);
        //        command.Parameters.AddWithValue("firstName", firstName);
        //        command.Parameters.AddWithValue("lastName", lastName);

        //        int i = command.ExecuteNonQuery();
        //        if (i == 1) MessageBox.Show("Данные добавлены");
        //    }
        //    catch (NpgsqlException ex)
        //    {
        //        MessageBox.Show(ex.Message + " User Add");
        //    }
        //    sqlConnection.Close();
        //}


        //public void UserUpdate(
        //    string firstName, 
        //    string lastName, 
        //    string patronymic, 
        //    DateTime dataOfBirth, 
        //    string login, 
        //    string address, 
        //    string phone)
        //{
        //    NpgsqlConnection sqlConnection = new NpgsqlConnection(Connection.connectionStr);
        //    try
        //    {
        //        sqlConnection.Open();
        //        string sqlQuery = $"update Users set FirsName = @firstName, LastName = @lastName, Patronymic = @patronymic, DateOfBirthday = @date, Login = @login, Phone = @phone, Adress = @address where UserId = @userId";
        //        NpgsqlCommand command = new NpgsqlCommand(sqlQuery, sqlConnection);
        //        command.Parameters.AddWithValue("@userId", AuthorizationWindow.currentUser.UserId);
        //        command.Parameters.AddWithValue("@firstName", firstName);
        //        command.Parameters.AddWithValue("@lastName", lastName);
        //        command.Parameters.AddWithValue("@patronymic", patronymic);
        //        command.Parameters.AddWithValue("@date", dataOfBirth);
        //        command.Parameters.AddWithValue("@login", login);
        //        command.Parameters.AddWithValue("@phone", phone);
        //        command.Parameters.AddWithValue("@address", address);
        //        int i = command.ExecuteNonQuery();
        //        if (i == 1)
        //        {
        //            MessageBox.Show("Данные обновлены");
        //        }
        //    }
        //    catch (NpgsqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    sqlConnection.Close();

        //}

        //public void PasswordUpdate(string login, string old_password, string new_password, string repeat_password)
        //{
        //    if (new_password != repeat_password)
        //    {
        //        MessageBox.Show("Пароли не совпадают");
        //        return;
        //    }
        //    //string hash_password = Verification.GetSHA512Hash(new_password);
        //    string hash_password = "";
        //    NpgsqlConnection sqlConnection = new NpgsqlConnection(Connection.connectionStr);
        //    try
        //    {
        //        sqlConnection.Open();
        //        string sqlQuery = $"update Users set Password = @password where UserId = @userId";
        //        NpgsqlCommand command = new NpgsqlCommand(sqlQuery, sqlConnection);
        //        command.Parameters.AddWithValue("@userId", AuthorizationWindow.currentUser.UserId);
        //        command.Parameters.AddWithValue("@password", hash_password);
        //        int i = command.ExecuteNonQuery();
        //        if (i == 1)
        //        {
        //            MessageBox.Show("Данные обновлены");
        //        }
        //    }
        //    catch (NpgsqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    sqlConnection.Close();
        //}
    }
}



