using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCApplication.Controllers;
using System.Data;

namespace MVCApplication.Models
{
    public class DbOperations
    {
        private readonly WebApplicationBuilder _applicationBuilder;

        string tablename = "UserInfo";
        string query = $"SELECT * FROM UserInfo";
        string query1 = $"INSERT INTO UserInfo( FirstName, LastName, Email, Pwd)values( 'Vimal', 'Mathew', 'vimal.j.mathew@gmail.com', 'Vimal@1996')";
        string query2 = $"CREATE  TABLE UserInfo(\r\n\tId INT IDENTITY(1,1) PRIMARY KEY,\r\n\tFirstName NVARCHAR(50),\r\n\tLastName NVARCHAR(50),\r\n\tEmail NVARCHAR(50),\r\n\tPwd NVARCHAR(50));";
        string query3 = $"IF OBJECT_ID('UserInfo','U') IS NULL\r\nCREATE  TABLE UserInfo(\r\n\tId INT IDENTITY(1,1) PRIMARY KEY,\r\n\tFirstName NVARCHAR(50),\r\n\tLastName NVARCHAR(50),\r\n\tEmail NVARCHAR(50),\r\n\tPwd NVARCHAR(50));";


        public DbOperations(WebApplicationBuilder applicationBuilder)
        {
            _applicationBuilder = applicationBuilder;

        }


        public async Task checkTable()
        {
            using (var connection = new SqlConnection(_applicationBuilder.Configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                using (var command3 = new SqlCommand(query3, connection))
                {
                    var result3 = await command3.ExecuteScalarAsync();
                    Console.WriteLine($"Table '{tablename}' created.");
                }

                using (var command = new SqlCommand(query, connection))
                {
                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        Console.WriteLine($"Table '{tablename}' is not empty.");
                    }
                    else
                    {
                        Console.WriteLine($"Table '{tablename}' is empty.");

                        using (var command1 = new SqlCommand(query1, connection))
                        {
                            var result1 = await command1.ExecuteScalarAsync();
                        }
                        Console.WriteLine("Data Entered successfully");
                    }
                }
                await connection.CloseAsync();
            }
        }


        //Db operation for login function

        public async Task<UserInfo> Login(string email)
        {
            int Id ;
            string FirstName = "";
            string LastName = "";
            string Email = "";
            string password = "";
            //string loginQuery = $"SELECT Pwd FROM UserInfo WHERE UserInfo.Email = @Email";
            string loginQuery = $"SELECT * FROM UserInfo WHERE UserInfo.Email = @Email";
            UserInfo user = new UserInfo();
            using (var connection = new SqlConnection(_applicationBuilder.Configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();


                using (var command3 = new SqlCommand(loginQuery, connection))
                {
                    command3.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.NVarChar) { Value = email });

                    using (var reader = await command3.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Access the columns in the current row using reader.GetXxx methods.
                            Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            LastName = reader.GetString(reader.GetOrdinal("LastName"));
                            Email = reader.GetString(reader.GetOrdinal("Email"));
                            
                            password = reader.GetString(reader.GetOrdinal("Pwd"));

                            user.Id = Id;
                            user.FirstName = FirstName; 
                            user.LastName = LastName;   
                            user.Email = Email;
                            user.Pwd = password;

                            //UserInfo user = new UserInfo(Id,FirstName,LastName,Email,password);
                            //Console.WriteLine($"{Id},{password}, {FirstName},{LastName},{Email}");
                            // Process the data for each row.
                            // You can add it to a list or perform any other action as needed.
                        }
                    }

                }
                await connection.CloseAsync();

            }
            return user;
            Console.WriteLine("inside login function");
        }

        public async Task<bool> Signup(string firstname, string lastname, string email, string password)
        {

            string signupquery = $"INSERT INTO UserInfo(FirstName,LastName,Email,Pwd)values(@Firstname,@Lastname,@Email,@Password)";

            using (var connection = new SqlConnection(_applicationBuilder.Configuration.GetConnectionString("DefaultConnection")))
            {
                using (var command4 = new SqlCommand(signupquery, connection))
                {
                    await connection.OpenAsync();
                    command4.Parameters.Add(new SqlParameter("@Firstname", SqlDbType.NVarChar) { Value = firstname });
                    command4.Parameters.Add(new SqlParameter("@Lastname", SqlDbType.NVarChar) { Value = lastname });
                    command4.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email });
                    command4.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = password });
                    int rowsAffected = await command4.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    await connection.CloseAsync();
                }
            }



        }

    }     
}


           