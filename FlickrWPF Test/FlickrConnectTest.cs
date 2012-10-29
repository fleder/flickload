using FlickrWPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FlickrNet;

namespace FlickrWPF_Test
{
    
    
    /// <summary>
    ///This is a test class for FlickrConnectTest and is intended
    ///to contain all FlickrConnectTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FlickrConnectTest
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

      
        private void setOAuthForTst(FlickrConnect fconnect)
        {
            System.IO.StreamReader cred_file = new System.IO.StreamReader("c:\\temp\\testcredentials.txt");
            String oauth_token = cred_file.ReadLine();
            String oauth_secret = cred_file.ReadLine();
            cred_file.Close();
            fconnect.setOAuth(oauth_token, oauth_secret);
        }

        /// <summary>
        ///A test for GetCollection
        ///</summary>
        [TestMethod()]
        public void GetCollectionTest()
        {
            FlickrConnect target = new FlickrConnect(null, null);

            setOAuthForTst(target);

            FlickrNet.CollectionCollection result = target.GetCollections();
            
            bool found_result = false;
            for (int i = 0; i < result.Count; i++)
                if (result[i].Title == "2009")
                    found_result = true;

            Assert.IsTrue( found_result );
        }

        /// <summary>
        ///A test for AddToSet
        ///</summary>
        [TestMethod()]
        public void AddToSetTest()
        {
            FlickrConnect target = new FlickrConnect(null, null);
            setOAuthForTst(target);

            string photoset_id = "72157631845760499"; // TestAlbum
            string filename = "c:\\temp\\Koala.jpg"; 
            string actual;
            actual = target.AddToSet(photoset_id, filename);
            Assert.IsTrue( actual.Length > 0 );
            target.DeletePhoto(actual);
        }

        /// <summary>
        ///A test for AddToSet failure
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.IO.FileNotFoundException), "Nonexistent file was found.")]
        public void AddToSetFailTest()
        {
            FlickrConnect target = new FlickrConnect(null, null);
            setOAuthForTst(target);

            string photoset_id = "72157631845760499"; // TestAlbum
            string filename = "c:\\nonexistentfile"; 
            string actual;            
            actual = target.AddToSet(photoset_id, filename);
            Assert.IsTrue( actual.Length == 0 );
        }

        /// <summary>
        ///A test for AddSet
        ///</summary>
        [TestMethod()]
        public void AddSetTest()
        {
            FlickrConnect target = new FlickrConnect(null, null);
            setOAuthForTst(target);

            string parent_collection_id = null; 
            string set_name = "DeleteMeEmptySet"; 
            string representative_filename = "c:\\temp\\koala.jpg"; 
            string actual;
            actual = target.AddSet(parent_collection_id, set_name, representative_filename);
            Assert.IsTrue(actual.Length > 0);

            target.GetFlickrHandle_forTesting().PhotosetsDelete(actual);
        }
        

        /// <summary>
        ///A test for PhotoSetExists
        ///</summary>
        [TestMethod()]
        public void PhotoSetExistsTest()
        {
            FlickrConnect target = new FlickrConnect(null, null);

            setOAuthForTst(target);

            //target.cached_collection_collection = new FakeCollectionCollection().fake_collection;

            string photoset_name = "TestAlbum";            
            bool actual;
            actual = target.PhotoSetExists(photoset_name);
            Assert.IsNotNull(actual);
        }



        /// <summary>
        ///A test for UploadPhoto
        ///</summary>
        [TestMethod()]
        public void UploadPhotoTest()
        {
            FlickrConnect target = new FlickrConnect(null, null);
            setOAuthForTst(target);

            string filename = "c:\\temp\\koala.jpg";
            string actual;
            actual = target.UploadPhoto(filename);
            Assert.IsNotNull(actual);
            target.GetFlickrHandle_forTesting().PhotosDelete(actual);
        }

        /// <summary>
        ///A test for GetPhotosInSet
        ///</summary>
        [TestMethod()]
        public void GetPhotosInSetTest()
        {            
            FlickrConnect target = new FlickrConnect(null, null);
            setOAuthForTst(target);

            string photoset_id = "72157631845760499";
            Photo[] actual;
            actual = target.GetPhotosInSet(photoset_id);
            
            List<Photo> actual_list = new List<Photo>(actual);

            Assert.IsTrue( actual_list.Count == 1);
            Assert.IsTrue(actual_list[0].Title == "IMG_0059");
        }


        /// <summary>
        ///A test for GetPhotoSetByName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void GetPhotoSetByNameTest()
        {
            FlickrConnect target = new FlickrConnect(null, null);
            string photoset_name = "TestAlbum";
            string actual;

            setOAuthForTst(target);

            /*
            CollectionCollection fcol = new FakeCollectionCollection().fake_collection;
            Collection col = fcol[1];
             */
            actual = target.GetPhotoSetByName(photoset_name);
            Assert.IsNotNull(actual);
            //Assert.AreEqual(expected, actual);
        }

    }
}
