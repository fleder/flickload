using System;

using FlickrNet;

namespace FlickrWPF
{
    interface IFlickrConnect
    {
        string AddToSet(string photoset_id, string filename);

        String AddSet(String parent_collection_id, String set_name, String representative_filename);
        //String AddCollection(String parent_collection_id, String collection_name);

        String GetPhotoSetByName(String photoset_name);
        bool PhotoSetExists(String photoset_name);

        //<summary>returns all photo_ids in set</summary>
        Photo[] GetPhotosInSet(String photoset_id);

        FlickrNet.CollectionCollection GetCollections();

        String UploadPhoto(String filename);
    }
}
