using FirstMVCSQL.Data;
using System.Data;
using Microsoft.Data.SqlClient;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//conexión con la BBDD
var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

builder.Services.AddScoped<IContactsRepository, ContactsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
