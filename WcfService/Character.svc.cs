using ApplicationService.DTOs;
using ApplicationService.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Character : ICharacter
    {
        #region Fields
        private CharacterManagementService service = new CharacterManagementService();
        #endregion

        public List<CharacterDTO> GetCharacters()
        {
            return service.Get();
        }

        public string PostCharacter(CharacterDTO characterDto)
        {
            if (!service.Save(characterDto))
                return "Character is not inserted";

            return "Character is inserted";
        }

        public string PutCharacter(CharacterDTO characterDto)
        {
            throw new NotImplementedException();
        }

        public string DeleteCharacter(int id)
        {
            if (!service.Delete(id))
                return "Character is not deleted";

            return "Character is deleted";
        }

        public string Message()
        {
            return "My first wcf service";
        }
    }
}