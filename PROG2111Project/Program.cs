/*
* FILE: Program.cs
* PROJECT: PROG2111 – Project
* PROGRAMMERS: Josh Visentin, Trent Beitz
* FIRST VERSION: 2025-12-03
* DESCRIPTION:
* Entry point for the PROG2111 project. Handles main menu navigation for
* Create, Read, Update, & Delete operations. Calls helper classes to interact
* with the MySQL database using ADO.NET. Supports table creation, table dropping,
* & directing user into CRUD operations for all ERD entities.
*/
using MySql.Data.MySqlClient;
using System.Data;

namespace PROG2111Project {
    internal class Program{
        private static void Main(string[] args){
            bool running = true;

            DataCreator.InitializeCreatedFlags();

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
        /**
         * FUNCTION: Create
         * DESCRIPTION:
         * Presents user with options to create initial data for each database table.
         * Calls DataCreator methods based on user selection & performs validation
         * to prevent duplicate or out-of-order creation.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
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
                    DataCreator.CreatePublishers();
                    break;
                case "2":
                    DataCreator.CreateDevelopers();
                    break;
                case "3":
                    DataCreator.CreateGenres();
                    break;
                case "4":
                    DataCreator.CreateGames();
                    break;
                case "5":
                    DataCreator.CreateGameGenres();
                    break;
                case "6":
                    DataCreator.CreateUsers();
                    break;
                case "7":
                    DataCreator.CreateGameLibrary();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }
        }
        /**
         * FUNCTION: Read
         * DESCRIPTION:
         * Displays read menu & retrieves data from selected tables using SQL
         * SELECT statements. Executes custom query when requested. Results are
         * displayed through PrintTable function.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void Read() {
            string[] tables = { "Publisher", "Developer", "Genre", "Game", "GameLibrary", "SteamUser" };

            Console.WriteLine("1. Show Publishers");
            Console.WriteLine("2. Show Developers");
            Console.WriteLine("3. Show Genres");
            Console.WriteLine("4. Show Games");
            Console.WriteLine("5. Show Game Libraries");
            Console.WriteLine("6. Show Users");
            Console.WriteLine("7. Show Custom Query");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice)) {

                if (choice == 0) return;
                if (choice == 7) {
                    DbHelper.RunCustomQuery();
                    return;
                }

                if (choice >= 1 && choice <= tables.Length) {
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
                    }
                    catch (Exception ex) {
                        Console.WriteLine("Error reading: " + ex.Message);
                    }
                }
                else {
                    Console.WriteLine("Invalid table choice.");
                }
            }
            else {
                Console.WriteLine("Invalid choice.");
            }
        }
        /**
         * FUNCTION: Update
         * DESCRIPTION:
         * Presents update menu & allows user to modify existing records
         * in any table. Routes user selections to appropriate update
         * function in DataUpdater class.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void Update(){ 
            Console.WriteLine("1. Update Publisher");
            Console.WriteLine("2. Update Developer");
            Console.WriteLine("3. Update Genre");
            Console.WriteLine("4. Update Game");
            Console.WriteLine("5. Update User");
            Console.WriteLine("6. Update GameLibrary entry");
            Console.WriteLine("0. Back");

            Console.Write("Choose an option: ");
            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input) {
                case "1":
                    DataUpdater.UpdatePublisher();
                    break;
                case "2":
                    DataUpdater.UpdateDeveloper();
                    break;
                case "3":
                    DataUpdater.UpdateGenre();
                    break;
                case "4":
                    DataUpdater.UpdateGame();
                    break;
                case "5":
                    DataUpdater.UpdateUser();
                    break;
                case "6":
                    DataUpdater.UpdateGameLibrary();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
        /**
         * FUNCTION: Delete
         * DESCRIPTION:
         * Displays deletion options & processes user requests to remove records.
         * Performs dependency checks before deletion & forwards calls to
         * DataDeleter class for removal.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
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
                    DataDeleter.DeleteDeveloper(); 
                    break;
                case "2": 
                    DataDeleter.DeletePublisher(); 
                    break;
                case "3": 
                    DataDeleter.DeleteGenre(); 
                    break;
                case "4": 
                    DataDeleter.DeleteGame(); 
                    break;
                case "5": 
                    DataDeleter.DeleteUser(); 
                    break;
                case "6": 
                    DataDeleter.DeleteGameGenreRelation(); 
                    break;
                case "7": 
                    DataDeleter.DeleteGameLibraryRelation(); 
                    break;
                case "0": 
                    return;
                default: 
                    Console.WriteLine("Invalid choice"); 
                    break;
            }
        }
        /**
         * FUNCTION: PrintTable
         * DESCRIPTION:
         * Formats & prints contents of a DataTable to console using
         * column-aligned output.
         * PARAMETERS:
         * DataTable table: Table returned from SQL query to display.
         * RETURNS:
         * None.
         */
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