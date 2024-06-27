//Apply defaults for app, looks at appsettings.json, includes Ketrel and sets up IIS, makes wwwroot folder the folder for static content
using Microsoft.AspNetCore.Builder;
using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using BethanysPieShop.App;
using BethanysPieShop.App.Pages;
using Microsoft.AspNetCore.Identity;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

//changes made by the identity scaffolder: the way the c. string is retrieved
var connectionString = builder.Configuration.GetConnectionString("BethanysPieShopDbContextConnection") ?? throw new InvalidOperationException("Connection string 'BethanysPieShopDbContextConnection' not found.");



// Add/register services to the container - the order is not important:



//changes were made here also by the identity scaffolder
//adding Framework services using an extension method with the DbContext class als type parameter (nuget package needs to be installed)
//the string is the concatenation of "ConnectionStrings:" + "BethanysPieShopDbContextConnection" in appsettings
builder.Services.AddDbContext<BethanysPieShopDbContext>(options => { options.UseSqlServer(builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]); });

//scaffolder added: default identity to the collection. I twill bring in the most commonly used identity-related services
//it is specified that for storing the user data BethanysPieShopDbContext is used
//options => options.SignIn.RequireConfirmedAccount = true in the parenthesis of <IdentityUser> was deleted for testing purposes
builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<BethanysPieShopDbContext>();

//Adds a scoped service of the type specified in I... with an implementation type specified in Mock --> now from Database...
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//invoke the getCart Method
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sc => ShoppingCart.GetCart(sc));

//to be able to store session ids in cookies and work with a shopping cart
builder.Services.AddSession();

//damit user data (session id) in cookie gespeichert werden kann
builder.Services.AddHttpContextAccessor();

//MVC Service is introduced -> can be used
builder.Services.AddControllersWithViews()
    //ignore cycles der Tabellenverknüpfungen (Pie und Category) in der Datenbank, when using the Api: GetById()-Method
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

//Razor pages is introduced, added auth needed for checking out
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/CheckoutPage");
});

//Adding Blazor functionality (support for server-side rendering) + support for client-side interactivity with Blazor Server
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

//in this case not necessary, but when I create an API Project without using MVC (AddControllersWithViews())
//builder.Services.AddControllers();



var app = builder.Build();



// Configure the HTTP request pipeline. Middleware - Order IS important


//Following block: when app published: Show Error when exception and use Hsts (use of https for 30 days after visit)
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. Can be changed, verhindert zukünftige http Anfragen (für 30 Tage) nach dem ersten Besuch
//    app.UseHsts();
//}

//zwingt die App https zu verwenden - sofortige Umleitung zu https, normalerweise vor UseHsTs verwendet.
//app.UseHttpsRedirection();

//makes sure requests to static files are responded by using the content of the wwwroot folder
app.UseStaticFiles();

//enables session management
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

//shows the exception page
if (app.Environment.IsDevelopment()) 
{
    app.UseDeveloperExceptionPage();
}

//wird implizit verwendet, kann aber explizit verwendet werden, wenn ich es nicht am anfang der pipeline haben will
//app.UseRouting();

//Endpoint middleware - da ich Controller verwende, benötige ich kein app.UseEndpoint
//Ability to navigate to the pages/views, makes sure that ASP.NET Core is able to handle requests correctly
app.MapDefaultControllerRoute();
//the following is the above routing but detailed - its doing the same. The ? behind the id says, that it isnt necessary
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//req. for Blazor endpoints without it there would be an error when using blazor
app.UseAntiforgery();

//registers RP files found under the pages folder as endpoints, because they need af protection by default
app.MapRazorPages();

//making sure that Blazor content can be rendered + add support for the server-side interactive mode
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

//also not necessary, just if I create a standalone API
//app.MapControllers();



//Methode zum Erstbefüllen der Db (local db sowie Azure oder andere db)
DbInitializer.Seed(app);

app.Run();
