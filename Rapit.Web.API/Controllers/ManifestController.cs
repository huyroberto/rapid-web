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
        public dynamic GetManifestByStatus(string Status,int PageSize, int PageIndex)
        {
            long TotalPage = 0;
            var listManifest = Rapid.Data.ManifestProvider.GetDataProvider(Rapid.Data.DATABASE_TYPE.MONGODB).FilterStandardPaging(
             String.Empty,
              String.Empty,
              String.Empty,
              String.Empty,
              null,
              null,
              null,
              null,
              Status,
              "_id",
              PageIndex,
              PageSize,
              out TotalPage
              );
            return
                new
                {
                    total_page = TotalPage,
                    data =
                listManifest
                };
        }
    }
}
