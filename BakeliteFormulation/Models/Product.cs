using System;
using System.Collections.Generic;
using System.Text;

namespace BakeliteFormulation.Models
{
    public class Product
    {
        public int SrNo { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public float RatePerKg { get; set; }
        public float Total { get; set; }
    }
}
