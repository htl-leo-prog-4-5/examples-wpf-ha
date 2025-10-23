using AuthenticationBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Visitors
{
    [Index(nameof(Number), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class District : EntityObject
    {
        [MaxLength(30)]
        public string Name { get; set; } = String.Empty;
        public int Number { get; set; }

        public IList<City> Cities { get; set; } = new List<City>();
    }
}
