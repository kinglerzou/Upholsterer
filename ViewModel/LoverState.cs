using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Upholsterer.Models;

namespace Upholsterer.ViewModel
{
    public class LoverState
    {
        public DateTime ActionTime { get; set; }
        public UninUser Lover { get; set; }
        public State LastState { get; set; }
    }
}