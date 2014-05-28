using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Upholsterer.Models;

namespace Upholsterer.ViewModel
{
    public class VisitUser
    {
        public UninUser Visitor { get; set; }
        public DateTime LastVisitTime { get; set; }
        public int Times { get; set; }
    }
}