using FewBox.ActiveDirectory.Core.Authentication;
using FewBox.TestFramwork.Configuration;
using NUnit.Framework;

namespace FewBox.ActiveDirectory.Core.TestSuite
{
    [TestFixture]
    abstract class BaseTest
    {
        [SetUp]
        public void SetUp() {
            ClientContext.Init(TF.PS["ClientPath"],
                TF.PS["ClientUserName"],
                TF.PS["ClientPassword"]);
        }
    }
}
