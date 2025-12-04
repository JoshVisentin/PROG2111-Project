/*
 * Authours: Josh, Trent
 * Start: 3/12/2025
 * End: 
 * Class: PROG2111
 * Assignment: PROJECT
 * Disription: 
 * Develop C# programs that connect to the MySQL database using the ADO.NET
• Perform CRUD operations (Create, Read, Update, Delete) for the entities defined
in your ERD.
• Ensure proper error handling and transaction control.
• Use the ADO.NET MySQL library to establish the connection and perform
operations. 
 */

using MySql.Data.MySqlClient;

internal class Program
{
    private static void Main(string[] args)
    {
        string connStr = "server=localhost;user=testuser;password=Password;database=steamdb;";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            Console.WriteLine("Connected!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Connection Error: " + ex.Message);
        }
    }
}