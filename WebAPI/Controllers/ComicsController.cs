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
   
    public class ComicsController : ApiController
    {
        private ComicsManagementService comicsService = new ComicsManagementService();


        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(comicsService.Get());
        }


        /*[HttpPost]
        public IHttpActionResult Get(int id)
        {
            return Json(comicsService.GetById(id));
        }*/

        [HttpGet]
        public IHttpActionResult GetComicsById(int id)
        {
            return Json(comicsService.GetComicsById(id));
        }


        [HttpPost]
        public IHttpActionResult Save(ComicsDTO comicsDto)
        {
        //    if (!nationalityDto.Validate())
        //        return Json(new ResponseMessage { Code = 500, Error = "Data is not valid!" });
            ResponseMessage response = new ResponseMessage();

            if (comicsService.Save(comicsDto))
            {
                response.Code = 200;
                response.Body = "Comics is saved.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Comics is not saved.";
            }

            return Json(response);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (comicsService.Delete(id))
            {
                response.Code = 200;
                response.Body = "Comics is deleted.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Comics is not deleted.";
            }

            return Json(response);
        }
    }
}