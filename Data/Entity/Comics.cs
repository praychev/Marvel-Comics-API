using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Comics:BaseEntity
    {
        [StringLength(50), Required]
        public string Title { get; set; }
        public int IssueNumber { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(10), Required]
        public string ISBN { get; set; }
        public int PageCount { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
    }
}
