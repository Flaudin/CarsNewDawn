using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Utilities
{
    internal class ColorManager
    {
        public List<int> HeaderRGB { get; set; } = new List<int> { 255, 206, 0 };
        public List<int> HeaderFontRGB { get; set; } = new List<int> { 5, 33, 66 };
        public List<int> BannerRGB { get; set; } = new List<int> { 5, 33, 66 };
        public List<int> TableHeaderRGB { get; set; } = new List<int> { 13, 152, 255 };
        //public List<int> HeaderRGB { get; set; } = new List<int> { 255, 206, 200 };
        //public List<int> HeaderFontRGB { get; set; } = new List<int> { 105, 33, 66 };
        //public List<int> BannerRGB { get; set; } = new List<int> { 5, 33, 66 };
        //public List<int> TableHeaderRGB { get; set; } = new List<int> { 123, 152, 255 };
        public List<int> TableHeaderFontRGB { get; set; } = new List<int>{ 230, 231, 232 };
        public List<int> BackgroundRGB { get; set; } = new List<int>{ 230, 231, 232 };

        
    }
}
