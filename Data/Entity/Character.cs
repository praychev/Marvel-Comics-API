using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Character : BaseEntity
    {
        [StringLength(50), Required]
        public string Name { get; set; }
        public int Age { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public int ComicsId { get; set; }
        public virtual Comics Comics { get; set; }
        public int SeriesId { get; set; }
        public virtual Series Series { get; set; }
    }
}
