using ManagerService.Data; // Пространство имен твоего контекста
using ManagerService.Enums; // добавляем Enum-ы
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ===== РЕГИСТРАЦИЯ КОНТЕКСТА БАЗЫ ДАННЫХ =====
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// ============================================

//// ===== добавление Enum-ов =====
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(
//        connectionString,
//        npgsqlOptions =>
//        {
//            // Маппинг всех enum'ов из твоего проекта
//            npgsqlOptions.MapEnum<DocumentCheckStatus>("document_check_status");
//            npgsqlOptions.MapEnum<SigningDocumentStatus>("signing_document_status");
//            npgsqlOptions.MapEnum<StudentApplicationStatus>("student_application_status");
//        }
//    ));
//// ============================================

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

//// Автоматическое применение миграций при запуске
//using (var scope = app.Services.CreateScope())
//{
//    var supervisorDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    supervisorDb.Database.Migrate();
//}

app.Run();
