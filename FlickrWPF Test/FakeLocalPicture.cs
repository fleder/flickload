using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FlickrWPF;

namespace FlickrWPF_Test
{
    class FakeLocalPicture : ILocalPicture
    {
        String m_filename = null;
        DateTime m_time_taken;

        public FakeLocalPicture(String filename)
        {
            m_filename = filename;
            m_time_taken = new DateTime(2010, 10, 10, 10, 10, 10);
        }

        public DateTime getTimeTakenOriginal()
        {
            return m_time_taken; //fake the time taken
        }
    }
}
