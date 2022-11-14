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
    public class CharacterManagementService
    {
        public List<CharacterDTO> Get()
        {
            List<CharacterDTO> characterDto = new List<CharacterDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.CharacterRepository.Get())
                {
                    characterDto.Add(new CharacterDTO
                    {
                        id = item.Id,
                        Name = item.Name,
                        Age = item.Age,
                        Gender = item.Gender,
                        Description = item.Description,
                        Comics = new ComicsDTO
                        {
                            idC = item.Comics.Id,
                            Title = item.Comics.Title,
                            IssueNumber = item.Comics.IssueNumber,
                            Description = item.Comics.Description,
                            PageCount = item.Comics.PageCount,
                            ISBN = item.Comics.ISBN,
                        },
                        Series = new SeriesDTO
                        {
                            id = item.Series.Id,
                            Title = item.Series.Title,                          
                            Description = item.Series.Description,
                            Rating = item.Series.Rating,
                            Comics = new ComicsDTO
                            {
                                idC = item.Series.Comics.Id,
                                Title = item.Series.Comics.Title,
                                IssueNumber = item.Series.Comics.IssueNumber,
                                Description = item.Series.Comics.Description,
                                PageCount = item.Series.Comics.PageCount,
                                ISBN= item.Series.Comics.ISBN,
                            }
                        }
                    });
                }
            }

            return characterDto;
        }

        public CharacterDTO GetCharacterById(int id)
        {
            CharacterDTO characterDTO = new CharacterDTO();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Character character = unitOfWork.CharacterRepository.GetByID(id);
                if (character != null)
                {
                    characterDTO = new CharacterDTO
                    {
                        id = character.Id,
                        Name = character.Name,
                        Age = character.Age,
                        Gender = character.Gender,
                        Description = character.Description,
                        Comics = new ComicsDTO
                        {
                            idC = character.Comics.Id,
                            Title = character.Comics.Title,
                            IssueNumber = character.Comics.IssueNumber,
                            Description = character.Comics.Description,
                            PageCount = character.Comics.PageCount,
                            ISBN = character.Comics.ISBN,
                        },
                        Series = new SeriesDTO
                        {
                            id = character.Series.Id,
                            Title = character.Series.Title,
                            Description = character.Series.Description,
                            Rating = character.Series.Rating,
                            Comics = new ComicsDTO
                            {
                                idC = character.Series.Comics.Id,
                                Title = character.Series.Comics.Title,
                                IssueNumber = character.Series.Comics.IssueNumber,
                                Description = character.Series.Comics.Description,
                                PageCount = character.Series.Comics.PageCount,
                                ISBN= character.Series.Comics.ISBN, 
                            }
                        }
                    };
                }
            }
            return characterDTO;
        }

        public bool Save(CharacterDTO characterDto)
        {
            Character character = new Character
            {
                Id=characterDto.id,
                Name = characterDto.Name,
                Age = characterDto.Age,
                Gender = characterDto.Gender,
                Description = characterDto.Description,
                ComicsId = characterDto.Comics.idC,
                SeriesId = characterDto.Series.id
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (character.Id == 0)
                    {
                        unitOfWork.CharacterRepository.Insert(character);
                    }
                    else
                    {
                        unitOfWork.CharacterRepository.Update(character);
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
                    Character character = unitOfWork.CharacterRepository.GetByID(id);
                    unitOfWork.CharacterRepository.Delete(character);
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