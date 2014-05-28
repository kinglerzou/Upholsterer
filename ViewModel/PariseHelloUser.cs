using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Upholsterer.Models;

namespace Upholsterer.ViewModel
{
    public class PariseHelloUser
    {
        public UninUser UninUser { get; set; }
        public Message Message { get; set; }
        public string Content { get; set; }
    }
}