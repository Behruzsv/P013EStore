using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace P013EStore.WebAPIUsing.Areas.Admin.Controllers
{
	public class MainController : Controller
	{
		[Area("Admin"), Authorize(Policy = "AdminPolicy")]

		public IActionResult Index()
		{
			return View();
		}
	}
}
