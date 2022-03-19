using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TestProjectWeb.Data;
using TestProjectWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(diContainer => 
    diContainer.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ));

builder.Services.AddScoped<UserRepository>(diContainer => 
    { 
        var applicationDbContext = diContainer.GetService<ApplicationDbContext>();
        var repository = new UserRepository(applicationDbContext);
        return repository;
    });
builder.Services.AddScoped<WordRepository>(diContainer =>
{
    var applicationDbContext = diContainer.GetService<ApplicationDbContext>();
    var repository = new WordRepository(applicationDbContext);
    return repository;
});

builder.Services.AddScoped<UserService>(diContainer => 
    new UserService(diContainer.GetService<UserRepository>(), 
                    diContainer.GetService<IHttpContextAccessor>()
)); 

builder.Services.AddAuthentication("AuthCookie")
    .AddCookie("AuthCookie", config => {
        config.LoginPath = "/User/Login";
        config.AccessDeniedPath = "/User/AccessDenied";
        config.Cookie.Name = "userInfo";
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

app.UseAuthentication();//���?
app.UseAuthorization();//����� �� �����?

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();