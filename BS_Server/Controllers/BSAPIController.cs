using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BS_Server.Models;

namespace BS_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BSAPIController : ControllerBase
    {
        private BSDbContext context;
        //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
        private IWebHostEnvironment webHostEnvironment;
        //Use dependency injection to get the db context and web host into the constructor
        public BSAPIController(BSDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.webHostEnvironment = env;
        }

    }
}
