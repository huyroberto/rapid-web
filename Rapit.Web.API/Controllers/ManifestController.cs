using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rapit.Web.API.Controllers
{
    /// <summary>
    /// Manifest Controller API
    /// </summary>
    public class ManifestController : ApiController
    {

        [Route("manifest/list/{Status}")]
        [HttpGet]
        public List<JObject> GetManifestByStatus(string Status)
        {
            return new List<JObject>() { };
        }
    }
}
