using JMC.Portal.Business.PortalModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<IntranetContext>(options => options.UseSqlServer(configuration.GetConnectionString("IntranetContext")));
builder.Services.AddDbContext<PortalContext>(options => options.UseSqlServer(configuration.GetConnectionString("PortalContext")));
builder.Services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.IsEssential = true;
});
builder.Services.AddKendo();
builder.Services.AddControllersWithViews()
            // Maintain property names during serialization. See:
            // https://github.com/aspnet/Announcements/issues/194
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options => { options.LoginPath = "/logon"; }
    //    options.AutomaticAuthenticate = true;
    //options.AutomaticChallenge = true;
    //options.LoginPath = "/Home/Login";
    );
builder.Services.AddControllersWithViews();
builder.Services.AddMvc(m => m.EnableEndpointRouting = false);


builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
//builder.Services.AddKendo();
//builder.Services.AddSession();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=LogOn}/{id?}");
    //endpoints.MapControllerRoute(
    //   name: "default",
    //   pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
