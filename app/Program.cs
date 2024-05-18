// using app.Invocables;
// using app.Services;
// using Coravel;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;

// var builder = WebApplication.CreateBuilder(args);

// // Adiciona serviços ao contêiner.
// builder.Services.AddControllersWithViews();
// builder.Services.AddSingleton<InfluxDBService>(); // Adiciona o serviço InfluxDBService
// builder.Services.AddTransient<WriteRandomPlaneAltitudeInvocable>();
// builder.Services.AddScheduler();

// var app = builder.Build();

// var serviceProvider = app.Services;

// serviceProvider.UseScheduler(scheduler =>
// {
//     scheduler
//         .Schedule<WriteRandomPlaneAltitudeInvocable>()
//         .EveryFiveSeconds();
// });

// // Configure o pipeline de solicitação HTTP.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // O valor padrão do HSTS é de 30 dias. Você pode querer mudar isso para cenários de produção, veja https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();

// app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// app.Run();
using app.Data;
using app.Invocables;
using app.Repositories;
using app.Services;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<InfluxDBService>(); // Adiciona o serviço InfluxDBService

// You'll create this class soon :)
builder.Services.AddTransient<WriteRandomPlaneAltitudeInvocable>();
builder.Services.AddScheduler();

//Configs do Banco em SQLSERVER

var connectionString = builder.Configuration.GetConnectionString("DataConnection");

builder.Services.AddDbContext<BancoContext>(opts =>
    opts.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

var serviceProvider = app.Services;

serviceProvider.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<WriteRandomPlaneAltitudeInvocable>()
        .EveryFiveSeconds();
});


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
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();