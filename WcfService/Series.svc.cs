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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Series" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Series.svc or Series.svc.cs at the Solution Explorer and start debugging.
    public class Series : ISeries
    {
        #region Fields
        private SeriesManagementService service = new SeriesManagementService();
        #endregion

        public List<SeriesDTO> GetSeries()
        {
            return service.Get();
        }

        public string PostSeries(SeriesDTO seriesDto)
        {
            if (!service.Save(seriesDto))
                return "Series is not inserted";

            return "Series is inserted";
        }

        public string PutSeries(SeriesDTO seriesDto)
        {
            throw new NotImplementedException();
        }

        public string DeleteSeries(int id)
        {
            if (!service.Delete(id))
                return "Series is not deleted";

            return "Series is deleted";
        }

        public string Message()
        {
            return "My first wcf service";
        }
    }
}