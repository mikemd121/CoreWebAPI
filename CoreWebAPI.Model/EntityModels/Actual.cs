using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebAPI.Model
{
   public class Actual
    {
        [Key]
        public int State { get; set; }
        public double ActualPopulation { get; set; }
        public double ActualHouseholds { get; set; }

    }
}
