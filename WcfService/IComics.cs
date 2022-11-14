using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IComics" in both code and config file together.
    [ServiceContract]
    public interface IComics
    {
        [OperationContract]
        List<ComicsDTO> GetComics();

        [OperationContract]
        ComicsDTO GetComicsByID(int id);


        [OperationContract]
        string PostComics(ComicsDTO comicsDto);

        [OperationContract]
        string PutComics(ComicsDTO comicsDto);

        [OperationContract]
        string DeleteComics(int id);

        [OperationContract]
        string Message();
    }
}