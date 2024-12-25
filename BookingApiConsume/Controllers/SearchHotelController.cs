using BookingApiConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApiConsume.Controllers
{
    public class SearchHotelController : Controller
    {
        public IActionResult Index()
        {
            var cities = new List<SelectListItem>
            {
                new SelectListItem { Text = "İstanbul", Value = "-755070" },
                new SelectListItem { Text = "Roma", Value = "-126693" },
                new SelectListItem { Text = "Paris", Value = "-1456928" },
                new SelectListItem { Text = "London", Value = "-2601889" },
                new SelectListItem { Text = "New York", Value = "20088325" },
                new SelectListItem { Text = "Berlin", Value = "-1746443" },
                new SelectListItem { Text = "Prag", Value = "-553173" },
                new SelectListItem { Text = "Kopenhag", Value = "-2745636" }
            };

            ViewBag.Cities = cities;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchHotels(string city, string arrival_date, string departure_date, int adults)
        {

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id={city}&search_type=CITY&arrival_date={arrival_date}&departure_date={departure_date}&adults={adults}&children_age=0%2C17&room_qty=1&page_number=1&units=metric&temperature_unit=c&languagecode=en-us&currency_code=AED"),
                    Headers =
            {
                { "x-rapidapi-key", "346952ad39msh7c6fa779b7c03ccp1d1f3ajsnaf29fa8a1e4e" },
                { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
            },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    // JSON verisini modele deserialize ediyoruz
                    var hotelData = JsonConvert.DeserializeObject<SearchHotelViewModel>(body);

                    // Modele View'da erişmek için ViewBag yerine Model ile gönderiyoruz
                    return View("HotelResults", hotelData);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Hata oluştu: {ex.Message}";
                return View("HotelResults");
            }
        }


        public IActionResult HotelResults()
        {
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> GetHotelDetails(int hotelId, string arrivalDate, string departureDate, int adults, string photoUrl)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/getHotelDetails?hotel_id={hotelId}&arrival_date={arrivalDate}&departure_date={departureDate}&adults={adults}&room_qty=1&units=metric&temperature_unit=c&languagecode=en-us&currency_code=EUR"),
                    Headers =
            {
                { "x-rapidapi-key", "346952ad39msh7c6fa779b7c03ccp1d1f3ajsnaf29fa8a1e4e" },
                { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
            },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    // JSON'u HotelDetails modeline deserialize et
                    var hotelDetails = JsonConvert.DeserializeObject<HotelDetailsViewModel>(body);
                    hotelDetails.photoUrl = photoUrl;

                    return View("GetHotelDetails", hotelDetails);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Hata oluştu: {ex.Message}";
                return RedirectToAction("HotelResults");
            }
        }







    }
}
