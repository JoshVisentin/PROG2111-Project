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
using System.Data;

namespace PROG2111Project {
    internal class Program{
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
            Console.WriteLine("5. Create Game-Genre Relations.");
            Console.WriteLine("6. Create all Users.");
            Console.WriteLine("7. Create Game-Library Relations.");
            Console.WriteLine("0. Back");
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
                    Creation.CreateGameGenres();
                    break;
                case "6":
                    Creation.CreateUsers();
                    break;
                case "7":
                    Creation.CreateGameLibrary();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }
        }
        public static void Read(){
            string[] tables = {"Publisher", "Developer", "Genre", "Game", "Library", "SteamUser"};

            Console.WriteLine("1. Show Publishers");
            Console.WriteLine("2. Show Developers");
            Console.WriteLine("3. Show Genres");
            Console.WriteLine("4. Show Games");
            Console.WriteLine("5. Show Libraries");
            Console.WriteLine("6. Show Users");
            Console.WriteLine("7. Show Custom Query");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice)){
                if (choice == 0) return;
                if (choice == 7) DbHelper.RunCustomQuery();
                if (choice >= 1 && choice <= tables.Length){
                    string table = tables[choice - 1];
                    Console.WriteLine("You chose table: " + table);

                    string query = $"SELECT * FROM {table}";
                    DataSet ds = new DataSet();

                    try {
                        using MySqlConnection conn = new MySqlConnection(DbHelper.connStr);
                        conn.Open();

                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        da.Fill(ds, table);

                        DataTable dt = ds.Tables[table];

                        PrintTable(dt);
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
            Console.WriteLine("Delete Menu:");
            Console.WriteLine("1. Delete Developer");
            Console.WriteLine("2. Delete Publisher");
            Console.WriteLine("3. Delete Genre");
            Console.WriteLine("4. Delete Game");
            Console.WriteLine("5. Delete User");
            Console.WriteLine("6. Delete Game-Genre relation");
            Console.WriteLine("7. Delete Game-Library relation");
            Console.WriteLine("0. Back");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice) {
                case "1": 
                    Deletion.DeleteDeveloper(); 
                    break;
                case "2": 
                    Deletion.DeletePublisher(); 
                    break;
                case "3": 
                    Deletion.DeleteGenre(); 
                    break;
                case "4": 
                    Deletion.DeleteGame(); 
                    break;
                case "5": 
                    Deletion.DeleteUser(); 
                    break;
                case "6": 
                    Deletion.DeleteGameGenreRelation(); 
                    break;
                case "7": 
                    Deletion.DeleteGameLibraryRelation(); 
                    break;
                case "0": 
                    return;
                default: 
                    Console.WriteLine("Invalid choice"); 
                    break;
            }
        }

        public static void PrintTable(DataTable table) {
            foreach (DataColumn col in table.Columns) {
                Console.Write($"{col.ColumnName,-20}");
            }
            Console.WriteLine();

            foreach (DataRow row in table.Rows) {
                foreach (var item in row.ItemArray) {
                    Console.Write($"{item,-20}");
                }
                Console.WriteLine();
            }
        }
    }
}