using MySql.Data.MySqlClient;
using System.Data;

namespace PROG2111Project {
    internal class DbHelper {
        public const string connStr = "server=localhost;user=testuser;password=Password;database=steamdb;";
        public static void RunCustomQuery(){
            Console.Write("Enter a SELECT query: ");
            string query = Console.ReadLine();

            if (!query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase)){
                Console.WriteLine("Only SELECT statements are allowed.");
            } else {
                try {
                    using MySqlConnection conn = new MySqlConnection(connStr);
                    conn.Open();

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if(ds.Tables.Count == 0) {
                        Console.WriteLine("Query returned no results.");
                        return;
                    }
                    DataTable table = ds.Tables[0];

                    foreach(DataRow row in table.Rows) {
                        foreach(DataColumn col in table.Columns) Console.Write($"{row[col]}\t");
                        Console.WriteLine();
                    }
                } catch(Exception ex) {
                    Console.WriteLine("Query failed: " + ex.Message);
                }
            }    
        }
        public static void InsertRows(string tableName, Action<DataTable> fillRows) {
            string query = $"SELECT * FROM {tableName}";

            try {
                using MySqlConnection conn = new MySqlConnection(connStr);
                DataSet ds = new DataSet();
                conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                da.Fill(ds, tableName);
                conn.Close();

                DataTable table = ds.Tables[tableName];

                fillRows(table);

                using MySqlConnection conn2 = new MySqlConnection(connStr);
                conn2.Open();

                MySqlDataAdapter da2 = new MySqlDataAdapter(query, conn2);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(da2);
                da2.Update(ds, tableName);

            } catch (Exception ex) {
                Console.WriteLine($"Error creating rows for {tableName}: {ex.Message}");
            }
        }
        public static bool EnsureTableExists(string tableName) {
            if (TableExists(tableName))
                return true;

            Console.WriteLine($"Table '{tableName}' does not exist. Creating all tables...");
            CreateAllTables();

            return TableExists(tableName);
        }
        public static bool TableExists(string tableName) {
            string query = @"SELECT COUNT(*) FROM information_schema.tables 
                             WHERE table_schema = DATABASE() AND table_name = @name";

            try {
                using MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();

                using MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", tableName);

                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
            } catch {
                return false;
            }
        }
        public static bool TableHasRows(string tableName) {
            if (!TableExists(tableName)) return false;

            string query = $"SELECT COUNT(*) FROM {tableName}";

            try {
                using MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();

                using MySqlCommand cmd = new MySqlCommand(query, conn);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
            } catch {
                return false;
            }
        }
        public static void DropAllTables() {
            string[] dropOrder = {
                "GameLibrary", "GameGenre", "SteamUser", 
                "Game", "Genre", "Developer", "Publisher"};

            bool anyExists = dropOrder.Any(t => DbHelper.TableExists(t));

            if (!anyExists){
                Console.WriteLine("No tables exist. Nothing to drop.");
            } else {
                using MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();

                foreach (string table in dropOrder){
                    if (!DbHelper.TableExists(table)){
                        Console.WriteLine($"Table '{table}' does not exist, skipping.");
                    } else {
                        using MySqlCommand cmd = new MySqlCommand($"DROP TABLE {table};", conn);
                        try {
                            cmd.ExecuteNonQuery();
                            Console.WriteLine($"Dropped table: {table}");
                        } catch (Exception ex){
                            Console.WriteLine($"Failed to drop {table}: {ex.Message}");
                        }
                    }
                }
                Creation.InitializeCreatedFlags();
                Console.WriteLine("All existing tables dropped."); 
            }
        }


        public static void CreateAllTables() {
            string sql = @"
                CREATE TABLE Developer(
	                DeveloperID INT PRIMARY KEY AUTO_INCREMENT,
	                Name VARCHAR(64) NOT NULL,
	                Website VARCHAR(128) NOT NULL
                );

                CREATE TABLE Publisher(
	                PublisherID INT PRIMARY KEY AUTO_INCREMENT,
	                Name VARCHAR(64) NOT NULL,
	                Website VARCHAR(128) NOT NULL
                );

                CREATE TABLE Genre(
	                GenreID INT PRIMARY KEY AUTO_INCREMENT,
	                Name VARCHAR(64) NOT NULL
                );

                CREATE TABLE Game(
	                GameID INT PRIMARY KEY AUTO_INCREMENT,
                    Title VARCHAR(64) NOT NULL,
                    SystemSupport VARCHAR(32) NOT NULL,
                    AgeRating CHAR NOT NULL,
                    Description VARCHAR(64) NOT NULL,
                    ReleaseDate DATE DEFAULT (CURRENT_DATE()),
                    Price FLOAT DEFAULT 0.0,
                    Achievements INT DEFAULT 0,
                    PublisherID INT,
                    DeveloperID INT,
                    FOREIGN KEY(PublisherID) REFERENCES Publisher(PublisherID),
                    FOREIGN KEY (DeveloperID) REFERENCES Developer(DeveloperID)
                );

                CREATE TABLE GameGenre(
                    GameID INT NOT NULL,
                    GenreID INT NOT NULL,
                    PRIMARY KEY (GameID, GenreID),
                    FOREIGN KEY (GameID) REFERENCES Game(GameID),
                    FOREIGN KEY (GenreID) REFERENCES Genre(GenreID)
                );

                CREATE TABLE SteamUser(
	                UserID INT PRIMARY KEY AUTO_INCREMENT,
	                Name VARCHAR(64),
	                Email VARCHAR(128),
	                Address VARCHAR(128),
	                DateCreated DATE DEFAULT (CURRENT_DATE()),
	                DateOFBirth DATE NOT NULL,
	                AccountBalance FLOAT DEFAULT 0.0
                );

                CREATE TABLE GameLibrary(
                    UserID INT NOT NULL,
                    GameID INT NOT NULL,
                    PurchaseDate DATE DEFAULT (CURRENT_DATE()),
                    HoursPlayed FLOAT DEFAULT 0,
                    PRIMARY KEY(UserID, GameID),
                    FOREIGN KEY(UserID) REFERENCES SteamUser(UserID),
                    FOREIGN KEY(GameID) REFERENCES Game(GameID)
                );";

            try {
                using MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();

                string[] commands = sql.Split(";", StringSplitOptions.RemoveEmptyEntries);

                foreach (string cmdText in commands) {
                    using MySqlCommand cmd = new MySqlCommand(cmdText, conn) {
                    };
                        cmd.ExecuteNonQuery();
                }

                Console.WriteLine("All tables created successfully.");
            } catch (Exception ex) {
                Console.WriteLine("Table creation failed: " + ex.Message);
            }
        }

        public static bool RowExists(string table, string keyColumn, int id) {
            string sql = $"SELECT COUNT(*) FROM {table} WHERE {keyColumn} = @id";
            using MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                return (long)cmd.ExecuteScalar() > 0;
        }

        public static bool HasDependencies(string table, string fkColumn, int id) {
            string sql = $"SELECT COUNT(*) FROM {table} WHERE {fkColumn} = @id";
            using MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                return (long)cmd.ExecuteScalar() > 0;
        }

        public static void DeleteRow(string table, string keyColumn, int id) {
            string sql = $"DELETE FROM {table} WHERE {keyColumn} = @id";
            using MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
        }
    }
}
