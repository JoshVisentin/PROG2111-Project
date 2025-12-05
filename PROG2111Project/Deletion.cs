using MySql.Data.MySqlClient;

namespace PROG2111Project {
    internal class Deletion {
        public static void DeleteDeveloper() {
            Console.Write("Enter DeveloperID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Invalid ID.");
                return;
            }

            if (!DbHelper.RowExists("Developer", "DeveloperID", id)) {
                Console.WriteLine("Developer does not exist.");
                return;
            }

            if (DbHelper.HasDependencies("Game", "DeveloperID", id)) {
                Console.WriteLine("Cannot delete developer — games depend on it.");
                return;
            }

            DbHelper.DeleteRow("Developer", "DeveloperID", id);
            Console.WriteLine("Developer deleted successfully.");
        }

        public static void DeletePublisher() {
            Console.Write("Enter PublisherID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Invalid ID.");
                return;
            }

            if (!DbHelper.RowExists("Publisher", "PublisherID", id)) {
                Console.WriteLine("Publisher does not exist.");
                return;
            }

            if (DbHelper.HasDependencies("Game", "PublisherID", id)) {
                Console.WriteLine("Cannot delete publisher — games depend on it.");
                return;
            }

            DbHelper.DeleteRow("Publisher", "PublisherID", id);
            Console.WriteLine("Publisher deleted successfully.");
        }

        public static void DeleteGenre() {
            Console.Write("Enter GenreID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Invalid ID.");
                return;
            }

            if (!DbHelper.RowExists("Genre", "GenreID", id)) {
                Console.WriteLine("Genre does not exist.");
                return;
            }

            if (DbHelper.HasDependencies("GameGenre", "GenreID", id)) {
                Console.WriteLine("Cannot delete genre — it is used by one or more games.");
                return;
            }

            DbHelper.DeleteRow("Genre", "GenreID", id);
            Console.WriteLine("Genre deleted successfully.");
        }
        public static void DeleteGame() {
            Console.Write("Enter GameID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Invalid ID.");
                return;
            }

            if (!DbHelper.RowExists("Game", "GameID", id)) {
                Console.WriteLine("Game does not exist.");
                return;
            }

            if (DbHelper.HasDependencies("GameLibrary", "GameID", id)) {
                Console.WriteLine("Cannot delete game — users own this game.");
                return;
            }

            if (DbHelper.HasDependencies("GameGenre", "GameID", id)) {
                Console.WriteLine("Cannot delete game — genres are linked to it.");
                return;
            }

            DbHelper.DeleteRow("Game", "GameID", id);
            Console.WriteLine("Game deleted successfully.");
        }
                public static void DeleteUser() {
            Console.Write("Enter UserID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Invalid ID.");
                return;
            }

            if (!DbHelper.RowExists("SteamUser", "UserID", id)) {
                Console.WriteLine("User does not exist.");
                return;
            }

            if (DbHelper.HasDependencies("GameLibrary", "UserID", id)) {
                Console.WriteLine("Cannot delete user — they own one or more games.");
                return;
            }

            DbHelper.DeleteRow("SteamUser", "UserID", id);
            Console.WriteLine("User deleted successfully.");
        }
        public static void DeleteGameGenreRelation() {
            Console.Write("Enter GameID: ");
            if (!int.TryParse(Console.ReadLine(), out int gameID)) {
                Console.WriteLine("Invalid GameID.");
                return;
            }

            Console.Write("Enter GenreID: ");
            if (!int.TryParse(Console.ReadLine(), out int genreID)) {
                Console.WriteLine("Invalid GenreID.");
                return;
            }

            string sql = "DELETE FROM GameGenre WHERE GameID=@gameID AND GenreID=@genreID";

            using MySqlConnection conn = new MySqlConnection(DbHelper.connStr);
            conn.Open();
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gameID", gameID);
                cmd.Parameters.AddWithValue("@genreID", genreID);
                int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine("Relation deleted.");
            else
                Console.WriteLine("Relation not found.");
        }
        public static void DeleteGameLibraryRelation() {
            Console.Write("Enter UserID: ");
            if (!int.TryParse(Console.ReadLine(), out int userID)) {
                Console.WriteLine("Invalid UserID.");
                return;
            }

            Console.Write("Enter GameID: ");
            if (!int.TryParse(Console.ReadLine(), out int gameID)) {
                Console.WriteLine("Invalid GameID.");
                return;
            }

            string sql = "DELETE FROM GameLibrary WHERE UserID=@userID AND GameID=@gameID";

            using MySqlConnection conn = new MySqlConnection(DbHelper.connStr);
            conn.Open();
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@gameID", gameID);
                int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine("Relation deleted.");
            else
                Console.WriteLine("Relation not found.");
        }




    }
}
