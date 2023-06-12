using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;

namespace P013EStore.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin"), Authorize(Policy = "AdminPolicy")]

	public class AppUsersController : Controller
	{
		private readonly HttpClient _httpClient; // _httpClient nesnesini kullanarak apı lere isstek gönderebiliriz
		private readonly string _apiAdresi = "https://localhost:7101/api/AppUsers"; // api adresini web api projesini çalıştırdığımızda adrs çubuğunda veya her hangi bir controllera istek atarak Request URL kısmında veya web api projensinde Properties altındaki launchSettings.json

		public AppUsersController(HttpClient httpClient)
		{
			_httpClient = httpClient; // _httpClient nesnesinin apiye ulaşması için api projesinin de bu projeyle birlikte çalışıyor olması lazım ! 
			// Aynı anda 2 projeyi çalıştırabilmek için Solution a sağ tıklayıp açılan menüden se configure startuyp prejects diyip açılan ekrandan multiple alanına tıklayıp  aynı anda başlatmak istediğimiz projeleri seçiyoruz!
		}

		// GET: AppUsersController
		public async Task<ActionResult> Index()
		{
			var model = await _httpClient.GetFromJsonAsync<List<AppUser>>(_apiAdresi); // _apiAdresi nesnesi içindeki GetFromJsonAsync metodu kendisine verdiğimiz _apiAdresi deki url e get isteği gönderir ve oradan gelen jsno formatındaki app user listesini List<AppUser> nesnesine dönüştürür
			return View(model);
		}

		// GET: AppUsersController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: AppUsersController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: AppUsersController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateAsync(AppUser collection)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync(_apiAdresi, collection);
				if (response.IsSuccessStatusCode) // api den başaşrılı bir istek kodu geldiyse (200 ok)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			catch
			{
				ModelState.AddModelError("", "Hata Oluştu!");
			}
			return View(collection);
		}

		// GET: AppUsersController/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdresi + "/" + id);
			return View(model);
		}

		// POST: AppUsersController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, AppUser collection)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync(_apiAdresi, collection);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ModelState.AddModelError("", "Hata Oluştu!");
			}
			return View();
		}

		// GET: AppUsersController/Delete/5
		public async Task<ActionResult> DeleteAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdresi + "/" + id);
			return View();
		}

		// POST: AppUsersController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
		{
			try
			{
				await _httpClient.DeleteAsync(_apiAdresi + "/" + id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
