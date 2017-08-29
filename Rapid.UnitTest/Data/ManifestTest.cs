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
    }
}
