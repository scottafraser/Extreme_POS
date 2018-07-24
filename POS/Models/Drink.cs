using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace POS.Models
{
    public class Drink
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public int Id { get; set; }

        public Drink(string name, double price, string category, int id = 0)
        {
            Name = name;
            Price = price;
            Category = category;
            Id = id;
        }
        public override bool Equals(System.Object otherDrink)
        {
            if (!(otherDrink is Drink))
            {
                return false;
            }
            else
            {
                Drink newDrink = (Drink)otherDrink;
                bool NameEquality = Name == newDrink.Name;
                bool IdEquality = Id == newDrink.Id;
                bool PriceEquality = Price == newDrink.Price;
                bool CategoryEquality = Category == newDrink.Category;
                return (IdEquality && NameEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO drink (name, price, category) VALUES (@Name, @Price, @Category);";

            cmd.Parameters.AddWithValue("@Name", this.Name);
            cmd.Parameters.AddWithValue("@Price", this.Name);
            cmd.Parameters.AddWithValue("@Category", this.Name);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }

        public static List<Drink> GetAll()
        {
            List<Drink> allDrinks = new List<Drink> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM drink;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int DrinkId = rdr.GetInt32(0);
                string DrinkName = rdr.GetString(1);
                double DrinkPrice = rdr.GetDouble(2);
                string DrinkCategory = rdr.GetString(3);
                Drink newDrink = new Drink(DrinkName, DrinkPrice, DrinkCategory, DrinkId);
                allDrinks.Add(newDrink);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allDrinks;
        }
        public static Drink Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM drink WHERE id = (@searchId);";

            cmd.Parameters.AddWithValue("@searchId", id);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int DrinkId = 0;
            string DrinkName = "";
            double DrinkPrice = 0;
            string DrinkCategory = "";

            while (rdr.Read())
            {
                DrinkId = rdr.GetInt32(0);
                DrinkName = rdr.GetString(1);
                DrinkPrice = rdr.GetDouble(2);
                DrinkCategory = rdr.GetString(3);
            }
            Drink newDrink = new Drink(DrinkName, DrinkPrice, DrinkCategory, DrinkId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newDrink;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM drink;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string newName, double newPrice, string newCategory)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE drink SET name = @newName, price = @newPrice, newCategory = @newCategory  WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            cmd.Parameters.AddWithValue("@newName", newName);
            cmd.Parameters.AddWithValue("@newPrice", newPrice);
            cmd.Parameters.AddWithValue("@newCategory", newCategory);

            cmd.ExecuteNonQuery();

            Name = newName;
            Price = newPrice;
            Category = newCategory;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM drink WHERE Id = @searchId;";

            cmd.Parameters.AddWithValue("@searchId", this.Id);

            cmd.ExecuteNonQuery();


            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

    }
}
