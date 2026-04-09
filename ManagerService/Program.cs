using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using ManagerService.Data; // Пространство имен твоего контекста
using ManagerService.Mappings; //for mapping
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi; // Добавьте этот using


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ===== РЕГИСТРАЦИЯ КОНТЕКСТА БАЗЫ ДАННЫХ =====
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// ============================================

// mapper registration
builder.Services.AddAutoMapper(cfg => { },
    typeof(ScheduledPracticeProfile));

// ========== НАСТРОЙКА ВЕРСИОНИРОВАНИЯ ==========
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// ========== НАСТРОЙКА SWAGGER (ВМЕСТО AddOpenApi) ==========
builder.Services.AddSwaggerGen(options =>
{
    // Настройка Swagger для поддержки нескольких версий
    IApiVersionDescriptionProvider provider = builder.Services.BuildServiceProvider()
        .GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new OpenApiInfo
        {
            Title = $"ManagerService API {description.GroupName}",
            Version = description.ApiVersion.ToString(),
            Description = description.IsDeprecated ? "⚠️ This API version is deprecated" : ""
        });
    }
});

var app = builder.Build();

//app.UseHttpsRedirection();//переадресация на https
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

// ========== НАСТРОЙКА SWAGGER UI ==========
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"ManagerService API {description.GroupName.ToUpperInvariant()}"
            );
        }
    });
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
