using Data.Contexts;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class UnitOfWork : IDisposable
    {
        private MarvelComicsDBContext context = new MarvelComicsDBContext();
        private GenericRepository<Character> chartacterRepository;
        private GenericRepository<Comics> comicsRepository;
        private GenericRepository<Series> seriesRepository;

        public GenericRepository<Character> CharacterRepository
        {
            get
            {

                if (this.chartacterRepository == null)
                {
                    this.chartacterRepository = new GenericRepository<Character>(context);
                }
                return chartacterRepository;
            }
        }

        public GenericRepository<Comics> ComicsRepository
        {
            get
            {
                if (this.comicsRepository == null)
                {
                    this.comicsRepository = new GenericRepository<Comics>(context);
                }
                return comicsRepository;
            }
        }

        public GenericRepository<Series> SeriesRepository
        {
            get
            {
                if (this.seriesRepository == null)
                {
                    this.seriesRepository = new GenericRepository<Series>(context);
                }
                return seriesRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}