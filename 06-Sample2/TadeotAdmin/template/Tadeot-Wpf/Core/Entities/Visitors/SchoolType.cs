using AuthenticationBase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Visitors
{
    public class SchoolType : EntityObject
    {
        [MaxLength(300)]
        public string Type { get; set; } = string.Empty;
        public int Rank { get; set; }
    }
}
