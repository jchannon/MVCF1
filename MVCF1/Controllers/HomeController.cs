using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MVCF1.API;
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

            DriverResults model = null;

            if (!string.IsNullOrWhiteSpace(response.ErrorMessage) || response.Data == null)
            {
                model = new DriverResults();
            }
            else
            {
                model = Mapper.Map<RootObject, DriverResults>(response.Data);
            }


            return View(model);
        }

    }
}
