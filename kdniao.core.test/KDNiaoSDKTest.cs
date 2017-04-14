using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kdniao.core.test
{
    [TestClass]
    public class KDNiaoSDKTest
    {
        [TestMethod]
        public void QueryTest()
        {
            var sdk = new KDNiaoSDK("1284223", "a7e4df09-f66c-4d09-b714-1f8b7ea74962");
            var result = sdk.Query("HHTT", "550565940770");
            Assert.IsTrue(result.Success == true);
        }
    }
}
