using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using POS;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class Ticket
    {
        public int SeatNumber { get; set; }
        public int Food_Id { get; set; }
        public int Drink_Id { get; set; }
        public int User_Id { get; set; }
        public int Table_Id { get; set; }
        public int Id { get; set; }

        public Ticket(int seatNumber, int food_id, int drink_id, int user_id, int table_id, int id = 0)
        {
            SeatNumber = seatNumber;
            Food_Id = food_id;
            Drink_Id = drink_id;
            User_Id = user_id;
            Table_Id = table_id;
            Id = id;
        }

        public override bool Equals(System.Object otherTicket)
        {
            if (!(otherTicket is Ticket))
            {
                return false;
            }
            else
            {
                Ticket newTicket = (Ticket)otherTicket;
                bool seatEquality = (this.SeatNumber == newTicket.SeatNumber);
                return (seatEquality);
            }
        }

        /*
        public override int GetHashCode()
        {
            return this..GetHashCode();
        }
        */

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM tickets;";
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
            cmd.CommandText = @"DELETE FROM tickets WHERE id = @searchId;";

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

        public static void RemoveLineItem(Ticket existingTicket)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM tickets WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = existingTicket.Id;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(int newSeat, int newFoodId, int newDrinkId, int newUserId, int newTableId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE tickets SET seat_number = @newSeat WHERE id = @searchId;
                                UPDATE tickets SET food_id = @newFood WHERE id = @searchId;
                                UPDATE tickets SET drink_id = @newDrink WHERE id = @searchId;
                                UPDATE tickets SET user_id = @newUser WHERE id = @searchId;
                                UPDATE tickets SET table_id = @newTable WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            MySqlParameter seat = new MySqlParameter();
            seat.ParameterName = "@newSeat";
            seat.Value = newSeat;
            cmd.Parameters.Add(seat);

            MySqlParameter food = new MySqlParameter();
            food.ParameterName = "@newFood";
            food.Value = newFoodId;
            cmd.Parameters.Add(food);

            MySqlParameter drink = new MySqlParameter();
            drink.ParameterName = "@newDrink";
            drink.Value = newDrinkId;
            cmd.Parameters.Add(drink);

            MySqlParameter user = new MySqlParameter();
            user.ParameterName = "@newUser";
            user.Value = newUserId;
            cmd.Parameters.Add(user);

            MySqlParameter table = new MySqlParameter();
            table.ParameterName = "@newTable";
            table.Value = newTableId;
            cmd.Parameters.Add(table);

            cmd.ExecuteNonQuery();
            this.SeatNumber = newSeat;
            this.Food_Id = newFoodId;
            this.Drink_Id = newDrinkId;
            this.User_Id = newUserId;
            this.Table_Id = newTableId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Ticket Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tickets WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int ticketId = 0;
            int seatNum = 0;
            int foodId = 0;
            int drinkId = 0;
            int userId = 0;
            int tableId = 0;

            while (rdr.Read())
            {
                ticketId = rdr.GetInt32(0);
                seatNum = rdr.GetInt32(1);
                foodId = rdr.GetInt32(2);
                drinkId = rdr.GetInt32(3);
                userId = rdr.GetInt32(4);
                tableId = rdr.GetInt32(5);
            }

            Ticket foundTicket = new Ticket(seatNum, foodId, drinkId, userId, tableId, ticketId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundTicket;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO tickets (seat_number, food_id, drink_id, user_id, table_id) VALUES (@Seat, @Food, @Drink, @User, @Table);";

            MySqlParameter seat = new MySqlParameter();
            seat.ParameterName = "@Seat";
            seat.Value = this.SeatNumber;
            cmd.Parameters.Add(seat);

            MySqlParameter food = new MySqlParameter();
            food.ParameterName = "@Food";
            food.Value = this.Food_Id;
            cmd.Parameters.Add(food);

            MySqlParameter drink = new MySqlParameter();
            drink.ParameterName = "@Drink";
            drink.Value = this.Drink_Id;
            cmd.Parameters.Add(drink);

            MySqlParameter user = new MySqlParameter();
            user.ParameterName = "@User";
            user.Value = this.User_Id;
            cmd.Parameters.Add(user);

            MySqlParameter table = new MySqlParameter();
            table.ParameterName = "@Table";
            table.Value = this.Table_Id;
            cmd.Parameters.Add(table);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Ticket> GetAll()
        {
            List<Ticket> allTickets = new List<Ticket> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tickets;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int ticketId = rdr.GetInt32(0);
                int seatNum = rdr.GetInt32(1);
                int foodId = rdr.GetInt32(2);
                int drinkId = rdr.GetInt32(3);
                int userId = rdr.GetInt32(4);
                int tableId = rdr.GetInt32(5);
                Ticket newTicket = new Ticket(seatNum, foodId, drinkId, userId, tableId, ticketId);
                allTickets.Add(newTicket);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allTickets;
        }

        public User GetUser()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT user_id from tickets WHERE id = @searchId;";

            MySqlParameter ticketId = new MySqlParameter();
            ticketId.ParameterName = "@searchId";
            ticketId.Value = Id;
            cmd.Parameters.Add(ticketId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            User existingUser = null;

            while (rdr.Read())
            {
                int userId = rdr.GetInt32(0);

                existingUser = User.Find(userId);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return existingUser;
        }

        public Table GetTable()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT table_id from tickets WHERE id = @searchId;";

            MySqlParameter ticketId = new MySqlParameter();
            ticketId.ParameterName = "@searchId";
            ticketId.Value = Id;
            cmd.Parameters.Add(ticketId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            Table existingTable = null;

            while (rdr.Read())
            {
                int tableId = rdr.GetInt32(0);

                existingTable = Table.Find(tableId);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return existingTable;
        }
    }
}
