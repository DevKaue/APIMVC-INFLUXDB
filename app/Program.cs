using app.Data;
using app.Helpers;
using app.Invocables;
using app.Repositories;
using app.Services;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<InfluxDBService>(); // Adiciona o serviço InfluxDBService

// You'll create this class soon :)
builder.Services.AddTransient<WriteRandomPlaneAltitudeInvocable>();
builder.Services.AddScheduler();

//Configs do Banco em SQLSERVER

// var connectionString = builder.Configuration.GetConnectionString("DataConnection");

//Connection String Banco 2
var connectionString2 = builder.Configuration.GetConnectionString("DataConnection2");


//BANCO 1
// builder.Services.AddDbContext<BancoContext>(opts =>
//     opts.UseSqlServer(connectionString));

//BANCO 2
builder.Services.AddDbContext<BancoContext>(opts =>
    opts.UseSqlServer(connectionString2));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISessions, Session>();


builder.Services.AddSession(x =>
{
    x.Cookie.HttpOnly = true;
    x.Cookie.IsEssential = true;
});

//Configuando HttpContext

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



//Configurando Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});


var app = builder.Build();

var serviceProvider = app.Services;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

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
app.MapControllers();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();