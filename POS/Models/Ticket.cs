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
        public int TicketNumber { get; set; }
        public Food Food_Id { get; set; }
        public Drink Drink_Id { get; set; }
        public User User_Id { get; set; }
        public int Table_Id { get; set; }
        public int Id { get; set; }

        public Ticket(int ticketNumber, int id = 0)
        {
            TicketNumber = ticketNumber;
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
                bool ticketEquality = (this.TicketNumber == newTicket.TicketNumber);
                return (ticketEquality);
            }
        }

        public void AddFood(Food newFood)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE tickets SET food_id = @FoodId WHERE id = @searchId;";

            MySqlParameter food_id = new MySqlParameter();
            food_id.ParameterName = "@FoodId";
            food_id.Value = newFood.Id;
            cmd.Parameters.Add(food_id);

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

        public void AddDrink(Drink newDrink)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE tickets SET drink_id = @DrinkId WHERE id = @searchId;";

            MySqlParameter drink_id = new MySqlParameter();
            drink_id.ParameterName = "@DrinkId";
            drink_id.Value = newDrink.Id;
            cmd.Parameters.Add(drink_id);

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

        public void AddUser(User newUser)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE tickets SET user_id = @UserId WHERE id = @searchId;";

            MySqlParameter user_id = new MySqlParameter();
            user_id.ParameterName = "@UserId";
            user_id.Value = newUser.Id;
            cmd.Parameters.Add(user_id);



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

        public void AddTable(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE tickets SET table_id = @TableId WHERE id = @searchId;";;

            MySqlParameter table_id = new MySqlParameter();
            table_id.ParameterName = "@TableId";
            table_id.Value = id;
            cmd.Parameters.Add(table_id);

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

            Table_Id = id;
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

        public static void CloseTicket(Ticket existingTicket)
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

        public static int GenerateTicketNumber() 
        {
            Random randomNum = new Random();
            int ticketNumber = randomNum.Next(200, 999);

            return ticketNumber;
        }

        public void Edit(int newTicket, int newFoodId, int newDrinkId, int newUserId, int newTableId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE tickets SET ticket_number = @newTicket WHERE id = @searchId;
                                UPDATE tickets SET food_id = @newFood WHERE id = @searchId;
                                UPDATE tickets SET drink_id = @newDrink WHERE id = @searchId;
                                UPDATE tickets SET user_id = @newUser WHERE id = @searchId;
                                UPDATE tickets SET table_id = @newTable WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            MySqlParameter ticket = new MySqlParameter();
            ticket.ParameterName = "@newTicket";
            ticket.Value = newTicket;
            cmd.Parameters.Add(ticket);

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
            this.TicketNumber = newTicket;
            this.Food_Id = Food.Find(newFoodId);
            this.Drink_Id = Drink.Find(newDrinkId);
            this.User_Id = User.Find(newUserId);
            this.Table_Id = newTableId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
            
        //public static Ticket Find(int id)
        //{
        //    MySqlConnection conn = DB.Connection();
        //    conn.Open();

        //    var cmd = conn.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"SELECT * FROM tickets WHERE id = @thisId;";

        //    MySqlParameter thisId = new MySqlParameter();
        //    thisId.ParameterName = "@thisId";
        //    thisId.Value = id;
        //    cmd.Parameters.Add(thisId);

        //    var rdr = cmd.ExecuteReader() as MySqlDataReader;

        //    int ticketId = 0;
        //    int ticketNum = 0;
        //    int userId = 0;
        //    int tableId = 0;

        //    Food newFood = null;
        //    Table newTable = null;


        //    while (rdr.Read())
        //    {
        //        ticketId = rdr.GetInt32(0);
        //        ticketNum = rdr.GetInt32(1);
        //        userId = rdr.GetInt32(3);
        //        tableId = rdr.GetInt32(4);
        //        active = rdr.GetString(5);


        //        newUser = User.Find(userId);
        //        newTable = Table.Find(tableId);
        //    }

        //    Ticket foundTicket = new Ticket(ticketNum);
        //    foundTicket.Food_Id = newFood;
        //    foundTicket.Drink_Id = newDrink;
        //    foundTicket.User_Id = newUser;
        //    foundTicket.Table_Id = newTable;

        //    conn.Close();
        //    if (conn != null)
        //    {
        //        conn.Dispose();
        //    }

        //    return foundTicket;
        //}


        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO tickets (ticket_number) VALUES (@TicketNumber);"; 

            MySqlParameter ticket_number = new MySqlParameter();
            ticket_number.ParameterName = "@TicketNumber";
            ticket_number.Value = this.TicketNumber;
            cmd.Parameters.Add(ticket_number);

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
                int ticketNum = rdr.GetInt32(1);
                int userId = rdr.GetInt32(2);
                int tableId = rdr.GetInt32(3);
                string active = rdr.GetString(4);

                Ticket newTicket = new Ticket(ticketNum, ticketId);


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

        public List<Food> GetFood()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT food_id from tickets WHERE id = @searchId;";

            MySqlParameter ticketId = new MySqlParameter();
            ticketId.ParameterName = "@searchId";
            ticketId.Value = Id;
            cmd.Parameters.Add(ticketId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Food> existingFood = new List<Food> { };

            while (rdr.Read())
            {
                int foodId = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                double price = rdr.GetDouble(2);
                string category = rdr.GetString(3);

                Food newFood = new Food(name, price, category, foodId);
                existingFood.Add(newFood);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return existingFood;
        }

        public List<Drink> GetDrinks()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT drink_id from tickets WHERE id = @searchId;";

            MySqlParameter ticketId = new MySqlParameter();
            ticketId.ParameterName = "@searchId";
            ticketId.Value = Id;
            cmd.Parameters.Add(ticketId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Drink> existingDrinks = new List<Drink> { };

            while (rdr.Read())
            {
                int drinkId = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                double price = rdr.GetDouble(2);
                string category = rdr.GetString(3);

                Drink newDrink = new Drink(name, price, category, drinkId);
                existingDrinks.Add(newDrink);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return existingDrinks;
        }

        public List<Food> GetFoodOrder()
        {

            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @" SELECT food.* FROM tickets
                JOIN orders ON(tickets.id = orders.ticket_id)
                JOIN food ON(orders.food_id = food.id)
                WHERE tickets.id = @ticketId";

            cmd.Parameters.AddWithValue("@ticketId", this.Id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Food> orderedFood = new List<Food> { };

            while (rdr.Read())
            {

                string name = rdr.GetString(1);
                double price = rdr.GetDouble(2);
                string category = rdr.GetString(3);
                int id = rdr.GetInt32(0);

                Food newFood = new Food(name, price, category, id);
                orderedFood.Add(newFood);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return orderedFood;
        }

        public List<Drink> GetDrinkOrder()
        {

            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT drinks.* FROM tickets
            JOIN orders ON(tickets.id = orders.ticket_id)
            JOIN drinks ON(orders.drink_id = drinks.id)
            WHERE tickets.id = 2 = @ticketId";

            cmd.Parameters.AddWithValue("@ticketId", this.Id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Drink> orderedDrink = new List<Drink> { };

            while (rdr.Read())
            {

                string name = rdr.GetString(1);
                double price = rdr.GetDouble(2);
                string category = rdr.GetString(3);
                int id = rdr.GetInt32(0);

                Drink newDrink = new Drink(name, price, category, id);
                orderedDrink.Add(newDrink);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return orderedDrink;
        }
    }
}
