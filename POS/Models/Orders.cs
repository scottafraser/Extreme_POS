using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using POS.Models;

namespace POS.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int Ticket_Id { get; set; }
        public int Food_Id { get; set; }
        public int Drink_Id { get; set; }

        public Orders(int ticket_id, int food_id, int drink_id, int id = 0)
        {
            Id = id;
            Ticket_Id = ticket_id;
            Food_Id = food_id;
            Drink_Id = drink_id;
        }

        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO orders (ticket_id, food_id, drink_id) VALUES (@ticket, @food, @drink);";

            cmd.Parameters.AddWithValue("@ticket", Ticket_Id);
            cmd.Parameters.AddWithValue("@food", Food_Id);
            cmd.Parameters.AddWithValue("@drink", Drink_Id);

            cmd.ExecuteNonQuery();

            Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
