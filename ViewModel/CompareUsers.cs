using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Upholsterer.Models;

namespace Upholsterer.ViewModel
{
    public class CompareUsers
    {
        public UninUser Wo { get; set; }
        public UninUser Ta { get; set; }
        public RecommendRate Rate { get; set; }
        public Requirement MyRequirement { get; set; }
        public Requirement OtherRequirement { get; set; }
        public int[] WoInts { get; set; }
        public int[] TaInts { get; set; }
        public int[] LoveInts { get; set; }

    }
}