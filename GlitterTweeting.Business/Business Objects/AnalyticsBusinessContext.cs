using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared.DTO;

namespace GlitterTweeting.Business.Business_Objects
{
    public class AnalyticsBusinessContext
    {
        
        TweetDBContext tbc = new TweetDBContext();
        UserDBContext ubc = new UserDBContext();
        public AnalyticsDTO Analytic()
        {
            AnalyticsDTO bonus = new AnalyticsDTO();
            bonus.MostTrending = tbc.MostTrending();
            bonus.MostLiked = tbc.MostLiked();
            bonus.MostTweetsBy = ubc.MostTweetsBy();
            bonus.TotalTweetsToday = tbc.TotalTweetsToday();
            return bonus;
        }
    }
}
