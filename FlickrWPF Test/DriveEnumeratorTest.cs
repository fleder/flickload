using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FlickrWPF;

namespace FlickrWPF_Test
{
    [TestClass]
    public class DriveEnumeratorTest
    {
        [TestMethod]
        public void TestEnumeration()
        {
            DriveEnumerator de = new DriveEnumerator();
            String[] drive_list = de.GetDriveList();
            Assert.IsTrue(drive_list.Contains("C:\\"));
        }

        /*
        [TestMethod]
        public void TestGetRemovableEnumeration()
        {
            DriveEnumerator de = new DriveEnumerator();
            String[] removable_drives = de.GetRemovableDrives();
            Assert.IsTrue(removable_drives.Contains("F:\\"));
            Assert.IsFalse(removable_drives.Contains("Z:\\"));
        }

        [TestMethod]
        public void TestIsRemovable()
        {
            DriveEnumerator de = new DriveEnumerator();
            Assert.IsFalse(de.IsRemovable("C"));
            Assert.IsFalse(de.IsRemovable("C:"));
            Assert.IsFalse(de.IsRemovable("C:\\"));

            if (de.GetDriveList().Contains("F:\\"))
            {
                Assert.IsTrue(de.IsRemovable("F"));
            }
        }
         */


    }
}