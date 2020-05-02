using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CCM.Service.ViewModels;


namespace CCM.Service.Interface
{
    public interface ITheatreService : IBaseService<TheatreViewModel>
    {
        Task<bool> SeedDataAsync();
    }
}
