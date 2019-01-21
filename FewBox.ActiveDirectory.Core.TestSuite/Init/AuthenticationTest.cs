using FewBox.ActiveDirectory.Core.Authentication;
using FewBox.TestFramwork.Configuration;
using NUnit.Framework;

namespace FewBox.ActiveDirectory.Core.TestSuite.Init
{
    [TestFixture]
    class AuthenticationTest
    {
        [TestCase]
        public void TestConnection() {
            ClientContext.Init(TF.PS["ClientPath"],
                TF.PS["ClientUserName"], 
                TF.PS["ClientPassword"]);
        }
    }
}
