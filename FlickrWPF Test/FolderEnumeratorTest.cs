using FlickrWPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FlickrWPF_Test
{
    
    /// <summary>
    ///This is a test class for FolderEnumeratorTest and is intended
    ///to contain all FolderEnumeratorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FolderEnumeratorTest
    {

        private readonly String MAIN_PATH = "C:\\temp\\enumtest";

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

        private readonly String[] test_dir_list =  {            
            "FolderA\\SubFolderA_A\\SubSubFolderA_A_A",
            "FolderA\\SubFolderA_A\\SubSubFolderA_A_B",
            "FolderA\\SubFolderA_B",
            "FolderA\\SubFolderA_C",

            "FolderB\\SubFolderB_A",
            "FolderB\\SubFolderB_B",
        };

        [TestCleanup()]
        public void FolderEnumeratorCleanup()
        {
            try
            {
                System.IO.Directory.Delete(MAIN_PATH, true);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
            }
        }
        
        [TestInitialize()]
        public void FolderEnumeratorInitialize()
        {
            FolderEnumeratorCleanup();
            for (int i=0; i < test_dir_list.Length; i++)
                System.IO.Directory.CreateDirectory(MAIN_PATH + "\\" + test_dir_list[i]);

            System.IO.FileStream newfile = System.IO.File.Create(MAIN_PATH + "\\FolderB\\SubFolderB_A\\test.jpg");
            newfile.Close();
        }




        /// <summary>
        ///A test for GetSubDirectories (non-recursive)
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void GetSubDirectoriesTest()
        {
            FolderEnumerator target = new FolderEnumerator();
            string path = MAIN_PATH;
            bool recursive = false;             
            string[] actual;
            actual = target.GetSubDirectories(path, recursive);
            Assert.AreEqual(actual.Length, 2);
        }

        /// <summary>
        ///A test for GetSubDirectories recursive
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void GetSubDirectoriesRecursiveTest()
        {
            FolderEnumerator target = new FolderEnumerator();
            string path = MAIN_PATH; 
            bool recursive = true; 
            string[] actual;
            actual = target.GetSubDirectories(path, recursive);
            Assert.AreEqual(9, actual.Length);
        }

        /// <summary>
        ///A test for Contains (really contains)
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void ContainsNotTest()
        {
            FolderEnumerator target = new FolderEnumerator();
            string sub_folder = "thisisnotasubdirectory";
            string folder = MAIN_PATH;
            bool expected = false;
            bool actual;
            actual = target.Contains(folder, sub_folder);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for Contains (does not contain)
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FlickrWPF.exe")]
        public void ContainsTest()
        {
            FolderEnumerator target = new FolderEnumerator();
            string sub_folder = "SubFolderA_A";
            string folder = MAIN_PATH + "\\" + "FolderA";
            bool expected = true;
            bool actual;
            actual = target.Contains(folder, sub_folder);
            Assert.AreEqual(expected, actual);
        }


        //<summary>Test if jpg in folder is found </summary>
        [TestMethod()]
        public void ContainsFileTypesTest()
        {
            FolderEnumerator target = new FolderEnumerator();
            string folder = MAIN_PATH + "\\FolderB\\SubFolderB_A";
            string[] suffixes = { "png", "jpg"};
            bool expected = true; 
            bool actual;
            actual = target.ContainsFileTypes(folder, suffixes);
            Assert.AreEqual(expected, actual);            
        }

        //<summary>Test if jpg in folder is NOT found </summary>
        [TestMethod()]
        public void ContainsFileTypesNotTest()
        {
            FolderEnumerator target = new FolderEnumerator();
            string folder = MAIN_PATH + "\\FolderB\\SubFolderB_B";
            string[] suffixes = { "jpg" };
            bool expected = false;
            bool actual;
            actual = target.ContainsFileTypes(folder, suffixes);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetFilesOfType
        ///</summary>
        [TestMethod()]
        public void GetFilesOfTypeTest()
        {
            FolderEnumerator target = new FolderEnumerator();
            string folder = MAIN_PATH + "\\FolderB\\SubFolderB_A";
            string[] suffixes = {"jpg", "png"}; 
            string[] actual;
            actual = target.GetFilesOfType(folder, suffixes);
            Assert.AreEqual(actual.Length, 1);
        }

        /// <summary>
        ///A test for StripParentPath
        ///</summary>
        [TestMethod()]
        public void StripParentPathTest()
        {
            FolderEnumerator target = new FolderEnumerator(); 
            string parent_path = "FolderA";
            string[] path_list = test_dir_list;
            string[] actual;
            actual = target.StripParentPath(parent_path, path_list);
            Assert.AreEqual(actual.Length, path_list.Length);
            Assert.IsFalse( actual[0].StartsWith(parent_path) );
            Assert.IsFalse( actual[0].StartsWith("\\") );
            Assert.IsFalse(actual[1].StartsWith(parent_path));
        }

        /// <summary>
        ///A test for StripParentPath
        ///</summary>
        [TestMethod()]
        public void StripParentPathTest2()
        {
            FolderEnumerator target = new FolderEnumerator();
            string parent_path = "FolderA\\";
            string[] path_list = test_dir_list;
            string[] actual;
            actual = target.StripParentPath(parent_path, path_list);
            Assert.AreEqual(actual.Length, path_list.Length);
            Assert.IsFalse(actual[0].StartsWith(parent_path));
            Assert.IsFalse(actual[0].StartsWith("\\"));
            Assert.IsFalse(actual[1].StartsWith(parent_path));
            Assert.IsTrue(actual[0].StartsWith("Sub"));
        }
    }
}
