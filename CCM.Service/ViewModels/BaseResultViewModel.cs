using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class BaseResultViewModel
    {
        public bool IsSuccess { get; set; }
        public String ErrorMessage { get; set; }
    }
}
