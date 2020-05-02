using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class TheatreViewModel: BaseViewModel
    {
        public String Name { get; set; }
        public String CityName { get; set; }
        public String Address { get; set; }
        public bool HasParking { get; set; }
        public String Telephone { get; set; }
        public String EmailAddress { get; set; }
    }
}
