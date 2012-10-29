using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FlickrWPF
{
    public class FolderEnumerator : FlickrWPF.IFolderEnumerator
    {
        public bool Contains(String folder, String sub_folder)
        {            
            String[] sub_folders = GetSubDirectories(folder, false);
            List<String> matching_subfolders = sub_folders.Where(d => d.EndsWith("\\" + sub_folder)).ToList();
            if (matching_subfolders.Count == 1)
                return true;

            return false;
        }

        public bool ContainsFileTypes(string folder, string[] suffixes)
        {
            String[] files = GetFilesOfType(folder, suffixes);
            if (files.Length > 0)
                return true;                 
            return false;
        }

        public String[] GetFilesOfType(String folder, string[] suffixes)
        {
            List<String> result = new List<String>();

            String[] files = Directory.GetFiles(folder);
            
            //This loop doesn't scale well. TODO: fix for performance reasons
            foreach (String f in files)
            {
                foreach (String suffix in suffixes)
                {
                    if ( f.ToLower().EndsWith( suffix.ToLower() ) )
                        result.Add(f);
                }
            }
            return result.ToArray();
        }

        public String[] GetSubDirectories(String path, bool recursive = true)
        {
            String[] result = new String[0];

            try
            {
                if (recursive)
                    result = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
                else
                    result = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            }
            catch (IOException e)
            {
                System.Diagnostics.Trace.WriteLine("Error getting subfolders: " + e.Message);
            }

            return result;
        }

        //<summary>Strip parent path name from all entries in the path list, by removing the parent path part</summary>
        public String[] StripParentPath(String parent_path, String[] path_list)
        {
            List<String> result = new List<String>();
            
            parent_path = parent_path.ToLower();            

            foreach (String path in path_list) {
                if (path.ToLower().StartsWith(parent_path))
                {
                    String stripped_path = path.Substring(parent_path.Length);
                    if (stripped_path.StartsWith("\\"))
                        stripped_path = stripped_path.Substring(1);
                    result.Add(stripped_path);
                }
                else
                    result.Add(path);
            }

            return result.ToArray();
        }


    }
}
