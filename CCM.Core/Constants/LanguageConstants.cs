using CCM.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Core.Constants
{
    public sealed class LanguageConstants
    {
        public static String GetString(LanguageEnum lang)
        {
            switch (lang)
            {
                case LanguageEnum.Tamil:
                    return "Tamil";
                case LanguageEnum.Sinhala:
                    return "Sinhala";
                case LanguageEnum.English:
                    return "English";
                case LanguageEnum.Hindi:
                    return "Hindi";
                default:
                    return null;
            }
        }
    }
}
