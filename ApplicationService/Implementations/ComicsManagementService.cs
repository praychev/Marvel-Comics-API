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
    public class ComicsManagementService
    {
        public List<ComicsDTO> Get()
        {
            List<ComicsDTO> comicsDto = new List<ComicsDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.ComicsRepository.Get())
                {
                    comicsDto.Add(new ComicsDTO
                    {
                        idC = item.Id,
                        Title = item.Title,
                        IssueNumber = item.IssueNumber,
                        Description = item.Description,
                        PageCount = item.PageCount,
                        ISBN = item.ISBN
                    });
                }
            }

            return comicsDto;
        }

        public ComicsDTO GetComicsById(int id)
        {
            ComicsDTO comicsDTO = new ComicsDTO();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Comics comics = unitOfWork.ComicsRepository.GetByID(id);
                if (comics != null)
                {
                    comicsDTO = new ComicsDTO
                    {
                        idC = comics.Id,
                        Title = comics.Title,
                        IssueNumber = comics.IssueNumber,
                        Description = comics.Description,
                        PageCount = comics.PageCount,
                        ISBN= comics.ISBN
                        
                    };
                }
            }
            return comicsDTO;
        }

      public bool Save(ComicsDTO comicsDto)
        {
            Comics comics = new Comics
            {
                Id = comicsDto.idC,
                Title = comicsDto.Title,
                IssueNumber = comicsDto.IssueNumber,
                Description=comicsDto.Description,
                PageCount = comicsDto.PageCount,
                ISBN=comicsDto.ISBN    
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (comicsDto.idC == 0)
                    {
                        unitOfWork.ComicsRepository.Insert(comics);
                    }
                    else
                    { 
                        unitOfWork.ComicsRepository.Update(comics); 
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
                    Comics comics = unitOfWork.ComicsRepository.GetByID(id);
                    unitOfWork.ComicsRepository.Delete(comics);
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