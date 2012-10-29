using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FlickrWPF;

namespace FlickrWPF_Test
{
    //<summary>This class implements a fake folder structure that looks like this
    //  F:\\Bilder\\Collection A\\Collection A.A\\PhotoSet 1\\
    //                                                        aa1_1.jpg
    //                                                        aa1_2.jpg
    //              Collection B\\PhotoSet 2\\
    //                                        b2_1.jpg
    //                                        b2_2.jpg
    //                             PhotoSet 3\\
    //                                        b3_1.jpg
    class FakeFolderEnumerator : IFolderEnumerator
    {

        String[] folder_list = { "F:\\Bilder",
            "F:\\Bilder\\Collection A",
            "F:\\Bilder\\Collection A\\Collection A.A",
            "F:\\Bilder\\Collection A\\Collection A.A\\PhotoSet 1",
            "F:\\Bilder\\Collection B",
            "F:\\Bilder\\Collection B\\PhotoSet 2",
            "F:\\Bilder\\Collection B\\PhotoSet 3"
        };

        public bool Contains(string folder, string sub_folder)
        {
            foreach(String cur_folder in folder_list)
            {
                if (!folder.Equals(cur_folder) && cur_folder.StartsWith(folder))
                    return true;
            }
            return false;
        }

        public bool ContainsFileTypes(string folder, string[] suffixes)
        {
            if (GetFilesOfType(folder, suffixes).Length > 0)
                return true;
            return false;
        }

        public String[] GetFilesOfType(String folder, string[] suffixes)
        {
            String[] result = new String[0];

            if (folder == "F:\\Bilder\\Collection A\\Collection A.A\\PhotoSet 1")
                result = new String[] { "F:\\Bilder\\Collection A\\Collection A.A\\PhotoSet 1\\aa1_1.jpg",
                                        "F:\\Bilder\\Collection A\\Collection A.A\\PhotoSet 1\\aa1_2.jpg" };
            else if (folder == "F:\\Bilder\\Collection B\\PhotoSet 2")
                result = new String[] { "F:\\Bilder\\Collection B\\PhotoSet 2\\b2_1.jpg",
                                        "F:\\Bilder\\Collection B\\PhotoSet 2\\b2_2.jpg" };
            else if (folder == "F:\\Bilder\\Collection B\\PhotoSet 3")
                result = result = new String[] { "F:\\Bilder\\Collection B\\PhotoSet 3\\b3_1.jpg" };

            return result;
        }

        public string[] GetSubDirectories(string path, bool recursive = true)
        {
            List<String> result = new List<String>();

            if (!recursive)
            {
                throw new NotImplementedException();
            }
            else
            {
                foreach (String fakefolder in folder_list)
                    if (fakefolder.StartsWith(path))
                        result.Add(fakefolder);
            }

            return result.ToArray();
        }

        public String[] StripParentPath(String parent_path, String[] path_list)
        {
            List<String> result = new List<String>();

            foreach (String cur_folder in path_list)
            {
                if (cur_folder.ToLower().StartsWith(parent_path.ToLower()))
                {
                    result.Add(cur_folder.Substring(parent_path.Length + 1));
                }
                else
                    result.Add(cur_folder);
            }
            
            return result.ToArray();
        }
    }
}
