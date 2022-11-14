using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class SeriesDTO
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public ComicsDTO Comics { get; set; }
        public int ComicsId { get; set; }
    }
}
