﻿using System;
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

            while (rdr.Read())
            {
                tableId = rdr.GetInt32(0);
                tableNum = rdr.GetInt32(1);
            }

            Table foundTable = new Table(tableNum, tableId);

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
            cmd.CommandText = @"INSERT INTO tables (number) VALUES (@number);";

            MySqlParameter number = new MySqlParameter();
            number.ParameterName = "@stylistName";
            number.Value = this.Number;
            cmd.Parameters.Add(number);

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
                Table newTable = new Table(tableNum, tableId);
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

                Ticket newTicket = new Ticket(seatNum, foodId, drinkId, userId, tableId, ticketId);
                tickets.Add(newTicket);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return tickets;
        }
    }
}

