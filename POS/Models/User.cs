using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using POS;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class User
    {
        public string Name { get; set; }
        public bool Admin { get; set; }
        public int Id { get; set; }

        public User(string name, bool admin = false, int id = 0)
        {
            Name = name;
            Admin = admin;
            Id = id;
        }

        public override bool Equals(System.Object otherUser)
        {
            if (!(otherUser is User))
            {
                return false;
            }
            else
            {
                User newUser = (User)otherUser;
                bool nameEquality = (this.Name == newUser.Name);
                return (nameEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM users; DELETE FROM tickets;";
            cmd.ExecuteNonQuery();
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
            cmd.CommandText = @"DELETE FROM users WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void CloseTicket(Ticket existingTicket)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM tickets WHERE user_id = @searchId AND table_id = @TableId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            MySqlParameter table_id = new MySqlParameter();
            table_id.ParameterName = "@TableId";
            table_id.Value = existingTicket.Id;
            cmd.Parameters.Add(table_id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string newName, bool adminRights)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE users SET name = @newName WHERE id = @searchId; UPDATE users SET admin = @newRights WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            MySqlParameter rights = new MySqlParameter();
            rights.ParameterName = "@newRights";
            rights.Value = adminRights;
            cmd.Parameters.Add(rights);

            cmd.ExecuteNonQuery();
            this.Name = newName;
            this.Admin = adminRights;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static User Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM users WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int userId = 0;
            bool userAdmin = false;
            string userName = "";

            while (rdr.Read())
            {
                userId = rdr.GetInt32(0);
                userName = rdr.GetString(1);
                userAdmin = rdr.GetBoolean(2);
            }

            User foundUser = new User(userName, userAdmin, userId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundUser;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO users (name, admin) VALUES (@userName, @userAdmin);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@userName";
            name.Value = this.Name;

            MySqlParameter admin = new MySqlParameter();
            admin.ParameterName = "@userAdmin";
            admin.Value = this.Admin;

            cmd.Parameters.Add(name);
            cmd.Parameters.Add(admin);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<User> GetAll()
        {
            List<User> allUsers = new List<User> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM users;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int userId = rdr.GetInt32(0);
                string userName = rdr.GetString(1);
                bool userAdmin = rdr.GetBoolean(2);
                User newUser = new User(userName, userAdmin, userId);
                allUsers.Add(newUser);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allUsers;
        }

        /*
        public List<Ticket> GetTickets()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT tickets WHERE user_id = @userId;";

            MySqlParameter userIdParameter = new MySqlParameter();
            userIdParameter.ParameterName = "@userId";
            userIdParameter.Value = Id;
            cmd.Parameters.Add(userIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Ticket> tickets = new List<Ticket> { };

            while (rdr.Read())
            {
                int ticketId = rdr.GetInt32(0);
                int seatNum = rdr.GetInt32(1);
                int foodId = rdr.GetInt32(2);
                int drinkId = rdr.GetInt32(3);
                int userId = rdr.GetInt32(4);
                int tableId = rdr.GetInt32(5);

                Food newFood = Food.Find(foodId);
                Drink newDrink = Drink.Find(drinkId);
                User newUser = User.Find(userId);
                Table newTable = Table.Find(tableId);

                Ticket newTicket = new Ticket(seatNum, newFood, newDrink, newUser, newTable, ticketId);
                tickets.Add(newTicket);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return tickets;
        }
        */

    }
}
