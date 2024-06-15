using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using book_mvc.Models;

namespace book_mvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44379"); // Altere para o endereço da sua API
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(loginModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await _httpClient.PostAsync("api/Authentication/Login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        // Aqui você pode armazenar ou processar a resposta conforme necessário
                        ViewBag.Message = responseData; // Exemplo: armazena a resposta em uma ViewBag

                        return RedirectToAction(nameof(Success)); // Redireciona para uma ação de sucesso
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Login failed. Please check your credentials.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                }
            }
            return View(loginModel);
        }

        public IActionResult Success()
        {
            // Ação de sucesso, por exemplo, redirecionar para uma página após o login bem-sucedido
            return View();
        }
    }
}
