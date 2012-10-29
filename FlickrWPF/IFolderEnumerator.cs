using System;
namespace FlickrWPF
{
    interface IFolderEnumerator
    {
        bool Contains(string folder, string sub_folder);        
        bool ContainsFileTypes(string folder, string[] suffixes);

        String[] GetFilesOfType(String folder, string[] suffixes);

        string[] GetSubDirectories(string path, bool recursive = true);

        String[] StripParentPath(String parent_path, String[] path_list);
    }
}
