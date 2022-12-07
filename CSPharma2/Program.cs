using CSPharma_DAL.Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CSPharma2.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<cspharma_informacionalContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("EFCConexion")));

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<LoginContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("EFCConexion"));
    });

builder.Services.AddDefaultIdentity<UserAuth>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LoginContext>();
    

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
