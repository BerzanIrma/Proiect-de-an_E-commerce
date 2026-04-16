var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services
    .AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
        options.Cookie.Name = "StylishStore.Auth";
    });
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Lax;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<Proiect__de_an.Core.Lab5.Flyweight.ProductFlyweightFactory>();
builder.Services.AddSingleton<Proiect__de_an.Core.Lab6.Strategy.ProductSortStrategyFactory>();
// Lab6 Observer: EventManager + abonare observatori (Subscribe), ca în diagrama clasică
builder.Services.AddScoped<Proiect__de_an.Core.Lab6.Observer.LoggingCartObserver>();
builder.Services.AddScoped<Proiect__de_an.Core.Lab6.Observer.CartMetricsObserver>();
builder.Services.AddScoped<Proiect__de_an.Core.Lab6.Observer.CartEventManager>(sp =>
{
    var events = new Proiect__de_an.Core.Lab6.Observer.CartEventManager();
    events.Subscribe(sp.GetRequiredService<Proiect__de_an.Core.Lab6.Observer.LoggingCartObserver>());
    events.Subscribe(sp.GetRequiredService<Proiect__de_an.Core.Lab6.Observer.CartMetricsObserver>());
    return events;
});
builder.Services.AddScoped<Proiect__de_an.Services.CartService>();
builder.Services.AddScoped<Proiect__de_an.Services.ICartService>(sp =>
    new Proiect__de_an.Core.Lab5.Proxy.CartServiceProxy(
        sp.GetRequiredService<Proiect__de_an.Services.CartService>(),
        sp.GetRequiredService<Microsoft.Extensions.Logging.ILogger<Proiect__de_an.Core.Lab5.Proxy.CartServiceProxy>>(),
        sp.GetRequiredService<IHttpContextAccessor>()));
builder.Services.AddScoped<Proiect__de_an.Core.Lab4.Facade.ECommerceFacade>();
builder.Services.AddScoped<Proiect__de_an.Core.Lab6.Memento.CartOriginator>();
builder.Services.AddScoped<Proiect__de_an.Core.Lab6.Memento.CartCaretaker>();
builder.Services.AddScoped<Proiect__de_an.Core.Lab6.Command.CartCommandInvoker>();

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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
