using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MVCF1.API;
using NUnit.Framework;

namespace MVCF1.Tests
{
    public class DriverResultsMapping
    {
        [Test]
        public void AutoMapper_Configuration_IsValid()
        {
            Mapper.Initialize(m =>
            {
                m.AllowNullDestinationValues = true;
                m.AllowNullCollections = true;
            });
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void AutoMapper_DriverMapping_IsValid()
        {
            Mapper.Initialize(m =>
            {
                m.AllowNullDestinationValues = true;
                m.AllowNullCollections = true;
            });


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

            Mapper.AssertConfigurationIsValid();

           // var apiDriver = new API.Driver { familyName = "Dude", givenName = "Cool" };
            //var modelDriver = Mapper.Map<API.Driver, Models.Driver>(apiDriver);

            RootObject root = new RootObject
                                  {
                                      MRData =
                                          new MRData
                                              {
                                                  StandingsTable =
                                                      new StandingsTable
                                                          {StandingsLists = new List<StandingsList>()}
                                              }
                                  };
            

            StandingsList standingsList = new StandingsList {DriverStandings = new List<DriverStanding>()};
            standingsList.season = "2012";
            root.MRData.StandingsTable.StandingsLists.Add(standingsList);
            
            DriverStanding driverStanding = new DriverStanding
                                                {
                                                    points = "1",
                                                    Driver = new Driver {familyName = "Button", givenName = "Jenson"}
                                                };

            standingsList.DriverStandings.Add(driverStanding);

            var result = Mapper.Map<RootObject, MVCF1.Models.DriverResults>(root);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Driver, Is.Not.Null);
        }
    }
}
