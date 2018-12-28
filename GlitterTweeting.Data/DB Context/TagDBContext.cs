using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Data.DB_Context
{
    public class TagDBContext : IDisposable
    {
        glitterEntities dbcontext = new glitterEntities();
        public bool AddTags(List<string> tags, Guid tweetid)
        {

            foreach (string s in tags)
            {
                Tag newtag = new Tag();
                newtag.ID = Guid.NewGuid();
                newtag.TweetID = tweetid;
                newtag.TagName = s;
                dbcontext.Tag.Add(newtag);
                dbcontext.SaveChanges();
            }

            return true;
        }
        
        public bool DeleteTag(Guid tweetId)
        {
           
                IList<Tag> taglist = dbcontext.Tag.Where(dr => dr.TweetID == tweetId).ToList();
                if (taglist.Count > 0)
                {
                    foreach (var item in taglist)
                    {
                        dbcontext.Entry(item).State = EntityState.Deleted;
                    dbcontext.SaveChanges();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
                
            
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// virtual dispose to class
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbcontext != null)
                { 
                    dbcontext.Dispose();
                }
            }
        }

        /// <summary>
        /// Destructor to class
        /// </summary>
        ~TagDBContext()
        {
            Dispose(false);
        }
    }
}
