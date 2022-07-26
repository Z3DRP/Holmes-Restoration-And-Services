using Holmes_Services.Models;
using Microsoft.EntityFrameworkCore; 
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// get the connection from appsetting eventually need to make it
// use user secrets
string connection = builder.Configuration.GetConnectionString("HolmesContext");

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddDbContext<HolmesContext>(options =>
{
options.UseMySql(connection, ServerVersion.AutoDetect(connection),
    mySqlOptions => mySqlOptions.EnableRetryOnFailure(
        maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)
);
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
app.UseSession();
app.UseAuthorization();


app.MapControllerRoute(
    name: "",
    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}/filter/{author}/{genre}/{price}");
app.MapControllerRoute(
    name: "",
    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
