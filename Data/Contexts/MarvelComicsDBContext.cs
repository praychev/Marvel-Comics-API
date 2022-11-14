using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts
{
    public class MarvelComicsDBContext:DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Comics> Comics { get; set; }
        public DbSet<Series> Series { get; set; }

    }
}
