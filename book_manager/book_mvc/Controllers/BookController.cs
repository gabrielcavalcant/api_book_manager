using book_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace book_mvc.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44379");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BookViewModel> booksList = new List<BookViewModel>();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/Search/books");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    booksList = JsonConvert.DeserializeObject<List<BookViewModel>>(data);
                }
                else
                {
                    ViewBag.ErrorMessage = "Unable to retrieve books.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return View(booksList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _httpClient.PostAsync($"api/Search/{book.Titulo}/{book.Autor}/{book.Genero}/{book.Emprestimo}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Unable to add book.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                }
            }
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            BookViewModel book = null;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Search/book/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<BookViewModel>(data);
                }
                else
                {
                    ViewBag.ErrorMessage = "Unable to retrieve book.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _httpClient.PutAsync($"api/Search/{book.Id}/{book.Titulo}/{book.Autor}/{book.Genero}/{book.Emprestimo}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Unable to update book.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                }
            }
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            BookViewModel book = null;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Search/book/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<BookViewModel>(data);
                }
                else
                {
                    ViewBag.ErrorMessage = "Unable to retrieve book.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Search/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "Unable to delete book.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
