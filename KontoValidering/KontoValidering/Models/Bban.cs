using System;
using System.Collections.Generic;
using System.Text;

namespace KontoValidering.Models
{
    public class Bban
    {
        public string Kontonummer { get; set; }

        public string Swift { get; set; }

        public string Banknavn { get; set; }

        public string[] Bankadresse { get; set; }

        public string Bankkode { get; set; }

        public string Landkode { get; set; }
    }
}
