using MySql.Data.MySqlClient;
using System;
using System.Threading;

namespace Malshinon
{
    public class ConnectionBack
    {
        public bool CheckIfIdInDb(string id)
        {   
            string connectionString =
                "server=localhost;" +
                "user=root;" +
                "database=malshinon;" +
                "port=3306;";

            if (!int.TryParse(id, out int intid))// plus tard onverra pour accepter les noms aussi
            {
                Console.WriteLine("ONLY NUMBERS ARE AVAILABLE!");
                return false;

            }


            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                try
                {
                    conn.Open();



                    string querystring = "SELECT COUNT(*) FROM malshinon.Users WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(querystring, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", intid);

                        int result = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();

                        if (result > 0)
                        {
                            
                            return true;
                        }
                        else
                        {
                            
                            return false;

                        }

                       

                    }


                }

                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR : {ex.Message}");
                    Thread.Sleep(2000);
                    return false;
                }


                
            }
        }


        public bool CheckIfPwInDb(string id ,string pw)
        {
            string connectionString =
                "server=localhost;" +
                "user=root;" +
                "database=malshinon;" +
                "port=3306;";


            int.TryParse(id, out int intid);
            //{
            //    Console.WriteLine("ONLY NUMBERS ARE AVAILABLE!");
            //    return false;

            //}


            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {

                    conn.Open();


                    string query = "SELECT password FROM Users WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", intid);


                        string result = Convert.ToString(cmd.ExecuteScalar());
                        conn.Close();

                        if (pw != result)
                        {
                            return false;
                        }


                        return true;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
                
                



            }
        }



    }

    
}

