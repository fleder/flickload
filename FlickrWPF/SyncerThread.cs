using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace FlickrWPF
{
    class SyncerThread
    {

        MainWindow m_main_win;
        String m_root_folder;        

        public SyncerThread(MainWindow main_win, String root_folder)
        {
            m_main_win = main_win;
            m_root_folder = root_folder;
        }

        public static void DisplayException(Exception e)
        {
            Trace.TraceError(e.ToString());
        }

        public void runner()
        {
            IFlickrConnect fc = new FlickrConnect();
            IDriveEnumerator de = new DriveEnumerator();
            IFolderEnumerator fe = new FolderEnumerator();

            FlickrDriveSynchronizer syncer = new FlickrDriveSynchronizer(de, fe, fc);
            try
            {
                syncer.Sync(m_root_folder);
            }
            catch (Exception e)
            {
                DisplayException(e);
            }

            if (Application.Current.Dispatcher.CheckAccess())
                EnableStartButton();
            else
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new Action(() => { EnableStartButton(); }));
            }            
        }

        void EnableStartButton()
        {
            m_main_win.StartButton.IsEnabled = true;
        }
    }
}
