using Microsoft.EntityFrameworkCore;
using ThucPham.Models;
using ThucPham.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the database context and register services
var connectionString = builder.Configuration.GetConnectionString("QlThucPhamHuuCoContext");
builder.Services.AddDbContext<QlthucPhamHuuCoContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IDanhMucRepository, DanhMucRepository>();
builder.Services.AddSession();
var app = builder.Build(); // Build the app after registering all services


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HSTS for production
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
