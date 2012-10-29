using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;

namespace FlickrWPF
{
    public class TextFieldStream : System.IO.Stream
    {

        private TextBox logging_text_box;

        public override bool CanRead { get { return false; } }
        public override bool CanSeek { get { return false; } }
        public override bool CanWrite { get { return true; } }
        public override long Length { get { return logging_text_box.Text.Length; } }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
      
        

        public TextFieldStream(TextBox textbox)
        {
            logging_text_box = textbox;
        }

        public override void Flush()
        {           
        }

        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }        

        public override void Write(byte[] buffer, int offset, int count)
        {            
            String new_text = System.Text.Encoding.ASCII.GetString(buffer, offset, count);
            if (Application.Current == null)
                UpdateTextBox(new_text);
            else if (Application.Current.Dispatcher.CheckAccess())
                UpdateTextBox(new_text);                
            else
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new Action(() => { UpdateTextBox(new_text); }));
            }
        }

        void UpdateTextBox(String added_text)
        {
            logging_text_box.Text += added_text;
            logging_text_box.ScrollToEnd();
        }

    }
}
