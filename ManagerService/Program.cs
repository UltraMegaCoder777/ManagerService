using ManagerService.Data; // Пространство имен твоего контекста
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//// Добавьте middleware для работы с proxy-заголовками
//builder.Services.Configure<ForwardedHeadersOptions>(options =>
//{
//    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
//});

// Add services to the container.
builder.Services.AddControllersWithViews();

// ===== РЕГИСТРАЦИЯ КОНТЕКСТА БАЗЫ ДАННЫХ =====
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// ============================================

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseForwardedHeaders(); // Используйте forwarded headers middleware ПЕРЕД другими middleware

//// Отключите HTTPS редирект для production (или настройте conditional)
//if (!app.Environment.IsDevelopment())
//{
//    app.UseHttpsRedirection();
//}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
