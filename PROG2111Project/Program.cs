/*
 * Authours: Josh, Trent
 * Start: 3/12/2025
 * End: 
 * Class: PROG2111
 * Assignment: PROJECT
 * Disription: 
 * Develop C# programs that connect to the MySQL database using the ADO.NET
• Perform CRUD operations (Create, Read, Update, Delete) for the entities defined in your ERD.
• Ensure proper error handling and transaction control.
• Use the ADO.NET MySQL library to establish the connection and perform operations. 
 */
using MySql.Data.MySqlClient;
using System.Text;
using System;
using System.Data;
using System.Collections.Generic;
using MySqlX.XDevAPI.CRUD;

namespace PROG2111Project {
    internal class Program{
        public const string connStr = "server=localhost;user=testuser;password=Password;database=steamdb;";
        public static MySqlConnection connection = new MySqlConnection(connStr);
        public static DataSet ds = new DataSet();
        private static void Main(string[] args){
            bool running = true;

            //opulate the defalt davlues

            while (running){
                Console.WriteLine("1. Create");
                Console.WriteLine("2. Read");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input){
                    case "1":
                        Create();
                        break;
                    case "2":
                        Read();
                        break;
                    case "3":
                        Update();
                        break;
                    case "4":
                        Delete();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine();
            }
        }

        public static void Create(){ 
            Console.WriteLine("1. Create all Publishers.");
            Console.WriteLine("2. Create all Developers.");
            Console.WriteLine("3. Create all Genres.");
            Console.WriteLine("4. Create all Games.");
            Console.WriteLine("5. Create all Libraries.");
            Console.WriteLine("6. Create all Users");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input){
                case "1":
                    Creation.CreatePublishers();
                    break;
                case "2":
                    Creation.CreateDevelopers();
                    break;
                case "3":
                    Creation.CreateGenres();
                    break;
                case "4":
                    Creation.CreateGames();
                    break;
                case "5":
                    Creation.CreateLibraries();
                    break;
                case "6":
                    Creation.CreateUsers();
                    break;
            }
        }
        public static void Read(){
            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input){
                case "1":
                    break;

            }
        }
        public static void Update(){ 
        
        }
        public static void Delete(){ 
        
        }

    
    }
}