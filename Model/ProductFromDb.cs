using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows;
using coursework3.Model;

namespace coursework3.Model
{
    public class ProductFromDb
    {
        //public List<Product> GetProducts()
        //{
        //    List<Product> products = new List<Product>();
        //    NpgsqlConnection connection = new NpgsqlConnection(Connection.connectionStr);
        //    try
        //    {
        //        connection.Open();
        //        string sqlQuery = "SELECT id, product, proteins, fats, carbohydrates FROM products_dish ORDER BY id ASC; ";

        //        NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection);
        //        NpgsqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            products.Add(new Product((int)reader[0], reader[1].ToString(), (int)reader[2], (int)reader[3], (int)reader[4]));
        //        }
        //        reader.Close();
        //    }
        //    catch (SqlException ex)
        //    { MessageBox.Show(ex.Message); }

        //    connection.Close();
        //    return products;
        //}

        //public List<String> GetProductsName()
        //{
        //    List<String> products = new List<String>();
        //    NpgsqlConnection connection = new NpgsqlConnection(Connection.connectionStr);
        //    try
        //    {
        //        connection.Open();
        //        string sqlQuery = "SELECT product FROM products_dish ORDER BY id ASC; ";

        //        NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection);
        //        NpgsqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            products.Add(reader[0].ToString());
        //        }
        //        reader.Close();
        //    }
        //    catch (SqlException ex)
        //    { MessageBox.Show(ex.Message); }

        //    connection.Close();
        //    return products;
        //}
    }
}