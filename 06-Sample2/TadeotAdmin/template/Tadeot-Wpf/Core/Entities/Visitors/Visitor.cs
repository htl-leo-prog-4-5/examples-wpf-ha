using AuthenticationBase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Visitors
{
    public class Visitor : EntityObject
    {

        public DateTime DateTime { get; set; }

        public bool IsMale { get; set; }

        [Range(0, 4)]
        public int Adults { get; set; }

        /// <summary>
        /// Übernahme des Textes aus der Nachschlagetabelle "SchoolType"
        /// </summary>
        [MaxLength(100)]
        public string SchoolType { get; set; } = String.Empty;

        /// <summary>
        /// Übernahme des Strings aus der Nachschlagetabelle "ReasonForVisit"
        /// </summary>
        [MaxLength(300)]
        public string ReasonForVisit { get; set; } = String.Empty;

        [Range(7, 13)]
        public int SchoolLevel { get; set; }

        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }
        public int CityId { get; set; }

        public bool InterestHIF { get; set; }
        public bool InterestHITM { get; set; }
        public bool InterestHEL { get; set; }
        public bool InterestHBG { get; set; }
        public bool InterestFEL { get; set; }

        [MaxLength(100)]
        public string? Comment { get; set; }

        public bool UsePhoto { get; set; }
    }
}
