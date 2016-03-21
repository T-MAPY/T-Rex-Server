using System.Collections.Generic;

namespace TRexServer.Models
{
    public class Connection
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public List<string> Ids { get; set; }
    }
}