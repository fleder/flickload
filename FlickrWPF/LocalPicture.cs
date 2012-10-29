using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace FlickrWPF
{

    class LocalPicture : FlickrWPF.ILocalPicture
    {
        String m_filename = null;
        Image m_picture = null;

        const int ExifDateTimeOriginal = 0x9003;

        public LocalPicture(String filename)
        {
            m_filename = filename;
            m_picture = new Bitmap(filename);
        }        

        public DateTime getTimeTakenOriginal()
        {
            String result = "";            

            if (m_picture == null)
            {
                throw new ArgumentException("Picture not loaded");
            }
            
            System.Drawing.Imaging.PropertyItem[] timetaken_prop = m_picture.PropertyItems;
            timetaken_prop = timetaken_prop.Where(d => d.Id == ExifDateTimeOriginal).ToArray();
            if (timetaken_prop.Length > 0)
                result = System.Text.Encoding.ASCII.GetString(timetaken_prop[0].Value).Trim("\0 \n\t".ToCharArray() );

            DateTime retval;
            try
            {
                retval = DateTime.ParseExact(result, "yyyy:MM:dd HH:mm:ss", null);
            }
            catch (Exception)
            {
                retval = new DateTime();
            }
            return retval;
        }

        ~LocalPicture()
        {
            m_picture.Dispose();
        }
        
    }
}
