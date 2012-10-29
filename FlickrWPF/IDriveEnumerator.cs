using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlickrWPF
{
    interface IDriveEnumerator
    {
        String[] GetDriveList();
        bool IsRemovable(String drive);
        String[] GetRemovableDrives();
    }
}
