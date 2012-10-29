using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlickrWPF
{
    interface IOAuthDialog
    {
        //<summary>Set the authentication link</summary>
        void setAuthLink(String authentication_link);
        
        //<summary>Return the authentication data the user has entered</summary>
        String getAuthenticationData();
        
        //<summary>Show a warning message</summary>
        void WarningMessage(String message);

        //<summary>Show the dialog</summary>
        System.Windows.Forms.DialogResult ShowDialog();
    }
}
