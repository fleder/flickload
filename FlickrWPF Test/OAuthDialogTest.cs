using FlickrWPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FlickrWPF_Test
{
    
    
    /// <summary>
    ///This is a test class for OAuthDialogTest and is intended
    ///to contain all OAuthDialogTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OAuthDialogTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for OAuthDialog Constructor
        ///</summary>
        [TestMethod()]
        public void OAuthDialogConstructorTest()
        {
            OAuthDialog target = new OAuthDialog();
            Assert.IsNotNull(target);
            target.Close();            
        }
        

        /// <summary>
        ///A test for WarningMessage
        ///</summary>
        [TestMethod()]
        public void WarningMessageTest()
        {
            OAuthDialog target = new OAuthDialog();
            string message = "Warning";
            target.WarningMessage(message);            
        }

        /// <summary>
        ///A test for getAuthenticationData
        ///</summary>
        [TestMethod()]
        public void getAuthenticationDataTest()
        {
            OAuthDialog target = new OAuthDialog();
            target.OAuthData.Text = "abc";
            string expected = "abc";
            string actual;
            actual = target.getAuthenticationData();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for setAuthLink
        ///</summary>
        [TestMethod()]
        public void setAuthLinkTest()
        {
            OAuthDialog target = new OAuthDialog();           
            string authentication_link = "http://www.flickr.com";
            target.setAuthLink(authentication_link);
            Assert.AreEqual(authentication_link, target.OAuthLink.Text);            
        }
    }
}
