using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISeries" in both code and config file together.
    [ServiceContract]
    public interface ISeries
    {
        [OperationContract]
        List<SeriesDTO> GetSeries();

        [OperationContract]
        string PostSeries(SeriesDTO seriesDto);

        [OperationContract]
        string PutSeries(SeriesDTO seriesDto);

        [OperationContract]
        string DeleteSeries(int id);

        [OperationContract]
        string Message();
    }
}