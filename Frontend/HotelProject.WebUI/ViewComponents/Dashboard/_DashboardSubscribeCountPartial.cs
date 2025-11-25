using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelProject.WebUI.Dtos.FollowersDto;
using Newtonsoft.Json;

namespace HotelProject.WebUI.ViewComponents.Dashboard
{
    public class _DashboardSubscribeCountPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
          
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://instagram-profile1.p.rapidapi.com/getprofile/buseyyesil"),
                    Headers =
                    {
                        { "x-rapidapi-key", "96cdb7a12bmsh04191d939672cb0p14f07djsna9713b1ea7c2" },
                        { "x-rapidapi-host", "instagram-profile1.p.rapidapi.com" },
                    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    ResultInstagramFollowersDto resultInstagramFollowersDtos = JsonConvert.DeserializeObject<ResultInstagramFollowersDto>(body);
                    ViewBag.v1 = resultInstagramFollowersDtos.followers;
                    ViewBag.v2 = resultInstagramFollowersDtos.following;
                }
            }
            catch (Exception)
            {
                ViewBag.v1 = 0;
                ViewBag.v2 = 0;
            }

          
            try
            {
                var client2 = new HttpClient();
                var request2 = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://twitter32.p.rapidapi.com/profile?username=buseyyesil"),
                    Headers =
                    {
                        { "x-rapidapi-key", "96cdb7a12bmsh04191d939672cb0p14f07djsna9713b1ea7c2" },
                        { "x-rapidapi-host", "twitter32.p.rapidapi.com" },
                    },
                };
                using (var response2 = await client2.SendAsync(request2))
                {
                    response2.EnsureSuccessStatusCode();
                    var body2 = await response2.Content.ReadAsStringAsync();
                    ResultTwitterFollowersDto resultTwittterFollowersDto = JsonConvert.DeserializeObject<ResultTwitterFollowersDto>(body2);

                    ViewBag.v3 = resultTwittterFollowersDto?.data?.user_info?.followers_count ?? 0;
                    ViewBag.v4 = resultTwittterFollowersDto?.data?.user_info?.friends_count ?? 0;
                }
            }
            catch (Exception)
            {
                ViewBag.v3 = 0;
                ViewBag.v4 = 0;
            }

            // LINKEDIN - MANUEL
            ViewBag.v5 = 250;
            ViewBag.v6 = 180;

            return View();
        }
    }
}