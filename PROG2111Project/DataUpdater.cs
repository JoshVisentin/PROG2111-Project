/*
* FILE: DataUpdater.cs
* PROJECT: PROG2111 – Project
* PROGRAMMERS: Josh Visentin, Trent Beitz
* FIRST VERSION: 2025-12-03
* DESCRIPTION:
* Provides update operations for all entities in the Steam database.
* Uses ADO.NET to update individual fields, with validation & error checking.
*/
using MySql.Data.MySqlClient;

namespace PROG2111Project {
    internal class DataUpdater {
        /**
         * FUNCTION: UpdateDeveloper
         * DESCRIPTION:
         * Allows user to update existing Developer record by selecting which
         * field to modify. Validates that the ID exists before performing update. 
         * Uses DbHelper.UpdateValue to push changes to database.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void UpdateDeveloper() {
            //Get valid ID before trying to update.
            Console.Write("Enter DeveloperID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;
            //Verify developer exists before updating.
            if (!DbHelper.RowExists("Developer", "DeveloperID", id)) {
                Console.WriteLine("Developer does not exist.");
                return;
            }
            //Display editable fields.
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Website");
            Console.Write("Choose field: ");
            string choice = Console.ReadLine();

            //Update chosen field.
            switch (choice) {
                case "1":
                    Console.Write("Enter new name: ");
                    DbHelper.UpdateValue("Developer", "Name", Console.ReadLine(), "DeveloperID", id);
                    break;
                case "2":
                    Console.Write("Enter new website: ");
                    DbHelper.UpdateValue("Developer", "Website", Console.ReadLine(), "DeveloperID", id);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            Console.WriteLine("Developer updated successfully.");
        }
        /**
         * FUNCTION: UpdatePublisher
         * DESCRIPTION:
         * Allows user to update existing Publisher record by selecting which
         * field to modify. Validates that the ID exists before performing update. 
         * Uses DbHelper.UpdateValue to push changes to database.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void UpdatePublisher() {
            //Get valid ID before trying to update.
            Console.Write("Enter PublisherID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;
            //Verify developer exists before updating.
            if (!DbHelper.RowExists("Publisher", "PublisherID", id)) {
                Console.WriteLine("Publisher does not exist.");
                return;
            }
            //Display editable fields.
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Website");
            Console.Write("Choose field: ");
            string choice = Console.ReadLine();
            
            //Update chosen field.
            switch (choice) {
                case "1":
                    Console.Write("Enter new name: ");
                    DbHelper.UpdateValue("Publisher", "Name", Console.ReadLine(), "PublisherID", id);
                    break;
                case "2":
                    Console.Write("Enter new website: ");
                    DbHelper.UpdateValue("Publisher", "Website", Console.ReadLine(), "PublisherID", id);
                    break;
            }
            Console.WriteLine("Publisher updated successfully.");
        }
        /**
         * FUNCTION: UpdateGenre
         * DESCRIPTION:
         * Allows user to update existing Genre record by selecting which
         * field to modify. Validates that the ID exists before performing update. 
         * Uses DbHelper.UpdateValue to push changes to database.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void UpdateGenre() {
            //Get valid ID before trying to update.
            Console.Write("Enter GenreID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;
            //Verify developer exists before updating.
            if (!DbHelper.RowExists("Genre", "GenreID", id)) {
                Console.WriteLine("Genre does not exist.");
                return;
            }

            //Update name.
            Console.Write("Enter new Name: ");
            DbHelper.UpdateValue("Genre", "Name", Console.ReadLine(), "GenreID", id);
            Console.WriteLine("Genre updated successfully.");
        }

        public static void UpdateGame() {
            //Get valid ID before trying to update.
            Console.Write("Enter GameID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;
            if (!DbHelper.RowExists("Game", "GameID", id)) {
                Console.WriteLine("Game does not exist.");
                return;
            }
            //Display editable fields.
            Console.WriteLine("1. Title");
            Console.WriteLine("2. SystemSupport");
            Console.WriteLine("3. AgeRating");
            Console.WriteLine("4. Description");
            Console.WriteLine("5. ReleaseDate");
            Console.WriteLine("6. Price");
            Console.WriteLine("7. Achievements");
            Console.WriteLine("8. PublisherID");
            Console.WriteLine("9. DeveloperID");
            Console.Write("Choose field: ");
            string choice = Console.ReadLine();
            
            //Update chosen field.
            switch (choice) {
                case "1":
                    Console.Write("New Title: ");
                    DbHelper.UpdateValue("Game", "Title", Console.ReadLine(), "GameID", id);
                    break;
                case "2":
                    Console.Write("New SystemSupport: ");
                    DbHelper.UpdateValue("Game", "SystemSupport", Console.ReadLine(), "GameID", id);
                    break;
                case "3":
                    Console.Write("New AgeRating: ");
                    DbHelper.UpdateValue("Game", "AgeRating", Console.ReadLine(), "GameID", id);
                    break;
                case "4":
                    Console.Write("New Description: ");
                    DbHelper.UpdateValue("Game", "Description", Console.ReadLine(), "GameID", id);
                    break;
                case "5":
                    Console.Write("New ReleaseDate (YYYY-MM-DD): ");
                    DbHelper.UpdateValue("Game", "ReleaseDate", DateTime.Parse(Console.ReadLine()), "GameID", id);
                    break;
                case "6":
                    Console.Write("New Price: ");
                    DbHelper.UpdateValue("Game", "Price", float.Parse(Console.ReadLine()), "GameID", id);
                    break;
                case "7":
                    Console.Write("New Achievements: ");
                    DbHelper.UpdateValue("Game", "Achievements", int.Parse(Console.ReadLine()), "GameID", id);
                    break;
                case "8":
                    Console.Write("New PublisherID: ");
                    int pub = int.Parse(Console.ReadLine());
                    //Ensure publisher with specified ID exists before updating.
                    if (!DbHelper.RowExists("Publisher", "PublisherID", pub)) {
                        Console.WriteLine("Publisher does not exist.");
                        return;
                    }
                    DbHelper.UpdateValue("Game", "PublisherID", pub, "GameID", id);
                    break;
                case "9":
                    Console.Write("New DeveloperID: ");
                    int dev = int.Parse(Console.ReadLine());
                    //Ensure developer with specified ID exists before updating.
                    if (!DbHelper.RowExists("Developer", "DeveloperID", dev)) {
                        Console.WriteLine("Developer does not exist.");
                        return;
                    }
                    DbHelper.UpdateValue("Game", "DeveloperID", dev, "GameID", id);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            Console.WriteLine("Game updated successfully.");
        }
        /**
         * FUNCTION: UpdateUser
         * DESCRIPTION:
         * Allows user to update existing SteamUser record by selecting which
         * field to modify. Validates that the ID exists before performing update. 
         * Uses DbHelper.UpdateValue to push changes to database.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void UpdateUser() {
            //Get valid ID before trying to update.
            Console.Write("Enter UserID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;
            //Verify developer exists before updating.
            if (!DbHelper.RowExists("SteamUser", "UserID", id)) {
                Console.WriteLine("User does not exist.");
                return;
            }
            //Display editable fields.
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Email");
            Console.WriteLine("3. Address");
            Console.WriteLine("4. DateOfBirth");
            Console.WriteLine("5. AccountBalance");
            Console.Write("Choose field: ");
            string choice = Console.ReadLine();
            
            //Update chosen field.
            switch (choice) {
                case "1":
                    Console.Write("New Name: ");
                    DbHelper.UpdateValue("SteamUser", "Name", Console.ReadLine(), "UserID", id);
                    break;
                case "2":
                    Console.Write("New Email: ");
                    DbHelper.UpdateValue("SteamUser", "Email", Console.ReadLine(), "UserID", id);
                    break;
                case "3":
                    Console.Write("New Address: ");
                    DbHelper.UpdateValue("SteamUser", "Address", Console.ReadLine(), "UserID", id);
                    break;
                case "4":
                    Console.Write("New DateOfBirth (YYYY-MM-DD): ");
                    DbHelper.UpdateValue("SteamUser", "DateOfBirth", DateTime.Parse(Console.ReadLine()), "UserID", id);
                    break;
                case "5":
                    Console.Write("New AccountBalance: ");
                    DbHelper.UpdateValue("SteamUser", "AccountBalance", float.Parse(Console.ReadLine()), "UserID", id);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            Console.WriteLine("User updated successfully.");
        }
        /**
         * FUNCTION: UpdateGameLibrary
         * DESCRIPTION:
         * Allows user to update existing user-game relationship in GameLibrary.
         * Confirms that the composite key (UserID, GameID) exists before modifying field. 
         * Uses DbHelper.UpdateValue to push changes to database.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void UpdateGameLibrary() {
            //Get valid composite key before updating.
            Console.Write("Enter UserID: ");
            int userID = int.Parse(Console.ReadLine());
            Console.Write("Enter GameID: ");
            int gameID = int.Parse(Console.ReadLine());
            
            try {
                //Check if entry with composite key exists.
                string existsSql = "SELECT COUNT(*) FROM GameLibrary WHERE UserID=@u AND GameID=@g";

                using var conn = new MySqlConnection(DbHelper.connStr);
                using var cmd = new MySqlCommand(existsSql, conn);

                cmd.Parameters.AddWithValue("@u", userID);
                cmd.Parameters.AddWithValue("@g", gameID);

                conn.Open();
                long count = (long)cmd.ExecuteScalar();
                conn.Close();

                //If no entry exists, return.
                if (count == 0) {
                    Console.WriteLine("Entry does not exist.");
                    return;
                }

                
                //If entry exists display editable fields.
                Console.WriteLine("1. Update PurchaseDate");
                Console.WriteLine("2. Update HoursPlayed");
                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                string sql;
                object newValue;

                if (choice == "1") {
                    Console.Write("New PurchaseDate: ");
                    newValue = DateTime.Parse(Console.ReadLine());
                    sql = "UPDATE GameLibrary SET PurchaseDate=@val WHERE UserID=@u AND GameID=@g";
                } else if (choice == "2") {
                    Console.Write("New HoursPlayed: ");
                    newValue = float.Parse(Console.ReadLine());
                    sql = "UPDATE GameLibrary SET HoursPlayed=@val WHERE UserID=@u AND GameID=@g";
                } else {
                    Console.WriteLine("Invalid option.");
                    return;
                }

                //Update entry with new data.
                using MySqlCommand cmd2 = new MySqlCommand(sql, conn);

                cmd2.Parameters.AddWithValue("@val", newValue);
                cmd2.Parameters.AddWithValue("@u", userID);
                cmd2.Parameters.AddWithValue("@g", gameID);

                conn.Open();
                cmd2.ExecuteNonQuery();
                conn.Close();

                Console.WriteLine("GameLibrary updated.");
            } catch (Exception ex) {
                Console.WriteLine("Error updating GameLibrary: " + ex.Message);
            }
        }

    }
}