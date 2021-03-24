using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Wabisabi.database;
using Wabisabi.Models;

namespace Wabisabi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string connectionString = "Server=172.16.160.21;Port=3306;Database=110146;Uid=110146;Pwd=inf2021sql;";

        private List<Dish> GetDishes()
        {
            List<Dish> dishes = new List<Dish>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Dish", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Dish d = new Dish
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Prijs = Decimal.Parse(reader["Prijs"].ToString()),
                            Afbeelding = reader["Afbeelding"].ToString(),

                        };
                        dishes.Add(d);
                    }
                }
            }

            return dishes;
        }

        private Country GetCountry(string naam)
        {
            List<Country> countries = new List<Country>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from country where naam = '{naam}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Country t = new Country
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["Beschrijving"].ToString(),
                            Afbeelding1 = reader["Afbeelding1"].ToString(),
                            Afbeelding2 = reader["Afbeelding2"].ToString(),
                            Afbeelding3 = reader["Afbeelding3"].ToString(),
                            Afbeelding4 = reader["Afbeelding4"].ToString(),
                        };
                        countries.Add(t);
                    }
                }
            }
            return countries.FirstOrDefault();
        }


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("country/{id}")]
        public IActionResult Country(string id)
        {
            var model = GetCountry(id);

            return View(model);
        }

        [Route("Bestel")]
        public IActionResult Bestel()
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
