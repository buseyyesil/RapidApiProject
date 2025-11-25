using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiConsume.Models;

namespace RapidApiConsume.Controllers
{
    public class BookingByCityController : Controller
    {
        public async Task<IActionResult> Index(string cityID)
        {
            try
            {
                // Eğer cityID boşsa varsayılan değer ver
                if (string.IsNullOrEmpty(cityID))
                {
                    cityID = "-1456928"; // Varsayılan şehir ID
                }

                // Bugünden 7 gün sonra check-in
                var checkinDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
                // 10 gün sonra check-out
                var checkoutDate = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v2/hotels/search?checkin_date={checkinDate}&checkout_date={checkoutDate}&filter_by_currency=TRY&dest_id={cityID}&room_number=1&units=metric&dest_type=city&locale=tr&adults_number=2&order_by=popularity&page_number=0&children_number=2&include_adjacency=true&children_ages=5%2C0&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "630ce9cc86msh271c60cffe62d5ep1b514djsn0fe292593744" },
                        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
                    },
                };

                using (var response = await client.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorBody = await response.Content.ReadAsStringAsync();
                        ViewBag.Error = $"API Hatası ({response.StatusCode}): {errorBody}";
                        return View(new List<BookingApiViewModel.Result>());
                    }

                    var body = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<BookingApiViewModel>(body);

                    if (values?.results == null)
                    {
                        ViewBag.Error = "Bu şehir için otel bulunamadı.";
                        return View(new List<BookingApiViewModel.Result>());
                    }

                    return View(values.results.ToList());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Hata: {ex.Message}";
                return View(new List<BookingApiViewModel.Result>());
            }
        }
    }
}
