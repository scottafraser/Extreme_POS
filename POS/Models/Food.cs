using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace POS.Models
    {
        public class Food
        {
            public string Name { get; set; }
            public double Price { get; set; }
            public string Category { get; set; }
            public int Id { get; set; }

            public Food(string name, double price, string category, int id = 0)
            {
                Name = name;
                Price = price;
                Category = category;
                Id = id;
            }
            public override bool Equals(System.Object otherFood)
            {
                if (!(otherFood is Food))
                {
                    return false;
                }
                else
                {
                    Food newFood = (Food)otherFood;
                    bool NameEquality = Name == newFood.Name;
                    bool IdEquality = Id == newFood.Id;
                    bool PriceEquality = Price.Equals(newFood.Price);
                    bool CategoryEquality = Category == newFood.Category;
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
                cmd.CommandText = @"INSERT INTO food (name, price, category) VALUES (@Name, @Price, @Category);";

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

            public static List<Food> GetAll()
            {
                List<Food> allFood = new List<Food> { };
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"SELECT * FROM food;";
                var rdr = cmd.ExecuteReader() as MySqlDataReader;
                while (rdr.Read())
                {
                    int FoodId = rdr.GetInt32(0);
                    string FoodName = rdr.GetString(1);
                    double FoodPrice = rdr.GetDouble(2);
                    string FoodCategory = rdr.GetString(3);
                    Food newFood = new Food(FoodName, FoodPrice, FoodCategory, FoodId);
                    allFood.Add(newFood);
                }
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
                return allFood;
            }
            public static Food Find(int id)
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"SELECT * FROM food WHERE id = (@searchId);";

                cmd.Parameters.AddWithValue("@searchId", id);

                var rdr = cmd.ExecuteReader() as MySqlDataReader;
                int FoodId = 0;
                string FoodName = "";
                double FoodPrice = 0;
                string FoodCategory = "";

                while (rdr.Read())
                {
                    FoodId = rdr.GetInt32(0);
                    FoodName = rdr.GetString(1);
                    FoodPrice = rdr.GetDouble(2);
                    FoodCategory = rdr.GetString(3);
                }
                Food newFood = new Food(FoodName, FoodPrice, FoodCategory, FoodId);
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
                return newFood;
            }

            public static void DeleteAll()
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"DELETE FROM food;";
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
                cmd.CommandText = @"UPDATE food SET name = @newName, price = @newPrice, newCategory = @newCategory  WHERE id = @searchId;";

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
                cmd.CommandText = @"DELETE FROM food WHERE Id = @searchId;";

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
