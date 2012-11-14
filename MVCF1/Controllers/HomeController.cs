using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCF1.Models;
using RestSharp;

namespace MVCF1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var client = new RestClient("http://ergast.com");

            var request = new RestRequest("api/f1/" + DateTime.Now.Year + "/driverStandings.json", Method.GET);

            request.AddHeader("Accept", "application/json");

            var response = client.Execute<RootObject>(request);

            RootObject model = null;

            if (!string.IsNullOrWhiteSpace(response.ErrorMessage) || response.Data == null)
                model = new RootObject();
            else
                model = response.Data;



            return View(model);
        }

    }
}
