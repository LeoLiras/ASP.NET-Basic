using Microsoft.EntityFrameworkCore;
using WebApplicationTest.Data;
using WebApplicationTest.Repositorio;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BancoContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
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
