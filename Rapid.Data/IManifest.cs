using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Data
{
    public interface IManifest
    {

        List<dynamic> Search(dynamic searchQuery);

        dynamic Create(dynamic manifest);

        dynamic Save(dynamic manifest);

        dynamic Remove(dynamic manifest);
    }
}
