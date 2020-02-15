Refered Blocks and Docs while creating this project 

https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/migrations?view=aspnetcore-3.1
https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
https://docs.microsoft.com/en-us/ef/core/modeling/keyless-entity-types
https://medium.com/@balramchavan/setup-entity-framework-core-for-mysql-in-asp-net-core-2-5b40a5a3af94
https://github.com/dotnet/efcore/issues/17788

dotnet ef command was not found then i have installed
https://stackoverflow.com/questions/56862089/cannot-find-command-dotnet-ef

commands need to fire 
Scaffold-DbContext "server=127.0.0.1;port=32769;user=root;password=root;database=MilkService" Pomelo.EntityFrameworkCore.MySql -OutputDir Temp -f


dotnet ef migrations add InitialCreate
dotnet ef database update


installed packages
Pomelo.EntityFrameworkCore.MySql