using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class TheatreHallViewModel : BaseViewModel
    {
        public String Name { get; set; }
        public int TotalSeatingCapacity { get; set; }
        public int TheatreId { get; set; }
        public String TheatreName { get; set; }
    }
}
