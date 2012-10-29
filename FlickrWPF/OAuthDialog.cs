using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlickrWPF
{
    public partial class OAuthDialog : Form, IOAuthDialog
    {
        public OAuthDialog()
        {
            InitializeComponent();
        }

        //<summary>Set the authentication link</summary>
        public void setAuthLink(String authentication_link)
        {
            OAuthLink.Text = authentication_link;
        }

        //<summary>Return the authentication data the user has entered</summary>
        public String getAuthenticationData()
        {
            return OAuthData.Text;
        }

        //<summary>Show a warning message</summary>
        public void WarningMessage(String message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            
        }
    }
}
