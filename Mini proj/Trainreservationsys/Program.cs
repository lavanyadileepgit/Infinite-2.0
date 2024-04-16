using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace Trainreservationsys
{
    class Program
        {
            static void Main(string[] args)
            {
                string connectionString = "Data Source=ICS-LT-FQW1ZD3;Initial Catalog=train_reservation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Clear();
                    string[] frames = new string[]
           {
            @"
       
 _ _ _     _                      _                  _ _                   __ __ 
| | | |___| |___ ___ _____ ___   | |_ ___    ___  __|_| |_ _ _  __ _ _ ___|  |  |
| | | | -_| |  _| . |     | -_|  |  _| . |  |  _||. | | | | | ||. | | |_ -|__|__|
|_____|___|_|___|___|_|_|_|___|  | | |___|  |_| |___|_|_|_____|___|_  |___|__|__|
                                 |__|                             |___|          
   
",
            @"
 _ _ _     _                      _                  _ _                   __ __ 
| | | |___| |___ ___ _____ ___   | |_ ___    ___  __|_| |_ _ _  __ _ _ ___|  |  |
| | | | -_| |  _| . |     | -_|  |  _| . |  |  _||. | | | | | ||. | | |_ -|__|__|
|_____|___|_|___|___|_|_|_|___|  | | |___|  |_| |___|_|_|_____|___|_  |___|__|__|
                                 |__|                             |___|          
   
"
           };

                    for (int i = 0; i < 5; i++)
                    {
                        foreach (var frame in frames)
                        {
                            Console.Clear();
                            if (i % 2 == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                            Console.WriteLine(frame);
                            Thread.Sleep(500); 
                        }
                    }

                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("---------------------------------------------------------------");
                    Console.WriteLine("Select Option:");
                    Console.WriteLine("1. Admin Login");
                    Console.WriteLine("2. User Login");
                    Console.Write("Enter your choice: ");
                    Console.ResetColor();
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AdminLogin(connection);
                            break;
                        case "2":
                            UserLogin(connection);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            Console.ReadKey(); 
                            Main(args);
                            break;
                    }
                }
            }

            static void AdminLogin(SqlConnection connection)
            {
                Console.Clear();
                PrintHeader("Admin Login");

                Console.Write("Enter Admin Username: ");
                string username = Console.ReadLine();
                Console.Write("Enter Admin Password: ");
                string password = Console.ReadLine();

                using (SqlCommand command = new SqlCommand("AdminLoginProcedure", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }

                    if (dataTable.Rows.Count > 0)
                    {
                        string userType = dataTable.Rows[0]["UserType"].ToString();
                        if (userType == "Admin")
                        {
                            Console.WriteLine("Admin logged in successfully.");
                            Console.ReadLine();
                            AdminOptionsLoop(connection);
                        }
                        else
                        {
                            Console.WriteLine("Invalid admin login credentials.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid admin login credentials.");
                    }
                }

                Console.ReadKey(); 
            }

            static void AdminOptionsLoop(SqlConnection connection)
            {
                bool isAdmin = true;

                while (isAdmin)
                {
                    Console.Clear();
                    PrintHeader("Admin Options");

                    Console.WriteLine("Select Option:");
                    Console.WriteLine("1. Add Train");
                    Console.WriteLine("2. Update Train");
                    Console.WriteLine("3. Delete Train");
                    Console.WriteLine("4. Delete User");
                    Console.WriteLine("5. View Train");
                    Console.WriteLine("6. Exit");
                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddTrain(connection);
                            break;
                        case "2":
                            UpdateTrain(connection);
                            break;
                        case "3":
                            DeleteTrain(connection);
                            break;
                        case "4":
                            DeleteUser(connection);
                            break;
                        case "5":
                            ViewTrains(connection);
                            break;
                        case "6":
                            isAdmin = false; 
                            Console.WriteLine("Logged out.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }

                    Console.ReadLine();
                }
            }

            static void AddTrain(SqlConnection connection)
            {
                try
                {
                    Console.WriteLine("Enter Train Details:");
                    Console.Write("Enter Train Name: ");
                    string trainName = Console.ReadLine();
                    Console.Write("Enter Source: ");
                    string source = Console.ReadLine();
                    Console.Write("Enter Destination: ");
                    string destination = Console.ReadLine();
                    Console.Write("Enter first Class: ");
                    string firstClass = Console.ReadLine();
                    Console.Write("Enter second Class: ");
                    string secondClass = Console.ReadLine();
                    Console.Write("Enter sleeper Class: ");
                    string sleeperClass = Console.ReadLine();
                    Console.Write("Enter Total Berths: ");
                    int totalBerths;
                    while (!int.TryParse(Console.ReadLine(), out totalBerths))
                    {
                        Console.Write("Invalid input. Enter valid total berths: ");
                    }
                    Console.Write("Enter Available Berths: ");
                    int availableBerths;
                    while (!int.TryParse(Console.ReadLine(), out availableBerths))
                    {
                        Console.Write("Invalid input. Enter valid available berths: ");
                    }
                    Console.Write("Enter Train Status (active/inactive): ");
                    string status = Console.ReadLine();

                    if (status != "active" && status != "inactive")
                    {
                        Console.WriteLine("Invalid status. Status must be 'active' or 'inactive'.");
                        return;
                    }

                    using (SqlCommand command = new SqlCommand("AddTrainBasedOnStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TrainName", trainName);
                        command.Parameters.AddWithValue("@departurestation", source);
                        command.Parameters.AddWithValue("@arrivalstation", destination);
                        command.Parameters.AddWithValue("@firstclass", firstClass);
                        command.Parameters.AddWithValue("@secondclass", secondClass);
                        command.Parameters.AddWithValue("@sleeperclass", sleeperClass);
                        command.Parameters.AddWithValue("@totalberths", totalBerths);
                        command.Parameters.AddWithValue("@availableBerths", availableBerths);
                        command.Parameters.AddWithValue("@TStatus", status);

                        command.ExecuteNonQuery();
                        Console.WriteLine("Train added successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding train: {ex.Message}");
                }

                Console.ReadLine(); 
            }


            static void UpdateTrain(SqlConnection connection)
            {
                try
                {
                    Console.Write("Enter Train ID to update: ");
                    int trainId = int.Parse(Console.ReadLine());
                    Console.Write("Enter New Train Status (active/inactive): ");
                    string status = Console.ReadLine();

                    if (status != "active" && status != "inactive")
                    {
                        Console.WriteLine("Invalid status. Status must be 'active' or 'inactive'.");
                        return;
                    }

                    using (SqlCommand command = new SqlCommand("UpdateTrainStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TrainId", trainId);
                        command.Parameters.AddWithValue("@TrainStatus", status);
                        Console.WriteLine("Train updated successfully.");
                        Console.ReadLine();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating train: {ex.Message}");
                }

                Console.ReadKey(); 
            }

            static void DeleteTrain(SqlConnection connection)
            {
                try
                {
                    Console.Write("Enter Train ID to delete: ");
                    int trainId = int.Parse(Console.ReadLine());

                    using (SqlCommand command = new SqlCommand("DeleteTrain", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TrainId", trainId);
                        Console.WriteLine("Train deleted successfully.");
                        Console.ReadLine();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting train: {ex.Message}");
                }

                Console.ReadLine();
            }

            static void DeleteUser(SqlConnection connection)
            {
                try
                {
                    Console.Write("Enter User ID to delete: ");
                    int userId = int.Parse(Console.ReadLine());

                    using (SqlCommand command = new SqlCommand("DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);
                        Console.WriteLine("User deleted successfully.");
                        Console.ReadLine();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting user: {ex.Message}");
                }

                Console.ReadLine(); 
            }

            static void UserLogin(SqlConnection connection)
            {
                bool continueLogin = true;
                bool newUserCreated = false; 
                while (continueLogin)
                {
                    Console.Clear();
                    PrintHeader("User Login");

                    string username, password;
                    if (!newUserCreated)
                    {
                        Console.Write("Are you an existing user? (Y/N): ");
                        string existingUserInput = Console.ReadLine()?.Trim().ToUpper();

                        if (existingUserInput == "Y")
                        {
                            Console.Write("Enter User Username: ");
                            username = Console.ReadLine()?.Trim();
                            Console.Write("Enter User Password: ");
                            password = Console.ReadLine()?.Trim();

                            using (SqlCommand command = new SqlCommand("UserLoginProcedure", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Username", username);
                                command.Parameters.AddWithValue("@Password", password);

                                DataTable dataTable = new DataTable();
                                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                                {
                                    dataAdapter.Fill(dataTable);
                                }

                                if (dataTable.Rows.Count > 0)
                                {
                                    string userType = dataTable.Rows[0]["UserType"].ToString();
                                    if (userType == "User")
                                    {
                                        int userId = Convert.ToInt32(dataTable.Rows[0]["UserId"]);
                                        Console.WriteLine("User logged in successfully with UserId: " + userId);
                                        while (UserOptions(connection)) ;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid user login credentials.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid user login credentials.");
                                }
                            }
                        }
                        else if (existingUserInput == "N")
                        {
                            Console.Write("Enter User Username: ");
                            string newUsername = Console.ReadLine()?.Trim();
                            Console.Write("Enter User Password: ");
                            string newPassword = Console.ReadLine()?.Trim();

                            try
                            {
                                using (SqlCommand command = new SqlCommand("AddUser", connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@Username", newUsername);
                                    command.Parameters.AddWithValue("@Password", newPassword);
                                    command.ExecuteNonQuery();
                                    Console.WriteLine("User account created successfully.");
                                    newUserCreated = true;                                 }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error creating user account: " + ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter 'Y' or 'N'.");
                        }
                    }
                    else
                    {
                        while (UserOptions(connection)) ;
                        newUserCreated = false; 
                    }

                    Console.Write("Do you want to continue? (Y/N): ");
                    string continueInput = Console.ReadLine()?.Trim().ToUpper();
                    if (continueInput != "Y")
                    {
                        continueLogin = false;
                    }
                }
            }




            static bool UserOptions(SqlConnection connection)
            {
                Console.Clear();
                PrintHeader("User Options");

                Console.WriteLine("Select Option:");
                Console.WriteLine("1. Book Ticket");
                Console.WriteLine("2. View Trains");
                Console.WriteLine("3. Cancel Ticket");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BookTrainTickets(connection);
                        return true;
                    case "2":
                        ViewTrain(connection);
                        return true;
                    case "3":
                        CancelTicket(connection);
                        return true;
                    case "4":
                        Console.WriteLine("Logging out...");
                        return false;


                    default:
                        Console.WriteLine("Invalid choice.");
                        return true;
                }
            }

        private static void BookTrainTickets(SqlConnection connection)
        {
            Console.Write("Enter Train ID: ");
            if (!int.TryParse(Console.ReadLine(), out int trainid))
            {
                Console.WriteLine("Invalid Train ID format.");
                return;
            }

            string query = $"SELECT COUNT(1) FROM Trains WHERE TrainId = {trainid}";
            using (SqlCommand checkCommand = new SqlCommand(query, connection))
            {
                int count = (int)checkCommand.ExecuteScalar();
                if (count == 0)
                {
                    Console.WriteLine("Train ID not found.");
                    return;
                }
            }

            Console.Write("Enter Train Name: ");
            string trainName = Console.ReadLine();

            query = $"SELECT COUNT(1) FROM Trains WHERE TrainName = @TrainName";
            using (SqlCommand checkNameCommand = new SqlCommand(query, connection))
            {
                checkNameCommand.Parameters.AddWithValue("@TrainName", trainName);
                int count = (int)checkNameCommand.ExecuteScalar();
                if (count == 0)
                {
                    Console.WriteLine("Train name not found.");
                    return;
                }
            }

            Console.Write("Enter user ID: ");
            int userId;
            if (!int.TryParse(Console.ReadLine(), out userId))
            {
                Console.WriteLine("Invalid User ID format.");
                return;
            }

            DateTime bookedDate = GetDateFromCalendar();

            Console.Write("Enter number of tickets: ");
            if (!int.TryParse(Console.ReadLine(), out int numberOfTickets))
            {
                Console.WriteLine("Invalid number of tickets format.");
                return;
            }

            Console.Write("Enter class: ");
            string Tclass = Console.ReadLine();

            List<string> passengerNames = new List<string>();
            for (int i = 0; i < numberOfTickets; i++)
            {
                Console.Write($"Enter Passenger Name for ticket {i + 1}: ");
                passengerNames.Add(Console.ReadLine());
            }

            try
            {

                foreach (var passengerName in passengerNames)
                {
                    using (SqlCommand command = new SqlCommand("BookTrainTickets", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TrainId", trainid);
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@TrainName", trainName);
                        command.Parameters.AddWithValue("@BookingDate", bookedDate);
                        command.Parameters.AddWithValue("@NumberOfTickets", numberOfTickets);
                        command.Parameters.AddWithValue("@Class", Tclass);
                        command.Parameters.AddWithValue("@BookingId", SqlDbType.Int).Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        int bookingId = Convert.ToInt32(command.Parameters["@BookingId"].Value);
                        Console.WriteLine();

                        Console.WriteLine($"Ticket for {passengerName} booked successfully!");
                        Console.WriteLine();
                        DisplayBookedTicket(connection, bookingId, passengerNames);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error booking ticket: {ex.Message}");
            }
            Console.Read();
        


        DateTime GetDateFromCalendar()
                {
                    DateTime currentDate = DateTime.Now;
                    int currentDay = currentDate.Day;
                    int currentMonth = currentDate.Month;
                    int currentYear = currentDate.Year;

                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"Selected month: {currentDate.ToString("MMMM yyyy")}");
                        Console.WriteLine("Sun Mon Tue Wed Thu Fri Sat");

                        int startingDay = (int)new DateTime(currentYear, currentMonth, 1).DayOfWeek;
                        int totalDays = DateTime.DaysInMonth(currentYear, currentMonth);
                        int currentDayOfMonth = 1;

                        for (int i = 0; i < startingDay; i++)
                        {
                            Console.Write("    ");
                        }

                        while (currentDayOfMonth <= totalDays)
                        {
                            for (int i = startingDay; i < 7 && currentDayOfMonth <= totalDays; i++)
                            {
                                if (currentDayOfMonth == currentDay && currentMonth == currentDate.Month && currentYear == currentDate.Year)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }
                                Console.Write($"{currentDayOfMonth,3}");
                                if (currentDayOfMonth < 10)
                                {
                                    Console.Write(" ");
                                }
                                Console.ResetColor();
                                currentDayOfMonth++;
                            }
                            Console.WriteLine();
                            startingDay = 0;
                        }

                        Console.WriteLine("\nUse arrow keys to navigate the calendar. Press Enter to select the date.");

                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                currentDay--;
                                if (currentDay < 1)
                                {
                                    currentDate = currentDate.AddMonths(-1);
                                    currentDay = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
                                    currentMonth = currentDate.Month;
                                    currentYear = currentDate.Year;
                                }
                                break;
                            case ConsoleKey.RightArrow:
                                currentDay++;
                                if (currentDay > DateTime.DaysInMonth(currentYear, currentMonth))
                                {
                                    currentDate = currentDate.AddMonths(1);
                                    currentDay = 1;
                                    currentMonth = currentDate.Month;
                                    currentYear = currentDate.Year;
                                }
                                break;
                            case ConsoleKey.UpArrow:
                                currentDate = currentDate.AddMonths(-1);
                                currentMonth = currentDate.Month;
                                currentYear = currentDate.Year;
                                break;
                            case ConsoleKey.DownArrow:
                                currentDate = currentDate.AddMonths(1);
                                currentMonth = currentDate.Month;
                                currentYear = currentDate.Year;
                                break;
                            case ConsoleKey.Enter:
                                return currentDate; 
                        }
                    }
                }

            }
        private static void DisplayBookedTicket(SqlConnection connection, int bookingId, List<string> passengerNames)
        {
            using (SqlCommand command = new SqlCommand("DisplayBookedTicket", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookingId", bookingId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        Console.WriteLine($"Booking ID: {reader["BookingId"]}");
                        Console.WriteLine($"Train ID: {reader["TrainId"]}");
                        Console.WriteLine($"Train Name: {reader["Trainame"]}");
                        Console.WriteLine($"User ID: {reader["UserId"]}");
                        Console.WriteLine($"Booking Date: {reader["BookingDate"]}");
                        Console.WriteLine("Passenger Names:");
                       /* foreach (var passengerName in passengerNames)
                        {
                            Console.WriteLine($"- {passengerName}");
                        }*/
                        Console.WriteLine($"Number of Tickets: {reader["NumberOfTickets"]}");
                        Console.WriteLine($"Class: {reader["Class"]}");
                        
                    }
                    else
                    {
                        Console.WriteLine("Booking not found.");
                    }
                }
            }
        }
        static void ViewTrains(SqlConnection connection)
        {
            try
            {
                SqlCommand command = new SqlCommand("ViewTrains", connection);
                command.CommandType = CommandType.StoredProcedure;

                DataTable dataTable = new DataTable();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(dataTable);
                }

                if (dataTable.Rows.Count == 0)
                {
                    Console.WriteLine("No records found in the Train table.");
                    return;
                }

                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-15} {5,-10} {6,-10} {7,-10} {8,-10} {9,-10}", "Train ID", "Train Name", "Departure Station", "Arrival Station", "First Class Fare", "Second Class Fare", "Sleeper Class Fare", "Total Berths", "Available Berths", "Status");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-15} {5,-10} {6,-10} {7,-10} {8,-10} {9,-10}",
                        row["trainId"], row["trainName"], row["departurestation"], row["arrivalstation"],
                        row["firstclassfare"], row["secondclassfare"], row["sleeperclassfare"],
                        row["totalberths"], row["availableberths"], row["status"]);
                }

                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error viewing trains: " + ex.Message);
            }
            Console.Read();
        }



        static void ViewTrain(SqlConnection connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("ViewTrain", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }

                    if (dataTable.Rows.Count == 0)
                    {
                        Console.WriteLine("No records found in the Train table.");
                        return;
                    }

                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-15} {5,-10} {6,-10} {7,-10} {8,-10}", "Train ID", "Train Name", "departurestation", "arrivalstation", "firstClassfare", "secondclassFare", "sleeperclassfare", "Total Berths", "Available Berths", "Status");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------");

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-15} {5,-10} {6,-10} {7,-10} {8,-10}", row["trainId"], row["trainName"], row["departurestation"], row["arrivalstation"], row["firstclassfare"], row["secondclassfare"], row["sleeperclassfare"], row["totalberths"], row["availableberths"], row["status"]);
                    }

                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error viewing trains: " + ex.Message);
                }
                Console.Read();
            }


            static void CancelTicket(SqlConnection connection)
            {
                try
                {
                    Console.Write("Enter booking ID to cancel: ");
                    int bookId = int.Parse(Console.ReadLine());
                    Console.Write("Enter userID to cancel: ");
                    int userId = int.Parse(Console.ReadLine());

                    using (SqlCommand command = new SqlCommand("Cancel", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@bookingId", bookId);
                        command.Parameters.AddWithValue("@userId", userId);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Ticket canceled successfully.");
                            Console.Read();
                        }
                        else
                        {
                            Console.WriteLine("Failed to cancel ticket. Please check the Booking ID.");
                            Console.Read();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error canceling ticket: {ex.Message}");
                }
            }


            static void PrintHeader(string text)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("===============================================");
                Console.WriteLine($"  {text}");
                Console.WriteLine("===============================================");
                Console.ResetColor();
            }
        }
    }



