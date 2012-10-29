using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using FlickrNet;

namespace FlickrWPF
{
    class FlickrDriveSynchronizer
    {
        IDriveEnumerator m_drive_enum;
        IFolderEnumerator m_folder_enum;
        IFlickrConnect m_flickr_connect;

        Type ILocalPictureObjectType = typeof(LocalPicture);

        String[] m_pictures_suffixes = { "jpg", "png" };

        public List<String> m_test_sync_root_folders = new List<string>();
        public List<String> m_test_sync_folders = new List<string>();        

        public FlickrDriveSynchronizer(IDriveEnumerator drive_enum, IFolderEnumerator folder_enum, IFlickrConnect flickr_connect)
        {
            m_drive_enum = drive_enum;
            m_folder_enum = folder_enum;
            m_flickr_connect = flickr_connect;
        }

        //<summary>
        // Synchronizes all removable drives that contain the given subfolder on top-level.        
        //</summary>
        public void Sync(String photo_folder)
        {
            String[] drives = m_drive_enum.GetDriveList();

            if (drives.Length == 0)
                Trace.WriteLine("No drives found");

            foreach (String drive in drives)
            {
                if (m_folder_enum.Contains(drive, photo_folder))
                {
                    Trace.WriteLine( String.Format("Searching drive {0} for folder {1}", drive, photo_folder) );
                    String root_path = drive;                    
                    if (! (root_path.Last() == Path.DirectorySeparatorChar) )
                        root_path += Path.DirectorySeparatorChar;
                    root_path += photo_folder;

                    //test and logging
                    Trace.WriteLine("Syncing: " + root_path);
                    m_test_sync_root_folders.Add(root_path);

                    SyncRootFolder(root_path);
                }
            }
            Trace.WriteLine("Finished");
        }

        //<summary>Check all subfolder that contain pictures</summary>
        void SyncRootFolder(String root_path)
        {            
            foreach (String folder in m_folder_enum.GetSubDirectories(root_path, true))
            {
                if (m_folder_enum.ContainsFileTypes(folder, m_pictures_suffixes))
                {
                    //test and logging
                    m_test_sync_folders.Add(folder);
                    Trace.WriteLine("Folder contains files: " + folder);

                    SyncFolder(folder);
                }
            }
        }


        //<summary>Checks of the folder already exists as an album on flickr and synchronizes all folder in the current folder.</summary>
        void SyncFolder(String path)
        {
            String[] picture_files = m_folder_enum.GetFilesOfType(path, m_pictures_suffixes);

            if (!(path.Last() == Path.DirectorySeparatorChar))
                path += Path.DirectorySeparatorChar;

            String base_path = Path.GetDirectoryName(path).Split(Path.DirectorySeparatorChar).Last();
            String photoset_id;
            
            if (picture_files.Length < 1) return;                

            if (!m_flickr_connect.PhotoSetExists(base_path))
            {
                m_flickr_connect.AddSet(null, base_path, picture_files[0]);
                picture_files = picture_files.Skip(1).ToArray();                
            }            
            
            photoset_id = m_flickr_connect.GetPhotoSetByName(base_path);

            SyncFilesToSet(photoset_id, picture_files);
        }

        //<summary>Gets the picture object for the given filename. 
        //Important for testing because will be overwritten by controllable object</summary>
        ILocalPicture GetPicture(String filename)
        {
            Object[] parameters = {filename};
            return (ILocalPicture)Activator.CreateInstance( ILocalPictureObjectType, parameters);
        }

        bool IsFileInSet(String filename, Photo[] photo_set)
        {            

            DateTime date_taken = GetPicture(filename).getTimeTakenOriginal();
            String pic_basename = Path.GetFileNameWithoutExtension(filename).ToLower();

            foreach (Photo photo in photo_set)
            {
                String lower_title = photo.Title.ToLower();
                if ((date_taken == photo.DateTaken) && (photo.Title.ToLower() == pic_basename))
                    return true;
                else if (lower_title == pic_basename)
                {
                    Trace.WriteLine("Pictures with same titles but different timestamps: " + lower_title, "WARNING");
                    return true; //don't upload for safety
                }
                else if (date_taken == photo.DateTaken)
                    Trace.WriteLine("Picture with same time taken but seems to have different name: " + pic_basename + " - Flickr name: " + photo.Title);
            }

            return false;
        }


        //<summary>Sync the given filenames against the photoset given</summary>
        void SyncFilesToSet(String photoset_id, String[] picture_filenames)
        {
            Photo[] set_photos = m_flickr_connect.GetPhotosInSet(photoset_id);            

            foreach (String picture_filename in picture_filenames)
            {
                if (!IsFileInSet(picture_filename, set_photos))
                    m_flickr_connect.AddToSet(photoset_id, picture_filename);
                else
                    Trace.WriteLine(String.Format("Skipping {0} because exists", picture_filename));                
            }
        }
        
    }
}
