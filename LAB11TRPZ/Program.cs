using LAB11TRPZ.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контекст базы данных в контейнер зависимостей
builder.Services.AddDbContext<Lab11DBContext>(options =>
    options.UseSqlServer("Data Source=ALEX;Initial Catalog=Lab11DB;Integrated Security=True;Pooling=False;TrustServerCertificate=True;"));

// Добавляем поддержку контроллеров и представлений
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Настраиваем конвейер обработки запросов
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Настраиваем маршруты для контроллеров
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Printer}/{action=Index}/{id?}");

app.Run();
