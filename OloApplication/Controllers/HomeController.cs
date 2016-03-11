using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json.Linq;
using OloApplication.Helpers;
using OloApplication.Models;

namespace OloApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string url = "http://files.olo.com/pizzas.json";
            IEnumerable<TopOrder> model;

            //Use WebClient to download json from Olo
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(url);

                //Parse using JSON.net
                JArray orders = JArray.Parse(json);

                //Group by toppings, order by count to get top 20
                //Had to use JToken.ToObject to convert the JArray to string[] so that toppings would group properly
                var topOrders = orders.GroupBy(o => o["toppings"].ToObject<string[]>(), new ArrayComparer<string>())
                                      .Select(group => new TopOrder { Toppings = group.Key, TimesOrdered = group.Count() })
                                      .OrderByDescending(x => x.TimesOrdered)
                                      .Take(20);

                model = topOrders;
            }
            return View(model);
        }

    }
}