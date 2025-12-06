/*
* FILE: DataDeleter.cs
* PROJECT: PROG2111 – Project
* PROGRAMMERS: Josh Visentin, Trent Beitz
* FIRST VERSION: 2025-12-03
* DESCRIPTION:
* Implements delete functionality for all entities in the database, 
* including reference checks before deletion. 
* Prevents removal of records that have dependent tables.
*/
using MySql.Data.MySqlClient;

namespace PROG2111Project {
    internal class DataDeleter {
        /**
         * FUNCTION: DeleteDeveloper
         * DESCRIPTION:
         * Deletes Developer record based on ID entered by user.
         * Checks whether developer is referenced by Game entries before 
         * allowing deletion. If no dependencies exist, 
         * record is removed using DbHelper.DeleteRow.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
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
        /**
         * FUNCTION: DeleteDeveloper
         * DESCRIPTION:
         * Deletes Publisher record based on ID entered by user.
         * Checks whether publisher is referenced by Game entries before 
         * allowing deletion. If no dependencies exist, 
         * record is removed using DbHelper.DeleteRow.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
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
        /**
         * FUNCTION: DeleteGenre
         * DESCRIPTION:
         * Deletes Genre record based on ID entered by user.
         * Checks whether Genre is referenced by GameGenre entries before 
         * allowing deletion. If no dependencies exist, 
         * record is removed using DbHelper.DeleteRow.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
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
        /**
         * FUNCTION: DeleteGame
         * DESCRIPTION:
         * Deletes Game record based on ID entered by user.
         * Checks whether Game is referenced by GameGenre or GameLibrary
         * entries before allowing deletion. If no dependencies exist, 
         * record is removed using DbHelper.DeleteRow.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
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
        /**
         * FUNCTION: DeleteUser
         * DESCRIPTION:
         * Deletes User record based on ID entered by user.
         * Checks whether User is referenced by GameLibrary entries 
         * before allowing deletion. If no dependencies exist, 
         * record is removed using DbHelper.DeleteRow.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
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
        /**
         * FUNCTION: DeleteGameGenreRelation
         * DESCRIPTION:
         * Deletes row from GameGenre bridge table based on 
         * composite key of GameID & GenreID. 
         * Validates entry exists before attempting deletion.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void DeleteGameGenreRelation() {
            try {
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

                using var conn = new MySqlConnection(DbHelper.connStr);
                using var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@gameID", gameID);
                cmd.Parameters.AddWithValue("@genreID", genreID);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    Console.WriteLine("Relation deleted.");
                else
                    Console.WriteLine("Relation not found.");
            }
            catch (Exception ex) {
                Console.WriteLine("Error deleting GameGenre relation: " + ex.Message);
            }
        }
        /**
         * FUNCTION: DeleteGameLibraryRelation
         * DESCRIPTION:
         * Deletes row from GameLibrary bridge table based on 
         * composite key of UserID & GameID. 
         * Validates entry exists before attempting deletion.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void DeleteGameLibraryRelation() {
            try {
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

                using var conn = new MySqlConnection(DbHelper.connStr);
                using var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@gameID", gameID);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    Console.WriteLine("Relation deleted.");
                else
                    Console.WriteLine("Relation not found.");
            }
            catch (Exception ex) {
                Console.WriteLine("Error deleting GameLibrary relation: " + ex.Message);
            }
        }
    }
}
