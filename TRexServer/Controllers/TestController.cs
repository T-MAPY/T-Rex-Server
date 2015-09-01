using System.Web.Http;
using System.Web.Http.Cors;
using NLog;

namespace TRexServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TestController : ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET api/test
        public string Get()
        {
            
            Logger.Debug("test");
            return "T-Mapy test";
        }
    }
}
