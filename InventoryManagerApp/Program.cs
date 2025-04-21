using MedicineInventoryApp.Data;
using MedicineInventoryApp.Interfaces;
using MedicineInventoryApp.Interfaces.Repositories;
using MedicineInventoryApp.Repositories;
using Microsoft.EntityFrameworkCore;
using MedicineInventoryApp.Controllers;
using InventoryManagerApp.Interfaces.Export;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IExportService, ExportService>();


builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();                         
    app.UseSwaggerUI();                       
}

app.UseHttpsRedirection();

app.UseStaticFiles(); 

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Medicines}/{action=Index}/{id?}");

await app.RunAsync();
