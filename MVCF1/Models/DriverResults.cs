using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCF1.Models
{
    public class DriverResults
    {
        public string Season { get; set; }
        public string Points { get; set; }
        public List<Driver> Driver { get; set; }
    }

    public class Driver
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}