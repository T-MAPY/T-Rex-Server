﻿using System.Web.Http;
using System.Web.Http.Cors;
using NLog;
using TRexServer.Dao;
using TRexServer.Models;

namespace TRexServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StatusController : ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET api/status
        public void Get(StatusDTO value)
        {
            var dtoFactory = new DtoFactory();
            dtoFactory.InsertOrUpdateDatabase(value);
        }

        // POST api/status
        public void Post([FromBody] StatusDTO value)
        {
            var dtoFactory = new DtoFactory();
            dtoFactory.InsertOrUpdateDatabase(value);
        }
    }
}