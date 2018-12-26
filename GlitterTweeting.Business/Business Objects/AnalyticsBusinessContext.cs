using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared.DTO;

namespace GlitterTweeting.Business.Business_Objects
{
    public class AnalyticsBusinessContext
    {
       
        UserDBContext UserDBContextObject = new UserDBContext();
        TweetDBContext tweetDBContextObject = new TweetDBContext();
        public AnalyticsDTO Analytic()
        {
            AnalyticsDTO bonus = new AnalyticsDTO();
            bonus.MostTweetsBy = UserDBContextObject.MostTweetsBy();
           
                bonus.MostTrending = tweetDBContextObject.MostTrending();
                bonus.MostLiked = tweetDBContextObject.MostLiked();
                bonus.TotalTweetsToday = tweetDBContextObject.TotalTweetsToday();
         
            return bonus;
        }
    }
}
