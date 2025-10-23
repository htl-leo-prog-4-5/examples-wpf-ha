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
   [Index(nameof(Rank), IsUnique = true)]
    public class ReasonForVisit : EntityObject
    {
        [MaxLength(300)]
        public string Reason { get; set; } = string.Empty;  
        public int Rank { get; set; }
    }
}
