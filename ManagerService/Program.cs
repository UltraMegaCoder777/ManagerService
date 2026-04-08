using ManagerService.Data; // Пространство имен твоего контекста
using Microsoft.EntityFrameworkCore;
using ManagerService.Mappings; //for mapping


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// ===== РЕГИСТРАЦИЯ КОНТЕКСТА БАЗЫ ДАННЫХ =====
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// ============================================

// mapper registration
builder.Services.AddAutoMapper(cfg => { },
    typeof(ScheduledPracticeProfile));

// регистрация опенапи
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// тоже что-то для openapi
if (app.Environment.IsDevelopment())
{
    // 2. Генерируем OpenAPI JSON (доступен по адресу /openapi/v1.json)
    app.MapOpenApi();

    // 3. НАСТРАИВАЕМ Swagger UI так, чтобы он читал JSON, сгенерированный .NET 10
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
    });
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
