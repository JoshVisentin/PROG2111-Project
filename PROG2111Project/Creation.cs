using MySql.Data.MySqlClient;
using System.Data;

namespace PROG2111Project {
    internal class Creation {
        private static bool publishersCreated = false;
        private static bool developersCreated = false;
        private static bool genresCreated = false;
        private static bool gamesCreated = false;
        private static bool librariesCreated = false;
        private static bool usersCreated = false;

        public static void CreatePublishers(){ 
            string query = "SELECT * FROM Publisher";
            try{
                MySqlConnection conn = new MySqlConnection(Program.connStr);
                DataSet ds = new DataSet();

                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(ds, "Publisher");
                conn.Close();

                DataTable table = ds.Tables["Publisher"];    
            
                DataRow newRow1 = table.NewRow();
                newRow1["PublisherID"] = "01";
                newRow1["Name"] = "GrandmawsBakery";
                newRow1["Website"] = "Cookies.net";
                table.Rows.Add(newRow1);
                DataRow newRow2 = table.NewRow();
                newRow2["PublisherID"] = "03";
                newRow2["Name"] = "SoftMintPublishing";
                newRow2["Website"] = "softmintpub.com";
                table.Rows.Add(newRow2);
                DataRow newRow3 = table.NewRow();
                newRow3["PublisherID"] = "04";
                newRow3["Name"] = "Pokemon Company";
                newRow3["Website"] = "pokemon.com";
                table.Rows.Add(newRow3);

                using (MySqlConnection conn2 = new MySqlConnection(Program.connStr)){
                    conn2.Open();

                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(query, conn2);
                    MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter2);

                    adapter2.Update(ds, "Publisher");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Publishers Successfully Created.");
            publishersCreated = true;
        }
    
        public static void CreateDevelopers(){ 
            string query = "SELECT * FROM Developer";
            try{
                MySqlConnection conn = new MySqlConnection(Program.connStr);
                DataSet ds = new DataSet();

                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(ds, "Developer");
                conn.Close();

                DataTable table = ds.Tables["Developer"];    
            
                DataRow newRow1 = table.NewRow();
                newRow1["DeveloperID"] = "01";
                newRow1["Name"] = "Dave";
                newRow1["Website"] = "dave.net";
                table.Rows.Add(newRow1);
                DataRow newRow2 = table.NewRow();
                newRow2["DeveloperID"] = "02";
                newRow2["Name"] = "Buster";
                newRow2["Website"] = "bust.com";
                table.Rows.Add(newRow2);
                DataRow newRow3 = table.NewRow();
                newRow3["DeveloperID"] = "03";
                newRow3["Name"] = "LTech";
                newRow3["Website"] = "lych.com";
                table.Rows.Add(newRow3);
                DataRow newRow4 = table.NewRow();
                newRow4["DeveloperID"] = "04";
                newRow4["Name"] = "Pokemon Company";
                newRow4["Website"] = "pokemon.com";
                table.Rows.Add(newRow4);

                using (MySqlConnection conn2 = new MySqlConnection(Program.connStr)){
                    conn2.Open();

                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(query, conn2);
                    MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter2);

                    adapter2.Update(ds, "Developer");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Developers Successfully Created.");
            developersCreated = true;
        }
    
        public static void CreateGenres(){ 
            string query = "SELECT * FROM Genre";
            try{
                MySqlConnection conn = new MySqlConnection(Program.connStr);
                DataSet ds = new DataSet();

                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(ds, "Genre");
                conn.Close();

                DataTable table = ds.Tables["Genre"];    
            
                DataRow newRow1 = table.NewRow();
                newRow1["GenreID"] = "01";
                newRow1["Name"] = "DatingSim";
                table.Rows.Add(newRow1);
                DataRow newRow2 = table.NewRow();
                newRow2["GenreID"] = "02";
                newRow2["Name"] = "Horror";
                table.Rows.Add(newRow2);
                DataRow newRow3 = table.NewRow();
                newRow3["GenreID"] = "03";
                newRow3["Name"] = "Grand Stratgey";
                table.Rows.Add(newRow3);
                DataRow newRow4 = table.NewRow();
                newRow4["GenreID"] = "04";
                newRow4["Name"] = "Romance";
                table.Rows.Add(newRow4);
                DataRow newRow5 = table.NewRow();
                newRow5["GenreID"] = "05";
                newRow5["Name"] = "SciFi";
                table.Rows.Add(newRow5);
                DataRow newRow6 = table.NewRow();
                newRow6["GenreID"] = "06";
                newRow6["Name"] = "FarmingSim";
                table.Rows.Add(newRow6);
                DataRow newRow7 = table.NewRow();
                newRow7["GenreID"] = "07";
                newRow7["Name"] = "Idle";
                table.Rows.Add(newRow7);
                DataRow newRow8 = table.NewRow();
                newRow8["GenreID"] = "08";
                newRow8["Name"] = "RPG";
                table.Rows.Add(newRow8);

                using (MySqlConnection conn2 = new MySqlConnection(Program.connStr)){
                    conn2.Open();

                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(query, conn2);
                    MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter2);

                    adapter2.Update(ds, "Genre");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Genres Successfully Created.");
            genresCreated = true;
        }

        public static void CreateGames(){
            if (!publishersCreated){
                Console.WriteLine("Publishers not yet created. Creating Publishers...");
                CreatePublishers();
            }
            if (!developersCreated){
                Console.WriteLine("Developers not yet created. Creating Developers...");
                CreateDevelopers();
            }
            if (!genresCreated){
                Console.WriteLine("Genres not yet created. Creating Genres...");
                CreateGenres();
            }


            string query = "SELECT * FROM Game";
            try{
                MySqlConnection conn = new MySqlConnection(Program.connStr);
                DataSet ds = new DataSet();

                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(ds, "Game");
                conn.Close();

                DataTable table = ds.Tables["Game"];

                DataRow newRow1 = table.NewRow();
                newRow1["GameID"] = "01";
                newRow1["DeveloperID"] = "01";
                newRow1["PublisherID"] = "01";
                newRow1["GenreID"] = "01";
                newRow1["Title"] = "CokieClicker";
                newRow1["SystemSupport"] = "Mac";
                newRow1["AgeRating"] = "M";
                newRow1["Description"] = "A Game";
                newRow1["ReleaseDate"] = "11/9/2001";
                newRow1["Price"] = "999.99";
                newRow1["Achievements"] = "200";
                table.Rows.Add(newRow1);
                DataRow newRow2 = table.NewRow();
                newRow2["GameID"] = "02";
                newRow2["DeveloperID"] = "03";
                newRow2["PublisherID"] = "03";
                newRow2["GenreID"] = "04";
                newRow2["Title"] = "StarBuilder";
                newRow2["SystemSupport"] = "Windows";
                newRow2["AgeRating"] = "T";
                newRow2["Description"] = "Build Your Own Galaxy";
                newRow2["ReleaseDate"] = "22/7/2010";
                newRow2["Price"] = "59.49";
                newRow2["Achievements"] = "145";
                table.Rows.Add(newRow2);
                DataRow newRow3 = table.NewRow();
                newRow3["GameID"] = "03";
                newRow3["DeveloperID"] = "01";
                newRow3["PublisherID"] = "03";
                newRow3["GenreID"] = "04";
                newRow3["Title"] = "StarBuilder";
                newRow3["SystemSupport"] = "Windows";
                newRow3["AgeRating"] = "E";
                newRow3["Description"] = "Keep you sane";
                newRow3["ReleaseDate"] = "2/6/1998";
                newRow3["Price"] = "0";
                newRow3["Achievements"] = "5";
                table.Rows.Add(newRow3);
                DataRow newRow4 = table.NewRow();
                newRow4["GameID"] = "04";
                newRow4["DeveloperID"] = "04";
                newRow4["PublisherID"] = "04";
                newRow4["GenreID"] = "08";
                newRow4["Title"] = "PokemonTCG";
                newRow4["SystemSupport"] = "Windows";
                newRow4["AgeRating"] = "R";
                newRow4["Description"] = "A Game";
                newRow4["ReleaseDate"] = "11/9/2001";
                newRow4["Price"] = "999.99";
                newRow4["Achievements"] = "2000";
                table.Rows.Add(newRow4);

                using (MySqlConnection conn2 = new MySqlConnection(Program.connStr)){
                    conn2.Open();

                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(query, conn2);
                    MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter2);

                    adapter2.Update(ds, "Game");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Games Successfully Created.");
            gamesCreated = true;
        }

        public static void CreateLibraries() {
            if (!gamesCreated){
                Console.WriteLine("Games not yet created. Creating Games...");
                CreateGames();
            }

            string query = "SELECT * FROM Library";
            try {
                MySqlConnection conn = new MySqlConnection(Program.connStr);
                DataSet ds = new DataSet();

                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(ds, "Library");
                conn.Close();

                DataTable table = ds.Tables["Library"];

                DataRow newRow1 = table.NewRow();
                newRow1["LibraryID"] = "01";
                newRow1["GameID"] = "01";
                newRow1["PurchaseDate"] = "6/9/2009";
                newRow1["HoursPlayed"] = "995.7";
                table.Rows.Add(newRow1);
                DataRow newRow2 = table.NewRow();
                newRow1["LibraryID"] = "02";
                newRow1["GameID"] = "02";
                newRow1["PurchaseDate"] = "1/12/2025";
                newRow1["HoursPlayed"] = "412.3";
                table.Rows.Add(newRow2);
                DataRow newRow3 = table.NewRow();
                newRow1["LibraryID"] = "03";
                newRow1["GameID"] = "03";
                newRow1["PurchaseDate"] = "5/9/2025";
                newRow1["HoursPlayed"] = "243";
                table.Rows.Add(newRow3);

                using (MySqlConnection conn2 = new MySqlConnection(Program.connStr)) {
                    conn2.Open();

                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(query, conn2);
                    MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter2);

                    adapter2.Update(ds, "Library");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Libraries Successfully Created.");
            librariesCreated = true;
        }

        public static void CreateUsers() {
            if (!librariesCreated){
                Console.WriteLine("Libraries not yet created. Creating Libraries...");
                CreateLibraries();
            }

            string query = "SELECT * FROM SteamUser";
            try {
                MySqlConnection conn = new MySqlConnection(Program.connStr);
                DataSet ds = new DataSet();

                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(ds, "SteamUser");
                conn.Close();

                DataTable table = ds.Tables["SteamUser"];
                
                DataRow newRow1 = table.NewRow();
                newRow1["UserID"] = "01";
                newRow1["Name"] = "Terry";
                newRow1["Email"] = "terryman@gmailcom";
                newRow1["Address"] = "123 That Street";
                newRow1["DateCreated"] = "1/1/2007";
                newRow1["DateOfBirth"] = "23/8/1832";
                newRow1["AccountBalance"] = "85.97";
                newRow1["LibraryID"] = "01";
                table.Rows.Add(newRow1);
                DataRow newRow2 = table.NewRow();
                newRow2["UserID"] = "02";
                newRow2["Name"] = "Lina";
                newRow2["Email"] = "lindaloo@fastmail.com";
                newRow2["Address"] = "77 Forest lane";
                newRow2["DateCreated"] = "14/3/2012";
                newRow2["DateOfBirth"] = "11/5/1979";
                newRow2["AccountBalance"] = "12.40";
                newRow2["LibraryID"] = "02";
                table.Rows.Add(newRow2);
                DataRow newRow3 = table.NewRow();
                newRow3["UserID"] = "03";
                newRow3["Name"] = "Marcus";
                newRow3["Email"] = "marcusg@yahoo.com";
                newRow3["Address"] = "904 Sunset Road";
                newRow3["DateCreated"] = "30/9/2020";
                newRow3["DateOfBirth"] = "18/4/1995";
                newRow3["AccountBalance"] = "203.11";
                newRow3["LibraryID"] = "03";
                table.Rows.Add(newRow3);

                using (MySqlConnection conn2 = new MySqlConnection(Program.connStr)) {
                    conn2.Open();

                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(query, conn2);
                    MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter2);

                    adapter2.Update(ds, "SteamUser");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Users Successfully Created.");
            usersCreated = true;
        }
    }
}
