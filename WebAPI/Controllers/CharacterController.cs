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
    public class CharacterController : ApiController
    {
        private CharacterManagementService characterService = new CharacterManagementService();


        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(characterService.Get());
        }


        /*[HttpPost]
        public IHttpActionResult Get(int id)
        {
            return Json(characterService.GetById(id));
        }*/

        [HttpGet]
        public IHttpActionResult GetCharacterById(int id)
        {
            return Json(characterService.GetCharacterById(id));
        }

        [HttpPost]
        public IHttpActionResult Save(CharacterDTO characterDto)
        {
            //if (!comicsDto.Validate())
            //    return Json(new ResponseMessage { Code = 500, Error = "Data is not valid!" });
            ResponseMessage response = new ResponseMessage();

            if (characterService.Save(characterDto))
            {
                response.Code = 200;
                response.Body = "Character is saved.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Character is not saved.";
            }

            return Json(response);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (characterService.Delete(id))
            {
                response.Code = 200;
                response.Body = "Character is deleted.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Character is not deleted.";
            }

            return Json(response);
        }
    }
}