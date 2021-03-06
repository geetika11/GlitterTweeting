﻿using AutoMapper;
using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Presentation.Models;
using GlitterTweeting.Shared.DTO;
using GlitterTweeting.Shared.DTO.RelationShip;
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
                HttpContext.Current.Session["ProfileImage"] = loginUser.Image;
                var Id = HttpContext.Current.Session["UserID"];
                var UserName = HttpContext.Current.Session["FirstName"];
                var Image = HttpContext.Current.Session["ProfileImage"];
                return Ok(new { ID=Id, Username=UserName, image=Image});
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

        
        [HttpPost]
        [Route("api/user/follow")]
        public bool Post(FollowModel followModel)
        {                        
            FollowDTO followdto = new FollowDTO();
            followdto.UserID=Guid.Parse(followModel.UserID);
            followdto.UserToFollowID = Guid.Parse(followModel.UserToFollowID);         
             bool result= UserBusinessContext.Follow(followdto);
            return result;
        }


       
        [HttpPost]
        [Route("api/user/unfollow")]
        public bool Unfollow(FollowModel followModel)
        {
            FollowDTO followdto = new FollowDTO();
            followdto.UserID = Guid.Parse(followModel.UserID);
            followdto.UserToFollowID = Guid.Parse(followModel.UserToFollowID);
            UserBusinessContext.UnFollow(followdto);
                return true;
        }


       
        [HttpGet]
        [Route("api/user/following/{userId}")]
        public IList<UserBasicDTO> Following(string userId)
        {
           
            Guid loggedinuserid = Guid.Parse(userId);
            IList<UserBasicDTO> gd = UserBusinessContext.GetAllFollowing(loggedinuserid);
            return gd;
        }
       
        [HttpGet]
        [Route("api/user/followers/{userId}")]
        public IList<UserBasicDTO> Followers(string userId)
        {
            Guid loggedinuserid = Guid.Parse(userId);
            IList<UserBasicDTO> gd = UserBusinessContext.GetAllFollowers(loggedinuserid);
            return gd;
        }


    }
}
