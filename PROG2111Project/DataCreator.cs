/*
* FILE: DataCreator.cs
* PROJECT: PROG2111 – Project
* PROGRAMMERS: Josh Visentin, Trent Beitz
* FIRST VERSION: 2025-12-03
* DESCRIPTION:
* Handles insertion of initial data into all database tables defined
* in the ERD. Ensures proper creation order, checks prerequisites, & prevents
* duplicate data loading. Uses ADO.NET & DbHelper utilities to populate
* Publisher, Developer, Genre, Game, GameGenre, SteamUser, & GameLibrary tables.
*/
using System.Data;

namespace PROG2111Project {
    internal class DataCreator {
        private static bool publishersCreated = false;
        private static bool developersCreated = false;
        private static bool genresCreated = false;
        private static bool gamesCreated = false;
        private static bool gameGenresCreated = false;
        private static bool usersCreated = false;
        private static bool gameLibraryCreated = false;
        /**
         * FUNCTION: InitializeCreatedFlags
         * DESCRIPTION:
         * Resets all internal boolean flags tracking whether initial data has been 
         * inserted into each table. This is used after dropping tables to 
         * ensure creation functions run correctly when called again.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void InitializeCreatedFlags() {
            publishersCreated  = DbHelper.TableExists("Publisher")   && DbHelper.TableHasRows("Publisher");
            developersCreated  = DbHelper.TableExists("Developer")   && DbHelper.TableHasRows("Developer");
            genresCreated      = DbHelper.TableExists("Genre")       && DbHelper.TableHasRows("Genre");
            gamesCreated       = DbHelper.TableExists("Game")        && DbHelper.TableHasRows("Game");
            gameGenresCreated  = DbHelper.TableExists("GameGenre")   && DbHelper.TableHasRows("GameGenre");
            usersCreated       = DbHelper.TableExists("SteamUser")   && DbHelper.TableHasRows("SteamUser");
            gameLibraryCreated = DbHelper.TableExists("GameLibrary") && DbHelper.TableHasRows("GameLibrary");
        }
        /**
         * FUNCTION: CreatePublishers
         * DESCRIPTION:
         * Inserts sample Publisher records into Publisher table.
         * Ensures publishers are only created once per program run by checking 
         * associated created flag. Uses DbHelper.InsertRows for insertion.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void CreatePublishers(){ 
            if (publishersCreated){
                Console.WriteLine("Publishers already created.");
            } else if (!DbHelper.EnsureTableExists("Publisher")) {
                Console.WriteLine("Publishers table not found, create table first.");
            } else {

                DbHelper.InsertRows("Publisher", table => {
                    DataRow r1 = table.NewRow();
                    r1["Name"] = "GrandmawsBakery";
                    r1["Website"] = "Cookies.net";
                    table.Rows.Add(r1);

                    DataRow r2 = table.NewRow();
                    r2["Name"] = "SoftMintPublishing";
                    r2["Website"] = "softmintpub.com";
                    table.Rows.Add(r2);

                    DataRow r3 = table.NewRow();
                    r3["Name"] = "Pokemon Company";
                    r3["Website"] = "pokemon.com";
                    table.Rows.Add(r3);
                });
                Console.WriteLine("Publishers Successfully Created.");
                publishersCreated = true;
            }   
        }
        /**
         * FUNCTION: CreateDevelopers
         * DESCRIPTION:
         * Inserts sample Developer records into Developers table.
         * Ensures developers are only created once per program run by checking 
         * associated created flag. Uses DbHelper.InsertRows for insertion.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void CreateDevelopers(){ 
            if (developersCreated){
                Console.WriteLine("Developers already created.");
            } else if (!DbHelper.EnsureTableExists("Developer")) {
                Console.WriteLine("Developers table not found, create table first.");
            } else {

                DbHelper.InsertRows("Developer", table => {
                    DataRow r1 = table.NewRow();
                    r1["Name"] = "Dave";
                    r1["Website"] = "dave.net";
                    table.Rows.Add(r1);

                    DataRow r2 = table.NewRow();
                    r2["Name"] = "Buster";
                    r2["Website"] = "bust.org";
                    table.Rows.Add(r2);
                    
                    DataRow r3 = table.NewRow();
                    r3["Name"] = "LTech";
                    r3["Website"] = "ltech.org";
                    table.Rows.Add(r3);

                    DataRow r4 = table.NewRow();
                    r4["Name"] = "Pokemon Company";
                    r4["Website"] = "pokemon.com";
                    table.Rows.Add(r4);
                });

                Console.WriteLine("Publishers Successfully Created.");
                publishersCreated = true; 
            }
        }
        /**
         * FUNCTION: CreateGenres
         * DESCRIPTION:
         * Inserts sample Genre records into Genres table.
         * Ensures genres are only created once per program run by checking 
         * associated created flag. Uses DbHelper.InsertRows for insertion.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void CreateGenres(){ 
            if (genresCreated){
                Console.WriteLine("Genres already created.");
            } else if (!DbHelper.EnsureTableExists("Genre")) {
                Console.WriteLine("Genres table not found, create table first.");
            } else {

                DbHelper.InsertRows("Genre", table => {
                    string[] genreNames = {
                        "DatingSim", "Horror", "Grand Strategy",
                        "Romance", "SciFi", "FarmingSim", "Idle", "RPG"};

                    foreach (string g in genreNames){
                        DataRow row = table.NewRow();
                        row["Name"] = g;
                        table.Rows.Add(row);
                    }
                });
                Console.WriteLine("Genres Successfully Created.");
                genresCreated = true;
            }
        }
        /**
         * FUNCTION: CreateGames
         * DESCRIPTION:
         * Inserts sample Game records into Games table.
         * Ensures games are only created once per program run by checking 
         * associated created flag. Uses DbHelper.InsertRows for insertion.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void CreateGames(){
            if (gamesCreated){
                Console.WriteLine("Games already created.");
            } else if (!DbHelper.EnsureTableExists("Game")) {
                Console.WriteLine("Games table not found, create table first.");
            } else {
                if (!publishersCreated){
                    Console.WriteLine("Publishers not yet created. Creating Publishers...");
                    CreatePublishers();
                }
                if (!developersCreated){
                    Console.WriteLine("Developers not yet created. Creating Developers...");
                    CreateDevelopers();
                }
            
                DbHelper.InsertRows("Game", table => {
                    DataRow g1 = table.NewRow();
                    g1["Title"] = "CookieClicker";
                    g1["SystemSupport"] = "Mac";
                    g1["AgeRating"] = "M";
                    g1["Description"] = "A Game";
                    g1["ReleaseDate"] = "2001-11-09";
                    g1["Price"] = 999.99;
                    g1["Achievements"] = 200;
                    g1["DeveloperID"] = 1;
                    g1["PublisherID"] = 1;
                    table.Rows.Add(g1);

                    DataRow g2 = table.NewRow();
                    g2["Title"] = "StarBuilder";
                    g2["SystemSupport"] = "Windows";
                    g2["AgeRating"] = "T";
                    g2["Description"] = "Build Your Galaxy";
                    g2["ReleaseDate"] = "2010-07-22";
                    g2["Price"] = 59.49;
                    g2["Achievements"] = 145;
                    g2["DeveloperID"] = 3;
                    g2["PublisherID"] = 2;
                    table.Rows.Add(g2);

                    DataRow g3 = table.NewRow();
                    g3["Title"] = "BongoCat";
                    g3["SystemSupport"] = "Windows";
                    g3["AgeRating"] = "E";
                    g3["Description"] = "Keep you sane";
                    g3["ReleaseDate"] = "2022-04-08";
                    g3["Price"] = 0;
                    g3["Achievements"] = 5;
                    g3["DeveloperID"] = 1;
                    g3["PublisherID"] = 2;
                    table.Rows.Add(g3);

                    DataRow g4 = table.NewRow();
                    g4["Title"] = "PokemonTCG";
                    g4["SystemSupport"] = "Windows";
                    g4["AgeRating"] = "R";
                    g4["Description"] = "Sell us your soul";
                    g4["ReleaseDate"] = "1998-06-02";
                    g4["Price"] = 0;
                    g4["Achievements"] = 18223;
                    g4["DeveloperID"] = 4;
                    g4["PublisherID"] = 3;
                    table.Rows.Add(g4);
                });
                Console.WriteLine("Games Successfully Created.");
                gamesCreated = true;
            }
        }
        /**
         * FUNCTION: CreateGameGenres
         * DESCRIPTION:
         * Inserts sample GameGenre records into GemeGenres table.
         * Ensures game genres are only created once per program run by checking 
         * associated created flag. Uses DbHelper.InsertRows for insertion.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void CreateGameGenres() {
            if (gameGenresCreated){
                Console.WriteLine("GameGenres already created.");
            } else if (!DbHelper.EnsureTableExists("GameGenre")) {
                Console.WriteLine("GameGenres table not found, create table first.");
            } else {
                if (!gamesCreated) {
                    Console.WriteLine("Games not yet created. Creating Games...");
                    CreateGames();
                }
                if (!genresCreated) {
                    Console.WriteLine("Genres not yet created. Creating Genres...");
                    CreateGenres();
                }

                DbHelper.InsertRows("GameGenre", table => {
                    DataRow gg1 = table.NewRow();
                    gg1["GameID"] = 1;
                    gg1["GenreID"] = 1;
                    table.Rows.Add(gg1);

                    DataRow gg2 = table.NewRow();
                    gg2["GameID"] = 1;
                    gg2["GenreID"] = 2;
                    table.Rows.Add(gg2);

                    DataRow gg3 = table.NewRow();
                    gg3["GameID"] = 1;
                    gg3["GenreID"] = 3;
                    table.Rows.Add(gg3);

                    DataRow gg4 = table.NewRow();
                    gg4["GameID"] = 2;
                    gg4["GenreID"] = 4;
                    table.Rows.Add(gg4);

                    DataRow gg5 = table.NewRow();
                    gg5["GameID"] = 2;
                    gg5["GenreID"] = 5;
                    table.Rows.Add(gg5);

                    DataRow gg6 = table.NewRow();
                    gg6["GameID"] = 2;
                    gg6["GenreID"] = 6;
                    table.Rows.Add(gg6);

                    DataRow gg7 = table.NewRow();
                    gg7["GameID"] = 3;
                    gg7["GenreID"] = 7;
                    table.Rows.Add(gg7);

                    DataRow gg8 = table.NewRow();
                    gg8["GameID"] = 4;
                    gg8["GenreID"] = 8;
                    table.Rows.Add(gg8);
                });
                Console.WriteLine("Game-Genre relations successfully created.");
                gameGenresCreated = true;
            }
        }
        /**
         * FUNCTION: CreateUsers
         * DESCRIPTION:
         * Inserts sample User records into SteamUser table.
         * Ensures users are only created once per program run by checking 
         * associated created flag. Uses DbHelper.InsertRows for insertion.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void CreateUsers() {
            if (usersCreated){
                Console.WriteLine("Users already created.");
            } else if (!DbHelper.EnsureTableExists("SteamUser")) {
                Console.WriteLine("Users table not found, create table first.");
            } else {

                DbHelper.InsertRows("SteamUser", table => {
                    DataRow u1 = table.NewRow();
                    u1["Name"] = "Terry";
                    u1["Email"] = "terryman@gmail.com";
                    u1["Address"] = "123 That Street";
                    u1["DateCreated"] = "2007-01-01";
                    u1["DateOfBirth"] = "1832-08-23";
                    u1["AccountBalance"] = 85.97;
                    table.Rows.Add(u1);

                    DataRow u2 = table.NewRow();
                    u2["Name"] = "Lina";
                    u2["Email"] = "lindaloo@fastmail.com";
                    u2["Address"] = "77 Forest Lane";
                    u2["DateCreated"] = "2012-03-14";
                    u2["DateOfBirth"] = "1979-11-05";
                    u2["AccountBalance"] = 12.40;
                    table.Rows.Add(u2);

                    DataRow u3 = table.NewRow();
                    u3["Name"] = "Marcus";
                    u3["Email"] = "marcusg@yahoo.com";
                    u3["Address"] = "904 Sunset Road";
                    u3["DateCreated"] = "2020-09-30";
                    u3["DateOfBirth"] = "1995-04-18";
                    u3["AccountBalance"] = 203.11;
                    table.Rows.Add(u3);
                });
                Console.WriteLine("Users Successfully Created.");
                usersCreated = true;
            }
        }
        /**
         * FUNCTION: CreateGameLibrary
         * DESCRIPTION:
         * Inserts sample GameLibrary records into GameLibrary table.
         * Ensures game libraries are only created once per program run by checking 
         * associated created flag. Uses DbHelper.InsertRows for insertion.
         * PARAMETERS:
         * None.
         * RETURNS:
         * None.
         */
        public static void CreateGameLibrary() {
            if (gameLibraryCreated){
                Console.WriteLine("GameLibrary already created.");
            } else if (!DbHelper.EnsureTableExists("GameLibrary")) {
                Console.WriteLine("GameLibrary table not found, create table first.");
            } else {
                if (!usersCreated){
                    Console.WriteLine("Users not yet created. Creating Users...");
                    CreateUsers();
                }
                if (!gamesCreated){
                    Console.WriteLine("Games not yet created. Creating Games...");
                    CreateGames();
                }

                DbHelper.InsertRows("GameLibrary", table => {
                    DataRow r1 = table.NewRow();
                    r1["UserID"] = 1;
                    r1["GameID"] = 1;
                    r1["PurchaseDate"] = "2009-06-09";
                    r1["HoursPlayed"] = 995.7;
                    table.Rows.Add(r1);

                    DataRow r2 = table.NewRow();
                    r2["UserID"] = 2;
                    r2["GameID"] = 2;
                    r2["PurchaseDate"] = "2010-01-12";
                    r2["HoursPlayed"] = 120.0;
                    table.Rows.Add(r2);

                    DataRow r3 = table.NewRow();
                    r3["UserID"] = 3;
                    r3["GameID"] = 3;
                    r3["PurchaseDate"] = "2015-09-05";
                    r3["HoursPlayed"] = 243;
                    table.Rows.Add(r3);

                    DataRow r4 = table.NewRow();
                    r4["UserID"] = 3;
                    r4["GameID"] = 4;
                    r4["PurchaseDate"] = DateTime.Parse("2019-05-14");
                    r4["HoursPlayed"] = 63539.99;
                    table.Rows.Add(r4);
                });
                Console.WriteLine("GameLibrary Successfully Created.");
                gameLibraryCreated = true;
            }
        }
    }
}
