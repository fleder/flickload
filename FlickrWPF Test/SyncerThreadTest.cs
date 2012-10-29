using FlickrWPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace FlickrWPF_Test
{
    
    
    /// <summary>
    ///This is a test class for SyncerThreadTest and is intended
    ///to contain all SyncerThreadTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SyncerThreadTest
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
        ///A test for DisplayException
        ///</summary>
        [TestMethod()]
        public void DisplayExceptionTest()
        {        
            System.IO.StringWriter swriter = new System.IO.StringWriter();
            Trace.Listeners.Add( new TextWriterTraceListener( swriter ) );
            Trace.WriteLine("Test");

            try
            {
                int b = 0;
                int a = 500 / b;
            }
            catch (Exception e)
            {
                SyncerThread.DisplayException(e);
            }

            String log = swriter.ToString();
            Assert.IsTrue(log.Contains("Test"));
            Assert.IsTrue(log.Contains("Exception"));
            Assert.IsTrue(log.Contains("DivideByZeroException"));
            Assert.IsTrue(log.Contains("DisplayExceptionTest"));
        }
    }
}
