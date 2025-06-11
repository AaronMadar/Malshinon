using System;
using System.Threading;

namespace Malshinon
{
    internal class ReportFront
    {
        string nameid;
        DateTime incidenttime;
        string reporttext;

        public void TheReportPage()
        {
            Console.WriteLine("-----  CREATE A NEW REPORT  ----- \n");

            Console.Write("NAME/ID OF THE TARGET: ");
            nameid = Console.ReadLine();

            Console.Write("INCIDENT TIME (DD/MM/YY HH:MM) : ");
            string timeInput = Console.ReadLine();
            DateTime.TryParse(timeInput, out incidenttime);

            Console.Write("REPORT TEXT: ");
            reporttext = Console.ReadLine();

            ReportBack back = new ReportBack();
            bool addreportvalid = back.AddReportInDb(nameid, incidenttime, reporttext);

            if (addreportvalid)
            {
                Console.WriteLine("\nREPORT SAVED SUCCESFULLY ! \n");
                Thread.Sleep(2000);
            }
        }
    }
}