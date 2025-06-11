using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Malshinon
{
    public static class SubscribeFront
    {

        private static string name;
        private static string pw;



        public static void TheSubscribtionPage()
        {
            SubscribtionTitle();
            AllFieldSusbsribtion();
            SubscribeBack back = new SubscribeBack();
           bool AddIsValid = back.AddUserInDb(name, pw);

            if (AddIsValid)
            {
                Console.WriteLine($"\n----WELCOME {name}---- \n");                
                back.ShowUserId(name);

                
                

                
               
            }


        }







        // Fonctions de la page de subscribtion


        public static void SubscribtionTitle()
        {
            Console.WriteLine(" -----SUBSCRIBTION----- \n ");

        }





        public static void AllFieldSusbsribtion()
        {
            FieldName();
            FieldPw();
        }




        public static void FieldName()
        {
            Console.WriteLine("Enter Your Name : ");
            name = Console.ReadLine();

        }

        public static void FieldPw()
        {
            Console.WriteLine("Enter Your PASSWORD :");
            pw = Console.ReadLine();
        }



    }
}
