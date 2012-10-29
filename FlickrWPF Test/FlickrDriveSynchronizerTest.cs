using FlickrWPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FlickrWPF_Test
{


    class FlickrDriveSynchronizerTestExtend : FlickrDriveSynchronizer
    {        
        public FlickrDriveSynchronizerTestExtend(IDriveEnumerator drive_enum, IFolderEnumerator folder_enum, IFlickrConnect flickr_connect) :
            base(drive_enum, folder_enum, flickr_connect)
        {
        }

        ILocalPicture GetPicture(String filename)
        {
            return new FakeLocalPicture(filename);
        }
    }

    /// <summary>
    ///This is a test class for FlickrDriveSynchronizerTest and is intended
    ///to contain all FlickrDriveSynchronizerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FlickrDriveSynchronizerTest
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

        FlickrDriveSynchronizer_Accessor CreateAccessor(IFlickrConnect fc)
        {
            IDriveEnumerator de = new FakeDriveEnumerator();
            IFolderEnumerator fe = new FakeFolderEnumerator();            

            FlickrDriveSynchronizer fds = new FlickrDriveSynchronizer(de, fe, fc);
            //FlickrDriveSynchronizer fds = new FlickrDriveSynchronizerTestExtend(de, fe, fc);            

            PrivateObject param0 = new PrivateObject(fds);
            FlickrDriveSynchronizer_Accessor retval = new FlickrDriveSynchronizer_Accessor(param0);
            retval.ILocalPictureObjectType = typeof(FakeLocalPicture);

            return retval;
        }

        /// <summary>
        ///A test for Sync
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void SyncTest()
        {
            IFlickrConnect fc = new FakeFlickrConnect();

            FlickrDriveSynchronizer_Accessor target = CreateAccessor(fc);
            string photo_folder = "Bilder"; 

            target.Sync(photo_folder);
            Assert.AreEqual(target.m_test_sync_root_folders.Count, 1);
            Assert.AreEqual(target.m_test_sync_root_folders[0], "F:\\Bilder");
        }

        /// <summary>
        ///A test for SyncFolder
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void SyncRootFolderTest()
        {
            IFlickrConnect fc = new FakeFlickrConnect();
            FlickrDriveSynchronizer_Accessor target = CreateAccessor(fc);
            
            string root_path = "F:\\Bilder"; 
            target.SyncRootFolder(root_path);

            Assert.IsTrue(target.m_test_sync_folders.Count == 3);
            Assert.IsTrue(target.m_test_sync_folders.Contains("F:\\Bilder\\Collection A\\Collection A.A\\PhotoSet 1"));
            Assert.IsTrue(target.m_test_sync_folders.Contains("F:\\Bilder\\Collection B\\PhotoSet 2"));
            Assert.IsTrue(target.m_test_sync_folders.Contains("F:\\Bilder\\Collection B\\PhotoSet 3"));
        }

        /// <summary>
        ///A test for SyncFolder
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void SyncFolderTest3()
        {
            FakeFlickrConnect fc = new FakeFlickrConnect();
            FlickrDriveSynchronizer_Accessor target = CreateAccessor(fc);
            string path = "F:\\Bilder\\Collection B\\PhotoSet 3";
            target.SyncFolder(path);

            Assert.AreEqual(fc.m_created_sets.Count, 1);
            Assert.IsTrue(fc.m_created_sets.Contains("PhotoSet 3"));

            Assert.AreEqual(fc.m_uploaded_files.Count, 1);
            Assert.AreEqual(fc.m_uploaded_files[0], "F:\\Bilder\\Collection B\\PhotoSet 3\\b3_1.jpg");
        }

        /// <summary>
        ///A test for SyncFolder
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void SyncFolderTest2()
        {
            FakeFlickrConnect fc = new FakeFlickrConnect();
            FlickrDriveSynchronizer_Accessor target = CreateAccessor(fc);
            string path = "F:\\Bilder\\Collection B\\PhotoSet 2";
            target.SyncFolder(path);

            Assert.AreEqual(fc.m_created_sets.Count, 0);

            Assert.AreEqual(fc.m_uploaded_files.Count, 2);
            Assert.IsTrue(fc.m_uploaded_files.Contains("F:\\Bilder\\Collection B\\PhotoSet 2\\b2_1.jpg"));
            Assert.IsTrue(fc.m_uploaded_files.Contains("F:\\Bilder\\Collection B\\PhotoSet 2\\b2_2.jpg"));
        }

        /// <summary>
        ///A test for SyncFolder
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void SyncFolderTest1()
        {
            FakeFlickrConnect fc = new FakeFlickrConnect();
            FlickrDriveSynchronizer_Accessor target = CreateAccessor(fc);            

            string path = "F:\\Bilder\\Collection A\\Collection A.A\\PhotoSet 1";
            target.SyncFolder(path);

            Assert.AreEqual(0, fc.m_created_sets.Count);

            Assert.AreEqual(1, fc.m_uploaded_files.Count);
            Assert.AreEqual("F:\\Bilder\\Collection A\\Collection A.A\\PhotoSet 1\\aa1_1.jpg", fc.m_uploaded_files[0]);
        }
    }
}
