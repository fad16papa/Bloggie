using Bloggie.Web.Data;
using Bloggie.Web.Repositories.Interfaces;
using Bloggie.Web.Repositories.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddDbContext<BloggieDbContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConntectionString"));
// });

builder.Services.AddDbContext<BloggieDbContext>(opt =>
{
   opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Dependecy Container
builder.Services.AddScoped<ITagInterface, TagService>();
builder.Services.AddScoped<IBlogPostInterface, BlogPostService>();
builder.Services.AddScoped<IImagesInterface, ImagesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var scope = app.Services.CreateAsyncScope();
var context = scope.ServiceProvider.GetRequiredService<BloggieDbContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    context.Database.Migrate();
    //DbInitializer.Initialize(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "A problem occured during migration");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
