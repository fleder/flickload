using FlickrWPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Controls;
using System.Diagnostics;

namespace FlickrWPF_Test
{
    
    
    /// <summary>
    ///This is a test class for TextFieldStreamTest and is intended
    ///to contain all TextFieldStreamTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TextFieldStreamTest
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
        ///A test for Write
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void WriteTest()
        {
            TextBox param0 = new TextBox();
            TextFieldStream_Accessor target = new TextFieldStream_Accessor(param0);
            byte[] buffer = {72, 101, 108, 108, 111, 32, 87, 111, 114, 108, 100}; //"Hello World"
            int offset = 1; 
            int count = 4; 
            target.Write(buffer, offset, count);
            Assert.AreEqual(param0.Text, "ello");            
        }

        /// <summary>
        ///A test for using TextFieldStream for Trace
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void WriteTraceTest()
        {
            TextBox param0 = new TextBox();
            TextFieldStream target = new TextFieldStream(param0);

            String input_text = "Hello World";

            Trace.Listeners.Add(new TextWriterTraceListener(target));
            Trace.Write(input_text);
            Trace.Flush();
            
            Assert.AreEqual(param0.Text, input_text);
        }
    }
}
