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

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
