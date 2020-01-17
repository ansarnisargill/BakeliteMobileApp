using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BakeliteFormulation.Helpers
{
    public static class FontHelper
    {
        public static void SetUrduFont(List<Label> elements)
        {
            foreach (var el in elements)
            {
                el.FontFamily = Device.RuntimePlatform == Device.Android ? "jurdu.ttf#Jameel Noori Nastaleeq" : null;
            }
        }
    }
}
