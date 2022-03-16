using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebAPI.Model
{
    public class Estimate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int State { get; set; }
        public int Districts { get; set; }
        public int EstimatesPopulation { get; set; }
        public int EstimatesHouseholds { get; set; }
    }
}
