using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.Models;
using RestSharp;
using System.Diagnostics;
using System.Net;
using System.Collections.Generic;

namespace MyFirstWebApp.Controllers
{
	public class HomeController : Controller
	{
		RestClient client = new RestClient("http://localhost:5205");

		public IActionResult Index()
		{

			RestRequest request = new RestRequest("/Player", Method.Get);
			//RestResponse<Player> player = client.ExecuteGet<Player>(request);
			List<Player> player = client.Get<List<Player>>(request);

			return View(player);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreatePlayerModel model)
		{

			if (ModelState.IsValid)
			{
				RestRequest request = new RestRequest("/Player", Method.Post);
				request.AddJsonBody(model);
				RestResponse<Player> response = client.ExecutePost<Player>(request);

				if (response.StatusCode != HttpStatusCode.Created)
				{
					ModelState.AddModelError("", "Servis Erişim Hatası");

					return View(model);
				}
			}

			return RedirectToAction("Index");
		}

		[HttpGet]

		public IActionResult Edit(int id)
		{
			RestRequest request = new RestRequest($"/Player/{id}", Method.Get);
			RestResponse<Player> response = client.ExecuteGet<Player>(request);


			if (response.StatusCode != HttpStatusCode.OK)
			{
				return RedirectToAction("Index");
			}

			return View(response.Data);
		}

		[HttpPost]
		public IActionResult Edit(int id, Player model)
		{
            RestRequest request = new RestRequest($"/Player/{id}", Method.Put);
			request.AddJsonBody(model);
            RestResponse response = client.ExecutePut<Player>(request);

			if (response.IsSuccessful)
			{
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError("", "Güncelleme Yapılamadı.");
			}


            return View(model);
		}

		public IActionResult Delete(int id)
		{

			RestRequest request = new RestRequest($"/Player/{id}", Method.Delete);
			RestResponse response = client.Execute(request);

			if(response.StatusCode == HttpStatusCode.NotFound)
			{
				TempData["result"] = "Kayıt Bulunamamıştır.";
			}
			else
			{
				TempData["result"] = "Kayıt Silinmiştir.";
			}
			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}