using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHere.Entities
{
    public class Config
    {
        [Key]
        [Column("Attribute")]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
