using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApiMVC.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebApiMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiMVCContext") ?? throw new InvalidOperationException("Connection string 'WebApiMVCContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
