namespace FlickrWPF
{
    partial class OAuthDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.OAuthLink = new System.Windows.Forms.LinkLabel();
            this.OAuthData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(260, 53);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Your browser should have opened up with an authorization page from Flickr. Please" +
                " copy&paste the 9 character authorization code (including \"-\") here.";
            // 
            // OAuthLink
            // 
            this.OAuthLink.AutoSize = true;
            this.OAuthLink.Location = new System.Drawing.Point(13, 69);
            this.OAuthLink.Name = "OAuthLink";
            this.OAuthLink.Size = new System.Drawing.Size(31, 13);
            this.OAuthLink.TabIndex = 2;
            this.OAuthLink.TabStop = true;
            this.OAuthLink.Text = "none";
            // 
            // OAuthData
            // 
            this.OAuthData.Location = new System.Drawing.Point(15, 115);
            this.OAuthData.Name = "OAuthData";
            this.OAuthData.Size = new System.Drawing.Size(100, 20);
            this.OAuthData.TabIndex = 3;
            this.OAuthData.Text = "xxx-yyy-zzz";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Authentication data";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(163, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "&OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OAuthDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 154);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OAuthData);
            this.Controls.Add(this.OAuthLink);
            this.Controls.Add(this.textBox1);
            this.Name = "OAuthDialog";
            this.Text = "OAuthDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox OAuthData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.LinkLabel OAuthLink;

    }
}