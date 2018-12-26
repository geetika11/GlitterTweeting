using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared.DTO.Search;
using System.Collections.Generic;

namespace GlitterTweeting.Business.Business_Objects
{
    public class SearchBusinessContext
    {
        private SearchDBContext searchDBContext;
        public SearchBusinessContext()
        {
            searchDBContext = new SearchDBContext();
        }
        public IList<SearchDTO> SearchAllUsers(string searchString)
        {
            IList<SearchDTO> getAllResults = searchDBContext.GetAllUsers(searchString);
            return getAllResults;
        }
        public IList<SearchDTO> SearchAllHashTag(string searchString)
        {
            IList<SearchDTO> getAllResults = searchDBContext.GetAllHashTag(searchString);
            return getAllResults;
        }

    }
}
