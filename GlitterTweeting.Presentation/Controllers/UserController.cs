using AutoMapper;
using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Presentation.Models;
using GlitterTweeting.Shared.DTO;
using GlitterTweeting.Shared.DTO.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GlitterTweeting.Presentation.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private UserBusinessContext UserBusinessContext;
        IMapper UserMapper;
        ModelFactory ModelFactory;

        /// <summary>
        /// Constructor, initializes user business objects, automappers and ModelFactory.
        /// </summary>
        public UserController()
        {
            UserBusinessContext = new UserBusinessContext();
            
            var userMappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegisterModel, UserRegisterDTO>();
            });
            UserMapper = new Mapper(userMappingConfig);

            ModelFactory = new ModelFactory();
        }
        /// <summary>
        /// login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/login")]        
        public async Task<IHttpActionResult> Post([FromBody] UserLoginModel user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid passed data");
                }
                if (!ModelState.IsValid)
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(string.Join(" | ", ModelState.Values))));
                }
                UserLoginDTO useLoginDTO = UserMapper.Map<UserLoginModel, UserLoginDTO>(user);
                UserCompleteDTO loginUser = await UserBusinessContext.LoginUserCheck(useLoginDTO);
                HttpContext.Current.Session["UserID"] = loginUser.ID;
                HttpContext.Current.Session["FirstName"] = loginUser.FirstName;

                var Id = HttpContext.Current.Session["UserID"];
                var UserName = HttpContext.Current.Session["FirstName"];

                return Ok(new { ID=Id, Username=UserName});
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(e.Message)));
            }
        }

        /// <summary>
        /// register
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/User")]
        public async Task<IHttpActionResult> Post([FromBody] UserRegisterModel user)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(string.Join(" | ", ModelState.Values))));

                }
                UserRegisterDTO userPostDTO = UserMapper.Map<UserRegisterModel, UserRegisterDTO>(user);
                UserCompleteDTO newUser = await UserBusinessContext.CreateNewUser(userPostDTO);
                return Ok(new { User = newUser });
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(e.Message)));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/follow")]
        public bool Post()
        {
            //fetch user to follow's userid from url and fetch  loggedin user id from session
            // string ass  = HttpContext.Current.Session["UserID"].ToString();            

            Guid loggedinuserid = Guid.Parse("776a7b91-dac4-4546-957c-2298dd72812c");
            Guid usertofollow = Guid.Parse("906c7730-6766-4bbc-8d29-1f7b13541728");
            UserBusinessContext.Follow(loggedinuserid, usertofollow);
            return true;
        }


        [AllowAnonymous]
        [HttpDelete]
        [Route("api/user/unfollow")]
        public bool Delete()
        {
            //fetch tweetid from url and fetch user id from session
            // string ass  = HttpContext.Current.Session["UserID"].ToString();            

            Guid loggedinuserid = Guid.Parse("776a7b91-dac4-4546-957c-2298dd72812c");
            Guid usertounfollow = Guid.Parse("cd52690b-cc07-45de-b0dd-7e0f2bd91ea8");
            UserBusinessContext.UnFollow(loggedinuserid, usertounfollow);
            return true;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("api/user/followers")]
        public IList<UserBasicDTO> Get()
        {
            // string ass = HttpContext.Current.Session["UserID"].ToString();
            // Guid abc = Guid.Parse(ass);

            //fetch the loggedin user id from session

            Guid loggedinuserid = Guid.Parse("776a7b91-dac4-4546-957c-2298dd72812c");
            IList<UserBasicDTO> gd = UserBusinessContext.GetAllFollowers(loggedinuserid);

            return gd;
        }

    }
}
