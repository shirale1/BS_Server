using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BS_Server.Models;
using BS_Server.DTO;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

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
                    HttpContext.Session.SetString("LoggedInUser", modelsUser.Email);
                    babySiterDto.ProfileImagePath = GetProfileImageVirtualPath(babySiterDto.Id);
                    return Ok(babySiterDto);
                }

                ParentDTO parentDto = new ParentDTO(p);
                HttpContext.Session.SetString("LoggedInUser", modelsUser.Email);
                parentDto.ProfileImagePath = GetProfileImageVirtualPath(parentDto.Id);
                return Ok(parentDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetParents")]
        public IActionResult GetParents()
        {
            #region Checking Login and Permissions
            //Check if who is logged in
            string? email = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("User is not logged in");
            }
            Models.User? theUser = context.Users.Where(u => u.Email == email).FirstOrDefault();
            if (theUser == null)
            {
                return Unauthorized("User is not logged in");
            }
            Models.Babysiter? thebabySiter = context.Babysiters.Where(b => b.BabysiterId == theUser.Id).FirstOrDefault();
            if (thebabySiter == null)
            {
                return Unauthorized("Only Babysiters allowed to run this method!");
            }
            #endregion

            List<Parent> parentList = context.Parents.Include(p => p.ParentNavigation)
            .ThenInclude(u => u.Ratings)
            .Include(p => p.ParentNavigation)
            .ThenInclude(u => u.Recommendations).ToList();

            List<DTO.ParentDTO> parentListDTO = new List<DTO.ParentDTO>();

            foreach (Parent p in parentList)
            {
                DTO.ParentDTO dto = new DTO.ParentDTO(p);
                dto.ProfileImagePath = GetProfileImageVirtualPath(dto.Id);
                parentListDTO.Add(dto);
            }
            return Ok(parentListDTO);
        }

        [HttpGet("GetBabysiters")]
        public IActionResult GetBabysiters()
        {
            #region Checking Login and Permissions
            //Check if who is logged in
            string? email = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("User is not logged in");
            }
            Models.User? theUser = context.Users.Where(u => u.Email == email).FirstOrDefault();
            if (theUser == null)
            {
                return Unauthorized("User is not logged in");
            }
            Models.Parent? theparent = context.Parents.Where(p => p.ParentId == theUser.Id).FirstOrDefault();
            if (theparent == null)
            {
                return Unauthorized("Only parents allowed to run this method!");
            }
            #endregion

            List<Babysiter> babysiterList = context.Babysiters.Include(p => p.BabysiterNavigation)
            .ThenInclude(u => u.Ratings)
            .Include(p => p.BabysiterNavigation)
            .ThenInclude(u => u.Recommendations).ToList();

            List<DTO.BabysiterDTO> babysiterListDTO = new List<DTO.BabysiterDTO>();

            foreach (Babysiter b in babysiterList)
            {
                DTO.BabysiterDTO dto = new DTO.BabysiterDTO(b);
                dto.ProfileImagePath = GetProfileImageVirtualPath(dto.Id);
                babysiterListDTO.Add(dto);
            }
            return Ok(babysiterListDTO);
        }

        [HttpPost("AddRating")]
        public IActionResult Addrating([FromBody] DTO.RatingDTO ratingDto)
        {
            try
            {
                #region Checking Login and Permissions
                //Check if who is logged in
                string? email = HttpContext.Session.GetString("LoggedInUser");
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("User is not logged in");
                }
                Models.User? theUser = context.Users.Where(u => u.Email == email).FirstOrDefault();
                if (theUser == null)
                {
                    return Unauthorized("User is not logged in");
                }

                #endregion
                //Create model dto class to be written in the DB
                Models.Rating modelsRating = ratingDto.GetModel();

                context.Ratings.Add(modelsRating);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("AddRecommendations")]
        public IActionResult AddRecommendations([FromBody] DTO.RecommendationDTO recommendationsDto)
        {
            try
            {
                #region Checking Login and Permissions
                //Check if who is logged in
                string? email = HttpContext.Session.GetString("LoggedInUser");
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("User is not logged in");
                }
                Models.User? theUser = context.Users.Where(u => u.Email == email).FirstOrDefault();
                if (theUser == null)
                {
                    return Unauthorized("User is not logged in");
                }

                #endregion
                //Create model dto class to be written in the DB
                Models.Recommendation modelsRecommendation = recommendationsDto.GetModel();

                context.Recommendations.Add(modelsRecommendation);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("UploadProfileImage")]
        public async Task<IActionResult> UploadProfileImageAsync(IFormFile file)
        {
            //Check if who is logged in
            string? userEmail = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User is not logged in");
            }

            //Get model user class from DB with matching email. 
            Models.User? user = context.GetUser(userEmail);
            //Clear the tracking of all objects to avoid double tracking
            context.ChangeTracker.Clear();

            if (user == null)
            {
                return Unauthorized("User is not found in the database");
            }


            //Read all files sent
            long imagesSize = 0;

            if (file.Length > 0)
            {
                //Check the file extention!
                string[] allowedExtentions = { ".png", ".jpg" };
                string extention = "";
                if (file.FileName.LastIndexOf(".") > 0)
                {
                    extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                }
                if (!allowedExtentions.Where(e => e == extention).Any())
                {
                    //Extention is not supported
                    return BadRequest("File sent with non supported extention");
                }

                //Build path in the web root (better to a specific folder under the web root
                string filePath = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{user.Id}{extention}";

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);

                    if (IsImage(stream))
                    {
                        imagesSize += stream.Length;
                    }
                    else
                    {
                        //Delete the file if it is not supported!
                        System.IO.File.Delete(filePath);
                    }

                }

            }

            DTO.UsersDTO dtoUser = new DTO.UsersDTO(user);
            dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }

        //this function gets a file stream and check if it is an image
        private static bool IsImage(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            List<string> jpg = new List<string> { "FF", "D8" };
            List<string> bmp = new List<string> { "42", "4D" };
            List<string> gif = new List<string> { "47", "49", "46" };
            List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
            List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

            List<string> bytesIterated = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                string bit = stream.ReadByte().ToString("X2");
                bytesIterated.Add(bit);

                bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                if (isImage)
                {
                    return true;
                }
            }

            return false;
        }

        //this function check which profile image exist and return the virtual path of it.
        //if it does not exist it returns the default profile image virtual path
        private string GetProfileImageVirtualPath(int userId)
        {
            string virtualPath = $"/profileImages/{userId}";
            string path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.jpg";
                if (System.IO.File.Exists(path))
                {
                    virtualPath += ".jpg";
                }
                else
                {
                    virtualPath = $"/profileImages/default.png";
                }
            }

            return virtualPath;
        }

        #region Tips
        //To DO:
        //0. Create Tip class in the DTO
        //1. Method that return ALL approved tips
        //2. Method that return ALL pending tips
        //3. Method that gets a tip and update it into the database.
        //4. Method that gets a tip and add it into the database.

   

        [HttpGet("GetApprovedTips")]
        public IActionResult GetApprovedTips()
        {
            #region Checking Login and Permissions
            //Check if who is logged in
            string? email = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("User is not logged in");
            }
            Models.User? theUser = context.Users.Where(u => u.Email == email).FirstOrDefault();
            if (theUser == null)
            {
                return Unauthorized("User is not logged in");
            }

            #endregion
            List<Tip> tips = context.Tips.Where(t => t.StatusId == 1).ToList();

            List<DTO.TipDTO> tipDTO = new List<DTO.TipDTO>();

            foreach (Tip t in tips)
            {
                DTO.TipDTO dto = new DTO.TipDTO(t);
                tipDTO.Add(dto);
            }
            return Ok(tipDTO);
        }

        [HttpGet("GetPendingTips")]
        public IActionResult GetPendingTips()
        {
            #region Checking Login and Permissions
            //Check if who is logged in
            string? email = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("User is not logged in");
            }
            Models.User? theUser = context.Users.Where(u => u.Email == email).FirstOrDefault();
            if (theUser == null)
            {
                return Unauthorized("User is not logged in");
            }

            #endregion
            List<Tip> tips = context.Tips.Where(t => t.StatusId == 3).ToList();

            List<DTO.TipDTO> tipDTO = new List<DTO.TipDTO>();

            foreach (Tip t in tips)
            {
                DTO.TipDTO dto = new DTO.TipDTO(t);
                tipDTO.Add(dto);
            }
            return Ok(tipDTO);
        }


        [HttpPost("AddTip")]
        public IActionResult AddTip([FromBody] DTO.TipDTO tipsDto)
        {
            try
            {
                #region Checking Login and Permissions
                //Check if who is logged in
                string? email = HttpContext.Session.GetString("LoggedInUser");
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("User is not logged in");
                }
                Models.User? theUser = context.Users.Where(u => u.Email == email).FirstOrDefault();
                if (theUser == null)
                {
                    return Unauthorized("User is not logged in");
                }

                #endregion
                //Create model dto class to be written in the DB
                Models.Tip modelsTip = tipsDto.GetModel();

                context.Tips.Add(modelsTip);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("UpdateTip")]
        


    }
}




    


        
    

