using MySql.Data.MySqlClient;
using System;
using System.Diagnostics.Metrics;

namespace Malshinon
{
    internal class ReportBack
    {
        public bool AddReportInDb(string nameid, DateTime incidenttime, string reporttext)
        {
            int reportlen = reporttext.Length;
            string idreporter = ConnectionFront.GetId();
            string connectionstring = "server=localhost; user=root; database=malshinon; port=3306;";

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // 🔹 1. Insertion dans reports
                    string queryinsert = @"
                        INSERT INTO malshinon.reports 
                        (target, txt, incidentime, numberchar, reporterid) 
                        VALUES (@nameid, @reporttext, @incidenttime, @reportlen, @idreporter)";

                    using (MySqlCommand cmdInsert = new MySqlCommand(queryinsert, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@nameid", nameid);
                        cmdInsert.Parameters.AddWithValue("@reporttext", reporttext);
                        cmdInsert.Parameters.AddWithValue("@incidenttime", incidenttime);
                        cmdInsert.Parameters.AddWithValue("@reportlen", reportlen);
                        cmdInsert.Parameters.AddWithValue("@idreporter", idreporter);
                        cmdInsert.ExecuteNonQuery();
                    }

                    UpdateTarget(nameid);
                    UpdateUser();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR : {ex.Message}");
                    return false;
                }
            }
        }




        public void UpdateTarget(string nameid)
        {

            string connectionstring = "server=localhost; user=root; database=malshinon; port=3306;";

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {

                conn.Open();
                string queryCheckTarget = "SELECT COUNT(*) FROM malshinon.targets WHERE name = @nameid";

                using (MySqlCommand cmdCheck = new MySqlCommand(queryCheckTarget, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@nameid", nameid);
                    object result = cmdCheck.ExecuteScalar();

                    int res = 0;

                    if (result != null)
                    {
                        res = Convert.ToInt32(result);

                        if (res > 0)
                        {
                            string queryIncrement = "UPDATE malshinon.targets SET reportnumber = reportnumber + 1 WHERE name = @nameid";
                            using (MySqlCommand cmdIncrement = new MySqlCommand(queryIncrement, conn))
                            {
                                cmdIncrement.Parameters.AddWithValue("@nameid", nameid);
                                cmdIncrement.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string queryInsertTarget = "INSERT INTO malshinon.targets (name, reportnumber) VALUES (@nameid, 1)";
                            using (MySqlCommand cmdInsertTarget = new MySqlCommand(queryInsertTarget, conn))
                            {
                                cmdInsertTarget.Parameters.AddWithValue("@nameid", nameid);
                                cmdInsertTarget.ExecuteNonQuery();
                            }
                        }
                    }














                }
            }

        }

        public void UpdateUser()
        {
            string userid = ConnectionFront.GetId();

            string connectionstring = "server=localhost; user=root; database=malshinon; port=3306;";

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();

                // 1re requête : incrément du nombre de signalements
                string query1 = "UPDATE malshinon.users SET reportnumbers = reportnumbers + 1 WHERE id = @userid;";

                // 2e requête : mise à jour de la qualité moyenne basée sur numberchar
                string query2 = @"UPDATE malshinon.users SET quality100char = (SELECT AVG(numberchar) FROM malshinon.reports WHERE reporterid = @userid) WHERE id = @userid;";

                using (MySqlCommand cmd1 = new MySqlCommand(query1, conn))
                using (MySqlCommand cmd2 = new MySqlCommand(query2, conn))
                {
                    cmd1.Parameters.AddWithValue("@userid", userid);
                    cmd2.Parameters.AddWithValue("@userid", userid);

                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }
            }
        }
    }
}
