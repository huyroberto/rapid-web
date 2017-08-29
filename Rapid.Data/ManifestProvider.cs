using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Data
{
    public enum DATABASE_TYPE
    {
        MONGODB= 1,
        ORACLE=2,
        SQL_SERVER = 3
    }
    public class ManifestProvider
    {
        public static IManifest GetDataProvider(DATABASE_TYPE DataType)
        {
            switch (DataType)
            {
                case DATABASE_TYPE.MONGODB:
                    return new Manifest.ManifestMongoDB();
                case DATABASE_TYPE.ORACLE:
                    break;
                case DATABASE_TYPE.SQL_SERVER:
                    break;
                default:
                    return new Manifest.ManifestMongoDB();
            }
            return new Manifest.ManifestMongoDB();
        }
    }
}
