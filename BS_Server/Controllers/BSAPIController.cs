﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BS_Server.Models;
using BS_Server.DTO;

namespace BS_Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class BSAPIController : ControllerBase
    {
        //a variable to hold a reference to the db context!
        private BSDbContext context;
        //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
        private IWebHostEnvironment webHostEnvironment;
        //Use dependency injection to get the db context and web host into the constructor
        public BSAPIController(BSDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.webHostEnvironment = env;
        }

        [HttpPost("registerParent")]
        public IActionResult RegisterParent([FromBody] DTO.ParentDTO parentDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Create model parent class to be written in the DB
                Models.Parent modelsParent = parentDto.GetModel();
              
                context.Parents.Add(modelsParent);
                context.SaveChanges();

                //User was added!
                DTO.ParentDTO dtoParent = new DTO.ParentDTO(modelsParent);
                return Ok(dtoParent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpPost("registerBabysiter")]
        public IActionResult RegisterBabysiter([FromBody] DTO.BabysiterDTO babysiterDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Create model babysiter class to be written in the DB
                Models.Babysiter modelsBabysiter = babysiterDto.GetModel();
            
                context.Babysiters.Add(modelsBabysiter);
                context.SaveChanges();

                //User was added!
                DTO.BabysiterDTO dtoBabysiter = new DTO.BabysiterDTO(modelsBabysiter);
                return Ok(dtoBabysiter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] DTO.LoginInfo loginDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt
                //Get model user class from DB with matching email. 
                 Models.User? modelsUser = context.GetUser(loginDto.Email, loginDto.Password);
                if (modelsUser == null)
                {
                    return Unauthorized();
                }

                Parent? p = context.GetParent(modelsUser.Id);
                if (p == null)
                {
                    Babysiter? b = context.GetBabySiter(modelsUser.Id);
                    if (b == null)
                    {
                        return BadRequest();
                    }
                    BabysiterDTO babySiterDto = new BabysiterDTO(b);
                    return Ok(babySiterDto);
                }

                ParentDTO parentDto = new ParentDTO(p);
                return Ok(parentDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
