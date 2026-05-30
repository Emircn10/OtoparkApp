using Microsoft.EntityFrameworkCore;
using OtoparkApp.Services;

var builder = WebApplication.CreateBuilder(args);

// DbContext kaydı - Invoice projesiyle aynı yapı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
