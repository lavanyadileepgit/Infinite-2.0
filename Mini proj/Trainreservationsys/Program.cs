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
                            if (i % 3 == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                            Console.WriteLine(frame);
                            Thread.Sleep(100); 
                        }
                    }

                    Console.ResetColor();
                
                   // Console.WriteLine("---------------------------------------------------------------");
                    Console.WriteLine("Select Option:");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Admin logged in successfully.");
                        Console.ResetColor();
                            Console.ReadLine();
                            AdminOptionsLoop(connection);
                        }
                        else
                        {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid admin login credentials.");
                        Console.ResetColor();
                        Console.ReadLine();
                        AdminLogin(connection);
                        }
                    }
                    else
                    {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid admin login credentials.");
                    Console.ResetColor();
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
                Console.WriteLine("6. View Booking Details");
                Console.WriteLine("7. View Cancel Details");
                Console.WriteLine("8. Exit");
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
                        ViewBookedTrain(connection);
                        break;
                    case "7":
                        viewcanceltrain(connection);
                        break;
                    case "8":
                        isAdmin = false;
                        Console.WriteLine("Logging out.");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice.");
                        Console.ResetColor();
                        break;
                }

                if (!isAdmin)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Main(new string[] { }); // Restart the main program
                }
                else
                {
                    Console.WriteLine();
                    Console.ReadKey();
                }
            }
        }

        static void viewcanceltrain(SqlConnection connection)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("viewctrain", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Cancelled Tickets:");
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-15} {4,-15} {5,-10}", "Cancel ID", "Booking ID", "User ID", "Train Number", "Refund Amount", "Number of Tickets");
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");

                        while (reader.Read())
                        {
                            Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-15} {4,-15} {5,-10}", reader["cancelid"], reader["bookid"], reader["userid"], reader["trainno"], reader["refund_amt"], reader["no_of_tickets"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error viewing cancelled tickets: {ex.Message}");
                Console.ResetColor();
            }
        }


        static void ViewBookedTrain(SqlConnection connection)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("viewBtrain", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Booked Tickets:");
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-10} {4,-10} {5,-15} {6,-25} {7,-10}", "Booking ID", "Train ID", "Train Name", "User ID", "Total Fare", "Class", "Booking Date", "Number of Tickets");
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");

                        while (reader.Read())
                        {
                            Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-10} {4,-10} {5,-15} {6,-25} {7,-10}", reader["BookingId"], reader["TrainId"], reader["trainame"], reader["UserId"], reader["TotalFare"], reader["Class"], reader["BookingDate"], reader["NumberOfTickets"]);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error viewing booked tickets: {ex.Message}");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid status. Status must be 'active' or 'inactive'.");
                    Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Train added successfully.");
                    Console.ResetColor();
                    }
                }
                catch (Exception )
                {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error adding train");
                Console.ResetColor();
                }

                Console.ReadLine(); 
            }


        static void UpdateTrain(SqlConnection connection)
        {
            try
            {
                Console.Write("Enter Train ID to update: ");
                int trainId = int.Parse(Console.ReadLine());

                // Check if the train is already active
                using (SqlCommand checkCommand = new SqlCommand("SELECT Status FROM Trains WHERE trainid = @TrainId", connection))
                {
                    checkCommand.Parameters.AddWithValue("@TrainId", trainId);
                    string currentStatus = checkCommand.ExecuteScalar()?.ToString();

                    if (currentStatus == "active")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Train is already in active state.");
                        Console.ResetColor();
                        Console.Write("Do you want to update the fares? (yes/no): ");
                        string updateFares = Console.ReadLine();

                        if (updateFares.ToLower() == "yes")
                        {
                            Console.Write("Enter New First Class Fare: ");
                            decimal firstClassFare = decimal.Parse(Console.ReadLine());

                            Console.Write("Enter New Second Class Fare: ");
                            decimal secondClassFare = decimal.Parse(Console.ReadLine());

                            Console.Write("Enter New Sleeper Class Fare: ");
                            decimal sleeperClassFare = decimal.Parse(Console.ReadLine());

                            using (SqlCommand updateFaresCommand = new SqlCommand("UPDATE Trains SET FirstClassFare = @FirstClassFare, SecondClassFare = @SecondClassFare, SleeperClassFare = @SleeperClassFare WHERE TrainId = @TrainId", connection))
                            {
                                updateFaresCommand.Parameters.AddWithValue("@TrainId", trainId);
                                updateFaresCommand.Parameters.AddWithValue("@FirstClassFare", firstClassFare);
                                updateFaresCommand.Parameters.AddWithValue("@SecondClassFare", secondClassFare);
                                updateFaresCommand.Parameters.AddWithValue("@SleeperClassFare", sleeperClassFare);
                                updateFaresCommand.ExecuteNonQuery();
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Fares updated successfully.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Fares not updated.");
                            Console.ResetColor();
                        }

                        Console.ReadLine();
                        return;
                    }
                    else if (currentStatus == "inactive")
                    {
                        Console.WriteLine("Train is currently inactive.");

                        Console.Write("Do you want to activate the train? (yes/no): ");
                        string activateTrain = Console.ReadLine();

                        if (activateTrain.ToLower() == "yes")
                        {
                            using (SqlCommand activateCommand = new SqlCommand("UPDATE Trains SET Status = 'active' WHERE TrainId = @TrainId", connection))
                            {
                                activateCommand.Parameters.AddWithValue("@TrainId", trainId);
                                activateCommand.ExecuteNonQuery();
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Train activated successfully.");
                            Console.ResetColor();

                            // Ask if the admin wants to update the class fares
                            Console.Write("Do you want to update the fares? (yes/no): ");
                            string updateFares = Console.ReadLine();

                            if (updateFares.ToLower() == "yes")
                            {
                                Console.Write("Enter New First Class Fare: ");
                                decimal firstClassFare = decimal.Parse(Console.ReadLine());

                                Console.Write("Enter New Second Class Fare: ");
                                decimal secondClassFare = decimal.Parse(Console.ReadLine());

                                Console.Write("Enter New Sleeper Class Fare: ");
                                decimal sleeperClassFare = decimal.Parse(Console.ReadLine());

                                using (SqlCommand updateFaresCommand = new SqlCommand("UPDATE Trains SET FirstClassFare = @FirstClassFare, SecondClassFare = @SecondClassFare, SleeperClassFare = @SleeperClassFare WHERE TrainId = @TrainId", connection))
                                {
                                    updateFaresCommand.Parameters.AddWithValue("@TrainId", trainId);
                                    updateFaresCommand.Parameters.AddWithValue("@FirstClassFare", firstClassFare);
                                    updateFaresCommand.Parameters.AddWithValue("@SecondClassFare", secondClassFare);
                                    updateFaresCommand.Parameters.AddWithValue("@SleeperClassFare", sleeperClassFare);
                                    updateFaresCommand.ExecuteNonQuery();
                                }

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Fares updated successfully.");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Fares not updated.");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Train not activated.");
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error updating train");
                Console.ResetColor();
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
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid train ID. Train not found.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Train deleted successfully.");
                        Console.ResetColor();
                    }

                    Console.ReadLine();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error deleting train");
                Console.ResetColor();
                Console.ReadLine();
            }
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
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid user ID. User not found.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("User deleted successfully.");
                        Console.ResetColor();
                    }

                    Console.ReadLine();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error deleting user");
                Console.ReadLine();
                Console.ResetColor();
            }
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
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("User logged in successfully with UserId: " + userId);
                                    Console.ReadLine();
                                    Console.ResetColor();
                                        while (UserOptions(connection)) ;
                                    }
                                    else
                                    {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid user login credentials.");
                                    Console.ResetColor();
                                    }
                                }
                                else
                                {
                                Console.ForegroundColor = ConsoleColor.Red;

                                Console.WriteLine("Invalid user login credentials.");
                                Console.ResetColor();
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
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("User account created successfully.");
                                Console.ResetColor();
                                    newUserCreated = true;                                 }

                            }
                            catch (Exception )
                            {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error creating user account: ");
                            Console.ResetColor();
                            }
                        }
                        else
                        {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter 'Y' or 'N'.");
                        Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Logging out...");
                    Console.ResetColor();
                        return false;


                    default:
                   Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice.");
                   Console.ResetColor();
                        return true;
                }
            }
        private static void DisplayActiveTrains(SqlConnection connection)
        {
            string query = "SELECT * FROM Trains WHERE Status = 'Active'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-15} {5,-15} {6,-15} {7,-15} {8,-15} {9,-10}", "Train ID", "Train Name", "Departure Station", "Arrival Station", "First Class Fare", "Second Class Fare", "Sleeper Class Fare", "Total Berths", "Available Berths", "Status");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                    while (reader.Read())
                    {
                        Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-15} {5,-15} {6,-15} {7,-15} {8,-15} {9,-10}",
                            reader["trainid"],
                            reader["TrainName"],
                            reader["DepartureStation"],
                            reader["ArrivalStation"],
                            reader["FirstClassFare"],
                            reader["SecondClassFare"],
                            reader["SleeperClassFare"],
                            reader["totalberths"],
                            reader["availableberths"],
                            reader["Status"]);
                    }
                }
            }

            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }



        private static void BookTrainTickets(SqlConnection connection)
        {
            DisplayActiveTrains(connection);

            Console.Write("Enter Train ID: ");
            if (!int.TryParse(Console.ReadLine(), out int trainid))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Train ID format.");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }

            string trainName;
            string query = $"SELECT TrainName FROM Trains WHERE TrainId = {trainid} AND Status = 'Active'";
            using (SqlCommand getNameCommand = new SqlCommand(query, connection))
            {
                trainName = getNameCommand.ExecuteScalar()?.ToString();
                if (string.IsNullOrEmpty(trainName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Train ID not found or train is inactive.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }
            }

            Console.Write("Enter user ID: ");
            int userId;
            if (!int.TryParse(Console.ReadLine(), out userId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid User ID format.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            DateTime bookedDate = GetDateFromCalendar();

            Console.Write("Enter number of tickets: ");
            if (!int.TryParse(Console.ReadLine(), out int numberOfTickets))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number of tickets format.");
                Console.ResetColor();
                Console.ReadLine();
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
                        command.Parameters.AddWithValue("@BookingDate", bookedDate);
                        command.Parameters.AddWithValue("@NumberOfTickets", numberOfTickets);
                        command.Parameters.AddWithValue("@Class", Tclass);
                        command.Parameters.Add("@BookingId", SqlDbType.Int).Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        int bookingId = Convert.ToInt32(command.Parameters["@BookingId"].Value);
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        DisplayBookedTicket(connection, bookingId, passengerName);
                        Console.WriteLine($"Ticket for {passengerName} booked successfully! Booking ID: {bookingId}");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error booking ticket: {ex.Message}");
                Console.ResetColor();
            }

            Console.ReadLine();
        
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

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nUse arrow keys ie up and down arroy to switch bw months , left and right arrow tos witch bw dates. Press Enter to select the date.");
                    Console.ResetColor();

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
                            DateTime selectedDate = new DateTime(currentYear, currentMonth, currentDay);
                            if (selectedDate.Date >= currentDate.Date)
                            {
                                return selectedDate;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Please select a date from today or in the future.");
                                Console.Read();
                                Console.ResetColor();
                            }
                            break;
                    }
                }
            }


        }
        private static void DisplayBookedTicket(SqlConnection connection, int bookingId, string passengerNames)
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
                        Console.WriteLine($"Train Name: {reader["TrainName"]}");
                        Console.WriteLine($"User ID: {reader["UserId"]}");
                        Console.WriteLine($"Booking Date: {reader["BookingDate"]}");
                        Console.WriteLine("Passenger Names:");
                        Console.WriteLine($"{passengerNames}");
                        Console.WriteLine($"Number of Tickets: {reader["NumberOfTickets"]}");
                        Console.WriteLine($"Class: {reader["Class"]}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Booking not found.");
                        Console.ResetColor();
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
            catch (Exception )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error viewing trains " );
                Console.ResetColor();
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
                catch (Exception )
                {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error viewing trains" );
                Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ticket canceled successfully.");
                        Console.ResetColor();
                            Console.Read();
                        }
                        else
                        {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed to cancel ticket. Please check the Booking ID.");
                        Console.ResetColor();
                            Console.Read();
                        }
                    }
                }
                catch (Exception )
                {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error canceling ticket");
                Console.ResetColor();
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



