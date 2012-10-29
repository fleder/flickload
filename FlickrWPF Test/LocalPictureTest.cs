using FlickrWPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FlickrWPF_Test
{
    
    
    /// <summary>
    ///This is a test class for LocalPictureTest and is intended
    ///to contain all LocalPictureTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LocalPictureTest
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
        ///A test for LocalPicture Constructor with empty string as filename
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),  "Empty file was found.")]
        public void LocalPictureConstructorFailTest()
        {
            string filename = "C:\\invalid_File_name.jpg"; 
            LocalPicture target = new LocalPicture(filename);
            
           Assert.Fail("Empty file name did not fail.");
        }

        /// <summary>
        ///A test for LocalPicture Constructor
        ///</summary>
        [TestMethod()]
        public void LocalPictureConstructorTest()
        {
            String filename = "c:\\temp\\Koala.jpg";
            LocalPicture target = new LocalPicture(filename);
        }

        /// <summary>
        ///A test for getTimeTakenOriginal
        ///</summary>
        [TestMethod()]
        public void getTimeTakenOriginalTest()
        {
            string filename = "c:\\temp\\Koala.jpg";
            LocalPicture target = new LocalPicture(filename);
            DateTime expected = new DateTime(2008, 02, 11, 11, 32, 43); //"2008:02:11 11:32:43\0";
            DateTime actual;
            actual = target.getTimeTakenOriginal();
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for getTimeTakenOriginal
        ///</summary>
        [TestMethod()]
        public void timeConversionTest()
        {
            String time = "2008:02:11 11:32:43\0".Trim("\0 \n\t".ToCharArray());

            DateTime expected = new DateTime(2008, 02, 11, 11, 32, 43); //"2008:02:11 11:32:43\0";
            DateTime actual = DateTime.ParseExact(time, "yyyy:MM:dd HH:mm:ss", null);
            
            Assert.AreEqual(expected, actual);
        }
    }
}
