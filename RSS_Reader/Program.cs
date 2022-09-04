using System.Net;
using RSS_Reader.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddXmlFile("appconfig.xml");

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRssParserService, RssParserService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Rss}/{action=Index}/");

app.Run();
