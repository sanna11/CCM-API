using CCM.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class MovieViewModel : BaseViewModel
    {
        public String Name { get; set; }
        public double? ScreeningDuration { get; set; }
        public bool IsThreeD { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Ratings { get; set; }
        public LanguageEnum Language { get; set; }
        public String MovieLanguage { get; set; }
        public bool HasSubtitles { get; set; }
        public String ThumbnailImage { get; set; }
    }
}
