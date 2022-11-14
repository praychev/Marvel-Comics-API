using ApplicationService.DTOs;
using Data.Entity;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public class SeriesManagementService
    {
        public List<SeriesDTO> Get()
        {
            List<SeriesDTO> seriesDto = new List<SeriesDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.SeriesRepository.Get())
                {
                    seriesDto.Add(new SeriesDTO
                    {
                        id = item.Id,
                        Title = item.Title,
                        Description = item.Description,
                        Rating = item.Rating,
                        Comics = new ComicsDTO
                        {   
                            idC=item.Comics.Id,
                            Title=item.Comics.Title,
                            IssueNumber=item.Comics.IssueNumber,
                            Description=item.Comics.Description,
                            PageCount=item.Comics.PageCount,
                            ISBN=item.Comics.ISBN
                        }
                    });
                }
            }

            return seriesDto;
        }

        public SeriesDTO GetSeriesById(int id)
        {
            SeriesDTO seriesDTO = new SeriesDTO();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Series series = unitOfWork.SeriesRepository.GetByID(id);
                if (series != null)
                {
                    seriesDTO = new SeriesDTO
                    {
                        id = series.Id,
                        Title = series.Title,
                        Description = series.Description,
                        Rating = series.Rating,
                        Comics = new ComicsDTO
                        {
                            idC = series.Comics.Id,
                            Title = series.Comics.Title,
                            IssueNumber = series.Comics.IssueNumber,
                            Description = series.Comics.Description,
                            PageCount = series.Comics.PageCount,
                            ISBN = series.Comics.ISBN   
                        }
                    };
                }
            }
            return seriesDTO;
        }

        public bool Save(SeriesDTO seriesDto)
        {
            Series series = new Series
            {
                Id=seriesDto.id,
                Title = seriesDto.Title,
                Description = seriesDto.Description,
                Rating = seriesDto.Rating,
                ComicsId=seriesDto.Comics.idC,
                
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (series.Id == 0)
                    {
                        unitOfWork.SeriesRepository.Insert(series);
                    }
                    else
                    {
                        unitOfWork.SeriesRepository.Update(series);
                    }
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Series series = unitOfWork.SeriesRepository.GetByID(id);
                    unitOfWork.SeriesRepository.Delete(series);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}