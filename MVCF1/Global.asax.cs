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

            Mapper.CreateMap<API.Driver, Models.Driver>()
        .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.givenName))
        .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.familyName));

            Mapper.CreateMap<API.DriverStanding, Models.DriverResults>()
                .ForMember(dest => dest.Driver, opt => opt.MapFrom(src => new List<Models.Driver> { Mapper.Map<API.Driver, Models.Driver>(src.Driver) }))
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.points))
                .ForMember(dest => dest.Season, opt => opt.Ignore());

            Mapper.CreateMap<API.StandingsList, Models.DriverResults>()
                .ForMember(dest => dest.Season, opt => opt.MapFrom(src => src.season))
                .ForMember(dest => dest.Driver, opt => opt.Ignore())
                .ForMember(dest => dest.Points, opt => opt.Ignore());

            Mapper.CreateMap<API.StandingsTable, Models.DriverResults>()
                .ForMember(dest => dest.Season, opt => opt.Ignore())
                .ForMember(dest => dest.Driver, opt => opt.Ignore())
                .ForMember(dest => dest.Points, opt => opt.Ignore());

            Mapper.CreateMap<API.MRData, Models.DriverResults>()
                .ForMember(dest => dest.Season, opt => opt.Ignore())
                .ForMember(dest => dest.Driver, opt => opt.Ignore())
                .ForMember(dest => dest.Points, opt => opt.Ignore());


            Mapper.CreateMap<API.RootObject, Models.DriverResults>()
                .ForMember(dest => dest.Season, opt => opt.Ignore())
                .ForMember(dest => dest.Driver, opt => opt.Ignore())
                .ForMember(dest => dest.Points, opt => opt.Ignore());
        }
    }


}