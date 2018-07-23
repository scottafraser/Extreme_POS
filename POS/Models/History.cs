using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using POS;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class History
    {
        public int Table_Id { get; set; }
        public int Id { get; set; }

        public History(int table_id, int id = 0)
        {
            Table_Id = table_id;
            Id = id;
        }

        public override bool Equals(System.Object otherHistory)
        {
            if (!(otherHistory is History))
            {
                return false;
            }
            else
            {
                History newHistory = (History)otherHistory;
                bool historyId = (this.Id == newHistory.Id);
                bool tableId = (this.Table_Id == newHistory.Table_Id);
                return (historyId && tableId);
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
            cmd.CommandText = @"DELETE FROM history;";
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
            cmd.CommandText = @"DELETE FROM history WHERE id = @searchId;";

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

        public void Edit(int newTableId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE history SET table_id = @newTableId WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            MySqlParameter newTable_Id = new MySqlParameter();
            newTable_Id.ParameterName = "@newTableId";
            newTable_Id.Value = newTableId;
            cmd.Parameters.Add(newTable_Id);

            cmd.ExecuteNonQuery();
            this.Table_Id = newTableId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static History Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM history WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int historyId = 0;
            int tableId = 0;

            while (rdr.Read())
            {
                historyId = rdr.GetInt32(0);
                tableId = rdr.GetInt32(1);
            }

            History foundHistory = new History(tableId, historyId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundHistory;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO history (table_id) VALUES (@tableId);";

            MySqlParameter tableId = new MySqlParameter();
            tableId.ParameterName = "@tableId";
            tableId.Value = this.Table_Id;
            cmd.Parameters.Add(tableId);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<History> GetAll()
        {
            List<History> allHistory = new List<History> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM history;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int historyId = rdr.GetInt32(0);
                int tableId = rdr.GetInt32(1);

                History newHistory = new History(tableId, historyId);
                allHistory.Add(newHistory);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allHistory;
        }
    }
}
