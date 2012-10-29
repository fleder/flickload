using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FlickrWPF
{
    public class DriveEnumerator : IDriveEnumerator
    {       

        // Implementation of IDriveEnumerator
        //<summary>
        //Enumerates all local drives
        //</summary>
        public String[] GetDriveList()
        {
            String[] result = new String[0];
            try{
                result = Environment.GetLogicalDrives();
            }
            catch (System.IO.IOException e)
            {
            
                System.Diagnostics.Trace.WriteLine("Failed to get drive letters: " + e.Message);
            }
            return result;
        }


        //<summary>
        //returns an array of drive letters that are from removable devices
        //</summary>
        public String[] GetRemovableDrives()
        {
            String[] result = new String[0];
            try
            {
                DriveInfo[] dinfo = DriveInfo.GetDrives();
                result = dinfo.Where(d=> d.DriveType == DriveType.Removable).Select(d => d.Name).ToArray();
            }
            catch (IOException e)
            {
                System.Diagnostics.Trace.WriteLine("Failed to get removable drive letters: " + e.Message);
            }
            return result;
        }

        //<summary>
        //Returns true if the given drive letter is a removable device
        //</summary>
        //<param name="drive">Name of the drive to check if removable device</param>
        public bool IsRemovable(String drive)
        {
            List<String> drive_list = GetRemovableDrives().Where(d => d.StartsWith(drive)).ToList();
            if (drive_list.Count == 1)
                return true;

            return false;
        }
    }
}
