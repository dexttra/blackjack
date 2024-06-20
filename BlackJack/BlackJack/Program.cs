using Blackjack.Db;
using Blackjack.Db.Models;
using BlackJack.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("blackjack");

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IGame, Game>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IPlayerStatsRepository, PlayerStatsDbRepository>();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));

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

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
