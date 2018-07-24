using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using POS;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class Table
    {
        public int Number { get; set; }
        public int Id { get; set; }
        public int Seats { get; set; }

        public Table(int number, int seats, int id = 0)
        {
            Number = number;
            Id = id;
            Seats = seats;
        }

        public override bool Equals(System.Object otherTable)
        {
            if (!(otherTable is Table))
            {
                return false;
            }
            else
            {
                Table newTable = (Table)otherTable;
                bool numberEquality = (this.Number == newTable.Number);
                return (numberEquality);
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
            cmd.CommandText = @"DELETE FROM tables;";
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
            cmd.CommandText = @"DELETE FROM tables WHERE id = @searchId;";

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

        public void Edit(int newNumber, int newSeats)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE tables SET number = @newNumber WHERE id = @searchId; UPDATE tables SET seats = @newSeats WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            MySqlParameter number = new MySqlParameter();
            number.ParameterName = "@newNumber";
            number.Value = newNumber;
            cmd.Parameters.Add(number);

            MySqlParameter seats = new MySqlParameter();
            seats.ParameterName = "@newSeats";
            seats.Value = newSeats;
            cmd.Parameters.Add(seats);

            cmd.ExecuteNonQuery();
            this.Number = newNumber;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Table Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tables WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int tableId = 0;
            int tableNum = 0;
            int tableSeats = 0;

            while (rdr.Read())
            {
                tableId = rdr.GetInt32(0);
                tableNum = rdr.GetInt32(1);
                tableSeats = rdr.GetInt32(2);
            }

            Table foundTable = new Table(tableNum, tableSeats, tableId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundTable;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO tables (number, seats) VALUES (@number, @seats);";

            MySqlParameter number = new MySqlParameter();
            number.ParameterName = "@number";
            number.Value = this.Number;
            cmd.Parameters.Add(number);

            MySqlParameter seats = new MySqlParameter();
            seats.ParameterName = "@seats";
            seats.Value = this.Seats;
            cmd.Parameters.Add(seats);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Table> GetAll()
        {
            List<Table> allTables = new List<Table> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tables;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int tableId = rdr.GetInt32(0);
                int tableNum = rdr.GetInt32(1);
                int tableSeats = rdr.GetInt32(2);
                Table newTable = new Table(tableNum, tableSeats, tableId);
                allTables.Add(newTable);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allTables;
        }

        public List<Ticket> GetTickets()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tickets WHERE table_id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

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

        public int GetRemainingSeats()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT seats FROM tables WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int remainingSeats = 0;

            while (rdr.Read())
            {
                remainingSeats = rdr.GetInt32(0);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return remainingSeats;
        }

        public double TableValue()
        {
            double total = 0.00;

            foreach(Ticket ticket in GetTickets())
            {
                total += ticket.Food_Id.Price;
                total += ticket.Drink_Id.Price;
            }

            return total;
        }

        public int TableItems()
        {
            int items = 0;

            foreach (Ticket ticket in GetTickets())
            {
                if(ticket.Food_Id != null)
                {
                    items++;
                }

                if (ticket.Drink_Id != null)
                {
                    items++;
                }
            }

            return items;
        }

        public void SellTable()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO history (table_id, items, value) VALUES (@table, @items, @value);";

            MySqlParameter table = new MySqlParameter();
            table.ParameterName = "@table";
            table.Value = Id;
            cmd.Parameters.Add(table);

            MySqlParameter items = new MySqlParameter();
            items.ParameterName = "@items";
            items.Value = TableItems();
            cmd.Parameters.Add(items);

            MySqlParameter value = new MySqlParameter();
            value.ParameterName = "@value";
            value.Value = TableValue();
            cmd.Parameters.Add(value);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            foreach (Ticket ticket in GetTickets())
            {
                Ticket.CloseTicket(ticket);
            }
        }
    }
}

