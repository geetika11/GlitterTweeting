using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared.DTO.Search;
using System;
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
        public IList<SearchDTO> SearchAllUsers(string searchString, Guid UserId)
        {
            IList<SearchDTO> getAllResults = searchDBContext.GetAllUsers(searchString, UserId);
            try
            {
               
                if (getAllResults != null)
                {
                    return getAllResults;
                }
                else
                {
                    throw new Exceptions.ResultIsNull("No item Exists matching your search criteria");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        public IList<SearchDTO> SearchAllHashTag(string searchString, Guid UserId)
        {
            IList<SearchDTO> getAllResults = searchDBContext.GetAllHashTag(searchString, UserId);
            if (getAllResults != null)
            {
                return getAllResults;
            }
            else
            {
                throw new Exceptions.ResultIsNull("No item Exists matching your search criteria");
            }

        }

    }
}
