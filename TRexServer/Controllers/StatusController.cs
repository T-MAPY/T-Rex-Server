using System.Web.Http;
using System.Web.Http.Cors;
using NLog;
using TRexServer.Dao;
using TRexServer.Models;
using TRexServer.Security;

namespace TRexServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StatusController : ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET api/status
        public void Get(StatusDTO value)
        {
            string logText = "i: " + value.i + ", a: " + value.a + ", o: " + value.o + ", l: " + value.l + ", s: " +
                             value.s + ", b: " + value.b + ", g: " + value.g + ", t: " +
                             value.t?.ToString("yyyy-MM-dd'T'HH:mm:ss") + ", k: " + value.k;
            if (SecurityHelper.CheckAccessKey(value))
            {
                var dtoFactory = new DtoFactory();
                dtoFactory.InsertOrUpdateDatabase(value);
            }
            else
            {
                Logger.Warn("Pozice odmítnuta: " + logText);
            }
        }

        // POST api/status
        public void Post([FromBody] StatusDTO value)
        {
            string logText = "i: " + value.i + ", a: " + value.a + ", o: " + value.o + ", l: " + value.l + ", s: " +
                 value.s + ", b: " + value.b + ", g: " + value.g + ", t: " +
                 value.t?.ToString("yyyy-MM-dd'T'HH:mm:ss") + ", k: " + value.k;
            if (SecurityHelper.CheckAccessKey(value))
            {
                var dtoFactory = new DtoFactory();
                dtoFactory.InsertOrUpdateDatabase(value);
            }
            else
            {
                Logger.Warn("Pozice odmítnuta: " + logText);
            }
        }
    }
}