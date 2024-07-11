# AuthApi
Console application for authentication and notification
This project consists of a console application in .NET C# and a backend using ASP.NET Core that provides user authentication and sending notifications via SignalR.

Requirements
.NET SDK 6.0 or later
SQL Server
SQL Server is installed and running
Microsoft Visual Studio or any other development environment for C#
Configuring and starting the server part
1. Cloning the repository:
git clone https://github.com/LeontievVlad/AuthApi
cd your-repository-folder
2. Database settings:
Open the appsettings.json file and change the database connection string:
"ConnectionStrings": {
 "DefaultConnection": "Server=your_sql_server;Database=your_database;User Id=your_username;Password=your_password;"
}
3. Installation of dependencies:
Open a terminal in the root of the project and run the command:
dotnet restore
4. Database migration:
Run the commands to apply migrations and create a database:
dotnet ef migrations add InitialCreate
dotnet ef database update
5. Starting the server part:
Run the command to start the server part:
dotnet run
The server will start at https://localhost:5001.
