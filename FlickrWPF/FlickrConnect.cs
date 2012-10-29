using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using FlickrNet;

namespace FlickrWPF
{
    public class FlickrConnect : FlickrWPF.IFlickrConnect
    {
        private readonly String DEFAULT_APIKEY = "0496ec05b614d3cceace6a4ec4ebe5d4";
        private readonly String DEFAULT_SECRET = "11d7ee91d11a5e28";
        
        public String authToken;

        private Flickr flickrHandle;        

        public FlickrConnect(String api_key = null, String secret = null)
        {
            if (api_key == null)
                api_key = DEFAULT_APIKEY;
            if (secret == null)
                secret = DEFAULT_SECRET;

            flickrHandle = new Flickr(api_key, secret);
            authToken = null;
        }

        private void authorize(IOAuthDialog authDialog = null)
        {
            if (authDialog == null)
                authDialog = new OAuthDialog();

            OAuthRequestToken rtoken = flickrHandle.OAuthGetRequestToken("oob");
            String url = flickrHandle.OAuthCalculateAuthorizationUrl(rtoken.Token, AuthLevel.Delete);

            System.Diagnostics.Process.Start(url);
            authDialog.setAuthLink(url);

            if (authDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    String access_code = authDialog.getAuthenticationData();
                    OAuthAccessToken atoken = flickrHandle.OAuthGetAccessToken(rtoken, access_code);
                    authToken = atoken.Token;
                }
                catch
                {
                    authDialog.WarningMessage("Authentication failed");
                }
            }
            
        }        

        public void setOAuth(String oauth_token, String oauth_secret)
        {
            authToken = oauth_token;
            flickrHandle.OAuthAccessToken = oauth_token;
            flickrHandle.OAuthAccessTokenSecret = oauth_secret;
            Auth myauth= flickrHandle.AuthOAuthCheckToken();
        }


        //<summary>Adds a new set go the given collection. The parent_collection_id is not used since Flickr doesn't officially expose this API, atm</summary>
        public String AddSet(String parent_collection_id, String set_name, String representative_photo_filename)        
        {
            String photo_id = UploadPhoto(representative_photo_filename);
            Photoset new_photo_set = flickrHandle.PhotosetsCreate(set_name, photo_id);

            if (authToken == null) authorize();  

            Trace.WriteLine("Added photo " + representative_photo_filename + " to set " + set_name);

            return new_photo_set.PhotosetId;
        }

        //<summary>uploads photo to flickr and returns photo_id</summary>
        public String UploadPhoto(String filename)
        {
            if (authToken == null) authorize();  

            Trace.WriteLine("Uploading photo: " + filename);
            return flickrHandle.UploadPicture(filename);
        }

        //<summary>
        // Uploads the given picture file and adds it to the given set        
        //</summary>
        public String AddToSet(String photoset_id, String filename)
        {
            if (authToken == null) authorize();  

            String photo_id = UploadPhoto(filename);
            if (photo_id != null)
                flickrHandle.PhotosetsAddPhoto(photoset_id, photo_id);
            return photo_id;
        }


        //<summary>Checks if Photoset with the given name exists</summary>
        public bool PhotoSetExists(String photoset_name)
        {
            return (GetPhotoSetByName(photoset_name) != null);
        }

        //<summary>Returns all photos in the given set</summary>
        public Photo[] GetPhotosInSet(String photoset_id)
        {
            if (authToken == null) authorize();  

            Photo[] test = flickrHandle.PhotosetsGetPhotos(photoset_id, PhotoSearchExtras.DateTaken).ToArray();
            return test;
        }


        public String GetPhotoSetByName(String photoset_name)
        {
            String photoset_id = null;

            if (authToken == null) authorize();

            for (int page = 0; ; page++)
            {
                PhotosetCollection ps_col = flickrHandle.PhotosetsGetList(page, 500);
                if (ps_col.Count == 0)
                    break;
                foreach (Photoset ps in ps_col)
                {
                    if (ps.Title.ToLower() == photoset_name.ToLower())
                        return ps.PhotosetId;
                }               
            }
            return photoset_id;
        }

                /*
            CollectionCollection cc = cached_collection_collection;

            if (cc == null)
                cc = GetCollections();

            foreach (Collection col in cc)
            {
                photoset_id = GetPhotoSetByName(photoset_name, col);
                if (photoset_id != null)
                    return photoset_id;

            }

            return photoset_id;
                 */

            /*
        //<summary>Get the photoset id of the set with the given name</summary>
        String GetPhotoSetByName(String photoset_name, Collection col = null)
        {           
            //1. find if matching set is part of this collection
            foreach (CollectionSet set in col.Sets)
            {                
                if (set.Title == photoset_name)
                    return set.SetId;                                
            }

            //2. resursively screen sub-collections
            foreach (Collection sub_col in col.Collections)
            {
                String sub_result = GetPhotoSetByName(photoset_name, sub_col);
                if (sub_result != null)
                    return sub_result;
            }

            return null;
        }
             */

        //<summary>Deletes a given photo - only used for testing so far</summary>
        public void DeletePhoto(String photo_id)
        {
            if (authToken == null) authorize();

            flickrHandle.PhotosDelete(photo_id);
        }


        public Flickr GetFlickrHandle_forTesting()
        {
            return flickrHandle;
        }

        public CollectionCollection GetCollections(){

            if (authToken == null) authorize();           
           
            CollectionCollection cc = flickrHandle.CollectionsGetTree();

            return cc;
        }
        

    }
}
