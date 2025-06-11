using System;
using System.Threading;

namespace Malshinon
{
    public class ConnectionFront
    {
        // Variables de classe pour stocker les champs
        private static string id { get; set; }
        private static string pw;
        static string lettermenu;

        public static void TheConnectionPage()
        {
            while (true)
            {
                Console.WriteLine("\n***** CONNECTION  *****\n");
                Console.Write("TO CONNECT PRESS C    ");
                Console.Write("TO SUBSCRIBE PRESS S   ");
                Console.WriteLine("TO EXIT PRESS X ");

                lettermenu = Console.ReadLine().ToUpper();

                switch (lettermenu)
                {

                    case "C":

                        AllFieldConnection();
                        ConnectionBack back = new ConnectionBack();
                        bool IdValid = back.CheckIfIdInDb(id);
                        bool PwValid = back.CheckIfPwInDb(id, pw);

                        if (IdValid && PwValid)
                        {
                            Console.WriteLine("-- SUCCESSFULLY LOGGED IN TO YOUR ACCOUNT  ! \n");
                            Thread.Sleep(2000);
                            ReportFront rf = new ReportFront();
                            rf.TheReportPage();
                            break;

                        }
                        else
                        {
                            Console.WriteLine("-- INVALID ACCESS ! \n");
                            Thread.Sleep(2000);
                        }
                        break;

                    case "S":

                        SubscribeFront.TheSubscribtionPage();
                        break;

                    case "X":


                        return ;

                    default:

                        Console.WriteLine("-- PLEASE ENTER A VALID OPTION ! \n");
                        break;



                }

              
                                              

                
                

            }

        }








        // Fonctions de la page de connexion

        

        

        public static void FieldId()
        {
            Console.WriteLine("Your ID :");
            id = Console.ReadLine();  
        }

        public static void FieldPw()
        {
            Console.WriteLine("Your PASSWORD :");
            pw = Console.ReadLine();  
        }

        public static void AllFieldConnection()
        {
            FieldId();
            FieldPw();
        }

        public static string GetId()
        {
            return id;
        }
        

        

       


       





        }
    }

