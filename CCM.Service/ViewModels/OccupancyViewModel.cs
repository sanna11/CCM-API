using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class OccupancyViewModel : BaseResultViewModel
    {
        public List<OccupancyPerMovie> Occupancy { get; set; }
    }
}
