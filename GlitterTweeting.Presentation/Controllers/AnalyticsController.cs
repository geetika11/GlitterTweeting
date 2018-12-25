using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GlitterTweeting.Presentation.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AnalyticsController : ApiController
    {
        [HttpGet]
        [Route("api/analytics")]
        public AnalyticsDTO Bonus()
        {
            AnalyticsBusinessContext analytics = new AnalyticsBusinessContext();
            return analytics.Analytic();
        }

    }
}
