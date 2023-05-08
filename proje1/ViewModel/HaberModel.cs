using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proje1.ViewModel
{
    public class HaberModel
    {
        public string haberId { get; set; }
        public string haberBasligi { get; set; }
        public string haberİcerigi { get; set; }
        public System.DateTime haberTarihi { get; set; }
        public byte[] haberResmi { get; set; }
    }
}