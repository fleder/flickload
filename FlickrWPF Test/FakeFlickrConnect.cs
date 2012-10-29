using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using FlickrWPF;
using FlickrNet;

namespace FlickrWPF_Test
{
    class FakeFlickrConnect : IFlickrConnect
    {

        public List<String> m_uploaded_files = new List<string>();
        public List<String> m_created_sets = new List<string>();

        public string AddToSet(string photoset_id, string filename)
        {
            UploadPhoto(filename);
            Trace.WriteLine("Adding photo " + filename + " to set " + photoset_id);            
            return filename;
        }

        public String AddSet(String parent_collection_id, String set_name, String representative_filename)
        {
            UploadPhoto(representative_filename);
            Trace.WriteLine("Adding set:" + set_name + " with representative filename: " + representative_filename);
            m_created_sets.Add(set_name);
            return representative_filename;
        }

        public bool PhotoSetExists(String photoset_name)
        {
            String[] fake_photosets = { "PhotoSet 1", "PhotoSet 2" };

            Trace.WriteLine("Checking existence of photo: " + photoset_name);

            foreach (String set in fake_photosets)
            {
                if (set.Equals(photoset_name))
                    return true;
            }

            return false;
        }

        public String GetPhotoSetByName(String photoset_name)
        {
            if (photoset_name == "PhotoSet 1")
                return "ps1";
            else if (photoset_name == "PhotoSet 2")
                return "ps2";
            else
                return photoset_name;
        }

        //<summary>returns all photo_ids in set</summary>
        public Photo[] GetPhotosInSet(String photoset_id)
        {
            List<Photo> fake_result = new List<Photo>();

            Trace.WriteLine("Getting photos for set: " + photoset_id);

            if (photoset_id == "ps1")
            {
                fake_result.Add(new Photo());
                fake_result[0].Title = "aa1_2";
                fake_result[0].DateTaken = new DateTime(2010, 10, 10, 10, 10, 10);
            }

            return fake_result.ToArray();
        }

        public FlickrNet.CollectionCollection GetCollections()
        {
            Trace.WriteLine("Returning collections");
            return new FakeCollectionCollection().fake_collection;
        }

        public String UploadPhoto(String filename)
        {
            Trace.WriteLine("Uploading photo " + filename);
            m_uploaded_files.Add(filename);
            return filename;
        }
    }
}
