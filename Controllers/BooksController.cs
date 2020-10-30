using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment1.Data;
using Assignment1.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Assignment1.Controllers
{
    public class BooksController : Controller
    {
        const string URL = "https://www.googleapis.com/books/v1/volumes?q=harry+potter";

        private readonly ILogger<HomeController> _logger;

        private readonly IHttpClientFactory _clientFactory;

        List<Book> books = new List<Book>();

        public BooksController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        // GET: Books
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{URL}");
            message.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                dynamic dynamicList = JObject.Parse(content);
                var items = dynamicList.items;
                foreach (var item in items)
                {
                    Book book = new Book();

                    book.Id = item.id;
                    book.Title = item.volumeInfo.title;
                    book.Authors = item.volumeInfo.authors.ToString();
                    book.Publisher = item.volumeInfo.publisher;
                    book.PublishedDate = item.volumeInfo.publishedDate;
                    book.Description = item.volumeInfo.description;
                    book.Isbn10 = item.volumeInfo.industryIdentifiers[1].identifier;
                    book.SmallThumbnail = item.volumeInfo.imageLinks.smallThumbnail;

                    books.Add(book);
                }
            }

            ViewBag.Books = books;
            return View(books);
        }

        // GET: Books/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{URL}");
            message.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(message);

            Book book = new Book();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                dynamic dynamicList = JObject.Parse(content);
                var items = dynamicList.items;
                foreach (var item in items)
                {
                    if (item.id == id)
                    {
                        book.Id = item.id;
                        book.Title = item.volumeInfo.title;
                        book.Authors = item.volumeInfo.authors.ToString();
                        book.Publisher = item.volumeInfo.publisher;
                        book.PublishedDate = item.volumeInfo.publishedDate;
                        book.Description = item.volumeInfo.description;
                        book.Isbn10 = item.volumeInfo.industryIdentifiers[1].identifier;
                        book.SmallThumbnail = item.volumeInfo.imageLinks.smallThumbnail;

                        break;
                    }
                }
            }

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}