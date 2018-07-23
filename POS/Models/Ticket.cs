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
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int stylistId = 0;
            int stylistExp = 0;
            string stylistName = "";

            while (rdr.Read())
            {
                stylistId = rdr.GetInt32(0);
                stylistName = rdr.GetString(1);
                stylistExp = rdr.GetInt32(2);
            }

            Stylist foundStylist = new Stylist(stylistName, stylistExp, stylistId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundStylist;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name, experience) VALUES (@stylistName, @stylistExp);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@stylistName";
            name.Value = this.Name;

            MySqlParameter experience = new MySqlParameter();
            experience.ParameterName = "@stylistExp";
            experience.Value = this.Experience;

            cmd.Parameters.Add(name);
            cmd.Parameters.Add(experience);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                int stylistExp = rdr.GetInt32(2);
                Stylist newStylist = new Stylist(stylistName, stylistExp, stylistId);
                allStylists.Add(newStylist);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allStylists;
        }

        public void AddSpecialty(Specialty newSpecialty)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialties (specialty_id, stylist_id) VALUES (@SpecialtyId, @StylistId);";

            MySqlParameter specialty_id = new MySqlParameter();
            specialty_id.ParameterName = "@SpecialtyId";
            specialty_id.Value = newSpecialty.Id;
            cmd.Parameters.Add(specialty_id);

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@StylistId";
            stylist_id.Value = Id;
            cmd.Parameters.Add(stylist_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Specialty> GetSpecialties()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT specialties.* FROM stylists
                                JOIN stylists_specialties ON (stylists.id = stylists_specialties.stylist_id)
                                JOIN specialties ON (stylists_specialties.specialty_id = specialties.id)
                                WHERE stylists.id = @StylistId;";

            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@StylistId";
            stylistIdParameter.Value = Id;
            cmd.Parameters.Add(stylistIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Specialty> specialties = new List<Specialty> { };

            while (rdr.Read())
            {
                int specialtyId = rdr.GetInt32(0);
                string specialtyName = rdr.GetString(1);

                Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
                specialties.Add(newSpecialty);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return specialties;
        }

        public void AddClient(Client newClient)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_clients (client_id, stylist_id) VALUES (@ClientId, @StylistId);";

            MySqlParameter client_id = new MySqlParameter();
            client_id.ParameterName = "@ClientId";
            client_id.Value = newClient.Id;
            cmd.Parameters.Add(client_id);

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@StylistId";
            stylist_id.Value = Id;
            cmd.Parameters.Add(stylist_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Client> GetClients()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT clients.* FROM stylists
                                JOIN stylists_clients ON (stylists.id = stylists_clients.stylist_id)
                                JOIN clients ON (stylists_clients.client_id = clients.id)
                                WHERE stylists.id = @StylistId;";

            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@StylistId";
            stylistIdParameter.Value = Id;
            cmd.Parameters.Add(stylistIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Client> clients = new List<Client> { };

            while (rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);

                Client newClient = new Client(clientName, clientId);
                clients.Add(newClient);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return clients;
        }

        public List<Client> GetAllClients()
        {
            return Client.GetAll();
        }
    }
}
