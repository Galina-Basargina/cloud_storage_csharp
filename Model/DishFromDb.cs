using coursework3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows.Controls.Primitives;
using coursework3.Model;

namespace coursework3.Model
{
    internal class DishFromDb
    {
        //public ObservableCollection<Dish> LoadDishes(out int count_dish)
        //{
        //    ObservableCollection<Dish> dishes = new ObservableCollection<Dish>();
        //    count_dish = 0;

        //    NpgsqlConnection sqlConnection = new NpgsqlConnection(Connection.connectionStr);
        //    try
        //    {
        //        sqlConnection.Open();
        //        string sqlExp = $"select d.id,d.dish,t.type,b.name,sum(cd.weight),d.img from "
        //            + $"dishes d join type_dish t on d.type=t.id "
        //            + $"join basics b on d.base = b.id " +
        //            $"join composition_dishes cd on d.id =cd.id " +
        //            $"group by d.id,t.type,b.name;";
        //        NpgsqlCommand command = new NpgsqlCommand(sqlExp, sqlConnection);
        //        NpgsqlDataReader reader = command.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //                dishes.Add(new Dish(
        //                    Convert.ToInt32(reader[0]),
        //                    reader[1].ToString(),
        //                    reader[2].ToString(),
        //                    reader[3].ToString(),
        //                    Convert.ToInt32(reader[4]),
        //                    reader[5].ToString()
        //                    ));
        //        }
        //        reader.Close();
        //        count_dish = dishes.Count;
        //        return dishes;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + " Load Dish");
        //        return dishes;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}

        //public ObservableCollection<Dish> FilterDishByType(int id_type, out int count_dish)
        //{
        //    ObservableCollection<Dish> dishes = new ObservableCollection<Dish>();

        //    NpgsqlConnection sqlConnection = new NpgsqlConnection(Connection.connectionStr);
        //    try
        //    {
        //        sqlConnection.Open();
        //        string sqlExp = $"select d.id, d.dish, t.type, b.name, weight, d.img from "
        //            + $" dishes d join type_dish t on d.type = t.id "
        //            + $" join basics b on d.base = b.id "
        //            + $" where t.id = @id_type;";
        //        NpgsqlCommand command = new NpgsqlCommand(sqlExp, sqlConnection);
        //        command.Parameters.AddWithValue("id_type", id_type);
        //        NpgsqlDataReader reader = command.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //                dishes.Add(new Dish(
        //                    Convert.ToInt32(reader[0]),
        //                    reader[1].ToString(),
        //                    reader[2].ToString(),
        //                    reader[3].ToString(),
        //                    Convert.ToInt32(reader[4]),
        //                    reader[5].ToString()
        //                    ));
        //        }
        //        reader.Close();
        //        return dishes;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + " Load Dish");
        //        return dishes;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //        count_dish = dishes.Count;
        //    }
        //}

        //public void DeleteDish(int dishId)
        //{
        //    using (NpgsqlConnection sqlConnection = new NpgsqlConnection(Connection.connectionStr))
        //    {
        //        try
        //        {
        //            sqlConnection.Open();

        //            // Удаляем связанные записи из composition_dishes
        //            string deleteCompositionSql = "delete from composition_dishes where id = @dishId";
        //            using (NpgsqlCommand compositionCommand = new NpgsqlCommand(deleteCompositionSql, sqlConnection))
        //            {
        //                compositionCommand.Parameters.AddWithValue("@dishId", dishId);
        //                compositionCommand.ExecuteNonQuery();
        //            }

        //            // Удаляем основную запись из dishes
        //            string deleteRecipeSql = "delete from recipes where id = @dishId";
        //            using (NpgsqlCommand dishCommand = new NpgsqlCommand(deleteRecipeSql, sqlConnection))
        //            {
        //                dishCommand.Parameters.AddWithValue("@dishId", dishId);
        //                int rowsAffected = dishCommand.ExecuteNonQuery();
        //            }

        //            // Удаляем основную запись из dishes
        //            string deleteDishSql = "delete from dishes where id = @dishId";
        //            using (NpgsqlCommand dishCommand = new NpgsqlCommand(deleteDishSql, sqlConnection))
        //            {
        //                dishCommand.Parameters.AddWithValue("@dishId", dishId);
        //                int rowsAffected = dishCommand.ExecuteNonQuery();
        //                sqlConnection.Close();
        //                return;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //            sqlConnection.Close();
        //        }
        //    }
        //}

        //public void AddDish(Dish newDish, ObservableCollection<Composition> composition, out int idDish)
        //    {
        //        idDish = -1;
        //        NpgsqlConnection connection = new NpgsqlConnection(Connection.connectionStr);
        //        connection.Open();
        //        NpgsqlTransaction transaction = connection.BeginTransaction();
        //        NpgsqlCommand cmd = connection.CreateCommand();
        //        cmd.Transaction = transaction;
        //        try
        //        {
        //            cmd.CommandText =
        //                "insert into dishes values( " +
        //                "(select max(id)+1 from dishes), " +
        //                "@dish, " +
        //                "(SELECT DISTINCT id from type_dish where \"type\" = @type), " +
        //                "(SELECT DISTINCT id from basics where \"name\" = @base), " +
        //                "@weight, @imgPath) returning id;";
        //            cmd.Parameters.AddWithValue("@dish", newDish.DishName);
        //            cmd.Parameters.AddWithValue("@type", newDish.Type);
        //            cmd.Parameters.AddWithValue("@base", newDish.Base);
        //            cmd.Parameters.AddWithValue("@weight", newDish.Weight);
        //            cmd.Parameters.AddWithValue("@imgPath", newDish.ImageWithPath);

        //            NpgsqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                    idDish = int.Parse(reader[0].ToString());
        //            }
        //            reader.Close();


        //            if (idDish == -1)
        //            {
        //                MessageBox.Show("Не получилось добавить блюдо");
        //                transaction.Rollback();
        //                return;
        //            }
        //            cmd = connection.CreateCommand();
        //            cmd.Transaction = transaction;
        //            for (int i = 0; i < composition.Count; i++)
        //            {
        //                cmd.CommandText = $"insert into composition_dishes values ({idDish},"
        //                    + $"(select id from products_dish where product = '{composition[i].ProductName}'),"
        //                    + $"{composition[i].Weight})";
        //                cmd.ExecuteNonQuery();
        //            }
        //            MessageBox.Show($"Блюдо добавлено");
        //            transaction.Commit();
        //        }
        //        catch (SqlException ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //            transaction.Rollback();
        //        }
        //    }
    }
}