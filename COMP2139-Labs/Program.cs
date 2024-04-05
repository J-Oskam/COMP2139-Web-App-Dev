using COMP2139_Labs.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using COMP2139_Labs.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using COMP2139_Labs.Services;
using COMP2139_Labs.Enum;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//ensures whenever IemailSender is inejcted our instance of EmailSender is provided
builder.Services.AddSingleton<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithRedirects("/Home/NotFound?statusCode={0}");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

using var scope = app.Services.CreateScope();
var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

try {
    //get services needed for role seeding
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await ContextSeed.SeedRolesAsync(userManager, roleManager);
    await ContextSeed.SuperSeedRoleAsync(userManager, roleManager);
} catch (Exception e) {

    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(e, "Error occurred when seeding roles.");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapRazorPages();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Projects}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();