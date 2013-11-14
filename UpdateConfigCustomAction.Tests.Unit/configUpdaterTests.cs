using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UpdateConfigCustomAction.Tests.Unit
{
    [TestClass]
    public class configUpdaterTests
    {
        private readonly configUpdater _ConfigUpdater;

        public configUpdaterTests()
        {
            _ConfigUpdater = new configUpdater();
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void ReplaceKeywordWithServername_WhenCalled_ReturnsString()
        {
            var actual = _ConfigUpdater.ReplaceKeywordWithServername("Test").GetType();
            var expected = typeof (string);
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void ReplaceKeywordWithServername_WhenPassedNonKeyWordString_ReturnsTheSameString()
        {
            const string inputString = "Test Keyword";
            var actual = _ConfigUpdater.ReplaceKeywordWithServername(inputString);
            const string expected = inputString;
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void ReplaceKeywordWithServername_WhenPassedStringWithKeywordPRODUCTIONSERVER_ReturnsStringWithTestServer()
        {
            const string inputString = @"http:\\PRODUCTIONSERVER:8000\TestSite\TestService.svc";
            var actual = _ConfigUpdater.ReplaceKeywordWithServername(inputString);
            string expected = string.Format(@"http:\\{0}:8000\TestSite\TestService.svc",System.Environment.MachineName);
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void ReplaceKeywordWithServername_WhenPassedStringWithKeywordPRODUCTIONTILL_ReturnsStringWithTestSVR()
        {
            const string inputString = @"http:\\PRODUCTIONTILL:8000\TestSite\TestService.svc";
            var actual = _ConfigUpdater.ReplaceKeywordWithServername(inputString);
            string expected = string.Format(@"http:\\{0}:8000\TestSite\TestService.svc",string.Format("{0}SVR",System.Environment.MachineName.Substring(0,4)));
            Assert.AreEqual(expected, actual);
        }
    }
}
