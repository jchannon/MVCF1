using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using MVCF1.API;
using MVCF1.Models;
using Driver = MVCF1.API.Driver;

namespace MVCF1
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.CreateMap<MVCF1.API.Driver, MVCF1.Models.Driver>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.givenName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.familyName));

            Mapper.CreateMap<MVCF1.API.DriverStanding, MVCF1.Models.DriverResults>();

            Mapper.CreateMap<MVCF1.API.RootObject, MVCF1.Models.DriverResults>();
        }
    }

   
}