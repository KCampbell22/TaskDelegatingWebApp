using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Web;
using TaskDelegatingWebApp.Data;
using AutoMapper.Internal.Mappers;
using AutoMapper;
using NuGet.Protocol;
using TaskDelegatingWebApp.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TaskDelegatingWebAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("appDB")));

builder.Services.AddScoped<WeekService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddMvc().AddNewtonsoftJson(options => {

    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
});

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.WriteIndented = true;

});

builder.Services.AddControllers(e =>
{
    e.RespectBrowserAcceptHeader = true;
});

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
