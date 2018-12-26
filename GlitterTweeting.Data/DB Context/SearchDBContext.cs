using GlitterTweeting.Shared.DTO.Search;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlitterTweeting.Data.DB_Context
{
    public class SearchDBContext : IDisposable
    {
        glitterEntities DBContext = new glitterEntities();
        TweetDBContext tweetDBContext = new TweetDBContext();
        public IList<SearchDTO> GetAllUsers(string searchString)
        {
            if (searchString != null)
            {
                IList<SearchDTO> resultList = new List<SearchDTO>();
                SearchDTO getAllUsers;
                  IList<User> user = DBContext.User.Where(ds => ds.FirstName.Contains(searchString)||ds.Email.Contains(searchString) || ds.LastName.Contains(searchString)).ToList();
                    if (user.Count > 0)
                    {
                        foreach (var item in user)
                        {
                            getAllUsers = new SearchDTO();
                            getAllUsers.Image = item.Image;
                            getAllUsers.LastName = item.LastName;
                            getAllUsers.FirstName = item.FirstName;
                            getAllUsers.Email = item.Email;
                            getAllUsers.UserId = item.ID;
                            resultList.Add(getAllUsers);
                        }
                            return resultList;
                }
                else
               {
                        throw new Exceptions.UserNotExist("User Not Exists");
                }             
            }
            else
            {
                return null;
            }            
        }

        public IList<SearchDTO> GetAllHashTag(string searchString)
        {
            if (searchString != null)
            {
                IList<SearchDTO> resultList = new List<SearchDTO>();
                SearchDTO getAllTags;
                IList<Tag> tag = DBContext.Tag.Where(de => de.TagName.Contains(searchString)).ToList();
                if (tag.Count > 0)
                {
                    foreach (var item in tag)
                    {
                        getAllTags = new SearchDTO();
                        tweetDBContext.updateSearchCount(item);
                        IList<Tweet> tweet = DBContext.Tweet.Where(dr => dr.ID == item.TweetID).ToList();                        
                            foreach (var item1 in tweet)
                            {
                                User user1 = DBContext.User.Where(dw => dw.ID == item1.UserID).FirstOrDefault();
                                getAllTags.Message = item1.Message;
                                getAllTags.CreatedAt = item1.CreatedAt;
                                getAllTags.UserName = user1.FirstName + user1.LastName;
                            }
                            resultList.Add(getAllTags);                        
                    }
                    return resultList;
                }
                else
                {
                     throw new Exceptions.TagNotExist("Tag Not Exists");
                }                
            }
            else
            { return null; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DBContext != null)
                {
                    DBContext.Dispose();
                }
            }
        }
        ~SearchDBContext()
        {
            Dispose(false);
        }
    }
}
