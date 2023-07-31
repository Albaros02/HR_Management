using HR_Management.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("V1", new OpenApiInfo { Title = "Mi API", Version = "V1" });
});
// builder.Services.AddApiVersioning(options =>
// {
//     options.ReportApiVersions = true;
//     options.AssumeDefaultVersionWhenUnspecified = true;
//     options.DefaultApiVersion = new ApiVersion(1, 0);
// });

//Added the TrustServerCertificate=True properties to the appsetting
builder.Services.AddDbContext<MVCEmployeesDbContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("EmployeesConnectionString")
        )
    );

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

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/V1/swagger.json", "Mi API");
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
