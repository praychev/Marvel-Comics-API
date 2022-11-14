using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class ComicsDTO
    {
        public int idC { get; set; }
        public string Title { get; set; }
        public int IssueNumber { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string ISBN { get; set; }
    }
}
