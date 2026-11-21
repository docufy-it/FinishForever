using FinishForever.Web.Components;
using FinishForever.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinishForever.Infrastructure.Configuration;
using FinishForever.Infrastructure.Services;
using ApexCharts;
using FinishForever.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<StravaOptions>(
    builder.Configuration.GetSection("Strava"));

builder.Services.AddHttpClient<StravaApiClient>();

builder.Services.AddScoped<StravaAthleteService>();
builder.Services.AddScoped<
    StravaActivityService>();
builder.Services.AddScoped<DashboardService>();


builder.Services.AddApexCharts();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<UserSessionService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db =
        scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

    db.Database.Migrate();
}
 
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
