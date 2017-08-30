using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rapid.UnitTest.Data
{
    [TestClass]
    public class ManifestTest
    {
        [TestMethod]
        public void InitDataProvider()
        {
            Rapid.Data.IManifest manifestDb = Rapid.Data.ManifestProvider.GetDataProvider(Rapid.Data.DATABASE_TYPE.MONGODB);
            Assert.IsNotNull(manifestDb);
        }

        [TestMethod]
        public void TestListManifest()
        {
            var listManifest = Rapid.Data.ManifestProvider.GetDataProvider(Rapid.Data.DATABASE_TYPE.MONGODB).Search(String.Empty);
            Assert.IsNotNull(listManifest);
            Assert.IsTrue(listManifest.Count > 0);
            
        }

        [TestMethod]
        public void TestFilterStandard()
        {
            var listManifest = Rapid.Data.ManifestProvider.GetDataProvider(Rapid.Data.DATABASE_TYPE.MONGODB).FilterStandard(
                "851-0876 4814",
               String.Empty,
               String.Empty,
               String.Empty,
               null,
               null,
               null,
               null,
               String.Empty);
            Assert.IsNotNull(listManifest);
        }
    }
}

