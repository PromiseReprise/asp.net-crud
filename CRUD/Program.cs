﻿using CRUD;
using CRUD.Data;
using CRUD.Services.Darbuotojai;
using CRUD.Services.Pacientai;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<DuomenuGeneratorius>();
builder.Services.AddDbContext<DuomenuKontekstas>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IDarbuotojuService, DarboutojuService>();
builder.Services.AddScoped<IPacientuService, PacientuService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Pradzia/Klaida");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pradzia}/{action=Pradzia}/{id?}");

// Sukuriami pirminiai duomenys
DuomenuGeneratorius.GeneruotiDuomenis(app);
app.Run();