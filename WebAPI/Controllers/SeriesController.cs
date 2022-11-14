using ApplicationService.DTOs;
using ApplicationService.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
    public class SeriesController : ApiController
    {
        private SeriesManagementService seriesService = new SeriesManagementService();


        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(seriesService.Get());
        }


        /*[HttpPost]
         public IHttpActionResult Get(int id)
         {
             return Json(seriesService.GetById(id));
         }*/
        [HttpGet]
        public IHttpActionResult GetSeriesById(int id)
        {
            return Json(seriesService.GetSeriesById(id));
        }


        [HttpPost]
        public IHttpActionResult Save(SeriesDTO seriesDto)
        {
            //if (!comicsDto.Validate())
            //    return Json(new ResponseMessage { Code = 500, Error = "Data is not valid!" });
            ResponseMessage response = new ResponseMessage();

            if (seriesService.Save(seriesDto))
            {
                response.Code = 200;
                response.Body = "Series are saved.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Series are not saved.";
            }

            return Json(response);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (seriesService.Delete(id))
            {
                response.Code = 200;
                response.Body = "Series are deleted.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Series are not deleted.";
            }

            return Json(response);
        }
    }
}