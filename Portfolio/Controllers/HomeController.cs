using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;



        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.People = from people in _context.People select people;
            return View();
        }
        //
        public IActionResult About()
        {
            return View();
        }
        public async Task<IActionResult> GetDef(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                ViewBag.Definitions = new List<string> { "Please enter a word." };
                return View("Index");
            }

            string apiUrl = $"https://api.dictionaryapi.dev/api/v2/entries/en/{word}";
            List<WordModel> definitions = new List<WordModel>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Definitions = new List<string> { "Could not fetch definition." };
                    return View("Index");
                }

                var jsonData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("RESPONSE: "+jsonData);
                definitions = JsonConvert.DeserializeObject<List<WordModel>>(jsonData);

                if (definitions != null && definitions.Count > 0)
                {
                    var allDefinitions = new List<string>();

                    foreach (var meaning in definitions[0].Meanings)
                    {
                        foreach (var definition in meaning.Definitions)
                        {
                            if (!string.IsNullOrWhiteSpace(definition.DefinitionText))
                            {
                                allDefinitions.Add(definition.DefinitionText);
                            }
                        }
                    }

                    ViewBag.Definitions = allDefinitions;
                }
                else
                {
                    ViewBag.Definitions = new List<string> { "No definition found." };
                }
            }

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(Person person)
        {
            if (ModelState.IsValid)
            {
                _context.People.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction("Thanks"); 
            }

            return View(person);
        }

        public IActionResult Thanks()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
