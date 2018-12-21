using AutoMapper;
using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Presentation.Models;
using GlitterTweeting.Shared.DTO.NewTweet;
using GlitterTweeting.Shared.DTO.Tweet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GlitterTweeting.Presentation.Controllers
{
    public class TweetController : ApiController
    {
        private TweetBusinessContext tweetBusinessContext;
        IMapper TweetMapper;
        public TweetController()
        {
            tweetBusinessContext = new TweetBusinessContext();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewTweetModel, NewTweetDTO>();
            });
            TweetMapper = new Mapper(config);

          
        }
        // GET: api/Tweet
        [AllowAnonymous]
        [Route("api/user/newTweet")]
        public async Task<IHttpActionResult> Post([FromBody] NewTweetModel newTweetModel )
        {
            try
            { 
                NewTweetDTO newTweetDTO = TweetMapper.Map<NewTweetModel, NewTweetDTO>(newTweetModel);
                // string ass  = HttpContext.Current.Session["UserID"].ToString();            
                // newTweetDTO.UserID = Guid.Parse(ass);
                Guid abc = Guid.Parse("84559e52-6ffd-4db7-a1eb-1ca25995cee0");
                newTweetDTO.UserID = abc;
                newTweetDTO = await tweetBusinessContext.CreateNewTweet(newTweetDTO);
                return Ok(new { Tweet = newTweetDTO });
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(e.Message)));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/user/playground")]
        public IList<GetAllTweetsDTO> Get()
        {
            // string ass  = HttpContext.Current.Session["UserID"].ToString();            
            // newTweetDTO.UserID = Guid.Parse(ass);
            Guid abc = Guid.Parse("84559e52-6ffd-4db7-a1eb-1ca25995cee0");
            IList<GetAllTweetsDTO>gd= tweetBusinessContext.GetAllTweets(abc);

            return gd;
        }

    }
}
