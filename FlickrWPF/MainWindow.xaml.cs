using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

using System.Diagnostics;

namespace FlickrWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread sync_thread = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            LogWindow.Text = "";                        

            SyncerThread st = new SyncerThread(this, rootFolder.Text);
            sync_thread = new Thread(new ThreadStart(st.runner));
            sync_thread.Start();

            while (!sync_thread.IsAlive);
            StartButton.IsEnabled = false;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            TextFieldStream textfield_stream = new TextFieldStream(LogWindow);
            Trace.Listeners.Add(new TextWriterTraceListener(textfield_stream));
            Trace.AutoFlush = true;
        }        

        private void Window_Closed(object sender, EventArgs e)
        {
            sync_thread.Abort();
            sync_thread.Join();
        }

    }
}
