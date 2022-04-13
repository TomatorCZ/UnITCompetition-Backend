using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
    public class AllertResponse
    {
        public string ProductName { get; set; }
        public string Reason { get; set; }
        public int Severenity { get; set; }
    }
}
