using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FlickrWPF;

namespace FlickrWPF_Test
{
    class FakeDriveEnumerator : IDriveEnumerator
    {
        public String[] GetDriveList()
        {
            String[] result = {"C:\\", "D:\\", "F:\\"};
            return result;
        }

        public bool IsRemovable(String drive)
        {
            if (drive.ToLower().StartsWith("f"))
                return true;

            return false;
        }

        public String[] GetRemovableDrives()
        {
            String[] result = {"F:\\"};
            return result;
        }
    }
}
