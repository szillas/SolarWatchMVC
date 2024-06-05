using SolarApp.Services.JsonProcessor;
using SolarApp.Services.Providers.CoordinateProvider;
using SolarApp.Services.Providers.SunriseSunsetProvider;
using SolarApp.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ICoordinateProvider, OpenWeatherCoordinateProvider>();
builder.Services.AddSingleton<ISunriseSunsetProvider, SunriseSunsetApi>();
builder.Services.AddSingleton<IJsonProcessor, JsonProcessor>();
builder.Services.AddScoped<ICityRepository, CityRepository>();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();