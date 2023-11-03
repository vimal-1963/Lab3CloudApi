using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCApplication;
using MVCApplication.Controllers;
using MVCApplication.Models;
using System.Collections.Generic;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

 
var builder = WebApplication.CreateBuilder(args);
BuilderContainer.builder = builder;

var ctx =new  WebLeadsContext();


// Add services to the container.
builder.Services.AddControllersWithViews();



try
{
    builder.Services.AddDbContext<WebLeadsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    Console.WriteLine("Connecting to database WebLeads database");
    await new DbOperations(builder).checkTable();
    
}
catch (Exception ex)
{
    Console.WriteLine($"Error connecting to database:{ex.Message}");
}



/*
 async Task checkTable(WebApplicationBuilder builder)
{
    using (var connection = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")))
    {
        string tablename = "UserInfo";
        await connection.OpenAsync();
        string query = $"SELECT * FROM {tablename}";
        string query1 = $"INSERT INTO UserInfo(Id, FirstName, LastName, Email, Pwd)values(100, 'Vimal', 'Mathew', 'vimal.j.mathew@gmail.com', 'Vimal@1996')";
        string query2 = $"CREATE  TABLE UserInfo(\r\n\tId INT PRIMARY KEY,\r\n\tFirstName NVARCHAR(50),\r\n\tLastName NVARCHAR(50),\r\n\tEmail NVARCHAR(50),\r\n\tPwd NVARCHAR(50));";
        string query3 = $"IF OBJECT_ID('UserInfo','U') IS NULL\r\nCREATE  TABLE UserInfo(\r\n\tId INT PRIMARY KEY,\r\n\tFirstName NVARCHAR(50),\r\n\tLastName NVARCHAR(50),\r\n\tEmail NVARCHAR(50),\r\n\tPwd NVARCHAR(50));";


        using(var command3 = new SqlCommand(query3,connection))
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
                
                using(var command1  = new SqlCommand(query1, connection))
                {
                    var result1 = await command1.ExecuteScalarAsync();
                }
                Console.WriteLine("Data Entered successfully");
            }
        }
        
    }

}*/
builder.Services.AddDistributedMemoryCache(); // Use an in-memory cache for session data
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Set the session timeout
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();

