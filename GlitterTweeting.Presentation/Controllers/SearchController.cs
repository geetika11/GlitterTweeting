using AutoMapper;
using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Presentation.Models;
using GlitterTweeting.Shared.DTO.Search;
using GlitterTweeting.Shared.DTO.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GlitterTweeting.Presentation.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class SearchController : ApiController
    { private SearchBusinessContext searchBusinessContext;
        public SearchController()
        {
            searchBusinessContext = new SearchBusinessContext();     
        }
        // GET: api/Search
       
        [HttpPost]
        [Route("api/user/searchUser")]
        public  IList<SearchDTO> SearchUser ([FromBody] SearchModel SearchString)
        {
            SearchDTO Dto = new SearchDTO();
            Dto.SearchString = SearchString.SearchString;
            IList<SearchDTO> AllResults = searchBusinessContext.SearchAllUsers(Dto.SearchString);
            return AllResults;
        }

        
        [HttpPost]
        [Route("api/user/searchHashTag")]
        public IList<SearchDTO> SearchTag([FromBody] SearchModel SearchString)
        {
            SearchDTO Dto = new SearchDTO();
            Dto.SearchString = SearchString.SearchString;
            IList<SearchDTO> AllResults = searchBusinessContext.SearchAllHashTag(Dto.SearchString);
            return AllResults;
        }
    }
}
