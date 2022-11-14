using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Series:BaseEntity
    {
        [StringLength(50), Required]
        public string Title { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public double Rating { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
        public int ComicsId { get; set; }
        public virtual Comics Comics { get; set; }
    }
}
