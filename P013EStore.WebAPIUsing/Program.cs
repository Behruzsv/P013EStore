using Microsoft.AspNetCore.Authentication.Cookies; // oturum i�lemleri i�in
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpClient();

// Uygulama admin paneli i�in oturum a�ma ayarlar�
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
	x.LoginPath = "/Admin/Login"; // oturum a�mayan kullan�c�lar�n giri�i i�in g�nderilece�i adres
	x.LogoutPath = "/Admin/Logout";
	x.AccessDeniedPath = "/AccessDenied"; // yetkilendirme ile ekrana eri�im hakk� olmayan kullan�c�lar�n g�nderilece�i sayfa 
	x.Cookie.Name = "Administrator"; //olu�acak kukinin ismi
	x.Cookie.MaxAge = TimeSpan.FromDays(1); // olu�acak kukinin ya�am s�resi(1 g�n)
}); // oturum i�lemleri i�in

// uygulama admin paneli i�in admin yetkilendirme ayarlar�
builder.Services.AddAuthorization(x =>
{
	x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin"));  // admin paneline giri� yapma yetkisine sahip olanlar� bu kuralla kontrol edece�iz
	x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User")); // admin d���nda yetkilendirme kullan�rsak bu kural� kullanabiliriz(siteye �ye giri�i yapanlar� �n y�zde bir panale eri�tirme gibi)
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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
		   name: "admin",
		   pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
		 );

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
