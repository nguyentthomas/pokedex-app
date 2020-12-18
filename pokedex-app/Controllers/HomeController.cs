using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using pokedex_app.Models;

namespace pokedex_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Create request to API
            WebRequest request = WebRequest.Create("http://pokeapi.co/api/v2/pokemon/534/");

            // Send that request out
            WebResponse response = request.GetResponse();

            // Get back response stream
            Stream stream = response.GetResponseStream();

            // Make Accessible
            StreamReader reader = new StreamReader(stream);

            // Parse as string, that is json formatted
            string responseFromServer = reader.ReadToEnd();

            JObject parsedString = JObject.Parse(responseFromServer);
            Pokemon myPokemon = parsedString.ToObject<Pokemon>();

            //Testing if it works... - Console.WriteLine(myPokemon.moves[0].move.name);

            return View(myPokemon);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
