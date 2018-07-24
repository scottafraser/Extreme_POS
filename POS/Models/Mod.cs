using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace POS.Models
{
    public class Mod
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public int Id { get; set; }

        public Mod(string name, int price, string category, int id = 0)
        {
            Name = name;
            Price = price;
            Category = category;
            Id = id;
        }
        public override bool Equals(System.Object otherMod)
        {
            if (!(otherMod is Mod))
            {
                return false;
            }
            else
            {
                Mod newMod = (Mod)otherMod;
                bool NameEquality = Name == newMod.Name;
                bool IdEquality = Id == newMod.Id;
                bool PriceEquality = Price == newMod.Price;
                bool CategoryEquality = Category == newMod.Category;
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
            cmd.CommandText = @"INSERT INTO mod (name, price, category) VALUES (@Name, @Price, @Category);";

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

        public static List<Mod> GetAll()
        {
            List<Mod> allMods = new List<Mod> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM mod;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int ModId = rdr.GetInt32(0);
                string ModName = rdr.GetString(1);
                int ModPrice = rdr.GetInt32(2);
                string ModCategory = rdr.GetString(3);
                Mod newMod = new Mod(ModName, ModPrice, ModCategory, ModId);
                allMods.Add(newMod);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allMods;
        }
        public static Mod Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM mod WHERE id = (@searchId);";

            cmd.Parameters.AddWithValue("@searchId", id);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int ModId = 0;
            string ModName = "";
            int ModPrice = 0;
            string ModCategory = "";

            while (rdr.Read())
            {
                ModId = rdr.GetInt32(0);
                ModName = rdr.GetString(1);
                ModPrice = rdr.GetInt32(2);
                ModCategory = rdr.GetString(3);
            }
            Mod newMod = new Mod(ModName, ModPrice, ModCategory, ModId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newMod;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM mod;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string newName, int newPrice, string newCategory)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE mod SET name = @newName, price = @newPrice, newCategory = @newCategory  WHERE id = @searchId;";

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
            cmd.CommandText = @"DELETE FROM mod WHERE Id = @searchId;";

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
