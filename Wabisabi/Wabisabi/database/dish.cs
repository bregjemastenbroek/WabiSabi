using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wabisabi.database
{
    public class Dish
    {
        public int Id { get; set; }

        public string Naam { get; set; }

        public Decimal Prijs { get; set; }

        public string Afbeelding { get; set; }
    }
}
