using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Malshinon
{
    internal class SubscribeBack
    {
        public bool AddUserInDb(string name, string pw)
        {
            string connectionstring = "server=localhost; user=root; database=malshinon; port=3306;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionstring))
                {
                    conn.Open();

                    string query = "INSERT INTO malshinon.users (Name,Password) VALUES (@name,@password)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@password", pw);

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR : {ex.Message}");
                return false;
            }
        }





        public void ShowUserId(string name)
        {
            string connectionstring = "server=localhost; user=root; database=malshinon; port=3306;";
            object result;
            int resultcheck = -1;
            bool checkUpdateID = false;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionstring))
                {
                    conn.Open();
                    string query = "SELECT id FROM users WHERE name = @name";

                    while (!checkUpdateID)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                resultcheck = Convert.ToInt32(result);
                                checkUpdateID = true;
                            }

                           
                        }
                    }

                    Console.WriteLine($"---- YOUR ID IS :  {resultcheck} ----- \n ");
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR : {ex.Message}");
            }
        }
    }
}
