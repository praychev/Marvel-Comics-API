using ApplicationService.DTOs;
using ApplicationService.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Comics" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Comics.svc or Comics.svc.cs at the Solution Explorer and start debugging.
    public class Comics : IComics
    {
        #region Fields
        private ComicsManagementService service = new ComicsManagementService();
        #endregion

        public List<ComicsDTO> GetComics()
        {
            return service.Get();
        }
        public ComicsDTO GetComicsByID(int id)
        {
            return service.GetComicsById(id);
        }
        public string PostComics(ComicsDTO comicsDto)
        {
            if (!service.Save(comicsDto))
                return "Comics is not inserted";

            return "Comics is inserted";
        }

        public string PutComics(ComicsDTO comicsDto)
        {
            throw new NotImplementedException();
        }

        public string DeleteComics(int id)
        {
            if (!service.Delete(id))
                return "Comics is not deleted";

            return "Comics is deleted";
        }

        public string Message()
        {
            return "My first wcf service";
        }
    }
}