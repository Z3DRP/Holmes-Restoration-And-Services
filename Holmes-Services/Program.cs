using Holmes_Services.Models;
using Microsoft.EntityFrameworkCore; 
using Newtonsoft.Json;

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
options.UseMySQL(connection)
.LogTo(Console.WriteLine, LogLevel.Information)
.EnableSensitiveDataLogging()
.EnableDetailedErrors());

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
