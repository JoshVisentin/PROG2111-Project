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

            Creation.InitializeCreatedFlags();

            while (running){
                Console.WriteLine("1. Create");
                Console.WriteLine("2. Read");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. Drop All Tables");
                Console.WriteLine("6. Create All Tables");
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
                    case "5":
                        DbHelper.DropAllTables();
                        break;
                    case "6":
                        DbHelper.CreateAllTables();
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
            string[] tables = {"Publisher", "Developer", "Genre", "Game", "Library", "SteamUser"};

            Console.WriteLine("1. Publishers");
            Console.WriteLine("2. Developers");
            Console.WriteLine("3. Genres");
            Console.WriteLine("4. Games");
            Console.WriteLine("5. Libraries");
            Console.WriteLine("6. Users");
            Console.WriteLine("7. Custom Query");
            Console.Write("Choose: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice)){
                if (choice == 7) RunCustomQuery();
                if (choice >= 1 && choice <= tables.Length){
                    string table = tables[choice - 1];
                    Console.WriteLine("You chose table: " + table);

                    string query = $"SELECT * FROM {table}";
                    DataSet ds = new DataSet();

                    try {
                        using MySqlConnection conn = new MySqlConnection(connStr);
                        conn.Open();

                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        da.Fill(ds, table);

                        DataTable dt = ds.Tables[table];

                        foreach (DataRow row in dt.Rows) {
                            foreach (DataColumn col in dt.Columns)
                                Console.Write($"{row[col]} \t");
                            Console.WriteLine();
                        }
                    } catch (Exception ex) {
                        Console.WriteLine("Error reading: " + ex.Message);
                    }
                }
            } else {
                Console.WriteLine("Invalid choice.");
            }
        }
        public static void Update(){ 
        
        }
        public static void Delete(){ 
        
        }

        public static void RunCustomQuery(){
            Console.Write("Enter a SELECT query: ");
            string query = Console.ReadLine();

            if (!query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase)){
                Console.WriteLine("Only SELECT statements are allowed.");
                return;
            }

            try {
                using MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count == 0){
                    Console.WriteLine("Query returned no results.");
                    return;
                }
                DataTable table = ds.Tables[0];

                foreach (DataRow row in table.Rows){
                    foreach (DataColumn col in table.Columns) Console.Write($"{row[col]}\t");
                    Console.WriteLine();
                }
            } catch (Exception ex){
                Console.WriteLine("Query failed: " + ex.Message);
            }
        }
    }
}